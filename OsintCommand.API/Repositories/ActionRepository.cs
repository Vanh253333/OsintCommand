using MongoDB.Bson;
using MongoDB.Driver;
using OsintCommand.API.Contexts;

namespace OsintCommand.API.Repositories
{
    public interface IActionRepository : IRepository<Entities.Action>
    {
        Task<IEnumerable<Entities.Action>> GetAllByScriptIdAsync(string scriptId);
        Task<(List<Entities.Action>, int total)> GetByScriptIdAsync(string scriptId, int page, int pageSize);
        Task<int> GetActionCountByScriptIdAsync(string scriptId);
        Task<int> DeleteActionByScriptId(string scriptId, string actionId);
        Task<int> CountActionByScriptId(string scriptId);
        Task<List<Entities.Action>> GetByScriptIdAsync(string scriptId);
        Task<Dictionary<string, int>> CountActionsByScriptIdsAsync(List<string> scriptIds);
    }
    public class ActionRepository : Repository<Entities.Action>, IActionRepository
    {

        public ActionRepository(MongoDbContext context) : base(context)
        {
            
        }

        public async Task<int> CountActionByScriptId(string scriptId)
        {
            var filter = Builders<Entities.Action>.Filter.Eq(x => x.ScriptId, scriptId);
            return (int)await _collection.CountDocumentsAsync(filter);
        }

        public async Task<List<Entities.Action>> GetByScriptIdAsync(string scriptId)
        {
            var filter = Builders<Entities.Action>.Filter.Eq(a => a.ScriptId, scriptId);
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Entities.Action>> GetAllByScriptIdAsync(string scriptId)
        {
            var filter = Builders<Entities.Action>.Filter.Eq(x => x.ScriptId, scriptId);
            var actions = await _collection.Find(filter).ToListAsync();
            return actions;
        }

        public async Task<(List<Entities.Action>, int total)> GetByScriptIdAsync(string scriptId, int page, int pageSize)
        {
            var filter = Builders<Entities.Action>.Filter.Eq(x => x.ScriptId, scriptId);
            var total = await _collection.CountDocumentsAsync(filter);
            var actions = await _collection
                .Find(filter)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();
            return (actions, (int)total);
        }


        public async Task<int> GetActionCountByScriptIdAsync(string scriptId)
        {
            var filter = Builders<Entities.Action>.Filter.Eq(x => x.ScriptId, scriptId);
            // Đếm số action của scriptId này
            return (int)await _collection.CountDocumentsAsync(filter);
        }

        public async Task<int> DeleteActionByScriptId(string scriptId, string actionId)
        {
            var filter = Builders<Entities.Action>.Filter.And(
                Builders<Entities.Action>.Filter.Eq(x => x.Id, actionId), //in
                Builders<Entities.Action>.Filter.Eq(x => x.ScriptId, scriptId));

            var result =  await _collection.DeleteOneAsync(filter);
            return (int)result.DeletedCount;
        }


        public async Task<Dictionary<string, int>> CountActionsByScriptIdsAsync(List<string> scriptIds)
        {
            var filter = Builders<Entities.Action>.Filter.In(x => x.ScriptId, scriptIds);

            var result = await _collection
                .Aggregate()
                .Match(filter)
                .Group(x => x.ScriptId, g => new
                {
                    ScriptId = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            return result.ToDictionary(x => x.ScriptId, x => x.Count);
        }
    }
}
