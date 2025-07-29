using AaronSanders.Website.Models;
using AaronSanders.Website.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AaronSanders.Website.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        // public access for JsonFileProductService product services
        public JsonFileProductService ProductService;
        // get access to the Website.Models for creating/accessing products
        public IEnumerable<Product> Products { get; private set; }

        // Added the JsonFileProductService parameter for product services
        public IndexModel(ILogger<IndexModel> logger,
            JsonFileProductService productService)
        {
            _logger = logger;
            // Ensure that the productService is not Null
            ProductService = productService;
        }

        public void OnGet()
        {
            Products = ProductService.GetProducts(); // Get the products
        }
    }
}
