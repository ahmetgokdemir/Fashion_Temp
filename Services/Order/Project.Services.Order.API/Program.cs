using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Services.Order.BLL.Handlers;
using Project.Services.Order.Infrastructure;
using Project.Shared.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Net.NetworkInformation;
using static System.Net.Mime.MediaTypeNames;
 


var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.


builder.Services.AddDbContext<OrderDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), configure =>
    {
        configure.MigrationsAssembly("Project.Services.Order.Infrastructure");
    });
});

builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
services.AddHttpContextAccessor();

//builder.Services.AddMediatR(typeof(Project.Services.Order.BLL.Handlers.CreateOrderCommandHandler).Assembly);
// builder.Services.AddMediatR(typeof(CreateOrderCommandHandler));
builder.Services.AddMediatR(cfg =>
     cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommandHandler).Assembly));

var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = configuration["IdentityServerURL"];
    options.Audience = "resource_order";
    options.RequireHttpsMetadata = false;
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
