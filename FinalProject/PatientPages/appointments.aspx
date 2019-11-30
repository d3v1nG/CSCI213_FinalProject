<%@ Page Title="Appointments" Language="C#" MasterPageFile="~/PatientMaster.Master" AutoEventWireup="true" CodeBehind="appointments.aspx.cs" Inherits="FinalProject.Pages.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Manage Appointments Here<br />
</h3>
<h3>
    My
    Appointments</h3>
<p>
    <asp:ListBox ID="AppointmentListBox" runat="server" Height="164px" Width="1126px"></asp:ListBox>
&nbsp;<asp:Button ID="DeleteButton" runat="server" OnClick="DeleteButton_Click" Text="Cancel Selected" Width="142px" />
    </p>
<h3>
    Schedule new appointment:</h3>
<p>
    Select Doctor:<asp:DropDownList ID="DoctorSelectDropDownList" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="LastName" DataValueField="LastName" OnSelectedIndexChanged="DoctorSelectDropDownList_SelectedIndexChanged" Width="212px" Height="16px">
    </asp:DropDownList>
&nbsp;<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [LastName], [FirstName] FROM [DoctorTable]"></asp:SqlDataSource>
</p>
<p>
    Select prefered date:</p>
<p>
    <asp:Calendar ID="AppointmentDaySelectCalendar" runat="server" OnSelectionChanged="AppointmentDaySelectCalendar_SelectionChanged"></asp:Calendar>
    <asp:Button ID="CheckButton" runat="server" OnClick="CheckButton_Click" Text="Check Availability" />
&nbsp;&nbsp;&nbsp;
</p>
<p>
    Times available:&nbsp;
    <asp:DropDownList ID="TimeDropDownList" runat="server" Width="159px" OnSelectedIndexChanged="TimeDropDownList_SelectedIndexChanged">
    </asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Reason for visit:
    <asp:TextBox ID="ReasonTextBox" runat="server" Width="246px"></asp:TextBox>
</p>
    <p>
        <asp:Button ID="ScheduleButton" runat="server" OnClick="ScheduleButton_Click" Text="Schedule Appointment" />
</p>
<p>
</p>
</asp:Content>
