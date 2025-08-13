using MongoDB.Bson;
using MongoDB.Driver;
using OsintCommand.API.Contexts;
using OsintCommand.API.Dtos;
using OsintCommand.API.Entities;

namespace OsintCommand.API.Repositories
{
    public interface IFakeAccountRepository : IRepository<FakeAccount>
    {
        // Define methods for the repository here
        Task<(IEnumerable<FakeAccount> Items, long TotalCount)> GetFilteredPaginatedAsync(FakeAccountQueryRequestDto queryRequestDto);
        Task<(IEnumerable<FakeAccount> Items, long TotalCount)> GetAllPaginatedAsync(int page, int pageSize);
        Task<bool> ExistsByUidAsync(string uid);
        Task<List<FakeAccount>> FindByIdsAsync(IEnumerable<string> fakeAccountIds, bool onlyWithoutProxy, CancellationToken ct);
        Task BulkSetProxyAsync(IEnumerable<(string AccountId, Proxy Proxy)> pairs, IClientSessionHandle s, CancellationToken ct);
        Task DetachProxyAsync(string accountId, string proxyId, IClientSessionHandle s, CancellationToken ct);
    }
    public class FakeAccountRepository : Repository<FakeAccount>, IFakeAccountRepository
    {
        public FakeAccountRepository(MongoDbContext context) : base(context)
        {

        }

        public async Task<bool> ExistsByUidAsync(string uid)
        {
            var count = await _collection.CountDocumentsAsync(x => x.Uid == uid);
            return count > 0;
        }


        public async Task<(IEnumerable<FakeAccount> Items, long TotalCount)> GetAllPaginatedAsync(int page, int pageSize)
        {
            var filterBuilder = Builders<FakeAccount>.Filter;
            var filter = filterBuilder.Empty;

            var total = await _collection.CountDocumentsAsync(filter);
            var data = await _collection.Find(filter)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            return (data, total);
        }

        public async Task<(IEnumerable<FakeAccount> Items, long TotalCount)> GetFilteredPaginatedAsync(FakeAccountQueryRequestDto queryRequestDto)
        {
            var filterBuilder = Builders<FakeAccount>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(queryRequestDto.Uid))
                filter &= filterBuilder.Regex(x => x.Uid, new MongoDB.Bson.BsonRegularExpression(queryRequestDto.Uid, "i"));

            if (!string.IsNullOrEmpty(queryRequestDto.Email))
                filter &= filterBuilder.Regex(x => x.Email, new MongoDB.Bson.BsonRegularExpression(queryRequestDto.Email, "i"));

            if (queryRequestDto.Platform != null && queryRequestDto.Platform.Any())
                filter &= filterBuilder.In(x => x.Platform, queryRequestDto.Platform);

            if (queryRequestDto.Job != null && queryRequestDto.Job.Any())
                filter &= filterBuilder.In(x => x.Job, queryRequestDto.Job);

            if (!string.IsNullOrEmpty(queryRequestDto.AccountType))
                filter &= filterBuilder.Eq(x => x.AccountType, queryRequestDto.AccountType);

            if (queryRequestDto.IsActive.HasValue)
                filter &= filterBuilder.Eq(x => x.IsActive, queryRequestDto.IsActive.Value);

            if (queryRequestDto.CreatedFrom.HasValue)
                filter &= filterBuilder.Gte(x => x.CreatedAt, queryRequestDto.CreatedFrom.Value.Date);

            if (queryRequestDto.CreatedTo.HasValue)
                filter &= filterBuilder.Lte(x => x.CreatedAt, queryRequestDto.CreatedTo.Value.Date.AddDays(1).AddTicks(-1));
            // Lấy hết ngày createdTo, nếu muốn lấy đúng trong ngày

            var page = queryRequestDto.Page < 1 ? 1 : queryRequestDto.Page;
            var pageSize = queryRequestDto.PageSize <= 0 ? 10 : queryRequestDto.PageSize;

            var totalCount = await _collection.CountDocumentsAsync(filter);

            var items = await _collection.Find(filter)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }


        public async Task<List<FakeAccount>> FindByIdsAsync(IEnumerable<string> ids, bool onlyWithoutProxy, CancellationToken ct)
        {
            var list = ids?.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList() ?? new();
            if (list.Count == 0) return new();

            var filter = Builders<FakeAccount>.Filter.In(x => x.Id, list);
            if (onlyWithoutProxy)
                filter &= Builders<FakeAccount>.Filter.Eq(x => x.ProxyId, null);

            return await _collection.Find(filter).ToListAsync(ct);
        }


        /// <summary>
        /// Gán proxy cho một loạt account. Dùng trong transaction (session 's').
        /// </summary>
        public async Task BulkSetProxyAsync(IEnumerable<(string AccountId, Proxy Proxy)> pairs, IClientSessionHandle s, CancellationToken ct)
        {
            var list = pairs.ToList();
            if (list.Count == 0) return;

            var writes = new List<WriteModel<FakeAccount>>(list.Count);
            foreach (var (accountId, proxy) in list)
            {
                var f = Builders<FakeAccount>.Filter.Eq(x => x.Id, accountId);
                var u = Builders<FakeAccount>.Update
                    .Set(x => x.ProxyId, proxy.Id)
                    .Set(x => x.ProxyType, proxy.Type)
                    .Set(x => x.ProxyHost, proxy.Host)
                    .Set(x => x.ProxyPort, proxy.Port);
                writes.Add(new UpdateOneModel<FakeAccount>(f, u));
            }

            var opt = new BulkWriteOptions { IsOrdered = false };
            if (s != null) await _collection.BulkWriteAsync(s, writes, opt, ct);
            else await _collection.BulkWriteAsync(writes, opt, ct);
        }

        /// <summary>
        /// Gỡ proxy ra khỏi account.
        /// </summary>
        public async Task DetachProxyAsync(string accountId, string proxyId, IClientSessionHandle s, CancellationToken ct)
        {
            var filter = Builders<FakeAccount>.Filter.And(
                Builders<FakeAccount>.Filter.Eq(x => x.Id, accountId),
                Builders<FakeAccount>.Filter.Eq(x => x.ProxyId, proxyId)
            );
            var update = Builders<FakeAccount>.Update
                .Unset(x => x.ProxyId)
                .Unset(x => x.ProxyType)
                .Unset(x => x.ProxyHost)
                .Unset(x => x.ProxyPort);

            if (s != null) await _collection.UpdateOneAsync(s, filter, update, cancellationToken: ct);
            else await _collection.UpdateOneAsync(filter, update, cancellationToken: ct);
        }


    }
}
