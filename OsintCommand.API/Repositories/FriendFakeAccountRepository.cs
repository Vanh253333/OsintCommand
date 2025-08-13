using MongoDB.Bson;
using MongoDB.Driver;
using OsintCommand.API.Contexts;
using OsintCommand.API.Dtos;
using OsintCommand.API.Entities;

namespace OsintCommand.API.Repositories
{
    public interface IFriendFakeAccountRepository : IRepository<FriendFakeAccount>
    {
        Task<(IEnumerable<Friend> Data, long Total)> SearchFriendsByFakeAccountAsync(string fakeAccountId, FriendSearchRequestDto dto);
    }
    public class FriendFakeAccountRepository : Repository<FriendFakeAccount>, IFriendFakeAccountRepository
    {
        public FriendFakeAccountRepository(MongoDbContext context) : base(context)
        {
            
        }

        // tên, giới tính, uid, link
        public async Task<(IEnumerable<Friend> Data, long Total)> SearchFriendsByFakeAccountAsync(string fakeAccountId, FriendSearchRequestDto dto)
        {
            var pipeline = new List<BsonDocument>();

            // 1. Lọc theo fakeAccountId ở join table (FriendFakeAccount)
            pipeline.Add(new BsonDocument("$match", new BsonDocument("fakeAccountId", new ObjectId(fakeAccountId))));

            // 2. Join sang bảng Friend
            pipeline.Add(new BsonDocument("$lookup", new BsonDocument
                {
                    { "from", "friend" }, 
                    { "localField", "friendId" }, 
                    { "foreignField", "_id" },
                    { "as", "friendInfo" }
                }));

            // 3. Unwind friendInfo
            pipeline.Add(new BsonDocument("$unwind", "$friendInfo"));

            // 4. Filter nâng cao
            var friendFilters = new List<BsonDocument>();
            if (!string.IsNullOrEmpty(dto.FriendName))
                friendFilters.Add(new BsonDocument("friendInfo.friendName", new BsonDocument("$regex", dto.FriendName).Add("$options", "i")));
            if (!string.IsNullOrEmpty(dto.FriendUid))
                friendFilters.Add(new BsonDocument("friendInfo.friendUid", new BsonDocument("$regex", dto.FriendUid).Add("$options", "i")));
            if (!string.IsNullOrEmpty(dto.Gender))
                friendFilters.Add(new BsonDocument("friendInfo.gender", new BsonDocument("$regex", dto.Gender).Add("$options", "i")));
            if (!string.IsNullOrEmpty(dto.Link))
                friendFilters.Add(new BsonDocument("friendInfo.link", new BsonDocument("$regex", dto.Link).Add("$options", "i")));
            if (friendFilters.Count > 0)
                pipeline.Add(new BsonDocument("$match", new BsonDocument("$and", new BsonArray(friendFilters))));

            // 5. Đếm total
            var totalPipeline = new List<BsonDocument>(pipeline) { new BsonDocument("$count", "total") };
            var totalResult = await _collection.Aggregate<BsonDocument>(totalPipeline).FirstOrDefaultAsync();

            long total = 0;
            if (totalResult != null && totalResult.Contains("total"))
            {
                if (totalResult["total"].IsInt32)
                    total = totalResult["total"].AsInt32;
                else if (totalResult["total"].IsInt64)
                    total = totalResult["total"].AsInt64;
            }

            // 6. Pagination
            pipeline.Add(new BsonDocument("$skip", (dto.Page - 1) * dto.PageSize));
            pipeline.Add(new BsonDocument("$limit", dto.PageSize));

            // 7. Project lấy document Friend
            pipeline.Add(new BsonDocument("$replaceRoot", new BsonDocument("newRoot", "$friendInfo")));

            var data = await _collection.Aggregate<Friend>(pipeline).ToListAsync();

            return (data, total);
        }
    }
}
