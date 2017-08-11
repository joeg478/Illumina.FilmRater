using Illumina.CotentRater.Dal;
using Illumina.CotentRater.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Illumina.CotentRater.Controllers
{
    public class FilmController : ApiController
    {
        IRepository<Film> _repository = new FilmRepository();

        public IHttpActionResult Get()
        {
            var films = _repository.Get();
            return Ok(films);
        }

        [HttpPost]
        public IHttpActionResult Save(Film film)
        {
            try
            {
                var savedFilm = _repository.Save(film);
                return Ok(savedFilm);
            }
            catch(ArgumentException)
            {
                return Conflict();
            }
        }

        [HttpPut]
        public IHttpActionResult Update(Film film)
        {
            try
            {
                _repository.Update(film);
                return Ok(film);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }
    }
}