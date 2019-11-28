using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace FinalProject.Pages
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        MedicalDBEntities medDB = new MedicalDBEntities();
        //AppointmentTable newAppointment = new AppointmentTable();
        static int docID = 0;
        PatientTable currUser;
        DoctorTable currDoc;

        protected void Page_Load(object sender, EventArgs e)
        {
            medDB.DoctorTables.Load();
            var user = User.Identity.Name;

            var currUserQuery = from patient in medDB.PatientTables
                           where patient.FirstName.Trim().Equals(user)
                           select patient;
            if (currUserQuery.Count() != 0)
            {
                currUser = currUserQuery.FirstOrDefault();
            }

            //get appointments

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

        protected void CheckButton_Click(object sender, EventArgs e)
        {
            var DoctorLastName = DoctorSelectDropDownList.SelectedValue.Trim();
            // when doctor is changed, show only availible times 
            var doctorQuery = from doc in medDB.DoctorTables
                              where doc.LastName.Trim().Equals(DoctorLastName)
                              select doc.DoctorID;

            // will always find doctor, dropdown only shows current docs
            int doctorID = doctorQuery.FirstOrDefault();
            docID = doctorID;
            currDoc = GetDoctorFromID(doctorID);

            var takenTime = (from a in medDB.AppointmentTables
                        where a.DoctorID == doctorID && a.Data == AppointmentDaySelectCalendar.SelectedDate.Date /*&& a.Time.Equals(TimeSpan.Parse(time))*/
                        select (TimeSpan)a.Time);

            List<string> workday = new List<string>
            { new TimeSpan(8, 15, 0).ToString(), new TimeSpan(9, 15, 0).ToString(), new TimeSpan(10, 15, 0).ToString(), new TimeSpan(11, 15, 0).ToString(), new TimeSpan(1, 15, 0).ToString(), new TimeSpan(2, 15, 0).ToString(), new TimeSpan(3, 15, 0).ToString(), new TimeSpan(4, 15, 0).ToString()};
            // find open times

            foreach (TimeSpan t in takenTime)
            {
                if (workday.Contains(t.ToString()))
                {
                    workday.Remove(t.ToString());
                }
            }
            

            //show times in dropdown 
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

        protected DoctorTable GetDoctorFromName(string name)
        {
            var doctorQuery = from doc in medDB.DoctorTables
                              where doc.LastName.Trim().Equals(name)
                              select doc;
            return doctorQuery.FirstOrDefault();
        }

        protected void ScheduleButton_Click(object sender, EventArgs e)
        {
            currDoc = GetDoctorFromName(DoctorSelectDropDownList.SelectedValue.Trim());

            //update DB with new appt
            AppointmentTable appt = new AppointmentTable();
            appt.AppointmentID = GenerateApptID();
            appt.DoctorID = docID;
            appt.PatientID = currUser.PatientID;
            appt.Data = AppointmentDaySelectCalendar.SelectedDate;
            appt.Time = TimeSpan.Parse(TimeDropDownList.SelectedValue);
            medDB.AppointmentTables.Add(appt);
            UpdateDB();


            //send message to inbox
            MessageTable msgToPatient = new MessageTable();
            msgToPatient.MessageID = GenerateMsgID();
            msgToPatient.MessageTo = currUser.Email;
            msgToPatient.MessageFrom = "System";
            msgToPatient.Date = DateTime.Now;
            //msgToPatient.Message = $"Appointment scheduled on {appt.Data} @ {appt.Time} with Doctor {currDoc.LastName}";
            msgToPatient.Message = "Appointment scheduled";
            medDB.MessageTables.Add(msgToPatient);
            UpdateDB();


            MessageTable msgToDoctor = new MessageTable();
            msgToDoctor.MessageID = GenerateMsgID();
            msgToDoctor.MessageTo = currDoc.Email;
            msgToDoctor.MessageFrom = "System";
            msgToDoctor.Date = DateTime.Now;
            //msgToDoctor.Message = $"Appointment scheduled on {appt.Data} @ {appt.Time} with Patient {currUser.FirstName + currUser.LastName}";
            msgToDoctor.Message = "Appointment scheduled";
            medDB.MessageTables.Add(msgToDoctor);
            UpdateDB();

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
    }
}