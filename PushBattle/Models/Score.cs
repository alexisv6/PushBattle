using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PushBattle.Models
{
    public class Score
    {
        public string team;
        public int score;


        public Score(string teamname, int v)
        {
            this.team = teamname;
            this.score = v;
        }
    }
}