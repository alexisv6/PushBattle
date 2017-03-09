using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PushBattle.Models;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace PushBattle.Controllers
{
    public class TeamsController : ApiController
    {
        
        private AmazonDynamoDBClient dynamoClient = new AmazonDynamoDBClient();

        [HttpGet]
        [Route("api/teams/{id}")]
        public IHttpActionResult GetTeam(string id)
        {
            DynamoDBContext context = new DynamoDBContext(dynamoClient);
            Team team = context.Load<Team>(id);
            if (team == null)
            {
                return NotFound();
            }
            return Ok(team);
        }

        [HttpPost]
        [Route("api/teams")]
        public HttpResponseMessage Post(Team team)
        {
            if (ModelState.IsValid)
            {
                DynamoDBContext context = new DynamoDBContext(dynamoClient);
                context.Save<Team>(team);
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
    }
}
