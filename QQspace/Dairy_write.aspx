<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Dairy_write.aspx.cs" Inherits="Dairy_write" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:TextBox ID="txttitle" runat="server" Width="500"></asp:TextBox><br />
        <asp:TextBox ID="txtdairycontent" runat="server" TextMode="MultiLine" Width="500" Height="300"></asp:TextBox><br />
        <asp:Button ID="btndairyprint" runat="server" OnClick="btndairyprint_Click"  Text="发表"/>
        <asp:Button ID="btndairycancel" runat="server" OnClick="btndairycancel_Click"  Text="取消" />
</asp:Content>

