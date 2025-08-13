using MongoDB.Bson;
using MongoDB.Driver;
using OsintCommand.API.Contexts;
using OsintCommand.API.Dtos;
using OsintCommand.API.Entities;

namespace OsintCommand.API.Repositories
{
    public interface IGroupRepository : IRepository<Group>
    {
    }

    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        public GroupRepository(MongoDbContext context) : base(context)
        {

        }
    }
}
