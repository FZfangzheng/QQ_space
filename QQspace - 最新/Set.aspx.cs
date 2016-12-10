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
            //个性设置
            string sql1 = "select visitway from Login where username='" + Session["name"].ToString() + "'";

            DataTable dt1 = myset.select(sql1);

            lbpersonalset.Text = dt1.Rows[0][0].ToString();
            //陌生人访问设置
            string sql2 = "select authority from Login where username='" + Session["name"].ToString() + "'";

            DataTable dt2 = myset.select(sql2);

            if(Convert.ToInt32( dt2.Rows[0][0].ToString())==0)
            {
                Labelauthority.Text = "允许陌生人访问";
            }
            else
            {
                Labelauthority.Text = "不允许陌生人访问";
            }
        }
        else
            Response.Write("<script>alert('尚未登录！');location='Login.aspx'</script>");
    }

    protected void btnlimit_Click(object sender, EventArgs e)
    {
        string otherusername = txtlimit.Text;

        string sql = "select * from Authority where username='" + Session["name"].ToString() + "' and otherusername='" + otherusername + "'";

        string sql1 = "select * from Friend where myusername='" + Session["name"].ToString() + "' and otherusername='" + otherusername + "'";

        DataTable dt1 = myset.select(sql1);

        string nickname = "";

        string id = "";

        if (dt1.Rows.Count == 0)
        {
            Response.Write("<script>alert('请输入正确好友用户名')</script>");
        }
        else
        {
            nickname = dt1.Rows[0][3].ToString();

            id = dt1.Rows[0][0].ToString();

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

    protected void lbauthority_Click(object sender, EventArgs e)
    {
        pnauthority.Visible = true;

        pnpersonalset.Visible = false;
    }

    protected void personalset_Click(object sender, EventArgs e)
    {
        string sql = "select rank from Login where username='" + Session["name"].ToString() + "'";

        DataTable dt = myset.select(sql);

        if(Convert.ToInt32(dt.Rows[0][0].ToString()) < 25)
        {
            Response.Write("<script>alert('你的空间等级过低，无法使用个性化功能！')</script>");
        }
        else
        {
            pnauthority.Visible = false;

            pnpersonalset.Visible = true;
        }
    }

    protected void btnpersonalset_Click(object sender, EventArgs e)
    {
        string sql = "update Login set visitway='" + ddlpersonalset.Text + "'";

        myset.store_change(sql);

        Response.Write("<script>alert('应用成功！');location='Personal_center.aspx'</script>");
    }

    protected void btnauthority_Click(object sender, EventArgs e)
    {
        if(rblauthority.SelectedValue=="允许" )
        {
            string sql = "update Login set authority='0' where username='" + Session["name"].ToString() + "'";

            myset.store_change(sql);

            Response.Write("<script>window.location='Set.aspx'</script>");
        }
        if(rblauthority.SelectedValue=="不允许")
        {
            string sql = "update Login set authority='1' where username='" + Session["name"].ToString() + "'";

            myset.store_change(sql);

            Response.Write("<script>window.location='Set.aspx'</script>");
        }
    }
}