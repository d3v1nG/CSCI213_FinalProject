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
            DoctorTable currDoc = GetCurrentUser();

            var msgList = from m in medDB.MessageTables.Local
                          where m.MessageTo.Trim() == (currDoc.Email.Trim())
                          select m;
            if (msgList.Count() != InboxListBox.Items.Count)
            {
                foreach (MessageTable m in msgList)
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
                            PatientTable tempDoc = GetPatientFromEmail(m.MessageFrom);
                            from = tempDoc.LastName.Trim();
                        }

                        InboxListBox.Items.Add($"(No. {m.MessageID} ) From {from}, sent {m.Date}, Message: {m.Message} ");
                    }
                }
            }
        }

        protected void UpdateOutbox()
        {
            DoctorTable currDoc = GetCurrentUser();

            var msgList = from m in medDB.MessageTables.Local
                          where m.MessageFrom.Trim() == (currDoc.Email.Trim())
                          select m;
            foreach (MessageTable m in msgList.ToList())
            {
                PatientTable patient = GetPatientFromEmail(m.MessageTo);
                if (m.MessageTo != null)
                {
                    OutboxListBox.Items.Add($"(No. {m.MessageID} ) Sent To {patient.LastName}, sent {m.Date}, Message: {m.Message} ");
                }
            }
        }

        protected DoctorTable GetCurrentUser()
        {
            var user = User.Identity.Name;

            var currUserQuery = from doc in medDB.DoctorTables
                                where doc.FirstName.Trim().Equals(user)
                                select doc;
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
            DoctorTable currDoc = GetCurrentUser();
            string selected = DropDownList1.SelectedValue;
            PatientTable currPatient = GetPatientFromLastName(selected);

            if (TextBox1.Text.Length < 50)
            {
                //PatientTable currentDoc = /*GetDoctorFromLastName(DropDownList1.SelectedValue.ToString())*/;
                MessageTable msg = new MessageTable();
                msg.Date = DateTime.Now;
                msg.MessageTo = currPatient.Email;
                msg.MessageFrom = currDoc.Email;
                msg.Message = TextBox1.Text;
                //msg.MessageID 
                medDB.MessageTables.Add(msg);
                medDB.SaveChanges();
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

        protected PatientTable GetPatientFromLastName(string name)
        {
            var pats = from pat in medDB.PatientTables
                       where pat.LastName.Trim().Equals(name.Trim())
                       select pat;
            return pats.FirstOrDefault();
        }

        protected DoctorTable GetDoctorFromEmail(string email)
        {
            var docs = from d in medDB.DoctorTables
                       where d.Email.Trim().Equals(email)
                       select d;
            return docs.FirstOrDefault();
        }

        protected PatientTable GetPatientFromEmail(string email)
        {
            var pats = from p in medDB.PatientTables
                       where p.Email.Trim().Equals(email)
                       select p;
            return pats.FirstOrDefault();
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

        protected void DeleteInBoxButton_Click(object sender, EventArgs e)
        {
            DeleteRecievedButton_Click(sender, e);
        }

        protected void DeleteSentButton_Click1(object sender, EventArgs e)
        {
            DeleteRecievedButton_Click(sender, e);

        }

        protected void SendButton_Click1(object sender, EventArgs e)
        {
            SendButton_Click(sender, e);
        }
    }
}