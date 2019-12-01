<%@ Page Title="" Language="C#" MasterPageFile="~/PatientMaster.Master" AutoEventWireup="true" CodeBehind="testresults.aspx.cs" Inherits="FinalProject.PatientPages.testresults" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Test Results</h3>
    <p>
        <asp:ListBox ID="ResultsListBox" runat="server" Height="190px" OnSelectedIndexChanged="ResultsListBox_SelectedIndexChanged" Width="950px"></asp:ListBox>
        <br />
    </p>
</asp:Content>
