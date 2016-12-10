<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Friend.aspx.cs" Inherits="Friend2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:LinkButton ID="lbfindfriend" runat="server" Text="寻找好友" OnClick="lbfindfriend_Click" ></asp:LinkButton>
     &nbsp<asp:LinkButton ID="lbmyfriend" runat="server" Text="我的好友" PostBackUrl="~/Myfriend.aspx" ></asp:LinkButton>
    &nbsp<asp:LinkButton ID="lbrecommend" runat="server" Text="推荐好友" PostBackUrl="~/Friend_recommend.aspx" ></asp:LinkButton>
    &nbsp<asp:LinkButton ID="lbfriendrequire" runat="server" Text="好友请求" OnClick="lbfriendrequire_Click" ></asp:LinkButton>  <asp:Label ID="friendmessage" runat="server" ForeColor="Red" Text=""></asp:Label><br />
    <asp:Panel ID="pnfindfriend" runat="server" Visible="true" >
    请输入您所要查询的QQ或昵称：<asp:TextBox ID="txtfriendname" runat="server" ></asp:TextBox>
    <asp:Button ID="btnfriendname" runat="server" Text="点击查询" OnClick="btnfriendname_Click" />
    </asp:Panel>
    <asp:Panel ID="pnfriendrequire" runat="server" Visible="false" >
        <asp:Repeater ID="rptfriendrequire" runat="server" OnItemCommand="rptfriendrequire_ItemCommand" >
            <HeaderTemplate> 
                <table>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                          <%# Eval("hisnickname") %>
                        <asp:LinkButton ID="friendspace" runat="server" Text=' <%# Eval("myusername") %>' CommandName="Friendspace" CommandArgument=' <%# Eval("myusername") %>'></asp:LinkButton>
                        <%--<%# Eval("myusername") %>--%>
                        <asp:LinkButton ID="lbfriendaccept" runat="server" Text="加为好友" CommandName="Accept" CommandArgument='<%# Eval("myusername") %>'></asp:LinkButton>
                          <asp:LinkButton ID="lbrefuse" runat="server" Text="拒绝" CommandName="Refusefriend" CommandArgument='<%# Eval("myusername") %>'></asp:LinkButton>
                    </td>
                    <td>

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
         <asp:Button ID="btnback" runat="server" Text="返回" PostBackUrl="~/Personal_center.aspx" />
    </asp:Panel>
</asp:Content>

