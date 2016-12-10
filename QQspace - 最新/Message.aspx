<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Message.aspx.cs" Inherits="Message2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <h3>留言板</h3>
     <asp:TextBox ID="saysay" runat="server" Height="47px" Width="255px" TextMode="MultiLine"></asp:TextBox>
        <asp:Button ID="printsay" runat="server" OnClick="printsay_Click"  Text="发表" />
     

         <asp:Repeater ID="RptPerson" runat="server"  OnItemDataBound="RptPerson_ItemDataBound" OnItemCommand ="RptPerson_ItemCommand" >
            <ItemTemplate>
                <h2>
                <%# Eval("nickname") %>
                <asp:LinkButton ID="friend" runat="server" Text='<%#Eval("username") %>' CommandArgument='<%# Eval("otherusername") %>' CommandName="Friendspace" ></asp:LinkButton>
                :</h2>
                <h3> <%#Eval("message") %></h3>
                <asp:Repeater ID="RptSay" runat="server"  OnItemCommand="RptSay_ItemCommand" >
                    <ItemTemplate>
                        <%# Eval("nickname") %>
                         <asp:LinkButton ID="friend" runat="server" Text='<%#Eval("username") %>' CommandArgument='<%# Eval("username") %>' CommandName="Friendspace" ></asp:LinkButton>:
                        <%#Eval("comment") %>
                        <br />
                    </ItemTemplate>
                </asp:Repeater>
                
                <asp:LinkButton ID="delete" runat="server" Text="删除" CommandName="Delete" CommandArgument='<%#Eval("id") %>'></asp:LinkButton><br />
                <asp:TextBox ID="reply" runat="server" TextMode="MultiLine"></asp:TextBox>
                <asp:LinkButton ID="reply1" runat="server" Text="回复" CommandName="Reply" CommandArgument='<%#Eval("id") %>' ></asp:LinkButton>
            </ItemTemplate>
        </asp:Repeater>
</asp:Content>

