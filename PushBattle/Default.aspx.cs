using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PushBattle.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PushBattle
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TextOutput.InnerText = Page.User.Identity.Name;
            if (Page.User.Identity.IsAuthenticated)
            {
                // User is authenticated
                // Load team data
                // Load user data
                // Load battle data

                ApplicationUser user = getActiveUser();
                if (user == null)
                {
                    return;
                }
                string userName = user.UserName;


                IRestResponse userResponse = RestDispatcher.ExecuteRequest("users/" + userName, Method.GET);
                if (userResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return;
                }
                User dbUser = RestDispatcher.Deserialize<User>(userResponse);
                IRestResponse teamResponse = RestDispatcher.ExecuteRequest("teams/" + dbUser.teamId, Method.GET);
                if (teamResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return;
                }
                Team dbTeam = RestDispatcher.Deserialize<Team>(teamResponse);

                callJavaScript("parseUserData", userResponse.Content);
                callJavaScript("parseTeamData", teamResponse.Content);
            }
            else
            {
                // User not authenticated
            }
        }
        
        private void loadUser(ApplicationUser current)
        {

        }

        protected void GetUserButton_Click(object sender, EventArgs e)
        {
            
            //            string url = Page.ResolveUrl("/api/users/");
            string url = "http://localhost:63131/api/users";
            RestClient client = new RestClient(url);
            //string userName = "test1";
            
            ApplicationUser user = getActiveUser();
            if (user == null)
            {
                return;
            }
            string userName = user.UserName;
            RestRequest request = new RestRequest(userName, Method.GET);
            IRestResponse<User> response = client.Execute<User>(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return;
            }
            TextOutput.InnerText = response.Data.username;

            callJavaScript("simpleAlert", response.Content);
        }

        

        private ApplicationUser getActiveUser()
        {
            if (Page.User == null)
            {
                TextOutput.InnerText = "Page.User was null";
                return null;
            }
            if (!Page.User.Identity.IsAuthenticated)
            {
                TextOutput.InnerText = "User not authenticated";
                return null;
            }
            ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

            return manager.FindById<ApplicationUser, string>(Page.User.Identity.GetUserId());
        }

        // function should not include parenthesis
        //
        private void callJavaScript(string function, string data)
        {
            if (!ClientScript.IsStartupScriptRegistered(function + "Alert"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(),
                    function + "Alert", function + "(" + data + ");", true);
            }
        }

        protected void DoBattle_Click(object sender, EventArgs e)
        {

            PushCoordinator.DeclareBattle(getActiveUser(), "red");
        }
    }
}