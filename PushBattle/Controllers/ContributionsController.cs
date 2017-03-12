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
using RestSharp;

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

        [HttpGet]
        [Route("api/contributions/{id}")]
        public IHttpActionResult GetContribution(string id)
        {
            DynamoDBContext context = new DynamoDBContext(dynamoClient);
            User dbUser = context.Load<User>(id);
            if (dbUser == null)
            {
                return NotFound();
            }
            Team dbTeam = context.Load<Team>(dbUser.teamId);
            if (dbTeam == null)
            {
                return NotFound();
            }
            Battle dbBattle = context.Load<Battle>(dbTeam.currentBattle);
            if (dbBattle == null)
            {
                return NotFound();
            }
            int index = 0;
            for (; index < dbBattle.participants.Count; index++)
            {
                if (dbBattle.participants[index].Equals(dbUser.teamId))
                {
                    break;
                }
            }
            dbBattle.scores[index] += dbBattle.offsets[index];
//            dbBattle.participants[dbUser.teamId] += 1;
            context.Save<Battle>(dbBattle);

            Guid contribGuid = new Guid();
            Contribution newContrib = new Contribution()
            {
                contributionId = contribGuid.ToString(),
                username = dbUser.username,
                teamId = dbUser.teamId,
                battleId = dbBattle.battleId
            };
            //RestRequest requ = new RestRequest("contributions/user/" + dbUser.username + "/battle/" + dbBattle.battleId, Method.GET);
            RestRequest contribPost = new RestRequest("contributions", Method.POST);
            contribPost.AddJsonBody(newContrib);
            Contribution userContrib = RestDispatcher.ExecuteRequest<Contribution>(contribPost);

            return Ok(userContrib);
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
