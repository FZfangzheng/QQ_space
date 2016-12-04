using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Friend2 : System.Web.UI.Page
{
    Class1 myfriend = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["name"] != null)
        {
            DataBindToRepeater(1);
            //读取好友申请信息
            string sql1 = "select * from Friend_make where otherusername='" + Session["name"].ToString() + "'";

            DataTable dt1 = myfriend.select(sql1);

            if (dt1.Rows.Count > 0)
            {
                friendmessage.Text = dt1.Rows.Count.ToString();
            }
        }
        else
            Response.Write("<script>alert('尚未登录！');location='Login.aspx'</script>");
    }

    protected void lbfindfriend_Click(object sender, EventArgs e)
    {
        pnfindfriend.Visible = true;

        pnfriendrequire.Visible = false;
    }

    protected void lbfriendrequire_Click(object sender, EventArgs e)
    {
        pnfriendrequire.Visible = true;

        pnfindfriend.Visible = false;
    }

    protected void btnfriendname_Click(object sender, EventArgs e)
    {
         Session["friendname"] = txtfriendname.Text;

        Response.Write("<script>window.location='Friend_find.aspx'</script>");
    }

    protected void rptfriendrequire_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if(e.CommandName=="Accept")
        {
            string otherusername = e.CommandArgument.ToString();

            string sql1 = "delete from Friend_make where otherusername='" + Session["name"].ToString() + "' and myusername='" + otherusername + "'";

            string sql2 = "select nickname from Login where username='" + otherusername + "'";

            DataTable dt = myfriend.select(sql2);

            string nickname = dt.Rows[0][0].ToString();

            string sql3 = "insert into Friend values('" + Session["name"].ToString() + "','" + otherusername + "','" + nickname + "')";

            string sql4 = "insert into Friend values('" + otherusername + "','" + Session["name"].ToString() + "','" + Session["nickname"].ToString() + "')";

            myfriend.store_change(sql1);

            myfriend.store_change(sql3);

            myfriend.store_change(sql4);

            Response.Write("<script>window.location='Myfriend.aspx'</script>");
        }
    }
    //分页操作
    protected void btnDown_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(lbNow.Text) + 1 <= Convert.ToInt32(lbTotal.Text))
        {
            lbNow.Text = Convert.ToString(Convert.ToInt32(lbNow.Text) + 1);

            DataBindToRepeater(Convert.ToInt32(lbNow.Text));
        }
    }
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        lbNow.Text = Convert.ToString(1);
        DataBindToRepeater(1);
    }
    protected void btnLast_Click(object sender, EventArgs e)
    {
        lbNow.Text = lbTotal.Text;

        DataBindToRepeater(Convert.ToInt32(lbTotal.Text));
    }
    protected void btnJump_Click(object sender, EventArgs e)
    {
        if (RequiredFieldValidator1.IsValid == true)
        {
            if (Convert.ToInt32(txtJump.Text) <= Convert.ToInt32(lbTotal.Text) && Convert.ToInt32(txtJump.Text) >= 1)
            {
                lbNow.Text = txtJump.Text;

                DataBindToRepeater(Convert.ToInt32(txtJump.Text));
            }
        }
    }
    protected void btnUp_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(lbNow.Text) - 1 >= 1)
        {
            lbNow.Text = Convert.ToString(Convert.ToInt32(lbNow.Text) - 1);

            DataBindToRepeater(Convert.ToInt32(lbNow.Text));
        }
    }

    void DataBindToRepeater(int currentPage)
    {
        string sql = "select * from Friend_make where otherusername='" + Session["name"].ToString() + "'";

        DataTable dt = new DataTable();

        dt = myfriend.select(sql);

        PagedDataSource pds = new PagedDataSource();

        pds.AllowPaging = true;

        pds.PageSize = 5;

        pds.DataSource = dt.DefaultView;

        lbTotal.Text = pds.PageCount.ToString();

        pds.CurrentPageIndex = currentPage - 1;//当前页数从零开始，故把接受的数减一

        rptfriendrequire.DataSource = pds;

        rptfriendrequire.DataBind();

    }
}