using MongoDB.Bson;
using MongoDB.Driver;
using OsintCommand.API.Contexts;
using OsintCommand.API.Entities;

namespace OsintCommand.API.Repositories
{
    public interface IProxyRepository : IRepository<Proxy>
    {
        Task<List<Proxy>> GetByGroupAsync(string groupId, CancellationToken ct);
        Task<List<Proxy>> InsertManualAsync(IEnumerable<Proxy> proxies, CancellationToken ct);
        Task<bool> TryIncInUseAsync(string proxyId, int maxUse, IClientSessionHandle s, CancellationToken ct);
        Task BulkSetInUseAsync(IEnumerable<(string ProxyId, int NewInUse)> items, IClientSessionHandle s, CancellationToken ct);
        Task<bool> TryDecInUseAsync(string proxyId, IClientSessionHandle s, CancellationToken ct);

    }
    public class ProxyRepository : Repository<Proxy>, IProxyRepository
    {
        public ProxyRepository(MongoDbContext context) : base(context)
        {


        }

        public Task<List<Proxy>> GetByGroupAsync(string groupId, CancellationToken ct)
        {
            var filter = Builders<Proxy>.Filter.Eq(x => x.ProxyGroupId, groupId);
            return _collection.Find(filter).ToListAsync(ct);
        }

        /// <summary>
        /// Nhập proxy thủ công.
        /// </summary>
        public async Task<List<Proxy>> InsertManualAsync(IEnumerable<Proxy> proxies, CancellationToken ct)
        {
            var list = proxies.ToList();
            if (!list.Any()) return list;
            await _collection.InsertManyAsync(list, cancellationToken: ct);
            return list;
        }

        /// <summary>
        /// Tăng InUseCount nếu chưa đạt MaxUse. 
        /// CAS: chỉ +1 khi InUseCount < maxUse      
        /// </summary>
        public async Task<bool> TryIncInUseAsync(string proxyId, int maxUse, IClientSessionHandle s, CancellationToken ct)
        {
            var filter = Builders<Proxy>.Filter.And(
                Builders<Proxy>.Filter.Eq(x => x.Id, proxyId),
                Builders<Proxy>.Filter.Where(x => x.InUseCount < maxUse)
            );
            var update = Builders<Proxy>.Update.Inc(x => x.InUseCount, 1);
            UpdateResult res = s != null
                ? await _collection.UpdateOneAsync(s, filter, update, cancellationToken: ct)
                : await _collection.UpdateOneAsync(filter, update, cancellationToken: ct);
            return res.ModifiedCount == 1;
        }

        /// <summary>
        /// Cập nhật lại InUseCount hàng loạt.
        /// </summary>
        public async Task BulkSetInUseAsync(IEnumerable<(string ProxyId, int NewInUse)> items, IClientSessionHandle s, CancellationToken ct)
        {
            var list = items.ToList();
            if (!list.Any()) return;

            var writes = new List<WriteModel<Proxy>>(list.Count);
            foreach (var (proxyId, newInUse) in list)
            {
                var f = Builders<Proxy>.Filter.Eq(x => x.Id, proxyId);
                var u = Builders<Proxy>.Update.Set(x => x.InUseCount, newInUse);
                writes.Add(new UpdateOneModel<Proxy>(f, u));
            }

            var opt = new BulkWriteOptions { IsOrdered = false };
            if (s != null) await _collection.BulkWriteAsync(s, writes, opt, ct);
            else await _collection.BulkWriteAsync(writes, opt, ct);
        }


        public async Task<bool> TryDecInUseAsync(string proxyId, IClientSessionHandle s, CancellationToken ct)
        {
            var filter = Builders<Proxy>.Filter.And(
                Builders<Proxy>.Filter.Eq(x => x.Id, proxyId),
                Builders<Proxy>.Filter.Where(x => x.InUseCount > 0)
            );
            var update = Builders<Proxy>.Update.Inc(x => x.InUseCount, -1);
            UpdateResult res = s != null
                ? await _collection.UpdateOneAsync(s, filter, update, cancellationToken: ct)
                : await _collection.UpdateOneAsync(filter, update, cancellationToken: ct);
            return res.ModifiedCount == 1;
        }
    }
}
