using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestaurantRater.Models
{
	public class Rating
	{
		[Key]
		public int Id { get; set; }

		// Foreign Key
		[ForeignKey(nameof(Restaurant))]
		public int RestaurantId { get; set; }

		// Navigational Property
		public virtual Restaurant Restaurant { get; set; }

		[Required]
		[Range(0, 10)]
		public double FoodScore { get; set; }

		[Required]
		[Range(0, 10)]
		public double Cleanliness { get; set; }

		[Required]
		[Range(0, 10)]
		public double EnvironmentScore { get; set; }

		public double AverageRating
		{
			get
			{
				var TotalScore = FoodScore + Cleanliness + EnvironmentScore;
				return TotalScore / 3;
			}
		}
	}
}