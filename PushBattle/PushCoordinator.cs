﻿using PushBattle.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PushBattle
{
    public class PushCoordinator
    {

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apUser"></param>
        /// <param name="targetTeam"></param>
        /// <returns></returns>
        public static bool DeclareBattle(ApplicationUser apUser, string targetTeam)
        {

            // Check if a battle is happening
            // If no battle, send message saying no battle happening
            //  and say that a battle will be declared

            User dbUser = RestDispatcher.ExecuteRequest<User>("users/" + apUser.UserName, Method.GET);
            if (dbUser == null)
            {
                Console.WriteLine("Could not find user with name: {0}", apUser.UserName);
                return false;
            }

            string teamName = dbUser.teamId;
            Team myTeam = RestDispatcher.ExecuteRequest<Team>("teams/" + teamName, Method.GET);
            if (myTeam == null)
            {
                Console.WriteLine("Could not find team with name: {0}", teamName);
                return false;
            }

            if (targetTeam.Equals(myTeam.teamname, StringComparison.OrdinalIgnoreCase))
            {
                // You're attacking your own team, that's not allowed
                // Send message saying as such

                // This one is for logging.
                Console.WriteLine("{0} has tried to attack own team.", apUser.UserName);
                return false;
            }

            if (myTeam.currentBattle != -1)
            {
                // We are in battle, so don't start another
                Console.WriteLine("{0} has tried to start a battle, but {1} is already engaged in one.", apUser.UserName, myTeam.teamname);
                return false;
            }

            Team theirTeam = RestDispatcher.ExecuteRequest<Team>("teams/" + targetTeam, Method.GET);
            if (theirTeam == null)
            {
                Console.WriteLine("Could not find target team with name: {0}", targetTeam);
                return false;
            }

            // We have the enemy team, are they in battle?
            if (theirTeam.currentBattle != -1)
            {
                Console.WriteLine("{0} has tried to start a battle against {1}, but {1} is already in battle.", apUser.UserName, targetTeam);
                return false;
            }



            return false;
        }


    }
}