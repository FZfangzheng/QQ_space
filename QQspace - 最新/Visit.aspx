<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Visit.aspx.cs" Inherits="Visit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>访客详情</h2>
    <asp:Repeater ID="Rptvisitor" runat="server" OnItemCommand="Rptvisitor_ItemCommand" >
        <HeaderTemplate>
            <table>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
            <td><img src='<%# Eval("touristhead_portrait") %>' runat="server" width="30" height="35" /></td>
            <td><%# Eval("touristnickname") %></td>

            <td><asp:LinkButton ID="username" runat="server" Text='<%# Eval("touristusername") %>' CommandName="Friendspace" CommandArgument='<%# Eval("touristusername") %>'></asp:LinkButton></td>
            <td><%# Eval("visitway") %></td>
            <td><h5><%# Eval("time") %></h5></td>
                </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
     <asp:Button ID="btnUp" runat="server" Text="上一页" OnClick="btnUp_Click" />
        <asp:Button ID="btnDown" runat="server" Text="下一页"  OnClick="btnDown_Click"/>
        <asp:Button ID="btnFirst" runat="server" Text="首页" OnClick="btnFirst_Click" />
        <asp:Button ID="btnLast" runat="server" Text="尾页"  OnClick="btnLast_Click"/>
        页次：<asp:Label ID="lbNow" runat="server" Text="1"></asp:Label>
        /<asp:Label ID="lbTotal" runat="server" Text="1"></asp:Label>
        转<asp:TextBox ID="txtJump" Text="1" runat="server" Width="16px" onkeyup="this.value=this.value.replace(/\D/g,'')"></asp:TextBox>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat ="server" ControlToValidate ="txtJump" ></asp:RequiredFieldValidator> 
        <asp:Button ID="btnJump" runat="server" Text="Go"  OnClick="btnJump_Click"/>
         <asp:Button ID="btnback" runat="server" Text="返回" PostBackUrl="~/Personal_center.aspx" />
</asp:Content>

