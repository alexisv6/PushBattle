using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Amazon.DynamoDBv2.DataModel;

namespace PushBattle.Models
{
    [DynamoDBTable("PushBattleUsers")]
    public class User
    {
        [Required]
        [DynamoDBHashKey]
        public string username { get; set; }
        //[Required]
        //public string password { get; set; }
        public string teamId { get; set; }
        //[Required]
        //public string email { get; set; }
        //public string phoneNumber { get; set; }
    }
}