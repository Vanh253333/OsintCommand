using MongoDB.Bson;
using MongoDB.Driver;
using OsintCommand.API.Contexts;
using OsintCommand.API.Dtos;
using OsintCommand.API.Entities;

namespace OsintCommand.API.Repositories
{
    public interface IFriendRepository : IRepository<Friend>
    {

    }
    public class FriendRepository: Repository<Friend>, IFriendRepository
    {

        public FriendRepository(MongoDbContext context) : base(context)
        {
        }
    }
}
