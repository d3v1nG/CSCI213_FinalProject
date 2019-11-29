<%@ Page Title="" Language="C#" MasterPageFile="~/DoctorMaster.Master" AutoEventWireup="true" CodeBehind="PatientList.aspx.cs" Inherits="FinalProject.DoctorPages.PatientList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <br />
    </p>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="PatientID" DataSourceID="SqlDataSource1" Width="380px">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="PatientID" HeaderText="PatientID" InsertVisible="False" ReadOnly="True" SortExpression="PatientID" />
            <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
            <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [PatientID], [FirstName], [LastName] FROM [PatientTable]"></asp:SqlDataSource>
    <asp:Button ID="PatientSelect" runat="server" OnClick="PatientSelect_Click" Text="Select Patient" />
    <p>
        <asp:ListBox ID="PatientInformation" runat="server" Height="119px" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged" Width="386px"></asp:ListBox>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
</asp:Content>
