﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Personal_center.aspx.cs" Inherits="Default3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
          <asp:TextBox ID="saysay" runat="server" Height="47px" Width="255px" TextMode="MultiLine"></asp:TextBox>
        <asp:Button ID="printsay" runat="server" OnClick="printsay_Click"  Text="发表" />
     

         <asp:Repeater ID="RptPerson" runat="server"  OnItemDataBound="RptPerson_ItemDataBound" OnItemCommand ="RptPerson_ItemCommand" >
            <ItemTemplate>
                <h2>
                    
                <img src='<%# Eval("head_portrait") %>' runat="server" width="30" height="35" />
                <%# Eval("nickname") %>
                <asp:LinkButton ID="friend" runat="server" Text='<%#Eval("username") %>' CommandArgument='<%# Eval("username") %>' CommandName="Friendspace" ></asp:LinkButton></h2>
                <h3> <%#Eval("say") %></h3>
                <h3><asp:LinkButton ID="album" runat="server" Text='<%# Eval("printphoto") %>' CommandArgument='<%# Eval("id") %>' CommandName="Album"></asp:LinkButton><br />
                   
                    <asp:Image ID="photo" runat="server" ImageUrl='<%# Eval("photo") %>' width="30" height="35" />
                </h3>
                <h3><asp:LinkButton ID="dairytitle" runat="server" Text='<%# Eval("printdairy") %>' CommandArgument='<%# Eval("id") %>' CommandName="Dairy"></asp:LinkButton>
                </h3>
                <h4><%# Eval("reprint") %></h4>
                <h6><%# Eval("time") %></h6>

                <asp:Repeater ID="RptSay" runat="server" OnItemCommand="RptSay_ItemCommand" >
                    <ItemTemplate>
                        <%# Eval("nickname") %>
                        <asp:LinkButton ID="friend" runat="server" Text='<%#Eval("username") %>' CommandArgument='<%# Eval("username") %>' CommandName="Friendspace"></asp:LinkButton>
                        <%#Eval("comment") %>
                        <br />
                    </ItemTemplate>
                </asp:Repeater>
                <asp:LinkButton ID="good" runat="server" Text="点赞" CommandName="Good" CommandArgument='<%#Eval("id") %>' ></asp:LinkButton>
               (<%# Eval("good") %>)
                 <asp:LinkButton ID="reprint" runat="server" Text="转载" CommandName="Reprint" CommandArgument='<%#Eval("id") %>' ></asp:LinkButton>
                <asp:LinkButton ID="collect" runat="server" Text="收藏" CommandName="Collect" CommandArgument='<%#Eval("id") %>'></asp:LinkButton><br />
                <asp:TextBox ID="reply" runat="server" TextMode="MultiLine"></asp:TextBox>
                <asp:LinkButton ID="reply1" runat="server" Text="回复" CommandName="Reply" CommandArgument='<%#Eval("id") %>' ></asp:LinkButton>
            </ItemTemplate>
        </asp:Repeater>
</asp:Content>

