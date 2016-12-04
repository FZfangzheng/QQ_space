using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dairy_write : System.Web.UI.Page
{
    Class1 mydairy = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["name"] != null)
        {

        }
        else
            Response.Write("<script>alert('尚未登录！');location='Login.aspx'</script>");
    }

    protected void btndairycancel_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.location='Dairy.aspx'</script>");
    }

    protected void btndairyprint_Click(object sender, EventArgs e)
    {
        string title = txttitle.Text;

        string content = txtdairycontent.Text;

        string time = DateTime.Now.ToString();

        string sql = "insert into Dairy values('" + Session["name"].ToString() + "','" + title + "','" + content + "',0,'" + Session["nickname"].ToString() + "','" + time + "')";

        if (title != "" && content != "")
        {

            mydairy.store_change(sql);

            Response.Write("<script>alert('发表成功！');location='Dairy.aspx'</script>");
        }
        else
            Response.Write("<script>alert('内容不能为空！');location='Dairy.aspx'</script>");
    }
}