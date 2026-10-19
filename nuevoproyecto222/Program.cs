using Microsoft.EntityFrameworkCore;
using nuevoproyecto222.Repositories;
using nuevoproyecto222.Services;
using nuevoproyecto222.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddDbContext<nuevoproyecto222Context>(dbContextOptions => dbContextOptions.UseSqlite(builder.Configuration["ConnectionStrings:nuevoproyecto222DBConnectionString"]));

var app = builder.Build();


    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();  