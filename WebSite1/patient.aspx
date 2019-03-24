<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="patient.aspx.cs" Inherits="patient" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        <asp:TextBox ID="bd" runat="server"></asp:TextBox>
        patient number: <asp:TextBox ID="number" runat="server"></asp:TextBox>
        patient name: <asp:TextBox ID="name" runat="server"></asp:TextBox>
<%--        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>--%>
    </div>
</asp:Content>



