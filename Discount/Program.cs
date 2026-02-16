using Discount.Handlers;
using Discount.Repositories;
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
//builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

//Might need below code to debug Repository registration issue - This will debug the dependency injection.
//using Microsoft.Extensions.DependencyInjection;

//var app = builder.Build();

//// Test the DI container
//using (var scope = app.Services.CreateScope())
//{
//    var discountRepo = scope.ServiceProvider.GetRequiredService<IDiscountRepository>();
//    Console.WriteLine(discountRepo != null ? "IDiscountRepository resolved successfully!" : "Failed to resolve IDiscountRepository.");
//}

//Grpc
builder.Services.AddGrpc();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
