using Bogus;
using MongoDB.Bson;
using MongoDB.Driver;
using OsintCommand.API.Contexts;
using OsintCommand.API.Entities;

namespace OsintCommand.API
{
    public class DataSeeder
    {
        public static async Task<List<FakeAccount>> SeedFakeAccountsAsync(MongoDbContext context, int count = 10)
        {
            var faker = new Faker<FakeAccount>()
                .RuleFor(x => x.Id, _ => ObjectId.GenerateNewId().ToString())
                .RuleFor(x => x.AccountName, f => f.Internet.UserName())
                .RuleFor(x => x.Platform, f => f.PickRandom("Facebook", "Telegram", "Zalo", "Tiktok"))
                .RuleFor(x => x.Uid, _ => Guid.NewGuid().ToString())
                .RuleFor(x => x.Password, f => f.Internet.Password())
                .RuleFor(x => x.TwoFA, f => f.Random.AlphaNumeric(8))
                .RuleFor(x => x.Email, f => f.Internet.Email())
                .RuleFor(x => x.EmailPassword, f => f.Internet.Password())
                .RuleFor(x => x.SecurityLevel, f => f.PickRandom("Low", "Medium", "High"))
                .RuleFor(x => x.AccountType, f => f.PickRandom("VIA", "Clone VIA", "ADS"))
                .RuleFor(x => x.IsActive, f => f.Random.Bool())
                .RuleFor(x => x.Job, f => f.PickRandom("dev", "engineer", "lawer"))
                .RuleFor(x => x.CreatedAt, f => f.Date.Past(2))
                .RuleFor(x => x.UpdatedAt, f => f.Date.Recent());

            var data = faker.Generate(count);
            await context.GetCollection<FakeAccount>().InsertManyAsync(data);
            return data;
        }

        public static async Task<List<Friend>> SeedFriendsAsync(MongoDbContext context, List<FakeAccount> fakeAccounts, int count = 30)
        {
            var faker = new Faker<Friend>()
                .RuleFor(x => x.Id, _ => ObjectId.GenerateNewId().ToString())
                .RuleFor(x => x.FakeAccountId, f => f.PickRandom(fakeAccounts).Id)
                .RuleFor(x => x.FriendUid, _ => Guid.NewGuid().ToString())
                .RuleFor(x => x.FriendName, f => f.Name.FullName())
                .RuleFor(x => x.FriendAvatar, f => f.Internet.Avatar())
                .RuleFor(x => x.Gender, f => f.PickRandom("Male", "Female"))
                .RuleFor(x => x.Link, f => f.Internet.Url())
                .RuleFor(x => x.ConnectedDate, f => f.Date.Past());

            var data = faker.Generate(count);
            await context.GetCollection<Friend>().InsertManyAsync(data);
            return data;
        }


        public static async Task<List<FriendFakeAccount>> SeedFriendFakeAccountsAsync(MongoDbContext context, List<Friend> friends, List<FakeAccount> fakeAccounts, int count = 50)
        {
            var faker = new Faker<FriendFakeAccount>()
                .RuleFor(x => x.Id, _ => ObjectId.GenerateNewId().ToString())
                .RuleFor(x => x.FriendId, f => f.PickRandom(friends).Id)
                .RuleFor(x => x.FakeAccountId, f => f.PickRandom(fakeAccounts).Id);

            var data = faker.Generate(count);
            await context.GetCollection<FriendFakeAccount>().InsertManyAsync(data);
            return data;
        }

        public static async Task<List<Group>> SeedGroupsAsync(MongoDbContext context, List<FakeAccount> fakeAccounts, int count = 15)
        {
            var faker = new Faker<Group>()
                .RuleFor(x => x.Id, _ => ObjectId.GenerateNewId().ToString())
                .RuleFor(x => x.FakeAccountId, f => f.PickRandom(fakeAccounts).Id)
                .RuleFor(x => x.GroupUid, _ => Guid.NewGuid().ToString())
                .RuleFor(x => x.GroupName, f => f.Company.CompanyName())
                .RuleFor(x => x.GroupAvatar, f => f.Internet.Avatar())
                .RuleFor(x => x.GroupLink, f => f.Internet.Url())
                .RuleFor(x => x.JoinedDate, f => f.Date.Past())
                .RuleFor(x => x.Status, f => f.PickRandom("Active", "Inactive", "Unknown"));

            var data = faker.Generate(count);
            await context.GetCollection<Group>().InsertManyAsync(data);
            return data;
        }

        public static async Task<List<GroupFakeAccount>> SeedGroupFakeAccountsAsync(MongoDbContext context, List<Group> groups, List<FakeAccount> fakeAccounts, int count = 30)
        {
            var faker = new Faker<GroupFakeAccount>()
                .RuleFor(x => x.Id, _ => ObjectId.GenerateNewId().ToString())
                .RuleFor(x => x.GroupId, f => f.PickRandom(groups).Id)
                .RuleFor(x => x.FakeAccountId, f => f.PickRandom(fakeAccounts).Id);

            var data = faker.Generate(count);
            await context.GetCollection<GroupFakeAccount>().InsertManyAsync(data);
            return data;
        }

        public static async Task SeedAllAsync(MongoDbContext context)
        {
            var fakeAccounts = await SeedFakeAccountsAsync(context, 15);
            var friends = await SeedFriendsAsync(context, fakeAccounts, 20);
            await SeedFriendFakeAccountsAsync(context, friends, fakeAccounts, 20);
            var groups = await SeedGroupsAsync(context, fakeAccounts, 25);
            await SeedGroupFakeAccountsAsync(context, groups, fakeAccounts, 20);

            // Nếu muốn seed thêm entity khác, chỉ cần viết thêm hàm và gọi tại đây
        }
    }
}
