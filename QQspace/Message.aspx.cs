using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Message2 : System.Web.UI.Page
{
    Class1 mymessage = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["name"] != null)
        {
            if (!IsPostBack)
            {
                string sql3 = "select * from Message where username='" + Session["name"].ToString() + "' order by id desc";

                DataTable dt2 = mymessage.select(sql3);

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

            string sql = "select * from Message_comment where id=" + id + "";

            DataTable dt = new DataTable();

            dt = mymessage.select(sql);

            Repeater rept = (Repeater)e.Item.FindControl("RptSay");

            rept.DataSource = dt;

            rept.DataBind();

        }
    }

    protected void printsay_Click(object sender, EventArgs e)
    {
        string message = saysay.Text;

        string sql = "insert into Message values('" + Session["name"].ToString() + "','" + message + "','" + Session["nickname"] + "')";

        if (message != "")
        {

            mymessage.store_change(sql);

            Response.Write("<script>alert('发表成功！');location='Message.aspx'</script>");
        }
        else
            Response.Write("<script>alert('内容不能为空！');location='Message.aspx'</script>");
    }

    protected void RptPerson_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        
        if (e.CommandName == "Reply")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());

            TextBox reply2 = (TextBox)e.Item.FindControl("reply");

            string rp = reply2.Text;

            string sql = "insert into Message_comment values('" + id + "','" + Session["nickname"].ToString() + "','" + Session["name"].ToString() + "','" + rp + "')";

            mymessage.store_change(sql);

            Response.Write("<script>window.location='Message.aspx'</script>");
        }
        if (e.CommandName == "Delete")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());

            string sql = "delete from Message where id='" + id + "'";

            mymessage.store_change(sql);

            Response.Write("<script>window.location='Message.aspx'</script>");
        }
       
    }
}