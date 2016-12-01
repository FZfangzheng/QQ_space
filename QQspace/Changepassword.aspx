<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Changepassword.aspx.cs" Inherits="Changepassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            原密码：
    <asp:TextBox ID="password" runat="server" TextMode="Password"  onkeyup="value=value.replace(/[^\w\.\/]/ig,'')"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat ="server" ErrorMessage ="密码不能为空！" ControlToValidate ="password" ForeColor ="Red"></asp:RequiredFieldValidator> <br />                    
            新密码：       
            <asp:TextBox ID="txtPwd" runat="server" TextMode="Password"  onkeyup="value=value.replace(/[^\w\.\/]/ig,'')"></asp:TextBox>       
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat ="server" ErrorMessage ="密码不能为空！" ControlToValidate ="txtPwd" ForeColor ="Red"></asp:RequiredFieldValidator> <br />                    
           密码确认：
            <asp:TextBox ID="txtPasswordConfirm" runat ="server" TextMode="Password"  onkeyup="value=value.replace(/[^\w\.\/]/ig,'')"></asp:TextBox>  
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat ="server" ErrorMessage ="密码确认不能为空！" ControlToValidate ="txtPasswordConfirm" ForeColor ="Red"></asp:RequiredFieldValidator> <br />  
             <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage ="前后密码不一致！" ControlToCompare="txtPwd" ControlToValidate="txtPasswordConfirm" ForeColor ="Red"></asp:CompareValidator> <br />   
    <asp:Button ID="btnchangpassword" runat="server" Text="确定" OnClick="btnchangpassword_Click" />
    <asp:Button ID="btnchangepassword_back" runat="server" Text="返回" PostBackUrl="~/Person.aspx" />                       
</asp:Content>

