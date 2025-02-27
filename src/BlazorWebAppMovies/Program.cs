using BlazorWebAppMovies.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BlazorWebAppMovies.Data;

var builder = WebApplication.CreateBuilder(args);

// Register the database context factory for dependency injection
builder.Services.AddDbContextFactory<BlazorWebAppMoviesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlazorWebAppMoviesContext") ?? throw new InvalidOperationException("Connection string 'BlazorWebAppMoviesContext' not found.")));

// Register the QuickGridEntityFrameworkAdapter for dependency injection
builder.Services.AddQuickGridEntityFrameworkAdapter();

// Register the database developer page exception filter for detailed error information
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to the container for Razor Components and Interactive Server Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Seed the database with initial data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    // Use the exception handler for non-development environments
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    // Use the migrations endpoint for database migrations
    app.UseMigrationsEndPoint();
}

// Use HTTPS redirection for secure communication
app.UseHttpsRedirection();

// Use antiforgery protection
app.UseAntiforgery();

// Map static assets for serving static files
app.MapStaticAssets();

// Map Razor Components and add interactive server render mode
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Run the application
app.Run();
