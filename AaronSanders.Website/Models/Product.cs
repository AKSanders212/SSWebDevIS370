/*
 * Aaron Keith Sanders
 * GUID: G00225605
 * UA Grantham
 * IS370: Server Side Web Development
 * Evelyn Simpson
 * Project: AaronSanders Web Project for IS370
 * 21 July 2025
 */

// Created Product.cs Model by following the provided tutorial
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AaronSanders.Website.Models
{
	public class Product
	{
		// Getters and setters for the string representation of the object from the products.json file
		public string Id {  get; set; }
		public string Maker	{ get; set; }

		[JsonPropertyName("img")]
		public string Image { get; set; }
		public string Url { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }		
		public int[] Ratings { get; set; } // Integer based array list

		// When ToString() is used, after json object conversion ToString() then => return back to json object after use
		public override string ToString() => JsonSerializer.Serialize<Product>(this);

	}
}
