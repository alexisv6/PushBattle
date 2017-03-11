using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PushBattle.Models;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace PushBattle.Controllers
{
    public class UsersController : ApiController
    {
        AmazonDynamoDBClient dynamoClient = new AmazonDynamoDBClient();

        [HttpGet]
        [Route("api/users/team/{id}")]
        public IEnumerable<User> GetAllUsersOnTeam(string id)
        {
            DynamoDBContext context = new DynamoDBContext(dynamoClient);
            List<User> allOnTeam = context.Scan<User>(
                new ScanCondition("teamId", ScanOperator.Equal, id)).ToList();
            return allOnTeam;
        }

        [HttpGet]
        public IHttpActionResult GetUser(string id)
        {
            DynamoDBContext context = new DynamoDBContext(dynamoClient);
            User user = context.Load<User>(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public HttpResponseMessage Post(User user)
        {
            if (ModelState.IsValid)
            {
                DynamoDBContext context = new DynamoDBContext(dynamoClient);
                context.Save<User>(user);
                Team dbTeam = context.Load<Team>(user.teamId);
                if (dbTeam != null)
                {
                    dbTeam.members.Add(user.username);
                    context.Save<Team>(dbTeam);
                }
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
    }
}
