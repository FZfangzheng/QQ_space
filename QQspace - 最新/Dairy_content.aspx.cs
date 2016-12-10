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
            if (Session["dairy_id"] != null)
            {
                if (!IsPostBack)
                {
                    if (Session["tourist"] != null)
                    {
                        dairyeditor.Visible = false;
                    }
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

                        myEditor.InnerHtml = dt.Rows[0][3].ToString();
                    }
                }
            }
            else
                Response.Write("<script>window.location='Dairy.aspx'</script>");
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
        //判断是否重复点赞
        string sql2 = "select state from Dairy where id='" + Session["dairy_id"] + "'";

        DataTable dt2 = mydairy.select(sql2);

        string str = dt2.Rows[0][0].ToString();

        string str1 = Session["name"].ToString();

        if (str.Contains(","+str1+","))
        {
            Response.Write("<script>alert('你已经点过赞了！')</script>");
        }
        else
        {
            mydairy.store_change(sql1);

             str=str+str1+',';

            string sql3 = "update Dairy set state='" + str + "' where id='" + Session["dairy_id"] + "'";

            mydairy.store_change(sql3);
        }
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

        mydairy.rank(Session["name"].ToString(), 1);

        Response.Write("<script>alert('发表成功！');location='Dairy_content.aspx'</script>");
    }

    protected void Rptdairycomment_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Friendspace")
        {
            Session["tourist"] = e.CommandArgument.ToString();

            string sql = "select nickname from Login where username='" + Session["tourist"].ToString() + "'";

            DataTable dt = mydairy.select(sql);

            Session["touristnickname"] = dt.Rows[0][0].ToString();

            string sql1 = "select * from Authority where username='" + Session["tourist"].ToString() + "' and otherusername='" + Session["name"].ToString() + "'";

            DataTable dt1 = mydairy.select(sql1);

            if (dt1.Rows.Count == 0)
            {
                //访问陌生人权限查看
                string sql4 = "select authority from Login where username='" + Session["tourist"].ToString() + "'";

                DataTable dt4 = mydairy.select(sql4);
                //等于零允许陌生人访问
                if (Convert.ToInt32(dt4.Rows[0][0].ToString()) == 0)
                {
                    if (Session["name"].ToString() != Session["tourist"].ToString())
                    {
                        string sql2 = "select * from Login where username='" + Session["name"].ToString() + "'";

                        DataTable dt2 = mydairy.select(sql2);

                        string visitway = dt2.Rows[0][9].ToString() + "访问你的空间";

                        string sql3 = "insert into Tourist values('" + Session["tourist"].ToString() + "','" + dt2.Rows[0][8].ToString() + "','" + Session["name"].ToString() + "','" + Session["nickname"].ToString() + "','" + visitway + "','" + DateTime.Now.ToString() + "')";

                        mydairy.store_change(sql3);
                    }
                    Response.Write("<script>window.location='Homepage.aspx'</script>");
                }
                //否则判断是否为好友或者自己
                else
                {
                    string sql5 = "select * from Friend where myusername='" + Session["tourist"].ToString() + "' and otherusername='" + Session["name"].ToString() + "'";

                    DataTable dt5 = mydairy.select(sql5);

                    if (dt5.Rows.Count == 0)
                    {
                        //是不是自己
                        if (Session["name"].ToString() == Session["tourist"].ToString())
                        {
                            Response.Write("<script>window.location='Homepage.aspx'</script>");
                        }
                        else
                        {
                            Session["tourist"] = null;

                            Session["touristnickname"] = null;

                            Response.Write("<script>alert('你没有访问权限！')</script>");
                        }
                    }
                    else
                    {
                        string sql6 = "select * from Login where username='" + Session["name"].ToString() + "'";

                        DataTable dt6 = mydairy.select(sql6);

                        string visitway = dt6.Rows[0][9].ToString() + "访问你的空间";

                        string sql7 = "insert into Tourist values('" + Session["tourist"].ToString() + "','" + dt6.Rows[0][8].ToString() + "','" + Session["name"].ToString() + "','" + Session["nickname"].ToString() + "','" + visitway + "','" + DateTime.Now.ToString() + "')";

                        mydairy.store_change(sql7);

                        Response.Write("<script>window.location='Homepage.aspx'</script>");
                    }
                }
            }
            else
            {
                Session["tourist"] = null;

                Session["touristnickname"] = null;

                Response.Write("<script>alert('你没有访问权限！')</script>");
            }

        }
    }

    protected void btndairyeditor_Click(object sender, EventArgs e)
    {
        string title = txttitle.Text;

        string content = Server.HtmlDecode(myEditor.InnerHtml);

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