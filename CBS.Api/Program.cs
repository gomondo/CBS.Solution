using CBS.Api.Data;
using CBS.BLL.Mapping;
using CBS.BLL.Services;
using CBS.Data;
using CBS.Data.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using CBS.Data.Entities;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("cbsConnectionString");

if (!string.IsNullOrEmpty(connectionString))
{
    // DbContexts
    builder.Services.AddDbContextFactory<ApplicationDbContext>(
        option => option.UseSqlServer(connectionString));
    builder.Services.AddDbContextFactory<CBSDbContext>(
        option => option.UseSqlServer(connectionString));

    // ASP.NET Core Identity
    builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
    {
        option.Password.RequiredLength = 8;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager<SignInManager<ApplicationUser>>();

    // Repositories & Services
    builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    builder.Services.AddScoped(typeof(IService<,>), typeof(Service<,>));
    builder.Services.AddScoped<IAppointmentService, AppointmentService>();
    builder.Services.AddScoped<ITimeslotService, TimeslotService>();

    // AutoMapper
    builder.Services.AddAutoMapper(option => option.AddProfile(typeof(DataMapping)));
}

// Swagger with JWT support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter your JWT token below."
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id   = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Controllers
builder.Services.AddControllers();

// CORS — allow the Blazor WASM client origin
// FIX: Removed leading space from origin https://localhost:44351'
builder.Services.AddCors(options =>
{
    options.AddPolicy("cbs-api", policy =>
        policy.WithOrigins("https://localhost:44351", "https://localhost:7272","http://localhost:5238")  // <- Blazor client origin
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// JWT Bearer Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Jwt:Authority"];

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
        };

        options.RequireHttpsMetadata = true; // set false only in local dev if needed
    });

// Authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "api.read");
    });
});

var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("cbs-api"); // Must be before UseAuthentication

// Order matters: Authentication -> Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
