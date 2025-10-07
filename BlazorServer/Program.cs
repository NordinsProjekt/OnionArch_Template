using ApiClient;
using BlazorServer.Components;
using Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// EF Core DbContext registration (InMemory for example/demo; change to SQL Server in production)
builder.Services.AddDbContext<CMSDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Register generic api client infrastructure
builder.Services.AddGenericApiClient();

// Example: register Api Ninjas client (values should come from configuration/secrets in real app)
var apiNinjasKey = builder.Configuration["ApiNinjas:ApiKey"] ?? "YOUR_DEV_API_KEY";
builder.Services.AddApiNinjasClient(
    "ApiNinjas",
    apiNinjasKey,
    opts =>
    {
        opts.BaseUrl = builder.Configuration["ApiNinjas:BaseUrl"] ?? "https://api.api-ninjas.com/v1/";
        opts.DefaultHeaders["Accept"] = "application/json";
    });

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
