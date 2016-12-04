<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Set.aspx.cs" Inherits="Set2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:TextBox ID="txtlimit" runat="server" ></asp:TextBox>
    &nbsp;<asp:Button ID="btnlimit" runat="server" OnClick="btnlimit_Click" Text="添加" />
    <h2>限制名单</h2>
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
</asp:Content>

