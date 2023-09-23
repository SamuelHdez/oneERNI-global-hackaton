using RobotController.Services;
using RobotController.Services.Hubs;
using RobotController.Infrastructure.Configuration;

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
         .WithOrigins("https://localhost:7018", "https://localhost:44474");
        //builder.AllowAnyOrigin()
        //         .AllowAnyMethod()
        //         .AllowAnyHeader();
    });
});

// Add configuration
builder.Services.Configure<RobotApiOptions>(builder.Configuration.GetSection("RobotApi"));

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddScoped<IRobotService, RobotService>();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<RobotService>();

var app = builder.Build();

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
