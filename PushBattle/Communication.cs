using PushBattle.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PushBattle
{
    public class Communication
    {

        public static async void sendMessageToTeam(ApplicationUserManager manager, string teamName, string passBack, Action<string, string> messageCall, string subject)
        {
            

            

            //ParallelQuery<User> pq = collection.AsParallel<User>();
            //pq.ForAll<User>(sendMessageToUser);
        }

        public static void sendMessageToUser(User user)
        {

        }
    }
}