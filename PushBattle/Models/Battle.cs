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
        /// <summary>
        /// The unique id that the battle can be referenced by.
        /// </summary>
        [Required]
        [DynamoDBHashKey]
        public string battleId { get; set; }

        public List<int> scores { get; set; }

        /// <summary>
        /// A string containing the username that initiated the battle.
        /// </summary>
        public string initiator { get; set; }
        
        /// <summary>
        /// The time that the battle was declared.
        /// </summary>
        public DateTime declaration { get; set; }
        
        /// <summary>
        /// The time that the battle actually began.
        /// Typically within 2 hours of declaration.
        /// </summary>
        public DateTime initiation { get; set; }
        
        /// <summary>
        ///  The duration, in milliseconds (or whatever it is that dotNet uses), of the battle.
        /// </summary>
        public long duration { get; set; }
        
        /// <summary>
        /// An string array of the teams participating in the battle.
        /// </summary>
        public List<string> participants { get; set; }


    }
}