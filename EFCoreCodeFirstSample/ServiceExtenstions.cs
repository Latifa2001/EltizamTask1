using EFCoreCodeFirstSample.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Microsoft.VisualStudio.Services.DelegatedAuthorization;
using Microsoft.VisualStudio.Services.Organization.Client;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using static System.Net.WebRequestMethods;

namespace EFCoreCodeFirstSample
{
    public static class ServiceExtenstions
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<ApiUser>(q => { q.User.RequireUniqueEmail = true; });

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddEntityFrameworkStores<EFCoreCodeFirstSampleContext>().AddDefaultTokenProviders();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration Configuration)
        {
            var jwtSettings = Configuration.GetSection("Jwt");
            var key = "hh33hhh-eee-3333-333";

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                //o.Challenge = $"Bearer authorization_uri=https://localhost:44326/api/department";
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                    ValidAudience = jwtSettings.GetSection("Audience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                };
            });
        }

        //public static void ConfigureSwaggerDoc(this IServiceCollection services)
        //{
        //    services.AddSwaggerGen(c =>
        //    {
        //        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        //        {
        //            Description = @"JWT Authorization header using the Bearer scheme. 
        //              Enter 'Bearer' [space] and then your token in the text input below.
        //              Example: 'Bearer 12345abcdef'",
        //            Name = "Authorization",
        //            In = ParameterLocation.Header,
        //            Type = SecuritySchemeType.ApiKey,
        //            Scheme = "Bearer"
        //        });

        //        c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
        //            {
        //                new OpenApiSecurityScheme
        //                {
        //                    Reference = new OpenApiReference {
        //                        Type = ReferenceType.SecurityScheme,
        //                        Id = "Bearer"
        //                    },
        //                    Scheme = "0auth2",
        //                    Name = "Bearer",
        //                    In = ParameterLocation.Header
        //                },
        //                new List<string>()
        //            }
        //        });
        //    });

        //}
        //public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        //{
        //    app.UseExceptionHandler(error =>
        //    {
        //        error.Run(async context =>
        //        {
        //            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        //            context.Response.ContentType = "application/json";
        //            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        //            if (contextFeature != null)
        //            {
        //                Log.Error($"Something Went Wrong in the {contextFeature.Error}");

        //                await context.Response.WriteAsync(new Error
        //                {
        //                    StatusCode = context.Response.StatusCode,
        //                    Message = "Internal Server Error. Please Try Again Later."
        //                }.ToString());
        //            }
        //        });
        //    });
        //}

        //public static void ConfigureVersioning(this IServiceCollection services)
        //{
        //    services.AddApiVersioning(opt =>
        //    {
        //        opt.ReportApiVersions = true;
        //        opt.AssumeDefaultVersionWhenUnspecified = true;
        //        opt.DefaultApiVersion = new ApiVersion(1, 0);
        //        opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
        //    });
        //}

        //public static void ConfigureHttpCacheHeaders(this IServiceCollection services)
        //{
        //    services.AddResponseCaching();
        //    services.AddHttpCacheHeaders(
        //        (expirationOpt) =>
        //        {
        //            expirationOpt.MaxAge = 120;
        //            expirationOpt.CacheLocation = CacheLocation.Private;
        //        },
        //        (validationOpt) =>
        //        {
        //            validationOpt.MustRevalidate = true;
        //        }
        //    );
        //}

        //public static void ConfigureAutoMapper(this IServiceCollection services)
        //{
        //    services.AddAutoMapper(Assembly.GetExecutingAssembly());
        //}

        //public static void ConfigureRateLimiting(this IServiceCollection services)
        //{
        //    var rateLimitRules = new List<RateLimitRule>
        //    {
        //        new RateLimitRule
        //        {
        //            Endpoint = "*",
        //            Limit= 1,
        //            Period = "5s"
        //        }
        //    };
        //    services.Configure<IpRateLimitOptions>(opt =>
        //    {
        //        opt.GeneralRules = rateLimitRules;
        //    });
        //    services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
        //    services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
        //    services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        //}

    }
}
