using Application.Configuration;
using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SQLiteConnection")));

builder.Services.Configure<ExternalConnection>(builder.Configuration.GetSection("ExternalConnection"));

builder.Services.AddScoped<IDataRepository, DataRepository>();
builder.Services.AddScoped<IDataService, DataService>();

builder.Services.AddHttpClient<DataService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Api Prueba Tecnica Gabriel Arroyo Pajares",
        Version = "v1",
        Description = @$"Esta aplicación esta creada para la realización de la prueba técnica de Gabriel Arroyo Pajares. {Environment.NewLine}
Las tecnologias utilizadas han sido: {Environment.NewLine}
         - .Net 8 {Environment.NewLine}
         - EntityFramework Core {Environment.NewLine}
         - Sqllite {Environment.NewLine}
         - Newtonsoft {Environment.NewLine}
         - Arquitectura DDD"
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
        c.RoutePrefix = string.Empty;
    });
}
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
