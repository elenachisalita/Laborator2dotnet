using Laborator2.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laborator2.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MoviesDbContext(serviceProvider.GetRequiredService<DbContextOptions<MoviesDbContext>>()))
            {
                if(!context.Users.Any())
                {
                    context.Users.Add(new User
                    {
                        FirstName = "First",
                        LastName = "Last",
                        Username = "FirstUsername",
                        Password = HashUtils.GetHashString("parolasigura")
                    }) ;
                    context.SaveChanges();
                }
                // Look for any movies.
                if (context.Movies.Any())
                {
                    return;   // DB has been seeded
                }

                context.Movies.AddRange(
                    new Movie
                    {
                        Title = "When Harry Met Sally",
                        Description = "description1",
                        Genre = Genre.Thriller,
                        Duration = 5,
                        YearOfRelease =1990,
                        Director = "John B",
                        DateAdded = DateTime.UtcNow,
                        Rating = 1,
                        Watched = true

                    }

                );
                context.SaveChanges();
            }
        }
    }
}
