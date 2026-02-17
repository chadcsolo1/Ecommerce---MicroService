using Discount.Extensions;
using Discount.Handlers;
using Discount.Repositories;
using Discount.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

//Mediatr
var assemblies = new Assembly[]
{
    Assembly.GetExecutingAssembly(),
    typeof(CreateDiscountHandler).Assembly
};
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

//Repositories
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

//Grpc
builder.Services.AddGrpc();

//builder.Services.AddControllers();
//// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

var app = builder.Build();

//Migrate Database
app.MigrateDatabase<Program>();

app.UseRouting();

app.UseEndpoints(endpoints => {
    endpoints.MapGrpcService<DiscountService>();
});

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

app.Run();
