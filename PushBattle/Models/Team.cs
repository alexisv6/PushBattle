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
        public string url { get; set; }
        [Required]
        public List<string> members { get; set; }
    }
}