using RobotController;
using RobotController.BackgroundWorkers;
using RobotController.Infrastructure.Configuration;
using RobotController.Services;
using RobotController.Services.Hubs;
using RobotController.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var policyName = "AllowAll";
builder.Services.AddCors(options =>
{
    options.AddPolicy(policyName, builder =>
    {
        builder
         .AllowAnyHeader()
         .AllowAnyMethod()
         .AllowCredentials()
         .WithOrigins("https://localhost:7018", "https://localhost:44474", "https://divertiteam-hackaton.azurewebsites.net")
         .SetIsOriginAllowed((host) => true);
    });
});

// Add configuration
builder.Services.Configure<RobotApiOptions>(builder.Configuration.GetSection("RobotApi"));

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddSingleton<IRobotService, RobotService>();
builder.Services.AddSingleton<IRobotCameraService, RobotCameraService>();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<RobotService>();
builder.Services.AddHttpClient<RobotCameraService>();
builder.Services.AddHostedService<KeepAliveService>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors(policyName);
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.MapHub<CommunicationHub>("/hub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");


app.Run();
