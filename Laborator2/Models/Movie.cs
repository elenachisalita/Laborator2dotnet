using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laborator2.Models
{
    public enum Genre
    {
        Action,
        Comedy,
        Horror,
        Thriller
    }
    public enum Watched
    {
        Yes,
        No
    }
  
    public class Movie
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Genre Genre { get; set; }
        public int Duration { get; set; }
        public int YearOfRelease { get; set; }
        public string Director { get; set; }
        public DateTimeOffset DateAdded { get; set; }
        public int Rating { get; set; }
        public Watched Watched { get; set; }


       
    }
}
