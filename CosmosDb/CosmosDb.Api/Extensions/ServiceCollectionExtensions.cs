using System.Text;
using CosmodeDb.Api.Handlers;
using CosmodeDb.Domain.Security.Interfaces;
using CosmodeDb.Domain.Settings;
using CosmosDb.Domain;
using CosmosDb.Domain.Products.Interfaces;
using CosmosDb.Domain.Security.Interfaces;
using CosmosDb.Domain.Settings;
using CosmosDb.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace CosmosDb.Api.Extensions;

public static class ServiceCollectionExtensions
{
  public const string API_VERSION = "v1";
  public const string API_TITLE = "Cosmos DB Lab API";
  public const string AUTH_SCHEME = "Bearer";

  public static IServiceCollection AddSettings(this IServiceCollection services, ConfigurationManager configuration)
  {
    services.Configure<CosmosDbSettings>(configuration.GetSection(nameof(CosmosDbSettings)));
    services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
    services.Configure<SecuritySettings>(configuration.GetSection(nameof(SecuritySettings)));
    
    return services;
  }

  public static IServiceCollection AddHandlers(this IServiceCollection services)
  {
    services.AddScoped<IRegisterHandler, RegisterHandler>();
    services.AddScoped<ILoginHandler, LoginHandler>();
    
    return services;
  }

  public static IServiceCollection AddServices(this IServiceCollection services)
  {
    services.AddScoped<IProductService, ProductService>();
    services.AddScoped<ITokenService, JwtTokenService>();

    return services;
  }

  public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
  {
    services.AddSwaggerGen(option => 
    {
      option.SwaggerDoc(API_VERSION, new OpenApiInfo { Title = API_TITLE, Version = API_VERSION });

      option.AddSecurityDefinition(AUTH_SCHEME, new OpenApiSecurityScheme
      {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = AUTH_SCHEME
      });

      option.AddSecurityRequirement(new OpenApiSecurityRequirement
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
            {
              Type = ReferenceType.SecurityScheme,
              Id = AUTH_SCHEME
            }
          }, []
        }
      });
    });

    return services;
  }

  public static IServiceCollection ConfigureSecurity(this IServiceCollection services, ConfigurationManager configuration)
  {
    var jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();

    if (jwtSettings is null)
      throw new NullReferenceException(nameof(jwtSettings));

    services
      .AddAuthentication(a => 
      {
        a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(j => 
      {
        j.TokenValidationParameters = new TokenValidationParameters() 
        {
          IssuerSigningKey = new SymmetricSecurityKey(key: Encoding.ASCII.GetBytes(jwtSettings.PrivateKey)),
          ValidateIssuer = false,
          ValidateAudience = false
        };
      });

    services.AddAuthorizationBuilder()
            .AddPolicy(Constants.ADMIN_POLICY, p => p.RequireRole(Constants.ADMIN_ROLE))
            .AddPolicy(Constants.STUDENT_POLICY, p => p.RequireRole(Constants.STUDENT_ROLE));

    return services;
  }
}