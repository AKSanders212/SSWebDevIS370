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
	public class JsonFileProductService
	{
		// Setting and then getting the WebHostEnvironment
		public JsonFileProductService(IWebHostEnvironment webHostEnvironment) {

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

		public void AddRating(string productID, int rating)
		{
			// Getting the products and their ID and if there is a lack of a rating, then we add one
			var products = GetProducts();
			var query = products.First(x => x.Id == productID);

			if (query.Ratings == null)
			{
				query.Ratings = new int[] { rating };
			}
			else
			{
				// We add a rating by initializing a new list of ratings, adding a new rating to the list,
				// then converting the integer rating to be an element in the array through casting it ToArray()
				var ratings = query.Ratings.ToList();
				ratings.Add(rating);
				query.Ratings = ratings.ToArray();
			}

			using (var outputStream = File.OpenWrite(JsonFileName))
			{
				// Opening the Json file as an output stream with write privileges
				// Serializing the json into text editable format, and then modifying that text format,
				// to apply changes to the Json products file
				JsonSerializer.Serialize<IEnumerable<Product>>(
					new Utf8JsonWriter(outputStream, new JsonWriterOptions
					{
						SkipValidation = true,
						Indented = true
					}),
					products
				);
			}
		}
	}
}
