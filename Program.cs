using HL7APIProject.Interfaces;
using HL7APIProject.Services;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using Microsoft.Extensions.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog for file logging
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() // Logs to the console
    .WriteTo.File("app.log", rollingInterval: RollingInterval.Day) // Logs to a file, app.log
    .CreateLogger();

builder.Logging.AddSerilog(); // Adds Serilog as the logger

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5146); 
});

// Configure RabbitMQ ConnectionFactory
builder.Services.AddSingleton<IConnectionFactory>(sp =>
{
    var factory = new ConnectionFactory()
    {
        HostName = "localhost",
        UserName = "guest",
        Password = "guest"
    };
    return factory;
});

// Register IModel for RabbitMQ
builder.Services.AddSingleton(sp =>
{
    var factory = sp.GetRequiredService<IConnectionFactory>();
    var connection = factory.CreateConnection();
    return connection.CreateModel();
});

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database connection
var connectionString = builder.Configuration.GetConnectionString("YourDatabaseConnection");
builder.Services.AddDbContext<YourDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Register services for dependency injection
builder.Services.AddScoped<IHL7MessageService, HL7MessageService>();
builder.Services.AddScoped<IAcknowledgmentService, AcknowledgmentService>();
builder.Services.AddScoped<IDatabaseService, DatabaseService>();
builder.Services.AddScoped<IRabbitMQPublisher, RabbitMQPublisher>();
builder.Services.AddHttpClient<IHttpAcknowledgmentSender, HttpAcknowledgmentSender>();
builder.Services.AddScoped<IMessageConsumer, MessageConsumer>();

var app = builder.Build();

// Trigger the RabbitMQ consumer to start consuming messages synchronously
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var messageConsumer = scope.ServiceProvider.GetRequiredService<IMessageConsumer>();
    messageConsumer.ConsumeMessage(); // Synchronous call
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

// Run the application and log activity
app.Run();
