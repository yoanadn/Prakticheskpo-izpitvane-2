using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Layer;
using Microsoft.EntityFrameworkCore;

namespace Data_Layer
{
    public class GenreContext : IDb<Genre, int>
    {
        private readonly AppDbContext dbContext;

        public GenreContext(AppDbContext context)
        {
            dbContext = context;
        }

        public void Create(Genre item)
        {
            dbContext.Genres.Add(item);
            dbContext.SaveChanges();
        }

        public Genre Read(int key, bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            IQueryable<Genre> query = dbContext.Genres;
            if (isReadOnly) query = query.AsNoTrackingWithIdentityResolution();
            Genre gameType = query.FirstOrDefault(x => x.Id == key);
            if (gameType is null) throw new KeyNotFoundException();
            return gameType;
        }

        public List<Genre> ReadAll(bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            IQueryable<Genre> query = dbContext.Genres;
            if (useNavigationalProperties) query = query.Include(q => q.Users);
            if (isReadOnly) query = query.AsNoTrackingWithIdentityResolution();
            return query.ToList();
        }

        public void Update(Genre item, bool useNavigationalProperties = false)
        {
            Genre gameType = Read(item.Id, useNavigationalProperties);
            gameType.Name = item.Name;
            if (useNavigationalProperties)
            {
                List<User> users = new List<User>();
                foreach (var user in item.Users)
                {
                    User newUser = dbContext.Users.FirstOrDefault(x => x.Id == user.Id);
                    if (newUser == null) users.Add(user);
                    else users.Add(newUser);
                }
                gameType.Users = item.Users;
            }
            dbContext.SaveChanges();
        }

        public void Delete(int key)
        {
            Genre genre = Read(key);

            if (genre == null)
                throw new ArgumentException("Genre not found");

            dbContext.Genres.Remove(genre);
            dbContext.SaveChanges();
        }
    }
}
