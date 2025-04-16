using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Layer;
using Microsoft.EntityFrameworkCore;

namespace Data_Layer
{
    public class GamesContext : IDb<Game, int>
    {
        public GamesContext() { }   
        private readonly AppDbContext dbContext;

        public GamesContext(AppDbContext context)
        {
            dbContext = context;
        }

        public void Create(Game item)
        {
            dbContext.Games.Add(item);
            dbContext.SaveChanges();
        }

        public Game Read(int key, bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            IQueryable<Game> query = dbContext.Games;
            if (useNavigationalProperties) query = query.Include(q => q.Genres);
            if (isReadOnly) query = query.AsNoTrackingWithIdentityResolution();
            Game game = query.FirstOrDefault(x => x.Id == key);
            if (game is null) throw new KeyNotFoundException();
            return game;
        }

        public List<Game> ReadAll(bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            IQueryable<Game> query = dbContext.Games;

            if (useNavigationalProperties)
            {
                query = query.Include(g => g.Genres);
            }

            if (isReadOnly)
            {
                query = query.AsNoTrackingWithIdentityResolution();
            }

            return query.ToList();
        }

        public void Update(Game item, bool useNavigationalProperties = false)
        {
            Game game = Read(item.Id, useNavigationalProperties);
            game.Name = item.Name;
            if (useNavigationalProperties)
            {
                List<Genre> genres = new List<Genre>();
                foreach (var type in item.Genres)
                {
                    Genre newGenre = dbContext.Genres.FirstOrDefault(x => x.Id == type.Id);
                    if (newGenre == null) genres.Add(type);
                    else genres.Add(newGenre);
                }
                game.Genres = item.Genres;
            }
            dbContext.SaveChanges();
        }

        public void Delete(int key)
        {
            Game game = Read(key);

            if (game == null)
                throw new ArgumentException("Game not found");

            dbContext.Games.Remove(game);
            dbContext.SaveChanges();
        }
    }
}
