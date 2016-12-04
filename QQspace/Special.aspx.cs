using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Special2 : System.Web.UI.Page
{
    Class1 myspecial = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["name"] != null)
        {
            if (pnspecialfriend.Visible == true)

            {
                DataTable dt2 = new DataTable();

                DataTable dt1 = new DataTable();

                DataTable dt3 = new DataTable();

                int count, numble;

                string sql2 = "select otherusername from  Care where username='" + Session["name"].ToString() + "'";

                dt3 = myspecial.select(sql2);//取出特别关心的用户

                count = dt3.Rows.Count;

                string sql1 = " ";

                for (numble = 0; numble < count; numble++)

                {
                    sql1 = "select * from Say where username='" + dt3.Rows[0][numble] + "'";

                    dt1 = myspecial.select(sql1);

                    dt2.Merge(dt1);
                }
                //对视图进行排序，以ID大小进行降序排序
                DataView dv = new DataView(dt2);

                if (dt2 == null)

                {
                    dv.Sort = "id desc";

                    dt2 = dv.ToTable();
                }

                RptPerson.DataSource = dt2;

                RptPerson.DataBind();
            }
            if(pnaddspecialfriend.Visible==true )
            {
                DataBindToRepeater(1);
            }
        }
        else
            Response.Write("<script>alert('尚未登录！');location='Login.aspx'</script>");
    }

    protected void specialfriend_Click(object sender, EventArgs e)
    {
        pnaddspecialfriend.Visible = false;

        pnspecialfriend.Visible = true;
    }

    protected void addspecialfriend_Click(object sender, EventArgs e)
    {
        pnspecialfriend.Visible = false;

        pnaddspecialfriend.Visible = true;
    }
    protected void RptPerson_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drvw = (DataRowView)e.Item.DataItem;

            int id = Convert.ToInt32(drvw["id"]);

            string sql = "select * from Say_comment where id=" + id + "";

            DataTable dt = new DataTable();

            dt = myspecial.select(sql);

            Repeater rept = (Repeater)e.Item.FindControl("RptSay");

            rept.DataSource = dt;

            rept.DataBind();

        }
    }

    protected void RptPerson_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Good")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());

            string sql = "select good from Say where id='" + id + "'";

            DataTable dt = new DataTable();

            dt = myspecial.select(sql);

            int good = Convert.ToInt32(dt.Rows[0][0].ToString());

            good += 1;

            string sql1 = "update Say set good='" + good + "' where id='" + id + "'";

            myspecial.store_change(sql1);

            Response.Write("<script>window.location='Personal_center.aspx'</script>");
        }
        if (e.CommandName == "Reply")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());

            TextBox reply2 = (TextBox)e.Item.FindControl("reply");

            string rp = reply2.Text;

            string sql = "insert into Say_comment values('" + id + "','" + Session["nickname"].ToString() + "','" + Session["name"].ToString() + "','" + rp + "')";

            myspecial.store_change(sql);

            Response.Write("<script>window.location='Personal_center.aspx'</script>");
        }
        if (e.CommandName == "Delete")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());

            string sql = "delete from Say where id='" + id + "'";

            myspecial.store_change(sql);

            Response.Write("<script>window.location='Personal_center.aspx'</script>");
        }
        if (e.CommandName == "Collect")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());

            string sql1 = "select * from Say where id= '" + id + "'";

            DataTable dt = myspecial.select(sql1);

            string sql3 = "select * from Collection_say where say='" + dt.Rows[0][2].ToString() + "' and myusername='" + Session["name"] + "'";

            DataTable dt1 = myspecial.select(sql3);

            if (dt1.Rows.Count == 0)
            {
                string sql2 = "insert into Collection_say values('" + Session["name"].ToString() + "','" + dt.Rows[0][1].ToString() + "','" + dt.Rows[0][2].ToString() + "')";

                myspecial.store_change(sql2);

                Response.Write("<script>alert('收藏成功！');location='Personal_center.aspx'</script>");
            }
            else
                Response.Write("<script>alert('不可重复收藏！')</script>");
        }
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
        string sql = "select * from Friend where myusername='" + Session["name"].ToString() + "' ";

        DataTable dt = new DataTable();

        dt = myspecial.select(sql);

        PagedDataSource pds = new PagedDataSource();

        pds.AllowPaging = true;

        pds.PageSize = 5;

        pds.DataSource = dt.DefaultView;

        lbTotal.Text = pds.PageCount.ToString();

        pds.CurrentPageIndex = currentPage - 1;//当前页数从零开始，故把接受的数减一

        rptfriend.DataSource = pds;

        rptfriend.DataBind();

    }

    protected void rptfriend_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Care")
        {
            string otherusername = e.CommandArgument.ToString();

            string sql0 = "select id from Friend where myusername='" + Session["name"].ToString() + "'and otherusername='" + otherusername + "'";

            DataTable dt0 = myspecial.select(sql0);

            int id = Convert.ToInt32(dt0.Rows[0][0].ToString());

            string sql = "insert into Care values('"+id+"','" + Session["name"].ToString() + "','" + otherusername + "')";

            string sql1 = "select * from Care where username='" + Session["name"].ToString() + "'and otherusername='" + otherusername + "'";

            DataTable dt = myspecial.select(sql1);

            if (dt.Rows.Count == 0)

            {
                myspecial.store_change(sql);

                Response.Write("<script>window.location='Special.aspx'</script>");
            }
            else
                Response.Write("<script>alert('你已经关注他（她）！');location='Special.aspx'</script>");
        }
    }
}