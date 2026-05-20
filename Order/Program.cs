using EventBus.Messages.Common;
using MassTransit;
using Order.Data;
using Order.Dispatcher;
using Order.EventBusConsumer;
using Order.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//ApplicationServices and InfraServices
builder.Services.AddApplicationServices();
builder.Services.AddInfraServices(builder.Configuration);

//OutboxMessageDispatcher
builder.Services.AddHostedService<OutboxMessageDispatcher>();

//MassTransit
builder.Services.AddMassTransit(config =>
{
    //Mark as Consumer
    config.AddConsumer<BasketOrderingConsumer>();
    config.AddConsumer<PaymentCompletedConsumer>();
    config.AddConsumer<PaymentFailedConsumer>();
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        //Provide the queue name with consumer settings
        cfg.ReceiveEndpoint(EventBusConstant.BasketCheckoutQueue, c =>
        {
            c.ConfigureConsumer<BasketOrderingConsumer>(ctx);
        });
        //Payment Completed Event Consumer
        cfg.ReceiveEndpoint(EventBusConstant.PaymentCompletedQueue, e =>
        {
            e.ConfigureConsumer<PaymentCompletedConsumer>(ctx);
        });
        //Payment Failed Event Consumer
        cfg.ReceiveEndpoint(EventBusConstant.PaymentFailedQueue, f =>
        {
            f.ConfigureConsumer<PaymentFailedConsumer>(ctx);
        });
    });
});

var app = builder.Build();

//Migration
app.MigrateDatabase<OrderContext>((context, services) =>
{
    var logger = services.GetService<ILogger<OrderContextSeed>>();
    OrderContextSeed.SeedAsync(context, logger).Wait();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//Enable Swagger
app.UseSwagger();
app.UseSwaggerUI();

//CORS
app.UseCors("AllowAll");

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
