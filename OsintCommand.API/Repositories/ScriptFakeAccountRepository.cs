using MongoDB.Bson;
using MongoDB.Driver;
using OsintCommand.API.Contexts;
using OsintCommand.API.Entities;

namespace OsintCommand.API.Repositories
{
    public interface IScriptFakeAccountRepository : IRepository<ScriptFakeAccount>
    {
        Task AssignScriptsToFakeAccount(string fakeAccountId, List<string> scriptIds);
        Task AssignScriptToFakeAccount(string fakeAccountId, string scriptId);
        //Task<(List<Script> Scripts, int Total)> GetScriptsOfFakeAccount(string fakeAccountId);
        Task<(IEnumerable<Script> Scripts, long Total)> SearchScriptsByFakeAccountIdAsync(string fakeAccountId, string? scriptName, int page, int pageSize);
        Task<int> CountFakeAccountByScriptId(string scriptId);
        Task<List<string>> GetFakeAccountIdsByScriptIdAsync(string scriptId);
        Task<List<string>> GetScriptIdsByFakeAccountIdAsync(string fakeaccountId);

    }   
    public class ScriptFakeAccountRepository : Repository<ScriptFakeAccount>, IScriptFakeAccountRepository
    {
        public ScriptFakeAccountRepository(MongoDbContext context) : base(context)
        {
        }


        public async Task AssignScriptsToFakeAccount(string fakeAccountId, List<string> scriptIds)
        {
            //validate input
            // Tìm tất cả script đã được gán với fakeAccountId trước đó để tránh bị trùng
            var existingScriptFakeAccounts = await _collection
                .Find(x => x.FakeAccountId == fakeAccountId && scriptIds.Contains(x.ScriptId))
                .Project(x => x.ScriptId)
                .ToListAsync();

            // Lọc ra những script chưa gán
            var newScriptIds = scriptIds.Except(existingScriptFakeAccounts).ToList();

            if (newScriptIds.Any())
            {
                var newRelations = newScriptIds
                    .Select(scriptId => new ScriptFakeAccount
                    {
                        FakeAccountId = fakeAccountId,
                        ScriptId = scriptId,
                        // Thêm các field khác nếu cần (vd: thời gian tạo, ...)
                    })
                    .ToList();

                await _collection.InsertManyAsync(newRelations);
            }
        }

        public async Task<int> CountFakeAccountByScriptId(string scriptId)
        {
            // Đếm số lượng tài khoản ảo đã được gán với scriptId
            var count = await _collection.CountDocumentsAsync(x => x.ScriptId == scriptId);
            return (int)count;
        }

        public async Task<List<string>> GetFakeAccountIdsByScriptIdAsync(string scriptId)
        {
            var filter = Builders<ScriptFakeAccount>.Filter.Eq(x => x.ScriptId, scriptId);
            var projection = Builders<ScriptFakeAccount>.Projection.Include(x => x.FakeAccountId);
            var result = await _collection.Find(filter).Project<ScriptFakeAccount>(projection).ToListAsync();
            return result.Select(x => x.FakeAccountId).ToList();
        }

        public async Task<List<string>> GetScriptIdsByFakeAccountIdAsync(string fakeaccountId)
        {
            var filter = Builders<ScriptFakeAccount>.Filter.Eq(x => x.FakeAccountId, fakeaccountId);
            var projection = Builders<ScriptFakeAccount>.Projection.Include(x => x.ScriptId);
            var result = await _collection.Find(filter).Project<ScriptFakeAccount>(projection).ToListAsync();
            return result.Select(x => x.ScriptId).ToList();
        }

        public async Task AssignScriptToFakeAccount(string fakeAccountId, string scriptId)
        {
            // Kiểm tra đã tồn tại hay chưa (tránh trùng lặp)
            var exists = await _collection
                .Find(x => x.FakeAccountId == fakeAccountId && x.ScriptId == scriptId)
                .AnyAsync();

            if (!exists)
            {
                var relation = new ScriptFakeAccount
                {
                    FakeAccountId = fakeAccountId,
                    ScriptId = scriptId,
                    // Thêm các trường khác nếu có, ví dụ:
                    // CreatedAt = DateTime.UtcNow
                };

                await _collection.InsertOneAsync(relation);
            }
            // Nếu đã tồn tại thì không làm gì (hoặc có thể throw exception hoặc log nếu bạn muốn)
        }


        // TODO: loại kịch bản, số lượng hành động, lập lịch (tần suất sử dụng)
        public async Task<(IEnumerable<Script> Scripts, long Total)> SearchScriptsByFakeAccountIdAsync(string fakeAccountId, string? scriptName, int page, int pageSize)
        {
            throw new NotImplementedException();
        }


    }
}
