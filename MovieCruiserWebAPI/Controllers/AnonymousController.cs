using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCruiserWebAPI.Models;

namespace MovieCruiserWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnonymousController : ControllerBase
    {
        IMovieListOperations operation;
        public AnonymousController(IMovieListOperations operation)
        {
            this.operation = operation;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {   

            var items = await operation.GetMovieAnonymousAsync();

            if (items.Count == 0)
                return BadRequest("No movies");
            return Ok(items);
        }

        [HttpGet("search/{name}")]
        public async Task<IActionResult> GetAsync(string name)
        {
            var itemList = await operation.SearchMovieNonAdminAsync(name);

            if (itemList==null)
                return NotFound("Object cannot be found");

            return Ok(itemList);
        }
    }
}
