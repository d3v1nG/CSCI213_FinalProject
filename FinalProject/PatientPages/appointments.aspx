<%@ Page Title="Appointments" Language="C#" MasterPageFile="~/PatientMaster.Master" AutoEventWireup="true" CodeBehind="appointments.aspx.cs" Inherits="FinalProject.Pages.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Manage Appointments Here<br />
</h3>
<h3>
    Appointments</h3>
<p>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="AppointmentID" DataSourceID="SqlDataSource2" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="AppointmentID" HeaderText="AppointmentID" InsertVisible="False" ReadOnly="True" SortExpression="AppointmentID" />
            <asp:BoundField DataField="PatientID" HeaderText="PatientID" SortExpression="PatientID" />
            <asp:BoundField DataField="DoctorID" HeaderText="DoctorID" SortExpression="DoctorID" />
            <asp:BoundField DataField="Purpose" HeaderText="Purpose" SortExpression="Purpose" />
            <asp:BoundField DataField="Data" HeaderText="Data" SortExpression="Data" />
            <asp:BoundField DataField="Time" HeaderText="Time" SortExpression="Time" />
            <asp:BoundField DataField="VisitSummary" HeaderText="VisitSummary" SortExpression="VisitSummary" />
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [AppointmentTable]"></asp:SqlDataSource>
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
</p>
    <p>
        <asp:Button ID="ScheduleButton" runat="server" OnClick="ScheduleButton_Click" Text="Schedule Appointment" />
</p>
<p>
</p>
</asp:Content>
