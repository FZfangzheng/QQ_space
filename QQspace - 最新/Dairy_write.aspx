<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Dairy_write.aspx.cs" Inherits="Dairy_write" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script  src="../ueditor/ueditor.config.js" type="text/javascript"></script>
     <script  src="../ueditor/ueditor.all.min.js" type="text/javascript"></script>
    <script type="text/javascript" charset="utf-8" src="../ueditor/lang/zh-cn/zh-cn.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:TextBox ID="txttitle" runat="server" Width="500"></asp:TextBox><br />
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
    </script>
    <br />
        <asp:Button ID="btndairyprint" runat="server" OnClick="btndairyprint_Click"  Text="发表"/>
        <asp:Button ID="btndairycancel" runat="server" OnClick="btndairycancel_Click"  Text="取消" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btndairydrafts" runat="server" OnClick="btndairydrafts_Click" Text="草稿箱"/>
</asp:Content>

