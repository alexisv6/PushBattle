using Amazon.DynamoDBv2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PushBattle.Models;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace PushBattle.Controllers
{
    public class ContributionsController : ApiController
    {
        AmazonDynamoDBClient dynamoClient = new AmazonDynamoDBClient();

        [HttpGet]
        [Route("api/contributions/team/{id}")]
        public IEnumerable<Contribution> GetAllContributionsByTeam(string id) 
        {
            DynamoDBContext context = new DynamoDBContext(dynamoClient);
            List<Contribution> allContributionsOnTeam = context.Scan<Contribution>(
                new ScanCondition("teamId", ScanOperator.Equal, id)).ToList();
            return allContributionsOnTeam;
        }

        [HttpGet]
        [Route("api/contributions/user/{id}")]
        public IEnumerable<Contribution> GetContributionsByUser(string id)
        {
            DynamoDBContext context = new DynamoDBContext(dynamoClient);
            List<Contribution> allContributionsByUser = context.Scan<Contribution>(
                new ScanCondition("username", ScanOperator.Equal, id)).ToList();
            return allContributionsByUser;
        }

        [HttpGet]
        [Route("api/contributions/user/{username}/battle/{battleId}")]
        public IEnumerable<Contribution> GetContributionsToBattleByUser(string username, string battleId)
        {
            DynamoDBContext context = new DynamoDBContext(dynamoClient);
            List<Contribution> allContributorsToBattle = context.Scan<Contribution>(
                new ScanCondition("username", ScanOperator.Equal, username),
                new ScanCondition("battleId", ScanOperator.Equal, battleId)).ToList();
            return allContributorsToBattle;
        }

        [HttpGet]
        [Route("api/contributions/team/{teamId}/battle/{battleId}")]
        public IEnumerable<Contribution> GetContributionsToBattleByTeam(string teamId, string battleId)
        {
            DynamoDBContext context = new DynamoDBContext(dynamoClient);
            List<Contribution> allContributorsToBattle = context.Scan<Contribution>(
                new ScanCondition("teamId", ScanOperator.Equal, teamId),
                new ScanCondition("battleId", ScanOperator.Equal, battleId)).ToList();
            return allContributorsToBattle;
        }

        [HttpPost]
        public HttpResponseMessage Post(Contribution contribution)
        {
            Guid guid = Guid.NewGuid();
            if (ModelState.IsValid)
            {
                contribution.contributionId = guid.ToString();
                DynamoDBContext context = new DynamoDBContext(dynamoClient);
                context.Save<Contribution>(contribution);
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
    }
}
