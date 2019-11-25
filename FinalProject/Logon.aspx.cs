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

        //Form login
        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            //get username/password from form
            string userName = LoginWidget.UserName.ToString();
            string password = LoginWidget.Password.ToString();

            // using username look for password
            var userQuery = from user in medDB.UsersTables
                            where user.UserLoginName == userName
                            select user.UserLoginPass; //should store password hash instead (add feature if time allows)

            // check user status
            var userCheck = from user in medDB.UsersTables
                            where user.UserLoginName == userName
                            select user.UserLoginType;
            var temp = userCheck.FirstOrDefault().Trim().ToString();

            // Check to see if any passwords are returned, if not, show failed
            if (userQuery.Count() != 0)
            { 
                //trim to get rid of accidental white space in DB
                string currentPass = userQuery.FirstOrDefault().ToString().Trim();

                if (currentPass.Equals(password))
                {
                    FormsAuthentication.RedirectFromLoginPage(LoginWidget.UserName, true);
                    if (temp.Equals("patient"))
                        Response.Redirect("~/PatientPages/home.aspx");
                    else if (temp.Equals("doctor"))
                        Response.Redirect("~/DoctorPages/home.aspx");
                    else
                        Response.Redirect("~/PatientPages/home.aspx");
                }
            }

        }
    }
}