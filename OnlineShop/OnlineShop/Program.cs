using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Repositories.CardRepository;
using OnlineShop.Repositories.CategoryRepositories;
using OnlineShop.Repositories.ProductsRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ProiectMDSContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IProductOrderRepository, ProductOrderRepository>();
builder.Services.AddTransient<ICartRepository, CartRepository>();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
