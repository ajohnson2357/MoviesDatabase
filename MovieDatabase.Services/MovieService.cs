using MovieDatabase.Data;
using MovieDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDatabase.Services
{
    public class MovieService
    {
        private readonly Guid _userId;

        public MovieService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateMovie(MovieCreate movie)
        {
            var entity =
                new MovieData()
                {
                    OwnerId = _userId,
                    Title = movie.Title,
                    Description = movie.Description,
                    Genre = movie.Genre,
                    HaveWatched = movie.HaveWatched,
                    IsStarred = movie.IsStarred,
                    MovieQuality = movie.MovieQuality,
                    RunTime = movie.RunTime,
                    MovieRating = movie.MovieRating,
                    MovieId = movie.MovieId
                };
            using(var ctx = new ApplicationDbContext())
            {
                ctx.MovieDatabases.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<MovieListItem> GetAllMovies()
        {
            using(var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .MovieDatabases
                    .Where(q => q.OwnerId == _userId)
                    .Select(m => new MovieListItem { MovieId = m.MovieId, Description = m.Description, Genre = m.Genre, HaveWatched = m.HaveWatched, IsStarred = m.IsStarred, MovieQuality = m.MovieQuality, MovieRating = m.MovieRating, RunTime = m.RunTime, Title = m.Title });
                return query.ToList();
            }
        }

        public MovieEdit GetMovieById(int id)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var movie =
                    ctx
                    .MovieDatabases
                    .Single(m => m.MovieId == id && m.OwnerId == _userId);
                return
                    new MovieEdit
                    {
                        Description = movie.Description,
                        Genre = movie.Genre,
                        HaveWatched = movie.HaveWatched,
                        IsStarred = movie.IsStarred,
                        MovieQuality = movie.MovieQuality,
                        MovieRating = movie.MovieRating,
                        RunTime = movie.RunTime,
                        Title = movie.Title,
                        MovieId = movie.MovieId
                    };
            }
        }

        public bool UpdateMovie(MovieEdit movie)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .MovieDatabases
                    .Single(e => e.MovieId == movie.MovieId && e.OwnerId == _userId);

                entity.Description = movie.Description;
                entity.Genre = movie.Genre;
                entity.HaveWatched = movie.HaveWatched;
                entity.IsStarred = movie.IsStarred;
                entity.MovieQuality = movie.MovieQuality;
                entity.MovieRating = movie.MovieRating;
                entity.RunTime = movie.RunTime;
                entity.Title = movie.Title;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteMovie(int id)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .MovieDatabases
                    .Single(e => e.MovieId == id && e.OwnerId == _userId);

                ctx.MovieDatabases.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
