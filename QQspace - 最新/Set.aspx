<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Set.aspx.cs" Inherits="Set2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:LinkButton ID="lbauthority" runat="server" OnClick="lbauthority_Click" Text="权限设置"></asp:LinkButton>
    &nbsp;&nbsp<asp:LinkButton ID="personalset" runat="server" OnClick="personalset_Click" Text="个性化"></asp:LinkButton><br />
        <asp:Panel ID="pnauthority" runat="server" Visible="true" >
            <h2>陌生人限制</h2>
            
            <asp:Label ID="Labelauthority" runat="server" ></asp:Label> <br />
            <br />
            陌生人访问设置：<br />
            
            <asp:RadioButtonList ID="rblauthority" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <asp:ListItem>允许</asp:ListItem>
                <asp:ListItem>不允许</asp:ListItem>
            </asp:RadioButtonList>
            <asp:Button ID="btnauthority" runat="server" OnClick="btnauthority_Click" Text="确定"/>
           
    <h2>好友限制名单</h2>
             <asp:TextBox ID="txtlimit" runat="server" ></asp:TextBox>
    &nbsp;<asp:Button ID="btnlimit" runat="server" OnClick="btnlimit_Click" Text="添加" />
    <asp:Repeater ID="limitfriend" runat="server" OnItemCommand ="limitfriend_ItemCommand" >
        <HeaderTemplate>
            <table>
        </HeaderTemplate>
        <ItemTemplate >
            <tr>
                <td><%# Eval("hisnickname") %>
                    <asp:LinkButton ID="limitfriendname" runat="server" Text=' <%# Eval("otherusername") %>' CommandArgument='<%# Eval("otherusername") %>' CommandName="Friendspace"></asp:LinkButton>
                    <asp:LinkButton ID="limitdelete" runat="server" Text="取消限制" CommandName="Delete" CommandArgument='<%# Eval("otherusername") %>'></asp:LinkButton>
                </td>

            </tr>

        </ItemTemplate>
        <FooterTemplate >
        </table>
            </FooterTemplate>
    </asp:Repeater>
        </asp:Panel>
    <asp:Panel ID="pnpersonalset" runat="server" Visible="false" >
        <h3>访客装扮：</h3>
        <h4>你的访客装扮是：</h4>
        <asp:Label ID="lbpersonalset" runat="server" ></asp:Label><br />
        <asp:DropDownList ID="ddlpersonalset" runat="server">
            <asp:ListItem ></asp:ListItem>
            <asp:ListItem>炫酷地</asp:ListItem>
            <asp:ListItem>孤独地</asp:ListItem>
            <asp:ListItem>唯美地</asp:ListItem>
            <asp:ListItem>恬静地</asp:ListItem>
            <asp:ListItem>搞怪地</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="btnpersonalset" runat="server" Text="确定" OnClick="btnpersonalset_Click" />

    </asp:Panel>
</asp:Content>

