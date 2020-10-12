using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCruiserWebAPI.Models
{
    public class MovieListOperations : IMovieListOperations
    {
        private readonly MovieDBContext movie;
        public MovieListOperations(MovieDBContext m)
        {
            this.movie = m;
        }
        public async Task<List<MovieList>> GetMovieAdminAsync()
        {
            var data = from m in movie.MovieList
                       select m;
            var movieItems = await data.ToListAsync();
            return movieItems;
        }
        public async Task<List<MovieList>> GetMovieAnonymousAsync()
        {
            return await GetMovieCustomerAsync();
        }
        public async Task<List<MovieList>> GetMovieCustomerAsync()
        {
            var data = from m in movie.MovieList
                       where m.IsAvailable && m.ReleaseDate <= DateTime.Now
                       select m;
            var movieItems = await data.ToListAsync();
            return movieItems;

        }
        public async Task<List<MovieList>> SearchMovieAdminAsync(string name)
        {
            var items = from f in movie.MovieList
                        select f;
            var movieList = new List<MovieList>();
            name = name.ToUpper().Trim();
            await items.ForEachAsync(f =>
            {
                var tempName = f.MovieName.ToUpper();
                if (tempName.StartsWith(name))
                {
                    movieList.Add(f);
                }
            });

            return movieList;
        }
        public async Task<MovieList> GetMovieById(int id)
        {
            return await movie.MovieList.FindAsync(id);
        }
        public async Task<MovieList> UpdateMovieList(MovieList move)
        {
            var item = movie.MovieList.Find(move.MovieName);
            if (item != null)
            {
                item.MovieName = move.MovieName;
                item.TicketPrice = move.TicketPrice;
                item.IsAvailable = move.IsAvailable;
                item.ReleaseDate = move.ReleaseDate;
                item.ImageURL = move.ImageURL;
            }
            await movie.SaveChangesAsync();

            return item;
        }
        public async Task<List<MovieList>> SearchMovieNonAdminAsync(string name)
        {
            var items = from m in movie.MovieList
                        where m.IsAvailable && m.ReleaseDate <= DateTime.Now
                        select m;
            var movieList = new List<MovieList>();
            name = name.ToUpper().Trim();
            await items.ForEachAsync(m =>
            {
                var tempName = m.MovieName.ToUpper();
                if (tempName.StartsWith(name))
                {
                    movieList.Add(m);
                }
            });
            return movieList;
        }
    }
}
