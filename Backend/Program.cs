using System.Reflection;
using Backend.Contexts;
using Backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL("server=127.0.0.1;uid=root;pwd=rsbr220Sql!;database=paleo"));
builder.Services.AddScoped<IntervalService>();
builder.Services.AddScoped<OccurrenceService>();
builder.Services.AddScoped<CladisticsService>();
// builder.Services.AddScoped<SpeciesService>();
// builder.Services.AddScoped<TaxaService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddSimpleConsole();

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.Logger.LogInformation("Starting application");
// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();

}
app.UseHttpsRedirection();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    // endpoints.MapControllerRoute(
    //     "default",
    //     "{controller=Home}/{action=Index}/{id?}");
});
app.UseRouting();
app.UseHttpsRedirection();
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}