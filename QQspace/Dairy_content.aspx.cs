using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dairy_content : System.Web.UI.Page
{
    Class1 mydairy = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["name"] != null)
        {
            if (!IsPostBack)
            {
                //取出对应ID下日志相关内容，和相关评论内容，将评论绑定到repeater上
                int id = Convert.ToInt32(Session["dairy_id"].ToString());

                string sql = "select * from Dairy where id='" + id + "'";

                string sql1 = "select * from Dairy_comment where id='" + id + "'";

                DataTable dt = mydairy.select(sql);

                DataTable dt1 = mydairy.select(sql1);

                if (dt.Rows.Count != 0)
                {
                    dairytitle.Text = dt.Rows[0][2].ToString();

                    dairycontent.Text = dt.Rows[0][3].ToString();

                    lbgood.Text = dt.Rows[0][4].ToString();

                    dairywriter.Text = dt.Rows[0][5].ToString();

                    dairyusername.Text = dt.Rows[0][1].ToString();

                    Rptdairycomment.DataSource = dt1;

                    Rptdairycomment.DataBind();

                    txttitle.Text = dt.Rows[0][2].ToString();

                    txtdairycontent.Text = dt.Rows[0][3].ToString();
                }
            }
        }
        else
            Response.Write("<script>alert('尚未登录！');location='Login.aspx'</script>");
    }

    protected void good_Click(object sender, EventArgs e)
    {
        //取出对应ID下点赞次数，加一后返回表中
        string sql = "select good from Dairy where id='" + Session["dairy_id"] + "'";

        DataTable dt = new DataTable();

        dt = mydairy.select(sql);

        int good = Convert.ToInt32(dt.Rows[0][0].ToString());

        good += 1;

        string sql1 = "update Dairy set good='" + good + "' where id='" + Session["dairy_id"] + "'";

        mydairy.store_change(sql1);

        Response.Write("<script>window.location='Dairy_content.aspx'</script>");
    }

    protected void dairycomment_Click(object sender, EventArgs e)
    {
        pndairycomment.Visible = true;
    }

    protected void dairyreprint_Click(object sender, EventArgs e)
    {
        string sql1 = "select * from Dairy where id= '" + Session["dairy_id"] + "'";

        DataTable dt = mydairy.select(sql1);

        string sql3 = "select * from Dairy where dairy='" + dt.Rows[0][3].ToString() + "' and username='" + Session["name"] + "'";

        DataTable dt1 = mydairy.select(sql3);

        if (dt1.Rows.Count == 0)
        {
            string sql2 = "insert into Dairy values('" + Session["name"].ToString() + "','" + dt.Rows[0][2].ToString() + "','" + dt.Rows[0][3].ToString() + "',0,'" + Session["nickname"].ToString() + "','" + dt.Rows[0][6].ToString() + "')";

            mydairy.store_change(sql2);

            Response.Write("<script>window.location='Dairy_content.aspx'</script>");
        }
        else
            Response.Write("<script>alert('不可转载自己的日志！')</script>");
    }

    protected void dairyeditor_Click(object sender, EventArgs e)
    {
        pndairycontent.Visible = false;

        pndairyeditor.Visible = true;
    }

    protected void btndairycomment_Click(object sender, EventArgs e)
    {
        string comment = this.comment.Text;

        string sql = "insert into Dairy_comment values('" + Session["dairy_id"].ToString() + "','" + Session["nickname"].ToString() + "','" + Session["name"].ToString() + "','" + comment + "')";

        mydairy.store_change(sql);

        Response.Write("<script>alert('发表成功！');location='Dairy_content.aspx'</script>");
    }

    protected void Rptdairycomment_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }

    protected void btndairyeditor_Click(object sender, EventArgs e)
    {
        string title = txttitle.Text;

        string content = txtdairycontent.Text;

        string sql = "update Dairy set title='" + title + "',dairy='" + content + "' where id='" + Session["dairy_id"] + "'";

        if (title != "" && content != "")
        {

            mydairy.store_change(sql);

            Response.Write("<script>alert('编辑成功！');location='Dairy_content.aspx'</script>");
        }
        else
            Response.Write("<script>alert('内容不能为空！');location='Dairy_content.aspx'</script>");
    }

    protected void btndairyback_Click(object sender, EventArgs e)
    {
        pndairycontent.Visible = true;

        pndairyeditor.Visible = false;
    }

    protected void dairycollect_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(Session["dairy_id"].ToString());

        string sql = "select * from Dairy where id='" + id + "'";

        DataTable dt = mydairy.select(sql);

        string title = dt.Rows[0][2].ToString();

        string dairy = dt.Rows[0][3].ToString();

        string username = dt.Rows[0][1].ToString();

        string sql1 = "insert into Collection_dairy values('" + Session["name"].ToString() + "','" + username + "','" + dairy + "','" + title + "')";

        string sql2 = "select * from Collection_dairy where myusername='" + Session["name"].ToString() + "' and dairy='" + dairy + "'";

        DataTable dt1 = mydairy.select (sql2);

        if (dt1.Rows.Count == 0)

        {
            mydairy.store_change(sql1);

            Response.Write("<script>alert('收藏成功！');location='Dairy_content.aspx'</script>");
        }
        else
            Response.Write("<script>alert('不可重复收藏！');location='Dairy_content.aspx'</script>");

    }
}