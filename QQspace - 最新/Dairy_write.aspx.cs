using System;
using System.Collections.Generic;
using System.Data;
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
            if (!IsPostBack)
            {
                string sql = "select * from Dairy_drafts where username='" + Session["name"].ToString() + "'";

                DataTable dt = mydairy.select(sql);

                if (dt.Rows.Count != 0)

                {
                    txttitle.Text = dt.Rows[0][1].ToString();

                    myEditor.InnerHtml = dt.Rows[0][2].ToString();
                }
            }
        }
        else
            Response.Write("<script>alert('尚未登录！');location='Login.aspx'</script>");
    }

   /* protected void Page_Error(object sender,EventArgs e)
    {
        Exception ex = Server.GetLastError();
        if(ex is HttpRequestValidationException )
        {
            //Response.Write("<script language=javascript>alert('非法字符')</script>");
            Server.ClearError();
            //Response.Write("<script language=javascript>window.location.href='Dairy.aspx'</script>");
        }
    }*/

    protected void btndairycancel_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.location='Dairy.aspx'</script>");
    }

    protected void btndairyprint_Click(object sender, EventArgs e)
    {
        string title = txttitle.Text;

        string content = Server.HtmlDecode(myEditor.InnerHtml);

        string time = DateTime.Now.ToString();

        string sql = "insert into Dairy values('" + Session["name"].ToString() + "','" + title + "','" + content + "',0,'" + Session["nickname"].ToString() + "','" + time + "',',')";

        string sql1 = "delete from Dairy_drafts where username='" + Session["name"].ToString() + "'";

        string sql0 = "select photo from Login where username='" + Session["name"].ToString() + "'";

        DataTable dt = mydairy.select(sql0);

        string printdairy = Session["nickname"].ToString() + "发表了日志：" + title;

        string sql2 = "insert into Dynamic values('" + Session["name"].ToString() + "','" + Session["nickname"] + "','" + dt.Rows[0][0].ToString() + "','','" + title + "','" + content + "','','','dairy',0,'" + DateTime.Now.ToString() + "',',','" + printdairy + "','','')";

        if (title != "" && content != "")
        {

            mydairy.store_change(sql);

            mydairy.store_change(sql1);

            mydairy.store_change(sql2);

            mydairy.rank(Session["name"].ToString(), 4);

            Response.Write("<script>alert('发表成功！');location='Dairy.aspx'</script>");
        }
        else
            Response.Write("<script>alert('内容不能为空！');location='Dairy.aspx'</script>");
    }

    protected void btndairydrafts_Click(object sender, EventArgs e)
    {
        string title = txttitle.Text;

        string content = Server.HtmlDecode(myEditor.InnerHtml);

        string sql = "insert into Dairy_drafts values('" + title + "','" + content + "','" + Session["name"].ToString() + "')";

        string sql1 = "select * from Dairy_drafts where username='" + Session["name"].ToString() + "' and title='" + title + "' and dairycontent ='" + content + "'";

        DataTable dt = mydairy.select(sql1);

        if (dt.Rows.Count == 0)

        {

            string sql2 = "delete from Dairy_drafts where username='" + Session["name"].ToString() + "'";

            mydairy.store_change(sql2);

            mydairy.store_change(sql);

           

            Response.Write("<script>alert('已存至草稿箱，下次写日志自动跳出内容！')</script>");
        }
        else
        {
            Response.Write("<script>alert('已经存至草稿箱！')</script>");
        }
    }
}