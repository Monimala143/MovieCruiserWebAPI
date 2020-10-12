using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieCruiserWebAPI.Models
{
    public interface IMovieListOperations
    {
        Task<List<MovieList>> GetMovieAdminAsync();
        Task<List<MovieList>> GetMovieAnonymousAsync();
        Task<MovieList> GetMovieById(int id);
        Task<List<MovieList>> GetMovieCustomerAsync();
        Task<List<MovieList>> SearchMovieAdminAsync(string name);
        Task<MovieList> UpdateMovieList(MovieList move);
        Task<List<MovieList>> SearchMovieNonAdminAsync(string name);


    }
}