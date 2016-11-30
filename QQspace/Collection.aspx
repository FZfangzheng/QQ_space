<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Collection.aspx.cs" Inherits="Collection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:LinkButton ID="say" runat="server" Text="说说" OnClick="say_Click" ></asp:LinkButton>
    <asp:LinkButton ID="dairy" runat="server" Text="日志" OnClick="dairy_Click" ></asp:LinkButton>
    <asp:Panel ID="pnsay" runat="server" Visible="false" >
    <asp:Repeater ID="say_dairy1" runat="server" OnItemCommand="say_dairy1_ItemCommand" >
        <HeaderTemplate >
            <table>

        </HeaderTemplate>
        <ItemTemplate >                        
          <tr>
              <td>
                   <h2>
                <%# Eval("myusername") %>收藏
                <%#Eval("username") %>:</h2>
                <h3> <%#Eval("say") %></h3>
              </td>
              <td><asp:LinkButton ID="delete1" runat="server" Text="删除" CommandName="delete" CommandArgument='<%#Eval("id") %>'></asp:LinkButton></td>
          </tr>
        </ItemTemplate>
        <FooterTemplate >
            </table>
        </FooterTemplate>
    </asp:Repeater>
    </asp:Panel>
    <asp:Panel ID="pndairy" runat="server" Visible ="false" >
        <asp:Repeater ID="say_dairy2" runat="server" OnItemCommand="say_dairy2_ItemCommand" >
        <HeaderTemplate >
            <table>

        </HeaderTemplate>
        <ItemTemplate >                        
          <tr>
              <td>
                   <h2>
                <%# Eval("myusername") %>收藏
                <%#Eval("username") %>:</h2>
                <h3> <%#Eval("dairy") %></h3>
              </td>
              <td><asp:LinkButton ID="delete2" runat="server" Text="删除" CommandName="delete" CommandArgument='<%#Eval("id") %>'></asp:LinkButton></td>
          </tr>
        </ItemTemplate>
        <FooterTemplate >
            </table>
        </FooterTemplate>
    </asp:Repeater>

    </asp:Panel>
</asp:Content>

