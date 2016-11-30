<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Person.aspx.cs" Inherits="Person2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    基本资料&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="change_data" runat="server" Text="修改" OnClick="change_data_Click" ></asp:LinkButton><br />
    <asp:Label ID="lbnickname" runat="server" ></asp:Label><br />
    <asp:Label ID="lbsex" runat="server" ></asp:Label><br />
    <asp:Label ID="lbage" runat="server" ></asp:Label><br />
    <asp:Label ID="lblocation" runat="server" ></asp:Label><br />

</asp:Content>

