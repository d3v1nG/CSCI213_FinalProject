using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace FinalProject.PatientPages
{
    public partial class medications : System.Web.UI.Page
    {
        MedicalDBEntities medDB = new MedicalDBEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            medDB.MedicationListTables.Load();
            GetMedicationList();
        }

        protected void GetMedicationList()
        {
            PatientTable currPatient = GetCurrentPatient();
            var medsList = from med in medDB.MedicationListTables
                           where currPatient.PatientID == (med.PatientID)
                           select med.Description;
            foreach (string s in medsList.ToList())
            {
                MedicationListBox.Items.Add(s.Trim());
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


    }
}