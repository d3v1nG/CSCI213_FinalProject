using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace FinalProject
{
    public class TestData
    {
        MedicalDBEntities medDB = new MedicalDBEntities();

        // Use this to generate test data
        public bool GenerateMedicationTableData()
        {
            medDB.MedicationListTables.Load();

            MedicationListTable meds = new MedicationListTable() { Description = "Pain Killer", PatientID = 1 };
            MedicationListTable meds2 = new MedicationListTable() { Description = "Cough Syrup", PatientID = 2 };

            medDB.MedicationListTables.Add(meds);
            medDB.MedicationListTables.Add(meds2);

            medDB.SaveChanges();

            return true;
        }

        //protected int GetMedIDNum()

    }
}