using Laborator2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Laborator2.ViewModels
{
    public class MovieDetails
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Genre Genre { get; set; }
        public int Duration { get; set; }
        public int YearOfRelease { get; set; }
        public string Director { get; set; }
        public DateTimeOffset DateAdded { get; set; }

        [Range(1, 10)]
        public int Rating { get; set; }
        public bool Watched { get; set; }

        public List<CommentGetModel> Comments { get; set; }

        public static MovieDetails GetMovieModel(Movie movie)
        {
            return new MovieDetails
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                Genre = movie.Genre,
                Duration = movie.Duration,
                YearOfRelease = movie.YearOfRelease,
                Director = movie.Director,
                DateAdded = movie.DateAdded,
                Rating = movie.Rating,
                Watched = movie.Watched,
                Comments = movie.Comments.Select(c => CommentGetModel.GetCommentModel(c)).ToList()
            };
        }

    }
}
