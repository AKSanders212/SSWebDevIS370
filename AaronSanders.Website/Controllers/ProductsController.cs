using AaronSanders.Website.Models;
using AaronSanders.Website.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AaronSanders.Website.Controllers
{
	[Route("[controller]")] // Point the route path directly to controller
	[ApiController]
	public class ProductsController : ControllerBase
	{
		public ProductsController(JsonFileProductService productService)
		{
			// Set the ProductService of ControllerBase parent class to the one for this method
			this.ProductService = productService;
		}

		public JsonFileProductService ProductService { get; } // Getting a copy of the ProductService

		public IEnumerable<Product> Get()
		{
			// Retrieves the products from the enumeration
			return ProductService.GetProducts();
		}

		// [HttpPatch] "[FromBody]" - Replace Get with Patch, or Post when needed
		[Route("Rate")]
		[HttpGet]
		public ActionResult Get([FromQuery] string ProductId, [FromQuery] int Rating)
		{
			ProductService.AddRating(ProductId, Rating);
			return Ok(); // Returns a 200 OK status response as an Ok Result object
		}
	}
}
