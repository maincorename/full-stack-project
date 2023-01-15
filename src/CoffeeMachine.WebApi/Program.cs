using System.Reflection;

using CoffeeMachine.Core;
using CoffeeMachine.Core.Services;
using CoffeeMachine.Core.Services.Interfaces;
using CoffeeMachine.Data.Db;
using CoffeeMachine.Data.Db.Dal;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console().CreateLogger();

Log.Information("Приложение запускается.");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, services, loggerConfiguration) => loggerConfiguration
        .WriteTo.Console()
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services));

    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer",
            new OpenApiSecurityScheme
            {
                Name = "Authentication",
                Description = "Enter the Bearer Authentication string as following: `Bearer Generated-JWT-Token`",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                },
                new List<string>()
            }
        });

        options.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Version = "v1",
                Title = "CoffeeMachine API",
                Description = "An ASP.NET Core Web API for managing work of Coffeemachine"
            });

        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

    builder.Services.AddAuthorization();

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = AuthOptions.ISSUER,
                ValidateAudience = true,
                ValidAudience = AuthOptions.AUDIENCE,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey()
            };
        });

    builder.Services.AddDbContext<CoffeeMachineContext>(options =>
        options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            .EnableSensitiveDataLogging());

    builder.Services.AddScoped<UnitOfWork>();
    builder.Services.AddScoped<UserService>();
    builder.Services.AddScoped<ICoffeeService, CoffeeService>();
    builder.Services.AddScoped<IChangeService, ChangeService>();
    builder.Services.AddScoped<IPurchaseService, PurchaseService>();
    builder.Services.AddScoped<IStatisticService, StatisticService>();

    await using (var context = builder.Services.BuildServiceProvider().GetRequiredService<CoffeeMachineContext>())
    {
        await context.Database.MigrateAsync();

        await DbInitializer.InitializeAsync(context);
    }

    builder.Services.AddControllers();

    var app = builder.Build();

    app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();

    app.MapControllers();

    app.UseAuthentication();

    app.UseAuthorization();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });
    }

    app.Run();
}
catch (Exception exception)
{
    Log.Fatal(exception.Message);
}
finally
{
    Log.Information("Завершение приложения успешно.");
    Log.CloseAndFlush();
}