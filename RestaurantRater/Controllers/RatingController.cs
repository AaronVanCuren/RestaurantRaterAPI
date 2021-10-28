using RestaurantRater.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRater.Controllers
{
    public class RatingController : ApiController
    {
      private readonly RestaurantDbContext _db = new RestaurantDbContext();

      // Create new Ratings
      // POST api/Rating
      [HttpPost]
		public async Task<IHttpActionResult> CreateRating([FromBody] Rating model)
		{
         // Check if model is null
         if (model is null)
			{
            return BadRequest("You request body cannot be empty");
			}

         // Check if ModelState is valid
         if (!ModelState.IsValid)
			{
            return BadRequest(ModelState);
			}

         // Find the Restaurant by the model.RestaurantId and see that it exists
         var restaurantEntity = await _db.Restaurants.FindAsync(model.RestaurantId);
         if (restaurantEntity is null)
			{
            return BadRequest($"The target restaurant with the ID of {model.RestaurantId} does not exist.");
			}

         // Create the Rating

         // Add to the Rating table

         // Add to the Restaurant Entity
         restaurantEntity.Ratings.Add(model);
         if (await _db.SaveChangesAsync() == 1)
			{
            return Ok($"You rated restaurant {restaurantEntity.Name} successfully!");
			}

         return InternalServerError();
		}

      // Get a rating by its ID

      // Get all Ratings

      // Get all Ratings for a specific restaurant by the Restaurant ID

      // Update a Rating

      // Delete a Rating
    }
}
