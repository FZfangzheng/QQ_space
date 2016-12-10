<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Dairy.aspx.cs" Inherits="Dairy2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Button ID="write_dairy" runat="server" PostBackUrl="~/Dairy_write.aspx" Text="写日志"/><br />
    <asp:Repeater ID="Rptdairy" runat="server" OnItemCommand="Rptdairy_ItemCommand" OnItemDataBound="Rptdairy_ItemDataBound"  >
        <HeaderTemplate> 
                <table>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:LinkButton ID="dairy" runat="server" Text='<%#Eval("title") %>' CommandName="Dairy" CommandArgument='<%# Eval("id") %>'></asp:LinkButton>
                          &nbsp;<%# Eval("time") %>
                        <asp:LinkButton ID="delete" runat="server" Text="删除" CommandArgument='<%# Eval("id") %>' CommandName="Delete"></asp:LinkButton>
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

</asp:Content>

