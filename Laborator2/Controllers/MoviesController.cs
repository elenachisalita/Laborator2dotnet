﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Laborator2.Models;
using Laborator2.ViewModels;

namespace Laborator2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        private readonly MoviesDbContext _context;


        public MoviesController(MoviesDbContext context)
        {
            _context = context;

        }


        /// <summary>
        /// Retrieves a list of all movies from DB.
        /// </summary>
        /// <returns>List of movies</returns>
        // GET: movies
        [HttpGet]
        public IEnumerable<MovieGetModel> GetMovies()     // get de view model care are nr de comm
        {
            IQueryable<Movie> result = _context.Movies
                                        .Include(m => m.Comments);

            return result.Select(m => MovieGetModel.GetMovieModel(m));
        }

        /// <summary>
        /// Retrieves a specific movie by id, list of its comments included.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns the movie specified by id and its list of comments.</returns>
        // GET: movies/5
        [HttpGet("{id}")]
        public MovieDetails GetMovieById(long id)
        {

            var movie = _context.Movies
                  .Include(m => m.Comments)
                  .FirstOrDefault(m => m.Id == id);
                 
                  


            return MovieDetails.GetMovieModel(movie);
        }



        /// <summary>
        /// Retrieves filtered movies, added between certain dates, also alphabetically ordered.
        /// </summary>
        /// <remarks>
        /// Sample URL request:
        ///    https://localhost:44335/movies/filter?$from=2020-05-15T00:00:00&to=2020-05-17T00:00:00
        /// Sample parameter: yyyy-MM-dd   
        /// </remarks>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>A list of movies with dateAdded between the two specified dates.</returns>
        // GET: movies/filter?from=a&to=b
        [HttpGet("filter")]
        public IEnumerable<MovieGetModel> GetFilteredMovies(
            [FromQuery] string from,
            [FromQuery] string to)
        {
            DateTime fromDate = DateTime.Parse(from);
            DateTime toDate = DateTime.Parse(to);

            var result = this.GetMovies()      
                .Where(o => (o.DateAdded > fromDate) && (o.DateAdded < toDate));

            var query = result
                .OrderBy(o => o.YearOfRelease);
            //  .ToListAsync();

            return query;
        }







        /// <summary>
        /// Edit any properties of a specific movie you mention by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movie"></param>
        /// <returns></returns>
        // PUT: movie/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(long id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }





        /// <summary>
        /// Creates a new movie.
        /// </summary>
        /// <param name="movie"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Movies
        ///      {
        ///         "dateAdded": "2019-04-05",
        ///         "description": "Frodo",
        ///         "director": "Steven Spielberg",
        ///         "duration": "120",
        ///         "rating": "10",
        ///         "title": "Stapanul inelelor3",
        ///         "watched": "True",
        ///         "yearOfRelease": "2000"
        ///       }
        ///
        /// </remarks>
        /// <param name="movie"></param>
        /// <returns>A newly created movie</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        // POST: /movies
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostMovie(Movie movie)
        {

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();


            return CreatedAtAction("GetMovieById", new { id = movie.Id }, movie);
            //  return Ok(movie);
        }





        /// <summary>
        /// Deletes the movie tou specify by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns the movie deleted.</returns>
        // DELETE: /5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> DeleteMovie(long id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            _context.Entry(movie).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            return movie;
        }



        private bool MovieExists(long id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }



    }


}
