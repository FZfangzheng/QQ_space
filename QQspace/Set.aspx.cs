using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Set2 : System.Web.UI.Page
{
    Class1 myset = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["name"] != null)
        {
            string sql = "select * from Authority where username='" + Session["name"].ToString() + "'";

            DataTable dt = myset.select(sql);

            limitfriend.DataSource = dt;

            limitfriend.DataBind();
        }
        else
            Response.Write("<script>alert('尚未登录！');location='Login.aspx'</script>");
    }

    protected void btnlimit_Click(object sender, EventArgs e)
    {
        string otherusername = txtlimit.Text;

        string sql = "select * from Authority where username='" + Session["name"].ToString() + "' and otherusername='" + otherusername + "'";

        string sql1 = "select * from Friend where myusername='" + otherusername + "'";

        DataTable dt1 = myset.select(sql1);

        string nickname = dt1.Rows[0][3].ToString();

        string id = dt1.Rows[0][0].ToString();

        string sql2 = "insert into Authority values('" + id + "','" + Session["name"].ToString() + "','" + otherusername + "','" + nickname + "')";

        DataTable dt = myset.select(sql);

        if (dt.Rows.Count == 0)
        {
            myset.store_change(sql2);

            Response.Write("<script>window.location='Set.aspx'</script>");
        }
        else
            Response.Write("<script>alert('该用户已经被添加！')</script>");
    }

    protected void limitfriend_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if(e.CommandName=="Delete")
        {
            string otherusername = e.CommandArgument.ToString();

            string sql = "delete from Authority where otherusername='" + otherusername + "'and username='" + Session["name"].ToString() + "'";

            myset.store_change(sql);

            Response.Write("<script>window.location='Set.aspx'</script>");
        }
    }
}