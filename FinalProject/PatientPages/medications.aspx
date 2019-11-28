<%@ Page Title="Medication" Language="C#" MasterPageFile="~/PatientMaster.Master" AutoEventWireup="true" CodeBehind="medications.aspx.cs" Inherits="FinalProject.PatientPages.medications" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Medications</h1>
<p>
    <asp:ListBox ID="MedicationListBox" runat="server" Height="189px" Width="354px"></asp:ListBox>
</p>
    </asp:Content>
