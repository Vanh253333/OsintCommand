using OsintCommand.API.Contexts;
using OsintCommand.API.Mappings;
using OsintCommand.API.Repositories;
using OsintCommand.API.Services;
using OsintCommand.API.Settings;


var builder = WebApplication.CreateBuilder(args);

// bind mongodb settings
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection(nameof(MongoDbSettings)));
builder.Services.AddSingleton<MongoDbContext>();

// Add services to the containerà
// Add the repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IFakeAccountRepository, FakeAccountRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IFriendRepository, FriendRepository>();
builder.Services.AddScoped<IGroupFakeAccountRepository, GroupFakeAccountRepository>();
builder.Services.AddScoped<IActionRepository, ActionRepository>();
builder.Services.AddScoped<IFriendFakeAccountRepository, FriendFakeAccountRepository>();
builder.Services.AddScoped<IScriptFakeAccountRepository, ScriptFakeAccountRepository>();
builder.Services.AddScoped<IScriptRepository, ScriptRepository>();
builder.Services.AddScoped<IProxyRepository, ProxyRepository>();
//builder.Services.AddScoped<IActionServices, ActionServices>();

builder.Services.AddScoped<IScriptServices, ScriptServices>();
builder.Services.AddScoped<IFakeAccountServices, FakeAccountServices>();



builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapperProfiles>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
