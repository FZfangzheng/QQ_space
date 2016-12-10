using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Say2 : System.Web.UI.Page
{
    Class1 mysay = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["name"] != null)
        {
        
            if (!IsPostBack)
            {
                string sql3 = "";
                if (Session["tourist"] != null)
                {
                    label.Text = "他的说说";

                    saysay.Visible = false;

                    printsay.Visible = false;

                    sql3 = "select * from Dynamic where username='" + Session["tourist"].ToString() + "' and class='say' order by id desc";
                }
                else
                {
                    label.Text = "我的说说";

                    sql3 = "select * from Dynamic where username='" + Session["name"].ToString() + "' and class='say' order by id desc";
                }

                

                DataTable dt2 = mysay.select(sql3);

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

            string sql = "select * from Dynamic_comment where id=" + id + "";

            DataTable dt = new DataTable();

            dt = mysay.select(sql);

            Repeater rept = (Repeater)e.Item.FindControl("RptSay");

            rept.DataSource = dt;

            rept.DataBind();

        }
    }

    protected void printsay_Click(object sender, EventArgs e)
    {
        string say = saysay.Text;

        string sql0 = "select photo from Login where username='" + Session["name"].ToString() + "'";

        DataTable dt = mysay.select(sql0);

        string sql = "insert into Dynamic values('" + Session["name"].ToString() + "','" + Session["nickname"] + "','" + dt.Rows[0][0].ToString() + "','" + say + "','','','','','say',0,'" + DateTime.Now.ToString() + "',',','','','')";

        if (say != "")
        {

            mysay.store_change(sql);

            mysay.rank(Session["name"].ToString(), 2);

            Response.Write("<script>alert('发表成功！');location='Say.aspx'</script>");
        }
        else
            Response.Write("<script>alert('内容不能为空！');location='Say.aspx'</script>");
    }

    protected void RptPerson_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Good")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());

            string sql = "select good from Dynamic where id='" + id + "'";

            DataTable dt = new DataTable();

            dt = mysay.select(sql);

            int good = Convert.ToInt32(dt.Rows[0][0].ToString());

            good += 1;

            string sql1 = "update Dynamic set good='" + good + "' where id='" + id + "'";

          

            //判断是否点过赞

            string sql2 = "select state from Dynamic where id='" + id + "'";

            DataTable dt2 = mysay.select(sql2);

            string str = dt2.Rows[0][0].ToString();

            string str1 = Session["name"].ToString();

            if (str.Contains(str1))
            {
                Response.Write("<script>alert('你已经点过赞了！')</script>");
            }
            else
            {
                mysay.store_change(sql1);

                str = str + str1 + ',';

                string sql3 = "update Dynamic set state='" + str + "' where id='" + id + "'";

                mysay.store_change(sql3);
            }

            Response.Write("<script>window.location='Say.aspx'</script>");
        }
        if (e.CommandName == "Reply")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());

            TextBox reply2 = (TextBox)e.Item.FindControl("reply");

            string rp = reply2.Text;

            string sql = "insert into Dynamic_comment values('" + id + "','" + Session["name"].ToString() + "','" + Session["nickname"].ToString() + "','" + rp + "','say_comment','" + DateTime.Now.ToString() + "')";

            mysay.store_change(sql);

            mysay.rank(Session["name"].ToString(), 1);

            Response.Write("<script>window.location='Say.aspx'</script>");
        }
        if (e.CommandName == "Delete")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());

            string sql = "delete from Dynamic where id='" + id + "'";

            mysay.store_change(sql);

            Response.Write("<script>window.location='Say.aspx'</script>");
        }
        if (e.CommandName == "Collect")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());

            string sql1 = "select * from Dynamic where id= '" + id + "'";

            DataTable dt = mysay.select(sql1);

            string sql3 = "select * from Collection_say where say='" + dt.Rows[0][2].ToString() + "' and myusername='" + Session["name"] + "'";

            DataTable dt1 = mysay.select(sql3);

            if (dt1.Rows.Count == 0)
            {
                string sql2 = "insert into Collection_say values('" + Session["name"].ToString() + "','" + dt.Rows[0][1].ToString() + "','" + dt.Rows[0][4].ToString() + "')";

                mysay.store_change(sql2);

                Response.Write("<script>alert('收藏成功！');location='Say.aspx'</script>");
            }
            else
                Response.Write("<script>alert('不可重复收藏！')</script>");
        }
        if (e.CommandName == "Friendspace")
        {
            Session["tourist"] = e.CommandArgument.ToString();

            string sql = "select nickname from Login where username='" + Session["tourist"].ToString() + "'";

            DataTable dt = mysay.select(sql);

            Session["touristnickname"] = dt.Rows[0][0].ToString();

            string sql1 = "select * from Authority where username='" + Session["tourist"].ToString() + "' and otherusername='" + Session["name"].ToString() + "'";

            DataTable dt1 = mysay.select(sql1);

            if (dt1.Rows.Count == 0)
            {
                //访问陌生人权限查看
                string sql4 = "select authority from Login where username='" + Session["tourist"].ToString() + "'";

                DataTable dt4 = mysay.select(sql4);
                //等于零允许陌生人访问
                if (Convert.ToInt32(dt4.Rows[0][0].ToString()) == 0)
                {
                    if (Session["name"].ToString() != Session["tourist"].ToString())
                    {
                        string sql2 = "select * from Login where username='" + Session["name"].ToString() + "'";

                        DataTable dt2 = mysay.select(sql2);

                        string visitway = dt2.Rows[0][9].ToString() + "访问你的空间";

                        string sql3 = "insert into Tourist values('" + Session["tourist"].ToString() + "','" + dt2.Rows[0][8].ToString() + "','" + Session["name"].ToString() + "','" + Session["nickname"].ToString() + "','" + visitway + "','" + DateTime.Now.ToString() + "')";

                        mysay.store_change(sql3);
                    }
                    Response.Write("<script>window.location='Homepage.aspx'</script>");
                }
                //否则判断是否为好友或者自己
                else
                {
                    string sql5 = "select * from Friend where myusername='" + Session["tourist"].ToString() + "' and otherusername='" + Session["name"].ToString() + "'";

                    DataTable dt5 = mysay.select(sql5);

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

                        DataTable dt6 = mysay.select(sql6);

                        string visitway = dt6.Rows[0][9].ToString() + "访问你的空间";

                        string sql7 = "insert into Tourist values('" + Session["tourist"].ToString() + "','" + dt6.Rows[0][8].ToString() + "','" + Session["name"].ToString() + "','" + Session["nickname"].ToString() + "','" + visitway + "','" + DateTime.Now.ToString() + "')";

                        mysay.store_change(sql7);

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

            DataTable dt = mysay.select(sql);

            Session["touristnickname"] = dt.Rows[0][0].ToString();

            string sql1 = "select * from Authority where username='" + Session["tourist"].ToString() + "' and otherusername='" + Session["name"].ToString() + "'";

            DataTable dt1 = mysay.select(sql1);

            if (dt1.Rows.Count == 0)
            {
                //访问陌生人权限查看
                string sql4 = "select authority from Login where username='" + Session["tourist"].ToString() + "'";

                DataTable dt4 = mysay.select(sql4);
                //等于零允许陌生人访问
                if (Convert.ToInt32(dt4.Rows[0][0].ToString()) == 0)
                {
                    if (Session["name"].ToString() != Session["tourist"].ToString())
                    {
                        string sql2 = "select * from Login where username='" + Session["name"].ToString() + "'";

                        DataTable dt2 = mysay.select(sql2);

                        string visitway = dt2.Rows[0][9].ToString() + "访问你的空间";

                        string sql3 = "insert into Tourist values('" + Session["tourist"].ToString() + "','" + dt2.Rows[0][8].ToString() + "','" + Session["name"].ToString() + "','" + Session["nickname"].ToString() + "','" + visitway + "','" + DateTime.Now.ToString() + "')";

                        mysay.store_change(sql3);
                    }
                    Response.Write("<script>window.location='Homepage.aspx'</script>");
                }
                //否则判断是否为好友或者自己
                else
                {
                    string sql5 = "select * from Friend where myusername='" + Session["tourist"].ToString() + "' and otherusername='" + Session["name"].ToString() + "'";

                    DataTable dt5 = mysay.select(sql5);

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

                        DataTable dt6 = mysay.select(sql6);

                        string visitway = dt6.Rows[0][9].ToString() + "访问你的空间";

                        string sql7 = "insert into Tourist values('" + Session["tourist"].ToString() + "','" + dt6.Rows[0][8].ToString() + "','" + Session["name"].ToString() + "','" + Session["nickname"].ToString() + "','" + visitway + "','" + DateTime.Now.ToString() + "')";

                        mysay.store_change(sql7);

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