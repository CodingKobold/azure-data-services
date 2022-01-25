using Azure.Data.Tables;
using CodingKobold.DataStores.Api.Builders;
using CodingKobold.DataStores.Api.Factories;
using CodingKobold.DataStores.Api.Helpers;
using CodingKobold.DataStores.Api.Mappers;
using CodingKobold.DataStores.Api.Services;
using System.Collections.ObjectModel;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Azure Table Storage
var storageConnectionString = builder.Configuration.GetConnectionString("AzureStorage");
builder.Services.AddSingleton<TableServiceClient>(new TableServiceClient(storageConnectionString));
ConfigureTableClientFactory(builder);

// My Services
builder.Services.AddScoped<IRatedDayTableEntityMapper, DayTableEntityMapper>();
builder.Services.AddScoped<IRatedDaysService, RatedDaysService>();
builder.Services.AddScoped<IRatedDaysTableFilterBuilder, RatedDaysTableFilterBuilder>();

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


void ConfigureTableClientFactory(WebApplicationBuilder builder)
{
    var type = typeof(StorageTableNames);
    var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);
    var namedTableClients = new Collection<NamedTableClient>();

    foreach (var field in fields)
    {
        var tableName = field.GetRawConstantValue()?.ToString();
        var namedTableClient = new NamedTableClient(field.Name, new TableClient(storageConnectionString, tableName));
        builder.Services.AddSingleton(namedTableClient);
        namedTableClients.Add(namedTableClient);
    }

    builder.Services.AddSingleton<ITableClientFactory>(new TableClientFactory(namedTableClients));
}