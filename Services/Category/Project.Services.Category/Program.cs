using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Project.Services.Category.OptionPatternSettings;
using Project.Services.Category.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();


builder.Services.AddAutoMapper(typeof(Program));

builder.Services.Configure<OptionSettings>(builder.Configuration.GetSection("OptionSettings"));
// builder --> Services, Configuration

// builder.Services.AddMvc();

builder.Services.AddSingleton<IOptionSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<OptionSettings>>().Value; 
});


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
