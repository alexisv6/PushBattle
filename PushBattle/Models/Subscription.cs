using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using System.ComponentModel.DataAnnotations;

namespace PushBattle.Models
{
    [DynamoDBTable("PushBattleTeamSubscriptions")]
    public class Subscription
    {
        [Required]
        [DynamoDBHashKey]
        public string teamname;

        [Required]
        public string topicARN;
    }
}