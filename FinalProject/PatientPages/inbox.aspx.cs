using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace FinalProject.Pages
{
    public partial class messages : System.Web.UI.Page
    {
        MedicalDBEntities medDB = new MedicalDBEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            medDB.MessageTables.Load();
            UpdateInbox();
        }

        protected void UpdateInbox()
        {
            PatientTable currPatient = GetCurrentPatient();

            var msgList = from m in medDB.MessageTables
                          where m.MessageTo.Trim() == (currPatient.Email.Trim())
                          select m;
            foreach (MessageTable m in msgList.ToList())
            {
                InboxListBox.Items.Add($"From {m.MessageFrom}, sent {m.Date}, Message: {m.Message} ");
            }
        }

        protected void UpdateOutbox()
        {

        }

        protected PatientTable GetCurrentPatient()
        {
            var user = User.Identity.Name;

            var currUserQuery = from patient in medDB.PatientTables
                                where patient.FirstName.Trim().Equals(user)
                                select patient;
            if (currUserQuery.Count() != 0)
            {
                return currUserQuery.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}