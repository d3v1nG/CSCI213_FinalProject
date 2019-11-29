using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject.DoctorPages
{
    public partial class PatientList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void PatientSelect_Click(object sender, EventArgs e)
        {
            PatientInformation.Items.Add(GridView1.SelectedDataKey[0].ToString());
            PatientInformation.Items.Add(GridView1.SelectedRow.ToString());
        }
    }
}