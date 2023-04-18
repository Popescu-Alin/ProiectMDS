using inceputproiectMds.Models.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieTracker.Services.UserService;
using OnlineShop.Data;
using OnlineShop.Helpers;
using OnlineShop.Repositories.CardRepository;
using OnlineShop.Repositories.CategoryRepositories;
using OnlineShop.Repositories.ProductsRepository;
using OnlineShop.Services.UserService;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//adaugare identity

builder.Services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ProiectMDSContext>()
                .AddDefaultTokenProviders();


//adaug jwt
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    //options.RequireHttpsMetadata = false;//permite sa folosesc jwt fara https
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        RequireExpirationTime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("iohwefhwefbwefwebfwenfwk")),
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero
    };

   
});




//conexiunea cu baza de date
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ProiectMDSContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IProductOrderRepository, ProductOrderRepository>();
builder.Services.AddTransient<ICartRepository, CartRepository>();


builder.Services.AddScoped<IUserService,UserService>();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var servicies = scope.ServiceProvider;
    SeedData.Initialize(servicies); 
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
