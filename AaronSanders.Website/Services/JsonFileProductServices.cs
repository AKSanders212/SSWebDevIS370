/*
 * Aaron Keith Sanders
 * G00225605
 * UA Grantham
 * IS370: Server Side Web Development
 * Evelyn Simpson
 * Aaron Sanders Web App Project
 * 21 July 2025
 */

using AaronSanders.Website.Models;
using System.Text.Json;

namespace AaronSanders.Website.Services
{
	public class JsonFileProductServices
	{
		// Setting and then getting the WebHostEnvironment
		public JsonFileProductServices(IWebHostEnvironment webHostEnvironment) {

			// Setting the webHostEnvironment
			WebHostEnvironment = webHostEnvironment;
		}

		public IWebHostEnvironment WebHostEnvironment { get; }

		// Gets the wwwroot and data path and the products.json file as a file name
		private string JsonFileName
		{
			get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); }
		}

		// Reads the json file as text, deserializes the Product Model with the jsonFileReader
		// from start to end with ReadToEnd()
		public IEnumerable<Product> GetProducts()
		{
			// The compiler was mad about the possible null references, so it used #pragma warning disable CS8603
			using (var jsonFileReader = File.OpenText(JsonFileName))
			{
#pragma warning disable CS8603 // Possible null reference return.
				return JsonSerializer.Deserialize<Product[]>(jsonFileReader.ReadToEnd(),

					new JsonSerializerOptions
					{
						PropertyNameCaseInsensitive = true
					});
#pragma warning restore CS8603 // Possible null reference return.
			}
		}
	}
}
