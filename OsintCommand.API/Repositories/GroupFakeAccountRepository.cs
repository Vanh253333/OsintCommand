using MongoDB.Bson;
using MongoDB.Driver;
using OsintCommand.API.Contexts;
using OsintCommand.API.Dtos;
using OsintCommand.API.Entities;

namespace OsintCommand.API.Repositories
{
    public interface IGroupFakeAccountRepository : IRepository<GroupFakeAccount>
    {
        Task<(IEnumerable<Group> Data, long Total)> SearchGroupsByFakeAccountAsync(string fakeAccountId, GroupSearchRequestDto dto);
    }
    public class GroupFakeAccountRepository : Repository<GroupFakeAccount>, IGroupFakeAccountRepository
    {
        public GroupFakeAccountRepository(MongoDbContext context) : base(context)
        {
            
        }

        public async Task<(IEnumerable<Group> Data, long Total)> SearchGroupsByFakeAccountAsync(string fakeAccountId, GroupSearchRequestDto dto)
        {
            List<BsonDocument> pipeline = new List<BsonDocument>();

            // 1. Match theo fakeAccountId ở join table
            pipeline.Add(new BsonDocument("$match", new BsonDocument("fakeAccountId", new ObjectId(fakeAccountId))));

            // 2. Join sang bảng Group
            pipeline.Add(new BsonDocument("$lookup", new BsonDocument
                {
                    { "from", "group" }, // Collection Group
                    { "localField", "groupId" },
                    { "foreignField", "_id" },
                    { "as", "groupInfo" }
                }));

            // 3. Unwind array
            pipeline.Add(new BsonDocument("$unwind", "$groupInfo"));

            // 4. Filter nâng cao (nếu có)
            var groupFilters = new List<BsonDocument>();
            if (!string.IsNullOrEmpty(dto.GroupName))
                groupFilters.Add(new BsonDocument("groupInfo.groupName", new BsonDocument("$regex", dto.GroupName).Add("$options", "i")));
            if (!string.IsNullOrEmpty(dto.GroupUid))
                groupFilters.Add(new BsonDocument("groupInfo.groupUid", new BsonDocument("$regex", dto.GroupUid).Add("$options", "i")));
            if (!string.IsNullOrEmpty(dto.GroupLink))
                groupFilters.Add(new BsonDocument("groupInfo.groupLink", new BsonDocument("$regex", dto.GroupLink).Add("$options", "i")));
            if (groupFilters.Count > 0)
                pipeline.Add(new BsonDocument("$match", new BsonDocument("$and", new BsonArray(groupFilters))));

            // 5. Count total
            var totalPipeline = new List<BsonDocument>(pipeline) { new BsonDocument("$count", "total") };
            var totalResult = await _collection.Aggregate<BsonDocument>(totalPipeline).FirstOrDefaultAsync();

            var total = totalResult == null ? 0 :
                totalResult["total"].IsInt64 ? totalResult["total"].AsInt64 :
                totalResult["total"].IsInt32 ? totalResult["total"].AsInt32 :
                0;


            // 6. Pagination
            pipeline.Add(new BsonDocument("$skip", (dto.Page - 1) * dto.PageSize));
            pipeline.Add(new BsonDocument("$limit", dto.PageSize));

            // 7. Project chỉ lấy document Group
            pipeline.Add(new BsonDocument("$replaceRoot", new BsonDocument("newRoot", "$groupInfo")));

            var data = await _collection.Aggregate<Group>(pipeline).ToListAsync();

            return (data, total);
        }
    }
}
