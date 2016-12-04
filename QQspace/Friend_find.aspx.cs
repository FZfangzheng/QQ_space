using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Friend_find : System.Web.UI.Page
{
    Class1 myfriend = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["name"] != null)
        {
            if(Session["friendname"] != null)
            {
                DataBindToRepeater(1);
            }    
        }
        else
            Response.Write("<script>alert('尚未登录！');location='Login.aspx'</script>");
    }
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
        string sql = "select * from Login where username like'%" + Session["friendname"].ToString() + "%' or nickname like'%" + Session["friendname"].ToString() + "%' ";

        DataTable dt = new DataTable();

        dt = myfriend.select(sql);

        PagedDataSource pds = new PagedDataSource();

        pds.AllowPaging = true;

        pds.PageSize = 5;

        pds.DataSource = dt.DefaultView;

        lbTotal.Text = pds.PageCount.ToString();

        pds.CurrentPageIndex = currentPage - 1;//当前页数从零开始，故把接受的数减一

        rptfriendfind.DataSource = pds;

        rptfriendfind.DataBind();

    }

    protected void rptfriendfind_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if(e.CommandName=="Friendfind")
        {
            string otherusername = e.CommandArgument.ToString();

            string sql1 = "select * from Friend where myusername='" + Session["name"].ToString() + "' and otherusername='" + otherusername + "'";

            string sql2 = "insert into Friend_make values('" + Session["name"].ToString() + "','" + otherusername + "','" + Session["nickname"].ToString() + "')";

            string sql4 = "select * from Friend_make where myusername='" + Session["name"].ToString() + "' and otherusername='" + otherusername + "'";

            DataTable dt1 = myfriend.select(sql1);

            DataTable dt2 = myfriend.select(sql4);

            if(dt1.Rows.Count > 0)
            {
                Response.Write("<script>alert('他已经是你的好友！')</script>");
            }
            else
            {
                if (Session["name"].ToString() == otherusername)
                {
                    Response.Write("<script>alert('不可以添加自己为好友！');location='Friend_find.aspx'</script>");
                }
                else
                {
                    if (dt2.Rows.Count == 0)
                    {
                        myfriend.store_change(sql2);

                        Response.Write("<script>alert('发送好友申请成功！');location='Friend_find.aspx'</script>");
                    }
                    else
                        Response.Write("<script>alert('已经发送申请，请耐心等待对方回应！');location='Friend_find.aspx'</script>");
                }
            }
        }
    }
}