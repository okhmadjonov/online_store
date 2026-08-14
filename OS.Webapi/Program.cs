var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.

// dotnet ef database update  --project OS.Persistence/OS.Persistence.csproj   --startup-project OS.Webapi/OS.Webapi.csproj
// dotnet ef migrations add <name> --project BB.Persistence/BB.Persistence.csproj   --startup-project BB.Webapi/BB.Webapi.csproj

// Shift+Option+F - Reformat (Macos)
// Ctrl+Shift+I - Reformat (Windows)
// dotnet publish BB.Webapi/BB.Webapi.csproj

// dotnet run --project BB.Webapi/BB.Webapi.csproj seed 
// dotnet run --project BB.Webapi/BB.Webapi.csproj temp
// dotnet run --project BB.Webapi/BB.Webapi.csproj promocodes
// dotnet run --project BB.Webapi/BB.Webapi.csproj deleteuser





if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
