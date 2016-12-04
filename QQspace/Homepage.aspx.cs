using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Homepage2 : System.Web.UI.Page
{
    Class1 myhome = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["name"] != null)
        {
            string sql1 = "select * from Photo where username='" + Session["name"].ToString() + "'";

            string sql2 = "select * from Say where username= '" + Session["name"].ToString() + "' order by id desc";

            string sql3 = "select * from Dairy where username='" + Session["name"].ToString() + "'";

            string sql4 = "select * from Login where username='" + Session["name"].ToString() + "'";

            DataTable dt1 = myhome.select(sql1);

            DataTable dt2 = myhome.select(sql2);

            DataTable dt3 = myhome.select(sql3);

            DataTable dt4 = myhome.select(sql4);

            numblephoto.Text = dt1.Rows.Count.ToString();

            numblesay.Text = dt2.Rows.Count.ToString();

            numbledairy.Text = dt3.Rows.Count.ToString();

            lbage.Text = dt4.Rows[0][5].ToString();

            lbsex.Text = dt4.Rows[0][4].ToString();

            lblocation.Text = dt4.Rows[0][6].ToString();

            if (!IsPostBack)
            {
                RptPerson.DataSource = dt2;

                RptPerson.DataBind();
            }
        }
        else
            Response.Write("<script>alert('尚未登录！');location='Login.aspx'</script>");
    }
    protected void RptPerson_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drvw = (DataRowView)e.Item.DataItem;

            int id = Convert.ToInt32(drvw["id"]);

            string sql = "select * from Say_comment where id=" + id + "";

            DataTable dt = new DataTable();

            dt = myhome.select(sql);

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

            dt = myhome.select(sql);

            int good = Convert.ToInt32(dt.Rows[0][0].ToString());

            good += 1;

            string sql1 = "update Say set good='" + good + "' where id='" + id + "'";

            myhome.store_change(sql1);

            Response.Write("<script>window.location='Personal_center.aspx'</script>");
        }
        if (e.CommandName == "Reply")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());

            TextBox reply2 = (TextBox)e.Item.FindControl("reply");

            string rp = reply2.Text;

            string sql = "insert into Say_comment values('" + id + "','" + Session["nickname"].ToString() + "','" + Session["name"].ToString() + "','" + rp + "')";

            myhome.store_change(sql);

            Response.Write("<script>window.location='Personal_center.aspx'</script>");
        }
        if (e.CommandName == "Delete")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());

            string sql = "delete from Say where id='" + id + "'";

            myhome.store_change(sql);

            Response.Write("<script>window.location='Personal_center.aspx'</script>");
        }
        if (e.CommandName == "Collect")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());

            string sql1 = "select * from Say where id= '" + id + "'";

            DataTable dt = myhome.select(sql1);

            string sql3 = "select * from Collection_say where say='" + dt.Rows[0][2].ToString() + "' and myusername='" + Session["name"] + "'";

            DataTable dt1 = myhome.select(sql3);

            if (dt1.Rows.Count == 0)
            {
                string sql2 = "insert into Collection_say values('" + Session["name"].ToString() + "','" + dt.Rows[0][1].ToString() + "','" + dt.Rows[0][2].ToString() + "')";

                myhome.store_change(sql2);

                Response.Write("<script>alert('收藏成功！');location='Personal_center.aspx'</script>");
            }
            else
                Response.Write("<script>alert('不可重复收藏！')</script>");
        }
    }
}