<<<<<<< HEAD
using inceputproiectMds.Models.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MovieTracker.Services.UserService;
using OnlineShop.Data;
using OnlineShop.Helpers;
using OnlineShop.Repositories.CardRepository;
using OnlineShop.Repositories.CategoryRepositories;
using OnlineShop.Repositories.ProductsRepository;
using OnlineShop.Repositories.ReviewRepository;
using OnlineShop.Services.UserService;
using System.Text;
=======
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Repositories.CardRepository;
using OnlineShop.Repositories.CategoryRepositories;
using OnlineShop.Repositories.ProductsRepository;
>>>>>>> 092e24880e1fba1f81168a843069f81a1c063986

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
<<<<<<< HEAD

//adaug swagger si autehtification cu jwt bearer token
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieTracker", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
                     Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
});



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
=======
builder.Services.AddSwaggerGen();

>>>>>>> 092e24880e1fba1f81168a843069f81a1c063986
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ProiectMDSContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
<<<<<<< HEAD
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ICartRepository, CartRepository>();
builder.Services.AddTransient<IReviewRepository, ReviewRepository>();


builder.Services.AddScoped<IUserService,UserService>();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var servicies = scope.ServiceProvider;
    SeedData.Initialize(servicies); 
}
=======
builder.Services.AddTransient<IProductOrderRepository, ProductOrderRepository>();
builder.Services.AddTransient<ICartRepository, CartRepository>();
var app = builder.Build();

>>>>>>> 092e24880e1fba1f81168a843069f81a1c063986

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

<<<<<<< HEAD:OnlineShop/Program.cs
<<<<<<< HEAD

=======
>>>>>>> parent of d5dc1de (AllFromAlinNeedMoreTestes):OnlineShop/OnlineShop/Program.cs
app.UseHttpsRedirection();

//add a public folder to the project in which we will store the images
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Public")),
    RequestPath = "/Public"
});

// Authentication & Authorization
app.UseAuthentication();
=======
app.UseHttpsRedirection();

>>>>>>> 092e24880e1fba1f81168a843069f81a1c063986
app.UseAuthorization();

app.MapControllers();

<<<<<<< HEAD:OnlineShop/Program.cs
<<<<<<< HEAD
app.UseCors(action => action.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

=======
>>>>>>> 092e24880e1fba1f81168a843069f81a1c063986
=======
>>>>>>> parent of d5dc1de (AllFromAlinNeedMoreTestes):OnlineShop/OnlineShop/Program.cs
app.Run();
