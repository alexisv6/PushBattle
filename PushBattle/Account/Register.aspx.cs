using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using PushBattle.Models;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using RestSharp;
using Amazon.SimpleNotificationService.Model;
using Amazon.SimpleNotificationService;

namespace PushBattle.Account
{
    public partial class Register : Page
    {
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();

            var user = new ApplicationUser() {
                UserName = Username.Text,
                //Email = Email.Text
                PhoneNumber = PhoneNumber.Text
            };

            

            IdentityResult result = manager.Create(user, Password.Text);
            if (result.Succeeded)
            {
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                //string code = manager.GenerateEmailConfirmationToken(user.Id);
                //string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                //manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");

                
                // create user 
                User dbUser = new User()
                {
                    username = user.UserName,
                    teamId = Team.SelectedValue
                };
                RestRequest request = new RestRequest("users/" + dbUser.username, Method.POST);
                request.AddJsonBody(dbUser);

                IRestResponse response = RestDispatcher.ExecuteRequest(request);

                //AmazonSimpleNotificationServiceClient snsClient = new AmazonSimpleNotificationServiceClient();

                //Models.Subscription sub = RestDispatcher.ExecuteRequest<Models.Subscription>("subscriptions/" + dbUser.teamId, Method.GET);
                //if (sub == null)
                //{

                //}
                //SubscribeRequest subRequ = new SubscribeRequest(sub.topicARN, "sms", user.PhoneNumber);
                //snsClient.Subscribe(subRequ);
                

                signInManager.SignIn( user, isPersistent: false, rememberBrowser: false);
                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            }
            else 
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}