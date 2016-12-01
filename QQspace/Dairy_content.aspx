<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Dairy_content.aspx.cs" Inherits="Dairy_content" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="pndairycontent" runat="server" Visible="true" >
        <h3><asp:Label ID="dairytitle" runat="server"></asp:Label><br /></h3>
        <h2><asp:Label ID="dairycontent" runat="server" Width="500"></asp:Label></h2><br />
        <asp:Label ID="dairywriter" runat="server" ></asp:Label>
        <asp:Label ID="dairyusername" runat ="server" ></asp:Label><br />
        <asp:LinkButton ID="good" runat="server" OnClick="good_Click" Text="点赞"></asp:LinkButton>
        (<asp:Label ID ="lbgood" runat="server" ></asp:Label>)
        &nbsp;<asp:LinkButton ID="dairycomment" runat="server" OnClick="dairycomment_Click" Text="评论" ></asp:LinkButton>
        &nbsp;<asp:LinkButton ID="dairyreprint" runat="server" OnClick="dairyreprint_Click" Text="转载"></asp:LinkButton>
         &nbsp;<asp:LinkButton ID="dairyeditor" runat="server" OnClick="dairyeditor_Click"  Text="编辑"></asp:LinkButton>
         &nbsp;<asp:LinkButton ID="dairycollect" runat="server" OnClick="dairycollect_Click" Text="收藏"></asp:LinkButton><br />
    评论：<br />

    <asp:Repeater ID="Rptdairycomment" runat="server" OnItemCommand="Rptdairycomment_ItemCommand" >
        <HeaderTemplate> 
                <table>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                          <%# Eval("nickname") %>
                        <%# Eval("username") %>:
                        <%#Eval("comment") %>
                    </td>
                    <td>

                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
               </table>
            </FooterTemplate>

    </asp:Repeater>
        <asp:Panel ID="pndairycomment" runat="server" Visible="false" >
            <asp:TextBox ID="comment" runat="server" TextMode="MultiLine" ></asp:TextBox>
            <asp:Button ID="btndairycomment" runat="server" OnClick="btndairycomment_Click" Text="发表" />      
        </asp:Panel>
   </asp:Panel><br />
    <asp:Panel ID="pndairyeditor" runat="server" Visible="false" >
        <asp:TextBox ID="txttitle" runat="server" ></asp:TextBox><br />
        <asp:TextBox ID="txtdairycontent" runat="server" TextMode="MultiLine" Width="500" Height="300"></asp:TextBox><br />
        <asp:Button ID="btndairyeditor" runat="server" OnClick="btndairyeditor_Click" Text="确定修改"/>
        <asp:Button ID="btndairyback" runat="server" OnClick="btndairyback_Click" Text="返回" />
    </asp:Panel>
</asp:Content>

