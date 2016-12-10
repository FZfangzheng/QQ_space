<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Dairy_content.aspx.cs" Inherits="Dairy_content" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script  src="../ueditor/ueditor.config.js" type="text/javascript"></script>
     <script  src="../ueditor/ueditor.all.min.js" type="text/javascript"></script>
    <script type="text/javascript" charset="utf-8" src="../ueditor/lang/zh-cn/zh-cn.js"></script>
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
                          <asp:LinkButton ID="friend" runat="server" Text='<%#Eval("username") %>' CommandArgument='<%# Eval("username") %>' CommandName="Friendspace" ></asp:LinkButton>:
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
        <div>内容：<br />
            <script id="myEditor" type="text/plain"></script>
            <textarea id="myEditor" name="myEditor" runat="server" onblur="setUeditor()" style="width: 1030px;
                height: 250px;"></textarea>
            <%-- 上面这个style这里是实例化的时候给实例化的这个容器设置宽和高，不设置的话，或默认为auto可能会造成部分显示的情况--%>
            
            <script type="text/javascript">
                var editor = new baidu.editor.ui.Editor();
                
                editor.render("<%=myEditor.ClientID%>");
            </script>
        </div>
<script type="text/javascript">
        function setUeditor() {
            var myEditor = document.getElementById("myEditor");
            myEditor.value = editor.getContent();//把得到的值给textarea
        }
    </script><br />
        <asp:Button ID="btndairyeditor" runat="server" OnClick="btndairyeditor_Click" Text="确定修改"/>
        <asp:Button ID="btndairyback" runat="server" OnClick="btndairyback_Click" Text="返回" />
    </asp:Panel>
</asp:Content>

