using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject.DoctorPages
{
    public partial class inbox : System.Web.UI.Page
    {
        MedicalDBEntities medDB = new MedicalDBEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            medDB.MessageTables.Load();
            UpdateInbox();
            UpdateOutbox();
        }

        protected void UpdateInbox()
        {
            PatientTable currPatient = GetCurrentPatient();

            var msgList = from m in medDB.MessageTables
                          where m.MessageTo.Trim() == (currPatient.Email.Trim())
                          select m;
            foreach (MessageTable m in msgList.ToList())
            {
                if (m.MessageTo != null)
                {
                    string from = "";
                    if (m.MessageFrom.Trim().Equals("System"))
                    {
                        from = "System";
                    }
                    else
                    {
                        DoctorTable tempDoc = GetDoctorFromEmail(m.MessageFrom);
                        from = tempDoc.LastName.Trim();
                    }

                    InboxListBox.Items.Add($"(No. {m.MessageID} ) From {from}, sent {m.Date}, Message: {m.Message} ");
                }
            }
        }

        protected void UpdateOutbox()
        {
            PatientTable currPatient = GetCurrentPatient();

            var msgList = from m in medDB.MessageTables
                          where m.MessageFrom.Trim() == (currPatient.Email.Trim())
                          select m;
            foreach (MessageTable m in msgList.ToList())
            {
                DoctorTable tempDoc = GetDoctorFromEmail(m.MessageTo);
                if (m.MessageTo != null)
                {
                    OutboxListBox.Items.Add($"(No. {m.MessageID} ) Sent To {tempDoc.LastName}, sent {m.Date}, Message: {m.Message} ");
                }
            }
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

        protected void SendButton_Click(object sender, EventArgs e)
        {
            PatientTable currPatient = GetCurrentPatient();

            if (TextBox1.Text.Length < 50)
            {
                DoctorTable currentDoc = GetDoctorFromLastName(DropDownList1.SelectedValue.ToString());
                MessageTable msg = new MessageTable();
                msg.Date = DateTime.Now;
                msg.MessageTo = currentDoc.Email;
                msg.MessageFrom = currPatient.Email;
                msg.Message = TextBox1.Text;
                //msg.MessageID 
                medDB.MessageTables.Add(msg);

            }
            UpdateDB();
            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        protected DoctorTable GetDoctorFromLastName(string name)
        {
            var docs = from d in medDB.DoctorTables
                       where d.LastName.Trim().Equals(name)
                       select d;
            return docs.FirstOrDefault();
        }

        protected DoctorTable GetDoctorFromEmail(string email)
        {
            var docs = from d in medDB.DoctorTables
                       where d.Email.Trim().Equals(email)
                       select d;
            return docs.FirstOrDefault();
        }

        protected void UpdateDB()
        {
            //medDB.SaveChanges();
            //medDB.AppointmentTables.Load
            try
            {
                medDB.SaveChanges();
            }
            // helpful for debugging entity errors
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        protected void DeleteRecievedButton_Click(object sender, EventArgs e)
        {
            // get msg
            string info = InboxListBox.SelectedValue;
            int index = InboxListBox.SelectedIndex;
            if (index != -1)
            {
                // get msg ID for query
                var msgID = info.Split(' ')[1];
                int id = Convert.ToInt32(msgID);

                var findMsg = from m in medDB.MessageTables
                              where m.MessageID == id
                              select m;
                medDB.MessageTables.Remove(findMsg.FirstOrDefault());
                InboxListBox.Items.RemoveAt(index);
                UpdateDB();
                Server.TransferRequest(Request.Url.AbsolutePath, false);
            }

        }

        protected void DeleteSentButton_Click(object sender, EventArgs e)
        {
            // get msg
            string info = OutboxListBox.SelectedValue;
            int index = OutboxListBox.SelectedIndex;
            if (index != -1)
            {
                // get msg ID for query
                var msgID = info.Split(' ')[1];
                int id = Convert.ToInt32(msgID);

                var findMsg = from m in medDB.MessageTables
                              where m.MessageID == id
                              select m;
                medDB.MessageTables.Remove(findMsg.FirstOrDefault());
                OutboxListBox.Items.RemoveAt(index);
                UpdateDB();
                Server.TransferRequest(Request.Url.AbsolutePath, false);
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}