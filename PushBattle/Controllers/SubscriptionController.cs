using Amazon.DynamoDBv2.DataModel;
using PushBattle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PushBattle.Controllers
{
    public class SubscriptionController : ApiController
    {

        [HttpGet]
        [Route("api/subscriptions/{team}")]
        public IHttpActionResult GetSubscription(string team)
        {
            DynamoDBContext context = new DynamoDBContext();
            Subscription sub = context.Load<Subscription>(team);
            if (sub == null)
            {
                return NotFound();
            }
            return Ok(sub);
        }
    }
}
