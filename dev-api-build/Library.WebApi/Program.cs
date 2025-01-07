using Microsoft.AspNetCore.Mvc.Formatters;
using Library.EntityModels;
using Microsoft.Extensions.Caching.Memory;
using static System.Console;
using System.Runtime.Serialization;
using Library.WebApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
