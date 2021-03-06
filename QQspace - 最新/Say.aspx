﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Say.aspx.cs" Inherits="Say2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3><asp:Label ID="label" runat="server"></asp:Label></h3>
     <asp:TextBox ID="saysay" runat="server" Height="47px" Width="255px" TextMode="MultiLine"></asp:TextBox>
        <asp:Button ID="printsay" runat="server" OnClick="printsay_Click"  Text="发表" />
     

         <asp:Repeater ID="RptPerson" runat="server"  OnItemDataBound="RptPerson_ItemDataBound" OnItemCommand ="RptPerson_ItemCommand" >
            <ItemTemplate>
                <h2>
                    <img src='<%# Eval("head_portrait") %>' runat="server" width="30" height="35" />
                <%# Eval("nickname") %>
                <asp:LinkButton ID="friend" runat="server" Text='<%#Eval("username") %>' CommandName="Friendspace" CommandArgument='<%# Eval("username") %>'></asp:LinkButton></h2>
                <h3> <%#Eval("say") %></h3>
                <asp:Repeater ID="RptSay" runat="server" OnItemCommand="RptSay_ItemCommand">
                    <ItemTemplate>
                        <%# Eval("nickname") %>
                       <asp:LinkButton ID="friend" runat="server" Text='<%#Eval("username") %>' CommandName="Friendspace" CommandArgument='<%# Eval("username") %>'></asp:LinkButton>
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
</asp:Content>

