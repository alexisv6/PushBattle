using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PushBattle.Models;

namespace PushBattle.Controllers
{
    public class UsersController : ApiController
    {
        User[] users = new User[]
        {
            new User { Username = "alexisv6", Password = "secretpass", Email = "alexisv6@uw.edu", PhoneNumber = "5098815865", TeamId = 2 },
            new User { Username = "Yo-yo", Password = "Toys", Email = "hi@gmail.com", PhoneNumber = "20655555555", TeamId = 2 },
            new User { Username = "Hammer", Password = "Hardware", Email = "whatsup@gmail.com", PhoneNumber = "5555555555", TeamId = 1 }
        };

        public IEnumerable<User> GetAllUsers()
        {
            return users;
        }

        public IHttpActionResult GetUser(string username)
        {
            var user = users.FirstOrDefault((p) => p.Username == username);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        public HttpResponseMessage Post(User newUser)
        {
            if (ModelState.IsValid)
            {
                List<User> list = users.ToList();
                list.Add(newUser);
                users = list.ToArray();
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
    }
}
