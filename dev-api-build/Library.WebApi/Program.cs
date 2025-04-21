using Microsoft.AspNetCore.Mvc.Formatters;
using Library.EntityModels;
using Microsoft.Extensions.Caching.Memory;
using Library.WebApi.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);
var domain = $"{builder.Configuration["Auth0:Domain"]}/";

// Add authentication service to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options => {
    options.Authority = domain;
    options.Audience = builder.Configuration["Auth0:Audience"];
    options.RequireHttpsMetadata = false;
});

builder.Services.AddAuthorization(options => {
    options.AddPolicy("read:privbooks", policy => policy.Requirements.Add(
        new HasScopeRequirement("read:privbooks", domain)));
});

// Add services to the container.
builder.Services.AddLibraryContext();
builder.Services.AddControllers(options =>
{
    WriteLine("Default output formatters: ");
    foreach (IOutputFormatter formatter in options.OutputFormatters)
    {
        OutputFormatter? mediaFormatter = formatter as OutputFormatter;
        if (mediaFormatter is null)
        {
            WriteLine($" {formatter.GetType().Name}");
        }
        else
        {
            WriteLine($"{mediaFormatter.GetType().Name}, Media Types: {mediaFormatter.SupportedMediaTypes}");
        }
    }
})
.AddXmlDataContractSerializerFormatters()
.AddXmlSerializerFormatters();

// Implement in-memory cache
builder.Services.AddSingleton<IMemoryCache>(new MemoryCache(new MemoryCacheOptions()));

// Add authorization handler
builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBookRepository, BookRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .WithOrigins("https://localhost:5151")
    .SetIsOriginAllowed(origin => true));

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
