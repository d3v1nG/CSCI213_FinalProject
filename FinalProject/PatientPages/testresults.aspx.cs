using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace FinalProject.PatientPages
{ 

    public partial class testresults : System.Web.UI.Page
    {
        MedicalDBEntities medDB = new MedicalDBEntities();
        PatientTable currUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            medDB.TestsTables.Load();
            var user = User.Identity.Name;

            var currUserQuery = from patient in medDB.PatientTables
                                where patient.FirstName.Trim().Equals(user)
                                select patient;
            if (currUserQuery.Count() != 0)
            {
                currUser = currUserQuery.FirstOrDefault();
            }

            LoadTable();
        }

        protected void LoadTable()
        {
            var results = from t in medDB.TestsTables
                          where currUser.PatientID == t.PatientID
                          select t;
            if (results.Count() != ResultsListBox.Items.Count)
            {
                foreach (TestsTable t in results)
                {
                    string msg = $"From test/appointment on {TrimTime((DateTime)t.TestDate)}, results: {t.TestResults}";
                    ResultsListBox.Items.Add(msg);
                }
            }
        }

        protected string TrimTime(DateTime dt)
        {
            string time = dt.ToString();
            var list = time.Split(' ');
            return list[0];
        }

        protected void ResultsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}