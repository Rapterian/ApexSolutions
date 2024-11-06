using ApexSolutions.Interfaces;
using ApexSolutions.Repositories;
using ApexSolutions.Services;
using ApexSolutions.Data; // Add this for DatabaseContext
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // For API controllers
builder.Services.AddRazorPages(); // For Razor Pages

// Retrieve the connection string from the configuration
var connectionString = builder.Configuration.GetConnectionString("ApexSolutionsDB");
builder.Services.AddScoped<DatabaseContext>(provider => new DatabaseContext(connectionString));
builder.Services.AddScoped<IClientRepository, ClientRepository>(); // Register the repository
builder.Services.AddScoped<ClientService>(); // Register ClientService

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Map API controllers
app.MapControllers();

// Map Razor Pages
app.MapRazorPages();

// Set the default route to your test page
app.MapGet("/", () => Results.Redirect("/Login")); // Redirect root URL to /test

app.Run();