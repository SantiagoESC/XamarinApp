using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestAPP.Models
{
	public class Drink
	{
		#region Properties

		[JsonProperty(PropertyName = "idDrink")]
		public int IdDrink { get; set; }

		[JsonProperty(PropertyName = "strDrink")]
		public string NameDrink { get; set; }

		[JsonProperty(PropertyName = "strDrinkThumb")]
		public string ImageDrink { get; set;}

		#endregion
	}
}
