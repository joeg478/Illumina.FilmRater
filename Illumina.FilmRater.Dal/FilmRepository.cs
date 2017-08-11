using Illumina.CotentRater.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Illumina.CotentRater.Dal
{
    public class FilmRepository : IRepository<Film> 
    {
        //create real DB (SQL, MongoDB, etc) to connect tos
        private static volatile List<Film> data = null;
        private static object syncRoot = new Object();

        private static void PopulateRepository()
        {
            if (data == null)
            {
                lock(syncRoot)
                {
                    if (data == null)
                    {
                        data = new List<Film>();
                        data.Add(new Film() { Id = Guid.NewGuid(), Title = "Thor", Director = "Kenneth Branagh", Rating = 3, ReleaseDate = new DateTime(2011, 7, 25) });
                        data.Add(new Film() { Id = Guid.NewGuid(), Title = "X-Men: First Class", Director = "Matthew Vaughn", Rating = 3, ReleaseDate = new DateTime(2011, 11, 3) });
                        data.Add(new Film() { Id = Guid.NewGuid(), Title = "Iron Man", Director = "Jon Favreau", Rating = 2, ReleaseDate = new DateTime(2008, 5, 25) });
                        data.Add(new Film() { Id = Guid.NewGuid(), Title = "The Illusionist", Director = "Neil Burger", Rating = 4, ReleaseDate = new DateTime(2006, 5, 4) });
                        data.Add(new Film() { Id = Guid.NewGuid(), Title = "The Prestige", Director = " Christopher Nolan", Rating = 5, ReleaseDate = new DateTime(2006, 7, 13) });
                        data.Add(new Film() { Id = Guid.NewGuid(), Title = "Die Hard", Director = "John McTiernan", Rating = 2, ReleaseDate = new DateTime(1988, 1, 15) });
                        data.Add(new Film() { Id = Guid.NewGuid(), Title = "Ace Ventura", Director = "Tom Shadyac", Rating = 1, ReleaseDate = new DateTime(1994, 12, 22) });
                        data.Add(new Film() { Id = Guid.NewGuid(), Title = "Guardians of the Galaxy", Director = "James Gunn", Rating = 5, ReleaseDate = new DateTime(2014, 2, 3) });
                        data.Add(new Film() { Id = Guid.NewGuid(), Title = "Empire Strikes Back", Director = "Irvin Kershner", Rating = 4, ReleaseDate = new DateTime(1978, 4, 8) });
                        data.Add(new Film() { Id = Guid.NewGuid(), Title = "Matrix", Director = "The Wachowskis", Rating = 5, ReleaseDate = new DateTime(1999, 1, 20) });
                    }
                }
            }
        }

        public FilmRepository()
        {
            PopulateRepository();
        }

        public IEnumerable<Film> Get()
        {
            return data.ToList();
        }

        public Film Save(Film film)
        {
            if (film.Id != Guid.Empty)
                throw new ArgumentException("Cannot save an existing film.  Use Update method");
            film.Id = Guid.NewGuid();
            data.Add(film);
            return film;
        }

        public void Update(Film film)
        {
            if (film.Id == Guid.Empty)
                throw new ArgumentException("Cannot update an new film.  Use Save method");
            var existing = data.FirstOrDefault(f => f.Id == film.Id);
            data.Remove(existing);
            data.Add(film);
        }
    }
}
