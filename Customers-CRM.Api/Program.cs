using Customers_CRM.Library.Classes;
using Customers_CRM.Library.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<Customer>();
builder.Services.AddScoped<ILoadXml, LoadXml>();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "IQTAXI - Customers CRM",
        Description = "API Customers CRM - DEMO",
        Contact = new OpenApiContact
        {
            Name = "IQTAXI - Customers CRM",
            Url = new Uri("https://www.iqtaxi.com/iqtaxi/")
        },
       
    });
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
