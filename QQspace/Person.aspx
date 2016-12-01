<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Person.aspx.cs" Inherits="Person2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    基本资料&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="change_data" runat="server" Text="修改" OnClick="change_data_Click" ></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="change_password" runat="server" Text="修改密码" PostBackUrl="~/Changepassword.aspx"  ></asp:LinkButton><br />
   <asp:Panel ID="pndata" runat="server" Visible="true" >
    昵称<asp:Label ID="lbnickname" runat="server" ></asp:Label><br />
    性别<asp:Label ID="lbsex" runat="server" ></asp:Label><br />
    年龄<asp:Label ID="lbage" runat="server" ></asp:Label><br />
    所在地<asp:Label ID="lblocation" runat="server" ></asp:Label><br />
    </asp:Panel>
     <asp:Panel ID="pndata_change" runat="server" Visible="false" >
    昵称<asp:TextBox ID="txtnickname" runat="server" ></asp:TextBox><br />
    性别<asp:TextBox ID="txtsex" runat="server" ></asp:TextBox><br />
    年龄<asp:TextBox ID="txtage" runat="server" ></asp:TextBox><br />
    所在地<asp:TextBox ID="txtlocation" runat="server" ></asp:TextBox><br />
         <asp:Button ID="yes" runat="server" Text="确定" OnClick="yes_Click" />
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="no" runat="server" Text="取消" OnClick="no_Click" />
     </asp:Panel>
</asp:Content>

