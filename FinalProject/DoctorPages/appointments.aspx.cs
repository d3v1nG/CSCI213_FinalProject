using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject.DoctorPages
{
    public partial class appointments : System.Web.UI.Page
    {
        MedicalDBEntities medDB = new MedicalDBEntities();
        static int docID = 0;
        DoctorTable currUser;
        DoctorTable currDoc;
        MedicalDBEntities appointmentDB = new MedicalDBEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            medDB.DoctorTables.Load();
            var user = User.Identity.Name;

            var currUserQuery = from doc in medDB.DoctorTables
                                where doc.FirstName.Trim().Equals(user)
                                select doc;
            if (currUserQuery.Count() != 0)
            {
                currUser = currUserQuery.FirstOrDefault();
            }
        }

        protected int GenerateApptID()
        {
            var count = (from appt in medDB.AppointmentTables
                         select appt).Count();
            return count + 1;
        }

        protected void AppointmentDaySelectCalendar_SelectionChanged(object sender, EventArgs e)
        {

        }

        protected void TimeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DoctorSelectDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected DoctorTable GetCurrentDoctor()
        {
            var temp = User.Identity.Name;
            var q = from d in medDB.DoctorTables
                    where d.FirstName.Trim().Equals(temp)
                    select d;
            return q.FirstOrDefault();
        }

        protected void CheckButton_Click(object sender, EventArgs e)
        {
            //currUser = 
            var PatientLastName = PatientsSelectDropDownList.SelectedValue.Trim();

            //Only shows the times that the doctor is available
            //Changes times when the doctor is changed
            var doctorQuery = from doc in medDB.DoctorTables
                              where doc.LastName.Trim().Equals(currUser.LastName)
                              select doc.DoctorID;

            //Dropdown will always show the doctors that are available
            int doctorID = doctorQuery.FirstOrDefault();
            docID = doctorID;
            currDoc = GetDoctorFromID(doctorID);

            var takenTime = (from a in medDB.AppointmentTables
                             where a.DoctorID == doctorID && a.Data == AppointmentDaySelectCalendar.SelectedDate.Date /*&& a.Time.Equals(TimeSpan.Parse(time))*/
                             select (TimeSpan)a.Time);

            List<string> workday = new List<string>
            { new TimeSpan(8, 15, 0).ToString(), new TimeSpan(9, 15, 0).ToString(), new TimeSpan(10, 15, 0).ToString(), new TimeSpan(11, 15, 0).ToString(), new TimeSpan(1, 15, 0).ToString(), new TimeSpan(2, 15, 0).ToString(), new TimeSpan(3, 15, 0).ToString(), new TimeSpan(4, 15, 0).ToString()};

            //Finding the open times
            foreach (TimeSpan t in takenTime)
            {
                if (workday.Contains(t.ToString()))
                {
                    workday.Remove(t.ToString());
                }
            }

            TimeDropDownList.Items.Clear();

            //Show times in dropdown 
            foreach (string time in workday)
            {
                TimeDropDownList.Items.Add(time.ToString());
            }
        }

        protected int GenerateMsgID()
        {
            var count = (from m in medDB.MessageTables
                         select m).Count();
            return count + 1;
        }

        protected DoctorTable GetDoctorFromID(int id)
        {
            var doctorQuery = from doc in medDB.DoctorTables
                              where doc.DoctorID.Equals(id)
                              select doc;

            return doctorQuery.FirstOrDefault();
        }

        protected PatientTable GetPatientFromName(string name)
        {
            var patientQuery = from P in medDB.PatientTables
                              where P.LastName.Trim().Equals(name)
                              select P;
            return patientQuery.FirstOrDefault();
        }

        protected void ScheduleButton_Click(object sender, EventArgs e)
        {
            currDoc = GetCurrentDoctor();
            PatientTable currPat = GetPatientFromName(PatientsSelectDropDownList.SelectedValue.Trim());

            //Update DB with new appointmnet
            AppointmentTable appt = new AppointmentTable();
            appt.AppointmentID = GenerateApptID();
            appt.DoctorID = docID;
            appt.PatientID = currPat.PatientID;
            appt.Data = AppointmentDaySelectCalendar.SelectedDate;
            appt.Time = TimeSpan.Parse(TimeDropDownList.SelectedValue);
            medDB.AppointmentTables.Add(appt);
            UpdateDB();


            ////Send message to inbox
            //MessageTable msgToPatient = new MessageTable();
            //msgToPatient.MessageID = GenerateMsgID();
            //msgToPatient.MessageTo = currUser.UserLoginName;
            //msgToPatient.MessageFrom = "System";
            //msgToPatient.Date = DateTime.Now;
            ////msgToPatient.Message = $"Appointment scheduled on {appt.Data} @ {appt.Time} with Doctor {currDoc.LastName}";
            //msgToPatient.Message = "Appointment scheduled";
            //medDB.MessageTables.Add(msgToPatient);
            //UpdateDB();


            //MessageTable msgToDoctor = new MessageTable();
            //msgToDoctor.MessageID = GenerateMsgID();
            //msgToDoctor.MessageTo = currDoc.UserLoginName;
            //msgToDoctor.MessageFrom = "System";
            //msgToDoctor.Date = DateTime.Now.Date;
            ////msgToDoctor.Message = $"Appointment scheduled on {appt.Data} @ {appt.Time} with Patient {currUser.FirstName + currUser.LastName}";
            //msgToDoctor.Message = "Appointment scheduled";
            //medDB.MessageTables.Add(msgToDoctor);
            //UpdateDB();

            Server.TransferRequest(Request.Url.AbsolutePath, false);
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

        protected void editButton_Click(object sender, EventArgs e)
        {
            appointmentDB.AppointmentTables.Load();

            var visitSummary = (from item in appointmentDB.AppointmentTables.Local
                               where item.PatientID == Convert.ToInt32(PatientsSelectDropDownList.SelectedValue)
                               select item.VisitSummary).First();

            visitSummary = TextBox1.Text;
            
        }

        protected void AppointmentDaySelectCalendar_SelectionChanged1(object sender, EventArgs e)
        {
            CheckButton_Click(sender, e);
        }
    }
}