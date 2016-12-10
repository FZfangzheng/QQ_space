using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dairy2 : System.Web.UI.Page
{
        Class1 mydairy = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["name"] != null)
        {
            if (Session["tourist"] != null)
            {
                write_dairy.Visible = false;

               // LinkButton link = e.id=("delete") as LinkButton;

                //link.Visible = false;

            }
            if (!IsPostBack)
            {
                DataBindToRepeater(1);
            }
        }
        else
            Response.Write("<script>alert('尚未登录！');location='Login.aspx'</script>");
    }

    protected void Rptdairy_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

        if (e.CommandName=="Dairy")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());

            Session["dairy_id"] = id;

            Response.Write("<script>window.location='Dairy_content.aspx'</script>");
        }
        if(e.CommandName=="Delete")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());

            string sql = "delete from Dairy where id='" + id + "'";

            mydairy.store_change(sql);

            Response.Write("<script>alert('删除成功！');location='Dairy.aspx'</script>");
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
        string sql = "";

        if (Session["tourist"] != null)
        {
            sql = "select * from Dairy where username='" + Session["tourist"].ToString() + "' order by id desc";
        }
        else

        {
            sql = "select * from Dairy where username='" + Session["name"].ToString() + "' order by id desc";
        }

        DataTable dt = new DataTable();

        dt = mydairy.select(sql);

        PagedDataSource pds = new PagedDataSource();

        pds.AllowPaging = true;

        pds.PageSize = 5;

        pds.DataSource = dt.DefaultView;

        lbTotal.Text = pds.PageCount.ToString();

        pds.CurrentPageIndex = currentPage - 1;//当前页数从零开始，故把接受的数减一

        Rptdairy.DataSource = pds;

        Rptdairy.DataBind();

    }

    protected void Rptdairy_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            //隐藏访问空间时的删除按钮
            if (Session["tourist"] != null)
            {
                LinkButton delete2 = (LinkButton)e.Item.FindControl("delete");

                delete2.Visible = false;
            }
        }
    }
}