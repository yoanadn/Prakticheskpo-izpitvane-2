using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Layer;
using Microsoft.EntityFrameworkCore;

namespace Data_Layer
{
    public class UsersContext : IDb<User, int>
    {
        private AppDbContext dbContext;

        public UsersContext(AppDbContext context)
        {
            dbContext = context;
        }

        public void Create(User item)
        {
            dbContext.Users.Add(item);
            dbContext.SaveChanges();
        }

        public User Read(int key, bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            IQueryable<User> query = dbContext.Users;
            if (useNavigationalProperties) query = query.Include(q => q.Friends).Include(q => q.Games);
            if (isReadOnly) query = query.AsNoTrackingWithIdentityResolution();
            User user = query.FirstOrDefault(x => x.Id == key);
            if (user is null) throw new KeyNotFoundException();
            return user;
        }

        public List<User> ReadAll(bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            IQueryable<User> query = dbContext.Users;

            if (useNavigationalProperties)
            {
                query = query.Include(u => u.Games).Include(u => u.Friends);
            }

            if (isReadOnly)
            {
                query = query.AsNoTrackingWithIdentityResolution();
            }

            return query.ToList();
        }

        public void Update(User item, bool useNavigationalProperties = false)
        {
            User user = Read(item.Id, useNavigationalProperties);
            dbContext.Entry(user).CurrentValues.SetValues(item);
            if (useNavigationalProperties)
            {
                List<User> users = new List<User>();
                foreach (var fr in item.Friends)
                {
                    User friend = dbContext.Users.FirstOrDefault(x => x.Id == fr.Id);
                    if (friend == null) users.Add(fr);
                    else users.Add(friend);
                }
                user.Friends = item.Friends;

                List<Game> games = new List<Game>();
                foreach (var gm in item.Games)
                {
                    Game game = dbContext.Games.FirstOrDefault(x => x.Id == gm.Id);
                    if (game == null) games.Add(gm);
                    else games.Add(game);
                }
                user.Games = item.Games;
            }
            dbContext.SaveChanges();
        }

        public void Delete(int key)
        {
            User user = Read(key);
            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
        }
    }
}
