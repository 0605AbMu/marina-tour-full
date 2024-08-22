using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using Serilog;
using WebApi.Brokers;
using WebApi.Common;
using WebApi.Filters;
using WebApi.Middlewares;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
     .WriteTo.Console()
     .CreateLogger();

Log.Information("Project started at {0} with PID: {1}",
     DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss"),
     Environment.ProcessId);

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

// Add services to the container.

builder.Services.AddControllers(options =>
{
     options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = false;
     options.Filters.Add<ModelValidationFilter>();
}).AddJsonOptions(options =>
{
     options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
     options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
});
builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
     var securityScheme = new OpenApiSecurityScheme()
     {
          In = ParameterLocation.Cookie,
          Type = SecuritySchemeType.ApiKey,
          Description = "Cookie based Authentication",
          Name = "Jwt Bearer",
          Scheme = JwtBearerDefaults.AuthenticationScheme,
     };

     options.AddSecurityDefinition("Bearer", securityScheme);

     options.AddSecurityRequirement(new OpenApiSecurityRequirement()
     {
          {
               securityScheme, new List<string>()
               {
                    "Bearer"
               }
          }
     });
});
builder.Services.AddSerilog();

builder.Services.AddScoped<GlobalExceptionHandlerMiddleware>();

builder.Services.AddDbContextPool<AppDbContext>(optionsBuilder =>
{
     var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
     var sourceBuilder = new NpgsqlDataSourceBuilder()
     {
          ConnectionStringBuilder =
          {
               ConnectionString = connectionString
          }
     };

     optionsBuilder
          .EnableDetailedErrors()
          .EnableSensitiveDataLogging()
          .UseNpgsql(sourceBuilder.Build());
});

builder.Services.AddAuthorization(options =>
{
     options.AddPolicy(Roles.Admin,
          policyBuilder =>
          {
               policyBuilder.RequireAuthenticatedUser();
               policyBuilder.RequireRole(Roles.Admin);
          });

     options.AddPolicy(Roles.Client,
          policyBuilder =>
          {
               policyBuilder.RequireAuthenticatedUser();
               policyBuilder.RequireRole(Roles.Client, Roles.Admin);
          });
});

builder.Services.AddAuthentication(options =>
     {
          options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
          options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
     })
     .AddJwtBearer(options =>
     {
          options.TokenValidationParameters = new TokenValidationParameters()
          {
               ValidateIssuer = false,
               ValidateAudience = false,
               ValidateLifetime = true,
               ClockSkew = TimeSpan.Zero,
               SaveSigninToken = true,
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtRequirements.SigningKey)),
          };

          options.Events = new JwtBearerEvents()
          {
               OnMessageReceived = context =>
               {
                    context.Token = context.Request.Cookies[JwtRequirements.TokenNameInCookie];
                    return Task.CompletedTask;
               }
          };
     })
     .AddCookie(options =>
     {
          options.Cookie.HttpOnly = true;
          options.Cookie.Expiration = TimeSpan.FromMinutes(60);
     });

builder.Services.AddCors(options =>
{
     options.AddDefaultPolicy(policyBuilder =>
     {
          policyBuilder.AllowAnyHeader().AllowAnyMethod().AllowCredentials();
          policyBuilder.WithOrigins("http://localhost:8081");
     });
});

builder.Services.AddHealthChecks();
builder.Services.AddMemoryCache();

builder.Services.AddSingleton<NotificationBroker>();
builder.Services.AddHttpClient("NotificationBrokerHttpClient",
     client =>
     {
          client.BaseAddress = new Uri(builder.Configuration.GetSection("Eskiz").GetValue<string>("BaseUrl")!);
     });

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHealthChecks("/healthy");

app.UseDefaultFiles();
var cacheMaxAgeOneWeek = (60 * 60 * 24 * 7).ToString();
app.UseStaticFiles(new StaticFileOptions()
{
     HttpsCompression = HttpsCompressionMode.Compress,
     ServeUnknownFileTypes = true,
     OnPrepareResponse = context =>
     {
          context.Context.Response.Headers.Append(
               "Cache-Control",
               $"public, max-age={cacheMaxAgeOneWeek}");
     }
});

app.UseCors();
app.UsePathBase("/api");
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();


app.UseRouting();
if (!app.Environment.IsProduction())
{
     app.UseSwagger();
     app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("index.html");

var uploadsDirPath = Path.Join(app.Environment.WebRootPath, "uploads");
if (!Directory.Exists(uploadsDirPath))
     Directory.CreateDirectory(uploadsDirPath);

app.Run();