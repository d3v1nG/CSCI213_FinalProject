using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace FinalProject.DoctorPages
{
    public partial class PatientList : System.Web.UI.Page
    {
        MedicalDBEntities patientDB = new MedicalDBEntities();
        MedicalDBEntities testResultDB = new MedicalDBEntities();
        MedicalDBEntities medicationDB = new MedicalDBEntities();
        MedicalDBEntities historyDB = new MedicalDBEntities();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void SelectButton_Click(object sender, EventArgs e)
        {
            patientDB.PatientTables.Load();
            testResultDB.TestsTables.Load();
            medicationDB.MedicationListTables.Load();
            historyDB.AppointmentTables.Load();

            

            var patientInfo = from item in patientDB.PatientTables.Local
                         where item.PatientID == Convert.ToInt32(GridView1.SelectedDataKey[0])
                         select item;

            var patientTestResults = (from item in testResultDB.TestsTables.Local
                                     where item.PatientID == Convert.ToInt32(GridView1.SelectedDataKey[0])
                                     select item);

            var patientMedications = (from item in medicationDB.MedicationListTables.Local
                                      where item.PatientID == Convert.ToInt32(GridView1.SelectedDataKey[0])
                                      select item);

            var patientHistory = from item in historyDB.AppointmentTables.Local
                                 where item.PatientID == Convert.ToInt32(GridView1.SelectedDataKey[0])
                                 select item;


            patietnInfoGridView.DataSource = patientInfo;
            patietnInfoGridView.DataBind();

            patientTestGridVeiw.DataSource = patientTestResults;
            patientTestGridVeiw.DataBind();

            MedicationGridView.DataSource = patientMedications;
            MedicationGridView.DataBind();

            historyGridView.DataSource = patientHistory;
            historyGridView.DataBind();

            //ListBox1.Items.Add(patientTestResults.ToList().First().ToString());
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            patientDB.PatientTables.Load();
            testResultDB.TestsTables.Load();
            medicationDB.MedicationListTables.Load();
            historyDB.AppointmentTables.Load();

            var inputString = TextBox1.Text;
            int idValue;

            if (int.TryParse(inputString, out idValue))
            {   
                // if input was an ID
                var patientInfo = (from item in patientDB.PatientTables.Local
                                   where item.PatientID == idValue
                                   select item);

                if (patientInfo.FirstOrDefault() == null)
                {
                    Response.Redirect("~/DoctorPages/PatientList.aspx");
                }

                var patientTestResults = (from item in testResultDB.TestsTables.Local
                                          where item.PatientID == idValue
                                          select item);

                var patientMedications = (from item in medicationDB.MedicationListTables.Local
                                          where item.PatientID == idValue
                                          select item);

                var patientHistory = from item in historyDB.AppointmentTables.Local
                                     where item.PatientID == idValue
                                     select item;


                patietnInfoGridView.DataSource = patientInfo;
                patietnInfoGridView.DataBind();

                patientTestGridVeiw.DataSource = patientTestResults;
                patientTestGridVeiw.DataBind();

                MedicationGridView.DataSource = patientMedications;
                MedicationGridView.DataBind();

                historyGridView.DataSource = patientHistory;
                historyGridView.DataBind();
            }
            else
            {
               var patientInfo = (from item in patientDB.PatientTables.Local
                               where item.LastName.Trim() == inputString
                               select item);

                if (patientInfo.FirstOrDefault() == null)
                {
                    Response.Redirect("~/DoctorPages/PatientList.aspx");
                }

                // other tables are only accessable by patient id
                int patId = patientInfo.FirstOrDefault().PatientID;

                var patientTestResults = (from item in testResultDB.TestsTables.Local
                                          where item.PatientID == patId
                                          select item);

                var patientMedications = (from item in medicationDB.MedicationListTables.Local
                                          where item.PatientID == patId
                                          select item);

                var patientHistory = from item in historyDB.AppointmentTables.Local
                                     where item.PatientID == patId
                                     select item;

                patietnInfoGridView.DataSource = patientInfo;
                patietnInfoGridView.DataBind();

                patientTestGridVeiw.DataSource = patientTestResults;
                patientTestGridVeiw.DataBind();

                MedicationGridView.DataSource = patientMedications;
                MedicationGridView.DataBind();

                historyGridView.DataSource = patientHistory;
                historyGridView.DataBind();
            }


        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectButton_Click(sender, e);
        }

        protected void AddMedButton_Click(object sender, EventArgs e)
        {
                medicationDB.MedicationListTables.Load();
                medicationDB.PatientTables.Load();

                string input = MedInputTextBox.Text;
                int id = Convert.ToInt32(patientIDTextBox.Text);
                if (!input.Equals(""))
                {
                    MedicationListTable newMed = new MedicationListTable();
                    newMed.Description = input;
                    newMed.PatientID = id;

                    //PatientTable currPat = (from x in medicationDB.PatientTables.Local
                    //                        where x.PatientID == id
                    //                        select x).FirstOrDefault();

                    medicationDB.MedicationListTables.Add(newMed);
                    medicationDB.SaveChanges();
                }
            
        }
    }
}