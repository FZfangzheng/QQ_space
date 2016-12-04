<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Homepage.aspx.cs" Inherits="Homepage2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:LinkButton ID="lbtnphoto" runat="server" Text="照片" PostBackUrl="~/Photo.aspx" ></asp:LinkButton>:<asp:Label ID="numblephoto" runat="server" ></asp:Label>
    <asp:LinkButton ID="lbtnsay" runat="server" Text="说说" PostBackUrl="~/Say.aspx" ></asp:LinkButton>:<asp:Label ID="numblesay" runat="server" ></asp:Label>
    <asp:LinkButton ID="lbtndairy" runat="server" Text="日志" PostBackUrl="~/Dairy.aspx" ></asp:LinkButton>:<asp:Label ID="numbledairy" runat="server" ></asp:Label><br />
    <asp:LinkButton ID="lbtnperson" runat="server" Text="个人档" PostBackUrl="~/Person.aspx" ></asp:LinkButton>:
    <br /><asp:Label ID="lbsex" runat="server" ></asp:Label><br />
    <asp:Label ID="lbage" runat="server" ></asp:Label><br />
    <asp:Label ID="lblocation" runat="server" ></asp:Label><br />
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
</asp:Content>

