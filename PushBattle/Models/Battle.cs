using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PushBattle.Models
{
    [DynamoDBTable("PushBattleBattles")]
    public class Battle
    {
        [DynamoDBHashKey]
        public string battleId { get; set; }
        [Required]
        public string redTeam { get; set; }
        [Required]
        public string blueTeam { get; set; }
        public int redScore { get; set; }
        public int blueScore { get; set; }
    }
}