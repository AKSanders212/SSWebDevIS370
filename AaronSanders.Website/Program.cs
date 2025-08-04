using AaronSanders.Website.Models;
using AaronSanders.Website.Services;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

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
    // Get the products json with json serializer and map it to /products for the URL
    app.MapGet("/products", context =>
    {
        // It is Services now because ApplicationServices is deprecated
        var products = app.Services.GetService<JsonFileProductService>().GetProducts();
        var json = JsonSerializer.Serialize<IEnumerable<Product>>(products);
        return context.Response.WriteAsync(json);
        
    });
});

app.Run();
