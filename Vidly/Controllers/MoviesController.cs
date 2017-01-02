using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            //var movies = _context.Movies.Include(g => g.Genre).ToList();
            if (User.IsInRole(RoleName.CanManageMovies))
            {
                return View("List");
            }
            return View("ReadOnlyList");
        }
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New(int? id)
        {
            var vm = new MovieFormViewModel
            {
                Genres = _context.Genres.ToList(),
                ViewTitle = "Add New Movie"
            };
            return View("MovieForm", vm);
        }
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.Single(m => m.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            var vm = new MovieFormViewModel(movie)
            {
                Genres = _context.Genres.ToList(),
                ViewTitle = "Edit Movie"
            };
            return View("MovieForm", vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var vm = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList(),
                    ViewTitle = "Edit Movie"
                };
                return View("MovieForm", vm);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieToEdit = _context.Movies.Single(m => m.Id == movie.Id);
                movieToEdit.Name = movie.Name;
                movieToEdit.GenreId = movie.GenreId;
                movieToEdit.ReleaseDate = movie.ReleaseDate;
                movieToEdit.NumberInStock = movie.NumberInStock;
            }
            try
            {
                _context.SaveChanges();
                return RedirectToAction("Index", "Movies");
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);

                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }

        }
        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(g => g.Genre).SingleOrDefault(m=>m.Id==id);
            if (movie == null)
            {
               return HttpNotFound();
            }
            return View(movie);
        }
        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie {Name = "Shrek!"};
            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1"},
                new Customer { Name = "Customer 2"}
            };
            var vm = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };
            return View(vm);
        }
        [Route("movies/released/{year:regex(\\d{2})}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        
    }
}