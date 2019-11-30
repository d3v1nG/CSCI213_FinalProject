<%@ Page Title="" Language="C#" MasterPageFile="~/DoctorMaster.Master" AutoEventWireup="true" CodeBehind="inbox.aspx.cs" Inherits="FinalProject.DoctorPages.inbox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            font-size: medium;
        }
        .auto-style2 {
            font-size: x-large;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p class="auto-style2">
        <strong>&nbsp; Inbox:</strong></p>
    <p>
        &nbsp;
        <asp:ListBox ID="InboxListBox" runat="server" Height="152px" Width="496px"></asp:ListBox>
&nbsp;
        <asp:Button ID="DeleteInBoxButton" runat="server" Text="Delete Message" />
        <br />
    </p>
    <p class="auto-style2">
        &nbsp;</p>
    <p class="auto-style2">
        <strong>Outbox:</strong></p>
    <asp:ListBox ID="OutboxListBox" runat="server" Height="125px" Width="494px"></asp:ListBox>
&nbsp;&nbsp;
    <asp:Button ID="DeleteSentButton" runat="server" Text="Delete Message" />
&nbsp;<br />
    <br />
    <strong><span class="auto-style2">Send Message: </span><span class="auto-style1">
    <br />
    <br />
    To:&nbsp;
    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="LastName" DataValueField="LastName" Height="16px" Width="125px">
    </asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [LastName] FROM [PatientTable]"></asp:SqlDataSource>
    <br />
    Message:<br />
    <asp:TextBox ID="TextBox1" runat="server" Height="109px" Width="288px"></asp:TextBox>
    <br />
    <asp:Button ID="SendButton" runat="server" Text="Send Message" />
    </span></strong>
</asp:Content>
