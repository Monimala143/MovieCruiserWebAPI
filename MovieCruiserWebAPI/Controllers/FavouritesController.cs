using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCruiserWebAPI.Models;

namespace MovieCruiserWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouritesController : ControllerBase
    {
        private readonly MovieDBContext context;
        public FavouritesController(MovieDBContext con)
        {
            this.context = con;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAsync(int userId)
        {
            var items = from i in context.Favourites
                        where i.UserId == userId
                        select i;

            var movies = (from m in context.MovieList
                          select m).ToList();

            await items.ForEachAsync(i =>
            {
                movies.ForEach(m =>
                {
                    if (m.MovieId == i.MovieId)
                        i.Movie = m;
                });
            });


            var movieList = await items.ToListAsync();

            if (movieList.Count == 0)
                return BadRequest("There is no movies");

            return Ok(movieList);
        }
        [HttpPost("{userId}")]
        public async Task<IActionResult> PostAsync([FromBody] int movieId, [FromRoute] int userId)
        {
            var fav = new Favourites();
            fav.MovieId = movieId;
            fav.UserId = userId;
            await context.Favourites.AddAsync(fav);
            int rows = await context.SaveChangesAsync();
            if (rows == 0)
                return BadRequest("item not added");
            return StatusCode(201);
        }
        [HttpDelete("{FavouriteId}")]
        public async Task<IActionResult> DeleteAsync(int FavouriteId)
        {

            Favourites fav = await context.Favourites.FindAsync(FavouriteId);
            if (fav == null)
                return BadRequest("movie not deleted");
            context.Favourites.Remove(fav);
            int rows = await context.SaveChangesAsync();
            if (rows == 0)
                return BadRequest("movie not deleted");
            return NoContent();
        }

    }
}

