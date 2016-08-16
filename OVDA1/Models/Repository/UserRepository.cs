using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OVDA1.Models.Interfaces;
using OVDA1.Models;

namespace OVDA1.Models.Repository
{
    public class UserRepository : IUserRepository
    {
        private List<User> users = new List<User>();
        private int _nextId = 1;

        public UserRepository()
        {
            Add(new User { Name = "Test1" });
            Add(new User { Name = "Test2" });
            Add(new User { Name = "Test3" });
        }
 
        public IEnumerable<User> GetAll()
        {
            return users;
        }

        public User Get(int id)
        {
            return users.Find(u => u.Id == id);
        }

        public User Add(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.Id = _nextId++;
            users.Add(user);
            return user;
        }

        public void Remove(int id)
        {
            users.RemoveAll(u => u.Id == id);
        }

        public bool Update(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            int index = users.FindIndex(u => u.Id == user.Id);
            if (index == -1)
            {
                return false;
            }
            users.RemoveAt(index);
            users.Add(user);
            return true;
        }
    }
}