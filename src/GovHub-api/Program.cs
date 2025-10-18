using GovHub.Api.Hubs;
using GovHub.Api.Services;
using GovHub.Lib.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// Add SignalR
builder.Services.AddSignalR();

// Register application services
builder.Services.AddSingleton<IServiceCatalogService, ServiceCatalogService>();
builder.Services.AddSingleton<IAIOrchestrationService, AIOrchestrationService>();
builder.Services.AddSingleton<ITrackerService, TrackerService>();
builder.Services.AddSingleton<IReminderService, ReminderService>();
builder.Services.AddScoped<ChatOrchestrator>();

// Add CORS for Blazor app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp", policy =>
    {
        policy.WithOrigins("https://localhost:5001", "http://localhost:5000", "https://localhost:7001", "http://localhost:5002")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseCors("AllowBlazorApp");

app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chathub");

app.Run();
