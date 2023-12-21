using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using Project.Services.Category.OptionPatternSettings;
using Project.Services.Category.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/// apiyi koruma altýna almak 
// builder.Services.AddControllers();
// ** step 1  
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter());
});

// ** step 2 artýk api koruma altýnda ve token'ý olan bu apiye istek yapabilir...
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerURL"];
    options.Audience = "resource_category";
    options.RequireHttpsMetadata = false;
});


builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();


builder.Services.AddAutoMapper(typeof(Program));

builder.Services.Configure<OptionSettings>(builder.Configuration.GetSection("OptionSettings")); // appsetting.json  --> OptionSettings
// builder --> Services, Configuration ...


// builder.Services.AddMvc();
// IOptions yerine  IOptionSettings kullanýlacak service.cs'lerde..
builder.Services.AddSingleton<IOptionSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<OptionSettings>>().Value;  // GetRequiredService, getservice'den farký, ilgili  servisi bulamazsa ise hata fýrlatýr..
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();
