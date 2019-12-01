<%@ Page Title="Inbox" Language="C#" MasterPageFile="~/PatientMaster.Master" AutoEventWireup="true" CodeBehind="inbox.aspx.cs" Inherits="FinalProject.Pages.messages" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            margin-left: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Inbox<br />
</h3>
    <asp:ListBox ID="InboxListBox" runat="server" Height="160px" Width="860px"></asp:ListBox>
    &nbsp;<asp:Button ID="DeleteRecievedButton" runat="server" OnClick="DeleteRecievedButton_Click" Text="Delete Selected" Width="150px" />
    <br />
<h3>Outbox</h3>
    &nbsp;<asp:ListBox ID="OutboxListBox" runat="server" CssClass="auto-style1" Height="160px" Width="860px"></asp:ListBox>
    <asp:Button ID="DeleteSentButton" runat="server" OnClick="DeleteSentButton_Click" Text="Delete Selected" Width="150px" />
&nbsp;<h3>
        Compose message</h3>
    <p>
        To: <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="LastName" DataValueField="LastName" Height="31px" Width="125px">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [LastName] FROM [DoctorTable]"></asp:SqlDataSource>
</p>
    <p>
        Message:</p>
    <p>
        <asp:TextBox ID="TextBox1" runat="server" Height="86px" Width="295px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="SendButton" runat="server" OnClick="SendButton_Click" Text="Send it!" />
</p>
<p>
    &nbsp;</p>
</asp:Content>
