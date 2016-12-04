<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Special.aspx.cs" Inherits="Special2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:LinkButton ID="specialfriend" runat="server" Text="关心的好友" OnClick="specialfriend_Click" ></asp:LinkButton>
    &nbsp;<asp:LinkButton ID="addspecialfriend" runat="server" Text="添加特别关心" OnClick="addspecialfriend_Click" ></asp:LinkButton>
    <asp:Panel ID="pnspecialfriend" runat="server" Visible="true" >
        <asp:Repeater ID="RptPerson" runat="server"  OnItemDataBound="RptPerson_ItemDataBound" OnItemCommand ="RptPerson_ItemCommand" >
            <ItemTemplate>
                <h2>
                <%# Eval("nickname") %>
                <%#Eval("username") %>:</h2>
                <h3> <%#Eval("say") %></h3>
                <asp:Repeater ID="RptSay" runat="server" >
                    <ItemTemplate>
                        <%# Eval("nickname") %>
                        <%# Eval("username") %>:
                        <%#Eval("comment") %>
                        <br />
                    </ItemTemplate>
                </asp:Repeater>
                <asp:LinkButton ID="good" runat="server" Text="点赞" CommandName="Good" CommandArgument='<%#Eval("id") %>' ></asp:LinkButton>
               (<%# Eval("good") %>)
                
                <asp:LinkButton ID="collect" runat="server" Text="收藏" CommandName="Collect" CommandArgument='<%#Eval("id") %>'></asp:LinkButton>
                <asp:LinkButton ID="delete" runat="server" Text="删除" CommandName="Delete" CommandArgument='<%#Eval("id") %>'></asp:LinkButton><br />
                <asp:TextBox ID="reply" runat="server" TextMode="MultiLine"></asp:TextBox>
                <asp:LinkButton ID="reply1" runat="server" Text="回复" CommandName="Reply" CommandArgument='<%#Eval("id") %>' ></asp:LinkButton>
            </ItemTemplate>
        </asp:Repeater>

    </asp:Panel>
    <asp:Panel ID="pnaddspecialfriend" runat="server" Visible="false" >
        <asp:Repeater ID="rptfriend" runat="server" OnItemCommand="rptfriend_ItemCommand" >
            <HeaderTemplate> 
                <table>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# Eval("hisnickname") %>
                    </td>
                    <td>
                       <asp:LinkButton ID="friendspace" runat="server" Text =' <%# Eval("otherusername") %>' ></asp:LinkButton>
                       
                    </td>
                    <td>
                        <asp:LinkButton ID="specialfriend" runat="server" Text ="关心" CommandName="Care" CommandArgument='<%# Eval("otherusername") %>'></asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
               </table>
            </FooterTemplate>
        </asp:Repeater>
        <br />
    <asp:Button ID="btnUp" runat="server" Text="上一页" OnClick="btnUp_Click" />
        <asp:Button ID="btnDown" runat="server" Text="下一页"  OnClick="btnDown_Click"/>
        <asp:Button ID="btnFirst" runat="server" Text="首页" OnClick="btnFirst_Click" />
        <asp:Button ID="btnLast" runat="server" Text="尾页"  OnClick="btnLast_Click"/>
        页次：<asp:Label ID="lbNow" runat="server" Text="1"></asp:Label>
        /<asp:Label ID="lbTotal" runat="server" Text="1"></asp:Label>
        转<asp:TextBox ID="txtJump" Text="1" runat="server" Width="16px" onkeyup="this.value=this.value.replace(/\D/g,'')"></asp:TextBox>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat ="server" ControlToValidate ="txtJump" ></asp:RequiredFieldValidator> 
        <asp:Button ID="btnJump" runat="server" Text="Go"  OnClick="btnJump_Click"/>
         <asp:Button ID="btnback" runat="server" Text="返回" PostBackUrl="~/Friend.aspx" />

    </asp:Panel>
</asp:Content>

