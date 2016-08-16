using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OVDA1.Models;
using OVDA1.Models.Interfaces;
using OVDA1.Models.Repository;
using MongoDB.Driver;

namespace OVDA1.Controllers
{
    public class UsersController : ApiController
    {

        static readonly IUserRepository repository = new UserRepository();

        public IEnumerable<User> GetAllUsers()
        {
            var client = new MongoClient();
            return repository.GetAll();            
        }

        public User GetUser(int id)
        {
            User user = repository.Get(id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return user;
        }

        public HttpResponseMessage PostUser(User user)
        {
            user = repository.Add(user);
            var response = Request.CreateResponse<User>(HttpStatusCode.Created, user);
            string uri = Url.Link("DefaultApi", new { id = user.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void PutUser(int id, User user)
        {
            user.Id = id;
            if (!repository.Update(user))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void DeleteUser(int id)
        {
            User user = repository.Get(id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            repository.Remove(id);
        }
    }
}
