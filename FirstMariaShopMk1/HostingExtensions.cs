using MariaShop.Api.Application.Mapping;
using MariaShop.Api.Application.Services;
using MariaShop.Api.Context;
using MariaShop.Api.Infrastructure.Repositories;
using MariaShop.Api.Infrastructure.UnitOfWork;
using MariaShop.Api.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace MariaShop.Api
{
    public static class HostingExtensions
    {
        public static WebApplicationBuilder ConfigureServices(
            this WebApplicationBuilder builder) {
            // =======================
            // Options validation
            // =======================

            builder.Services
                .AddOptions<DatabaseOptions>()
                .Bind(builder.Configuration.GetSection("Database"))
                .Validate(o => !string.IsNullOrWhiteSpace(o.ConnectionString),
                    "Database:ConnectionString não configurada")
                .ValidateOnStart();

            // Controllers
            builder.Services.AddControllers();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MariaShop API",
                    Version = "v1"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Informe o token no formato: Bearer {token}"
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
                        }
                    },
                    Array.Empty<string>()
                }
            });
            });

            // DbContext
            builder.Services.AddDbContext<AppDbContext>((sp, options) =>
            {
                var dbOptions = sp
                    .GetRequiredService<IOptions<DatabaseOptions>>()
                    .Value;

                options.UseMySql(
                    dbOptions.ConnectionString,
                    ServerVersion.AutoDetect(dbOptions.ConnectionString)
                );
            });

            // Authentication / Authorization
            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = builder.Configuration["Identity:Authority"];
                    options.Audience = builder.Configuration["Identity:Audience"];
                    options.RequireHttpsMetadata = true;
                });

            builder.Services.AddAuthorization();

            // Repositories / UoW / Services
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();

            // AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

            return builder;
        }

        public static WebApplication ConfigurePipeline(this WebApplication app) {
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            return app;
        }
    }

}
