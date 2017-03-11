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

                string url = "http://localhost:63131/api/";
                RestClient client = new RestClient(url);


                
                RestRequest userRequest = new RestRequest("users/" + userName, Method.GET);
                IRestResponse<User> response = client.Execute<User>(userRequest);
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return;
                }
                callJavaScript("parseUsers", response.Content);
            }
            else
            {
                // User not authenticated
            }
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
    }
}