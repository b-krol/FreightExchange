using Application;
using Microsoft.OpenApi.Models;
using Persistence;

const string ApiKeyName = "SessionId";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = $"{ApiKeyName} must appear in header",
        Type = SecuritySchemeType.ApiKey,
        Name = ApiKeyName,
        In = ParameterLocation.Header
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
            {
              Type = ReferenceType.SecurityScheme,
              Id = "ApiKey",
            },
            Scheme = "ApiKey",
            Name = ApiKeyName,
            In = ParameterLocation.Header,
          },
          new List<string>()
        },
      });
}
);

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddPersistenceInfrastructure(builder.Configuration);

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
