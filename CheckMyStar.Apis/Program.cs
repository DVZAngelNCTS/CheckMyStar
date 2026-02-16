using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using CheckMyStar.Data;
using CheckMyStar.Apis.Services.Extensions;
using CheckMyStar.Dal.Extensions;
using CheckMyStar.Bll.Extensions;
using CheckMyStar.Bll.Mappings.Extensions;

var applicationName = $"CheckMyStar.Apis";
var version = typeof(Program).Assembly.GetName().Version;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://checkmystar.site.local:50477")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Host.UseDefaultServiceProvider(o =>
{
    o.ValidateOnBuild = true;
    o.ValidateScopes = true;
});

builder.Logging
    .ClearProviders()
    .AddSimpleConsole(options =>
    {
        options.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
    });

builder.WebHost.UseIIS();

builder.Services
    .AddEndpointsApiExplorer()
    .AddHttpContextAccessor();

// Configuration JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services
    .AddAuthorization()
    .AddServices()
    .AddDatabase(builder.Configuration)
    .AddDal()
    .AddBus()
    .AddProfile()
    .AddControllers();

// Configure OpenAPI avec les packages Microsoft natifs
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Components ??= new OpenApiComponents
        {
            SecuritySchemes = new Dictionary<string, IOpenApiSecurityScheme>()
        };

        document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();

        document.Components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = "Entrez votre token JWT dans le champ ci-dessous"
        };

        document.Security =
        [
            new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("Bearer", null, null)
                ] = []
            }
        ];

        return Task.CompletedTask;
    });
});

var app = builder.Build();

app.UseCors("AllowFrontend");

// Utiliser le logger après la construction de l'app
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Initialising {ApplicationName} v{Version}", applicationName, version);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "CheckMyStar API v1");
    });
}


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();