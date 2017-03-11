using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PushBattle.Models;

namespace PushBattle.Controllers
{
    public class BattlesController : ApiController
    {
        AmazonDynamoDBClient dynamoClient = new AmazonDynamoDBClient();

        [HttpGet]
        [Route("api/battles/{id}")]
        public IHttpActionResult GetBattle(string id)
        {
            DynamoDBContext context = new DynamoDBContext(dynamoClient);
            Battle battle = context.Load<Battle>(id);
            if (battle == null)
            {
                return NotFound();
            }
            return Ok(battle);
        }

        [HttpPost]
        public HttpResponseMessage Post(Battle battle)
        {
            Guid guid = Guid.NewGuid();
            if (ModelState.IsValid)
            {
                battle.battleId = guid.ToString();
                DynamoDBContext context = new DynamoDBContext(dynamoClient);
                context.Save<Battle>(battle);
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
    }
}
