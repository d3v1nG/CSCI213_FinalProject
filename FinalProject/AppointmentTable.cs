//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FinalProject
{
    using System;
    using System.Collections.Generic;
    
    public partial class AppointmentTable
    {
        public int AppointmentID { get; set; }
        public Nullable<int> PatientID { get; set; }
        public Nullable<int> DoctorID { get; set; }
        public string Purpose { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
        public Nullable<System.TimeSpan> Time { get; set; }
        public string VisitSummary { get; set; }
    }
}
