using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.DynamoDBv2.DataModel;
using System.ComponentModel.DataAnnotations;

namespace PushBattle.Models
{
    [DynamoDBTable("PushBattleTeams")]
    public class Team
    {
        [Required]
        [DynamoDBHashKey]
        public string teamname { get; set; }
        public string imageUrl { get; set; }
        [Required]
        public List<string> members { get; set; }

        /// <summary>
        /// The id of the current battle or -1 if no battle is taking place.
        /// </summary>
        public string currentBattle { get; set; }
    }
}