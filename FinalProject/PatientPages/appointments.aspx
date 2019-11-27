<%@ Page Title="Appointments" Language="C#" MasterPageFile="~/PatientMaster.Master" AutoEventWireup="true" CodeBehind="appointments.aspx.cs" Inherits="FinalProject.Pages.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .auto-style1 {
        margin-left: 50px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Manage Appointments Here<br />
</h3>
<h3>
    History</h3>
<p>
    &nbsp;</p>
<h3>
    Schedule new appointment:</h3>
<p>
    Select Doctor:<asp:DropDownList ID="DoctorSelectDropDownList" runat="server" CssClass="auto-style1" DataSourceID="SqlDataSource1" DataTextField="FirstName" DataValueField="FirstName" Width="211px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [FirstName], [LastName] FROM [DoctorTable]"></asp:SqlDataSource>
    <asp:EntityDataSource ID="EntityDataSource1" runat="server">
    </asp:EntityDataSource>
</p>
<p>
    &nbsp;</p>
    <p>
        Select prefered date:</p>
    <p>
        <asp:Calendar ID="AppointmentDaySelectCalendar" runat="server"></asp:Calendar>
    </p>
    <p>
    Times available:&nbsp; <asp:DropDownList ID="DropDownList1" runat="server" Width="159px">
        </asp:DropDownList>
    </p>
<p>
</p>
</asp:Content>
