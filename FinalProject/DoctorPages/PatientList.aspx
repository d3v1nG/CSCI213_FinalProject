<%@ Page Title="" Language="C#" MasterPageFile="~/DoctorMaster.Master" AutoEventWireup="true" CodeBehind="PatientList.aspx.cs" Inherits="FinalProject.DoctorPages.PatientList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        Search for Patient:
    </p>
    <p>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
&nbsp;
        <asp:Button ID="SearchButton" runat="server" OnClick="SearchButton_Click" Text="Search" />
    </p>
    <p>
        &nbsp;</p>
    <p>
        Enter prescription:
        <asp:TextBox ID="MedInputTextBox" runat="server" Width="166px"></asp:TextBox>
&nbsp;Enter patientID:
        <asp:TextBox ID="patientIDTextBox" runat="server" Width="167px"></asp:TextBox>
    </p>
    <p>
        &nbsp;
        <asp:Button ID="AddMedButton" runat="server" OnClick="AddMedButton_Click" Text="Add Medication" Width="169px" />
&nbsp;<br />
    </p>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="PatientID" DataSourceID="SqlDataSource1" Width="380px" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="PatientID" HeaderText="PatientID" InsertVisible="False" ReadOnly="True" SortExpression="PatientID" />
            <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
            <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [PatientID], [FirstName], [LastName] FROM [PatientTable]"></asp:SqlDataSource>
    <br />
    <asp:Button ID="SelectButton" runat="server" OnClick="SelectButton_Click" Text="Select" Width="113px" />
    <br />
    <br />
    Patient Info:<br />
&nbsp;<asp:GridView ID="patietnInfoGridView" runat="server">
    </asp:GridView>
    <br />
    Test Results:<br />
    <p>
        <asp:GridView ID="patientTestGridVeiw" runat="server">
        </asp:GridView>
    </p>
    <p>
        Medications:
    </p>
    <p>
        <asp:GridView ID="MedicationGridView" runat="server">
        </asp:GridView>
    </p>
    <p>
        Appointment History:</p>
    <p>
        <asp:GridView ID="historyGridView" runat="server">
        </asp:GridView>
    </p>
    <p>
    </p>
</asp:Content>
