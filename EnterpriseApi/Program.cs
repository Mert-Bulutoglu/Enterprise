using EnterpriseAPI;
using EnterpriseBusinessLayer;
using EnterpriseBusinessLayer.Abstract;
using EnterpriseBusinessLayer.Concrete;
using EnterpriseDataAccessLayer.Abstract;
using EnterpriseDataAccessLayer.AppDbContext;
using EnterpriseDataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EnterpriseContext>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRolePermissionsRepository, RolePermissionsRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:4200") 
              .AllowAnyHeader()                     
              .AllowAnyMethod();                     
    });
});

builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 5001;
    options.HttpsPort = 5000;
    options.HttpsPort = 443;
    options.HttpsPort = 80;
    options.HttpsPort = 8080;
    options.HttpsPort = 8081;
});

var redisConfig = builder.Configuration.GetSection("Redis");
var redisConnectionString = redisConfig["ConnectionString"];
var instanceName = redisConfig["InstanceName"];
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = ConfigurationOptions.Parse(redisConnectionString, true);
    configuration.ClientName = instanceName;
    return ConnectionMultiplexer.Connect(configuration);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    using var dbContext = scope.ServiceProvider.GetRequiredService<EnterpriseContext>();
    dbContext?.Database.Migrate();
}

app.UseCors("AllowLocalhost");


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
