using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Collection : System.Web.UI.Page
{
    Class1 mycollection = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["name"] != null)
        {//读取相关数据绑定到repeater
            string sql = "select * from Collection_dynamic where myusername='" + Session["name"].ToString() + "'";

            string sql1 = "select * from Collection_dairy where myusername='" + Session["name"].ToString() + "'";

            DataTable dt = mycollection.select(sql);

            DataTable dt1 = mycollection.select(sql1);

            say_dairy1.DataSource = dt;

            say_dairy1.DataBind();

            say_dairy2.DataSource = dt1;

            say_dairy2.DataBind();
        }
        else
            Response.Write("<script>alert('尚未登录！');location='Login.aspx'</script>");
    }

    protected void dairy_Click(object sender, EventArgs e)
    {
        pnsay.Visible = false;

        pndairy.Visible = true;
    }

    protected void say_Click(object sender, EventArgs e)
    {
        pnsay.Visible = true;

        pndairy.Visible = false;
    }

    protected void say_dairy1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if(e.CommandName=="delete")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());

            string sql = "delete from Collection_dynamic where id='" + id + "'";

            mycollection.store_change(sql);

            Response.Write("<script>window.location='Collection.aspx'</script>");
        }
    }
    protected void say_dairy2_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delete")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());

            string sql = "delete from Collection_dairy where id='" + id + "'";

            mycollection.store_change(sql);

            Response.Write("<script>window.location='Collection.aspx'</script>");
        }
    }

    protected void say_dairy1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drvw = (DataRowView)e.Item.DataItem;

            int id = Convert.ToInt32(drvw["id"]);

            //找出是否photo列有值，如果为空就隐藏图片控件
            string sql1 = "select * from Collection_dynamic where id='" + id + "'";

            DataTable dt1 = new DataTable();

            dt1 = mycollection.select(sql1);

            if (dt1.Rows[0][7].ToString() == "")
            {
                Image photo = (Image)e.Item.FindControl("photo");

                photo.Visible = false;
            }
        }
    }
}