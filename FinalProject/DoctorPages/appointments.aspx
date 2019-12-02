<%@ Page Title="" Language="C#" MasterPageFile="~/DoctorMaster.Master" AutoEventWireup="true" CodeBehind="appointments.aspx.cs" Inherits="FinalProject.DoctorPages.appointments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            font-size: x-large;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <strong>Manage Appointments</strong><br />
    </p>
    <p>
        <strong>Appointments:</strong></p>
    <p>
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="AppointmentID" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="AppointmentID" HeaderText="AppointmentID" ReadOnly="True" SortExpression="AppointmentID" />
                <asp:BoundField DataField="PatientID" HeaderText="PatientID" SortExpression="PatientID" />
                <asp:BoundField DataField="DoctorID" HeaderText="DoctorID" SortExpression="DoctorID" />
                <asp:BoundField DataField="Purpose" HeaderText="Purpose" SortExpression="Purpose" />
                <asp:BoundField DataField="Data" HeaderText="Data" SortExpression="Data" />
                <asp:BoundField DataField="Time" HeaderText="Time" SortExpression="Time" />
                <asp:BoundField DataField="VisitSummary" HeaderText="VisitSummary" SortExpression="VisitSummary" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [AppointmentID], [PatientID], [DoctorID], [Purpose], [Data], [Time], [VisitSummary] FROM [AppointmentTable]"></asp:SqlDataSource>
    </p>
    <p class="auto-style1">
        <strong>Add New Appointment</strong></p>
    <p>
        Select Patient:&nbsp;
        <asp:DropDownList ID="PatientsSelectDropDownList" runat="server" DataSourceID="SqlDataSource2" DataTextField="LastName" DataValueField="LastName" Height="18px" Width="116px">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [LastName] FROM [PatientTable]"></asp:SqlDataSource>
    </p>
    <p>
        Select Preferred Time:</p>
    <p>
        <asp:Calendar ID="AppointmentDaySelectCalendar" runat="server" OnSelectionChanged="AppointmentDaySelectCalendar_SelectionChanged1"></asp:Calendar>
    </p>
    <p>
        <asp:Button ID="CheckButton" runat="server" OnClick="CheckButton_Click" Text="Check Availability" />
    </p>
    <p>
        Times Available:&nbsp;
        <asp:DropDownList ID="TimeDropDownList" runat="server" Height="17px" Width="119px">
        </asp:DropDownList>
    </p>
    <p>
        <asp:Button ID="ScheduleButton" runat="server" OnClick="ScheduleButton_Click" Text="Schedule Appointmnet" />
    </p>
    <p>
        <asp:TextBox ID="TextBox1" runat="server" Height="85px" Width="343px"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="editButton" runat="server" OnClick="editButton_Click" Text="Edit Vist Summary" />
    </p>
</asp:Content>
