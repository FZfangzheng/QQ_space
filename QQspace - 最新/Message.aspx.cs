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
                string sql3 = "";
                if (Session["tourist"] != null)
                {
                    sql3 = "select * from Message where username='" + Session["tourist"].ToString() + "' order by id desc";
                }
                else
                {
                    sql3 = "select * from Message where username='" + Session["name"].ToString() + "' order by id desc";
                }
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
            //隐藏访问空间时的删除按钮
            if (Session["tourist"] != null)
            {
                LinkButton delete2 = (LinkButton)e.Item.FindControl("delete");

                delete2.Visible = false;
            }


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
        string sql = "";
        if (Session["tourist"] != null)
        {
            sql = "insert into Message values('" + Session["tourist"].ToString() + "','" + message + "','" + Session["nickname"] + "','" + Session["name"].ToString() + "')";
        }
        else
        {
            sql = "insert into Message values('" + Session["name"].ToString() + "','" + message + "','" + Session["nickname"] + "','" + Session["name"].ToString() + "')";
        }
        if (message != "")
        {

            mymessage.store_change(sql);

            mymessage.rank(Session["name"].ToString(), 2);

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

            mymessage.rank(Session["name"].ToString(), 1);

            Response.Write("<script>window.location='Message.aspx'</script>");
        }
        if (e.CommandName == "Delete")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());

            string sql = "delete from Message where id='" + id + "'";

            mymessage.store_change(sql);

            Response.Write("<script>window.location='Message.aspx'</script>");
        }
        if (e.CommandName == "Friendspace")
        {
            Session["tourist"] = e.CommandArgument.ToString();

            string sql = "select nickname from Login where username='" + Session["tourist"].ToString() + "'";

            DataTable dt = mymessage.select(sql);

            Session["touristnickname"] = dt.Rows[0][0].ToString();

            string sql1 = "select * from Authority where username='" + Session["tourist"].ToString() + "' and otherusername='" + Session["name"].ToString() + "'";

            DataTable dt1 = mymessage.select(sql1);

            if (dt1.Rows.Count == 0)
            {
                //访问陌生人权限查看
                string sql4 = "select authority from Login where username='" + Session["tourist"].ToString() + "'";

                DataTable dt4 = mymessage.select(sql4);
                //等于零允许陌生人访问
                if (Convert.ToInt32(dt4.Rows[0][0].ToString()) == 0)
                {
                    if (Session["name"].ToString() != Session["tourist"].ToString())
                    {
                        string sql2 = "select * from Login where username='" + Session["name"].ToString() + "'";

                        DataTable dt2 = mymessage.select(sql2);

                        string visitway = dt2.Rows[0][9].ToString() + "访问你的空间";

                        string sql3 = "insert into Tourist values('" + Session["tourist"].ToString() + "','" + dt2.Rows[0][8].ToString() + "','" + Session["name"].ToString() + "','" + Session["nickname"].ToString() + "','" + visitway + "','" + DateTime.Now.ToString() + "')";

                        mymessage.store_change(sql3);
                    }
                    Response.Write("<script>window.location='Homepage.aspx'</script>");
                }
                //否则判断是否为好友或者自己
                else
                {
                    string sql5 = "select * from Friend where myusername='" + Session["tourist"].ToString() + "' and otherusername='" + Session["name"].ToString() + "'";

                    DataTable dt5 = mymessage.select(sql5);

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

                        DataTable dt6 = mymessage.select(sql6);

                        string visitway = dt6.Rows[0][9].ToString() + "访问你的空间";

                        string sql7 = "insert into Tourist values('" + Session["tourist"].ToString() + "','" + dt6.Rows[0][8].ToString() + "','" + Session["name"].ToString() + "','" + Session["nickname"].ToString() + "','" + visitway + "','" + DateTime.Now.ToString() + "')";

                        mymessage.store_change(sql7);

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

    protected void RptSay_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Friendspace")
        {
            Session["tourist"] = e.CommandArgument.ToString();

            string sql = "select nickname from Login where username='" + Session["tourist"].ToString() + "'";

            DataTable dt = mymessage.select(sql);

            Session["touristnickname"] = dt.Rows[0][0].ToString();

            string sql1 = "select * from Authority where username='" + Session["tourist"].ToString() + "' and otherusername='" + Session["name"].ToString() + "'";

            DataTable dt1 = mymessage.select(sql1);

            if (dt1.Rows.Count == 0)
            {
                //访问陌生人权限查看
                string sql4 = "select authority from Login where username='" + Session["tourist"].ToString() + "'";

                DataTable dt4 = mymessage.select(sql4);
                //等于零允许陌生人访问
                if (Convert.ToInt32(dt4.Rows[0][0].ToString()) == 0)
                {
                    if (Session["name"].ToString() != Session["tourist"].ToString())
                    {
                        string sql2 = "select * from Login where username='" + Session["name"].ToString() + "'";

                        DataTable dt2 = mymessage.select(sql2);

                        string visitway = dt2.Rows[0][9].ToString() + "访问你的空间";

                        string sql3 = "insert into Tourist values('" + Session["tourist"].ToString() + "','" + dt2.Rows[0][8].ToString() + "','" + Session["name"].ToString() + "','" + Session["nickname"].ToString() + "','" + visitway + "','" + DateTime.Now.ToString() + "')";

                        mymessage.store_change(sql3);
                    }
                    Response.Write("<script>window.location='Homepage.aspx'</script>");
                }
                //否则判断是否为好友或者自己
                else
                {
                    string sql5 = "select * from Friend where myusername='" + Session["tourist"].ToString() + "' and otherusername='" + Session["name"].ToString() + "'";

                    DataTable dt5 = mymessage.select(sql5);

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

                        DataTable dt6 = mymessage.select(sql6);

                        string visitway = dt6.Rows[0][9].ToString() + "访问你的空间";

                        string sql7 = "insert into Tourist values('" + Session["tourist"].ToString() + "','" + dt6.Rows[0][8].ToString() + "','" + Session["name"].ToString() + "','" + Session["nickname"].ToString() + "','" + visitway + "','" + DateTime.Now.ToString() + "')";

                        mymessage.store_change(sql7);

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
}