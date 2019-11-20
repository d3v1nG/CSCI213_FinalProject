using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.Entity;

namespace FinalProject
{
    public partial class Logon : System.Web.UI.Page
    {
        MedicalDBEntities medDB = new MedicalDBEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            medDB.UsersTables.Load();
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {

            string userName = LoginWidget.UserName.ToString();
            string password = LoginWidget.Password.ToString();

            var userQuery = from user in medDB.UsersTables
                            where user.UserLoginName == userName
                            select user.UserLoginPass;

            string temp = userQuery.FirstOrDefault().ToString();

            bool fuckingVariableThatShouldntFuckinBeHere = temp.Equals(password); //literally wont fuccking evaluate to true

            if (fuckingVariableThatShouldntFuckinBeHere) 
            {
                FormsAuthentication.RedirectFromLoginPage(LoginWidget.UserName, true);
            }

        }
    }
}