using PushBattle.Models;
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
            IIdentity iden = Page.User.Identity;
            ApplicationUser user = (ApplicationUser)iden;
            
        }
    }
}