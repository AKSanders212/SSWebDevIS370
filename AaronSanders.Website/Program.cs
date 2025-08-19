using AaronSanders.Website.Models;
using AaronSanders.Website.Services;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers(); // Add the Controllers to the project
builder.Services.AddServerSideBlazor(); // Add Blazor server

// Adding the JsonFileProductServices service from the tutorial
builder.Services.AddTransient<JsonFileProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

// Added the UseEndpoints extension method lambda expression here
app.UseEndpoints(endpoints =>
{
    // Map the Razor Pages and the Controllers
    endpoints.MapRazorPages();
    endpoints.MapControllers();

    // Map Blazor to endpoints
    endpoints.MapBlazorHub();

    // Get the products json with json serializer and map it to /products for the URL
    //app.MapGet("/products", context =>
    //{
    //    // It is Services now because ApplicationServices is deprecated
    //    var products = app.Services.GetService<JsonFileProductService>().GetProducts();
    //    var json = JsonSerializer.Serialize<IEnumerable<Product>>(products);
    //    return context.Response.WriteAsync(json);
        
    //});
});

app.Run();
