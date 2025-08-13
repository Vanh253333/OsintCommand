using MongoDB.Bson;
using MongoDB.Driver;
using OsintCommand.API.Contexts;
using OsintCommand.API.Entities;

namespace OsintCommand.API.Repositories
{
    public interface IScriptRepository : IRepository<Script>
    {
        // Define any additional methods specific to Script repository if needed
        Task<(IEnumerable<Script> Items, long TotalCount)> GetAllPaginatedAsync(int page, int pageSize);
        Task<(IEnumerable<Script> Items, long TotalCount)> SearchPaginatedAsync(int page, int pageSize, string name);
        Task<List<Script>> GetByIdsFilteredAsync(List<string> ids, string? nameKeyword, int page, int pageSize);
        Task<int> CountByIdsFilteredAsync(List<string> ids, string? nameKeyword);
    }
    public class ScriptRepository : Repository<Script>, IScriptRepository
    {
        public ScriptRepository(MongoDbContext context) : base(context)
        {
        }


        // TODO: number of fake account, number of action
        public async Task<(IEnumerable<Script> Items, long TotalCount)> GetAllPaginatedAsync(int page, int pageSize)
        {
            var filterBuilder = Builders<Script>.Filter;
            var filter = filterBuilder.Empty;

            var total = await _collection.CountDocumentsAsync(filter);
            var data = await _collection.Find(filter)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            return (data, total);
        }

        // TODO: number of fake account, number of action
        public async Task<(IEnumerable<Script> Items, long TotalCount)> SearchPaginatedAsync(int page, int pageSize, string name)
        {
            var filterBuilder = Builders<Script>.Filter;
            var filter = filterBuilder.Empty;
            
            // Nếu có search theo tên (case-insensitive, chứa từ khoá)
            if (!string.IsNullOrEmpty(name))
            {
                filter &= filterBuilder.Regex(x => x.Name, new BsonRegularExpression(name, "i"));
            }

            var total = await _collection.CountDocumentsAsync(filter);
            var data = await _collection.Find(filter)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            return (data, total);
        }


        public async Task<List<Script>> GetByIdsFilteredAsync(List<string> ids, string? nameKeyword, int page, int pageSize)
        {
            var builder = Builders<Script>.Filter;
            var filter = builder.In(x => x.Id, ids);

            if (!string.IsNullOrEmpty(nameKeyword))
            {
                var nameFilter = builder.Regex(x => x.Name, new BsonRegularExpression(nameKeyword, "i")); // Case-insensitive
                filter = builder.And(filter, nameFilter);
            }

            return await _collection
                .Find(filter)
                .Skip(page)
                .Limit(pageSize)
                .ToListAsync();
        }

        public async Task<int> CountByIdsFilteredAsync(List<string> ids, string? nameKeyword)
        {
            var builder = Builders<Script>.Filter;
            var filter = builder.In(x => x.Id, ids);

            if (!string.IsNullOrEmpty(nameKeyword))
            {
                var nameFilter = builder.Regex(x => x.Name, new BsonRegularExpression(nameKeyword, "i"));
                filter = builder.And(filter, nameFilter);
            }

            return (int)await _collection.CountDocumentsAsync(filter);
        }
    }
}
