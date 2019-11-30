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

        protected void SelectButton_Click(object sender, EventArgs e)
        {
            //Session.Add("", GridView1.SelectedValue);
            GridView1.DataSource = GridView1.SelectedValue; 
        }
    }
}