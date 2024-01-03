using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization; 
using Microsoft.Extensions.Options;
using Project.Services.Cart.OptionPatternSettings;
using Project.Services.Cart.Services;
using Project.Shared.Services;
using System.IdentityModel.Tokens.Jwt; 

// -1 
var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//0
// protect microserv. 
var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build(); // Policy kimlik doðrulma için

// builder.Services.AddControllers();
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy)); // Policy
});

//1
builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("RedisSettings"));


builder.Services.AddSingleton<RedisService>(sp =>
{
    var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;

    var redis = new RedisService(redisSettings.Host, redisSettings.Port); // RedisService.cs

    redis.Connect(); //* 

    return redis;
});

// 2
builder.Services.AddHttpContextAccessor(); // * SharedIdentityService --> IHttpContextAccessor ( httpcontext'e eriþilebilir)

builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddScoped<ICartService, CartService>();

// 3
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerURL"];
    options.Audience = "resource_cart"; // resource_basket
    options.RequireHttpsMetadata = false;
});


/////////////////////////////////////////////////////////////////////////
///

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}


app.UseAuthentication(); //**
app.UseAuthorization();

app.MapControllers();

app.Run();
