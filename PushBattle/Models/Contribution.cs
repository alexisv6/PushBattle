using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PushBattle.Models
{
    [DynamoDBTable("PushBattleContributions")]
    public class Contribution
    {
            [DynamoDBHashKey]
            public string contributionId { get; set; }
            [Required]
            public string username { get; set; }
            [Required]
            public string teamId { get; set; }
            [Required]
            public string battleId { get; set; }
    }
}