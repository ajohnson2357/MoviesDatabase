using Microsoft.AspNet.Identity;
using MovieDatabase.Models;
using MovieDatabase.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieDatabase.Controllers
{
    [Authorize]
    public class MovieDatabaseController : Controller
    {
        // GET: MovieDatabase
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MovieService(userId);
            var model = service.GetAllMovies();

            return View(model);
        }

        //GET
        public ActionResult Create()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MovieService(userId);
            ViewBag.Movie = service.GetAllMovies();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovieCreate movie)
        {
            if (!ModelState.IsValid) return View(movie);

            var service = CreateMovieService();

            if(service.CreateMovie(movie))
            {
                TempData["SaveResult"] = "Your movie was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Movie could not be created.");

            return View(movie);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateMovieService();
            var movie = svc.GetMovieById(id);

            return View(movie);
        }

        //GET
        public ActionResult Edit(int id)
        {
            //var userId = Guid.Parse(User.Identity.GetUserId());
            //var service = new MovieService(userId);
            //ViewBag.MovieList = service.GetAllMovies();

            var svc = CreateMovieService();
            var detail = svc.GetMovieById(id);
            var newMovie =
                new MovieEdit
                {
                    Description = detail.Description,
                    Genre = detail.Genre,
                    HaveWatched = detail.HaveWatched,
                    IsStarred = detail.IsStarred,
                    MovieQuality = detail.MovieQuality,
                    MovieRating = detail.MovieRating,
                    RunTime = detail.RunTime,
                    Title = detail.Title,
                    MovieId = detail.MovieId
                };
            return View(newMovie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MovieEdit movie)
        {
            if (!ModelState.IsValid) return View(movie);

            if(movie.MovieId != id)
            {
                ModelState.AddModelError("", "Id does not match");
                return View(movie);
            }

            var service = CreateMovieService();

            if(service.UpdateMovie(movie))
            {
                TempData["SaveResult"] = "Your movie was edited.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your movie could not be edited.");
            return View();
        }

        //GET: My movies
        public ActionResult ViewMyMovies(int id)
        {
            MovieService movie = new MovieService(Guid.Parse(User.Identity.GetUserId()));
            var detail = movie.GetMovieById(id);
            return View(detail);
        }

        //GET: Delete Movie
        public ActionResult Delete(int id)
        {
            var svc = CreateMovieService();
            var movie = svc.GetMovieById(id);
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMovie(int id)
        {
            if (!ModelState.IsValid) return View();

            if (ModelState.IsValid)
            {
                var service = CreateMovieService();
                if (service.DeleteMovie(id))
                {
                    TempData["SaveResult"] = "This movie was deleted.";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "You can't delete this movie, you didn't create it.");
                    return View();
                }
            }
            else return View();
        }

        private MovieService CreateMovieService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MovieService(userId);
            return service;
        }
    }
}