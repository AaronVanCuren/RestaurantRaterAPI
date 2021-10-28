using RestaurantRater.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRater.Controllers
{
	public class RestaurantController : ApiController
	{
		private readonly RestaurantDbContext _db = new RestaurantDbContext();

		// POST (create)
		// api/Restaurant
		[HttpPost]
		public async Task<IHttpActionResult> CreateRestaurant([FromBody] Restaurant model)
		{
			if (model is null)
			{
				return BadRequest("Your request body cannot be empty");
			}
			// If the model is valid
			if (ModelState.IsValid)
			{
				// Store the model in the database
				_db.Restaurants.Add(model);
				await _db.SaveChangesAsync();

				return Ok("Your restaurant was created!");
			}
			// The model is not valid, go ahead and reject it
			return BadRequest(ModelState);
		}

		// GET ALL
		// api/Restauarant
		[HttpGet]
		public async Task<IHttpActionResult> GetAllRestaurants()
		{
			List<Restaurant> restaurants = await _db.Restaurants.ToListAsync();
			return Ok(restaurants);
		}

		// GET BY ID
		// api/Restaurant/{id}
		[HttpGet]
		public async Task<IHttpActionResult> GetRestaurantByID([FromUri] int id)
		{
			Restaurant restaurant = await _db.Restaurants.FindAsync(id);

			if (restaurant != null)
			{
				return Ok(restaurant);
			}

			return NotFound();
		}

		// PUT (UPDATE)
		// api/Restaurant/{id}
		[HttpPut]
		public async Task<IHttpActionResult> UpdateRestaurant([FromUri] int id, [FromBody] Restaurant updateRestauarant)
		{
			// Check the ids if they match
			if (id != updateRestauarant?.Id)
			{
				return BadRequest("Ids do not match");
			}

			// check the ModelState
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			// find the restaurant in the database
			Restaurant restaurant = await _db.Restaurants.FindAsync(id);

			// If the restaurant does not exist then do something
			if (restaurant is null)
			{
				return NotFound();
			}

			// Update the properties
			restaurant.Name = updateRestauarant.Name;
			restaurant.Address = updateRestauarant.Address;

			// Save the changes
			await _db.SaveChangesAsync();
			return Ok("The restaurant was updated!");
		}

		// DELETE
		// api/Restaurant/{id}
		[HttpDelete]
		public async Task<IHttpActionResult> DeleteRestaurant([FromUri] int id)
		{
			Restaurant restaurant = await _db.Restaurants.FindAsync(id);

			if (restaurant is null)
			{
				return NotFound();
			}

			_db.Restaurants.Remove(restaurant);

			if (await _db.SaveChangesAsync() == 1)
			{
				return Ok("The restaurant was deleted");
			}

			return InternalServerError();
		}
	}
}
