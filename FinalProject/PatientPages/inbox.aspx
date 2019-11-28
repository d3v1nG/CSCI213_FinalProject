<%@ Page Title="Inbox" Language="C#" MasterPageFile="~/PatientMaster.Master" AutoEventWireup="true" CodeBehind="inbox.aspx.cs" Inherits="FinalProject.Pages.messages" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Inbox<br />
</h3>
    <asp:ListBox ID="InboxListBox" runat="server" Height="159px" Width="449px"></asp:ListBox>
    <br />
<h3>Outbox</h3>
&nbsp;&nbsp;&nbsp;
    <p>
    Compose message:
</p>
<p>
    &nbsp;</p>
</asp:Content>
