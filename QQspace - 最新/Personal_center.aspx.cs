using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default3 : System.Web.UI.Page
{
    Class1 mycenter = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["name"] != null)
        {
            if (!IsPostBack)
            {
                string sql3 = "";

                string sql2 = "";

                if (Session["tourist"] != null)
                {
                    saysay.Visible = false;

                    printsay.Visible = false;

                    sql3 = "select * from Dynamic where username='" + Session["tourist"].ToString() + "'";

                    sql2 = "select otherusername from  Friend where myusername='" + Session["tourist"].ToString() + "'";
                }
                else
                {
                    sql3 = "select * from Dynamic where username='" + Session["name"].ToString() + "'";

                    sql2 = "select otherusername from  Friend where myusername='" + Session["name"].ToString() + "'";
                }

                DataTable dt2 = mycenter.select(sql3);

                DataTable dt1 = new DataTable();

                DataTable dt3 = new DataTable();

                int count, numble;

                dt3 = mycenter.select(sql2);

                count = dt3.Rows.Count;

                string sql1 = "";

                for (numble = 0; numble < count; numble++)

                {
                    sql1 = "select * from Dynamic where username='" + dt3.Rows[numble][0] + "'";

                    dt1 = mycenter.select(sql1);

                    dt2.Merge(dt1);
                }
                //对视图进行排序，以ID大小进行降序排序
                DataView dv = new DataView(dt2);

                dv.Sort = "id desc";

                dt2 = dv.ToTable();

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

            string sql = "select * from Dynamic_comment where id='" + id + "'";

            DataTable dt = new DataTable();

            dt = mycenter.select(sql);
            //找出是否photo列有值，如果为空就隐藏图片控件
            string sql1 = "select * from Dynamic where id='" + id + "'";

            DataTable dt1 = new DataTable();

            dt1 = mycenter.select(sql1);

            if (dt1.Rows[0][8].ToString()=="")
            {
                Image photo = (Image)e.Item.FindControl("photo");

                photo.Visible = false;
            }

            Repeater rept = (Repeater)e.Item.FindControl("RptSay");

            rept.DataSource = dt;

            rept.DataBind();

        }
    }

    protected void printsay_Click(object sender, EventArgs e)
    {
        string say = saysay.Text;

        string sql0 = "select photo from Login where username='" + Session["name"].ToString() + "'";

        DataTable dt = mycenter.select(sql0);

        string sql = "insert into Dynamic values('" + Session["name"].ToString() + "','" + Session["nickname"] + "','" + dt.Rows[0][0].ToString() + "','" + say + "','','','','','say',0,'" + DateTime.Now.ToString() + "',',','','','')";
     
     
        //计算等级分


        if (say != "")
        {
             mycenter.store_change(sql);
          
            mycenter.rank(Session["name"].ToString(), 2);


            Response.Write("<script>alert('发表成功！');location='Personal_center.aspx'</script>");
        }
        else
            Response.Write("<script>alert('内容不能为空！');location='Personal_center.aspx'</script>");
    }


    protected void RptPerson_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if(e.CommandName=="Good")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());

            string sql = "select good from Dynamic where id='" + id + "'";

            DataTable dt = new DataTable();

            dt = mycenter.select(sql);

            int good = Convert.ToInt32(dt.Rows[0][0].ToString());

            good += 1;

            string sql1 = "update Dynamic set good='" + good + "' where id='" + id + "'";

            //判断是否点过赞

            string sql2 = "select state from Dynamic where id='" + id + "'";

            DataTable dt2 = mycenter.select(sql2);

            string str = dt2.Rows[0][0].ToString();

            string str1 = Session["name"].ToString();

            if (str.Contains("," + str1 + ","))
            {
                Response.Write("<script>alert('你已经点过赞了！')</script>");
            }
            else
            {
                mycenter.store_change(sql1);

                str = str + str1 + ',';

                string sql3 = "update Dynamic set state='" + str + "' where id='" + id + "'";

                mycenter.store_change(sql3);
            }

            Response.Write("<script>window.location='Personal_center.aspx'</script>");
        }
        if(e.CommandName=="Reply")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());

            TextBox reply2 = (TextBox)e.Item.FindControl("reply");

            string rp = reply2.Text;

            string sql = "insert into Dynamic_comment values('" + id + "','" + Session["name"].ToString() + "','" + Session["nickname"].ToString() + "','" + rp + "','say_comment','" + DateTime.Now.ToString() + "')";
           
            mycenter.store_change(sql);
          
            mycenter.rank(Session["name"].ToString(), 1);
          
            Response.Write("<script>window.location='Personal_center.aspx'</script>");
        }
        if(e.CommandName=="Reprint")
        {
            string sql0 = "select photo from Login where username='" + Session["name"].ToString() + "'";

            DataTable dt3 = mycenter.select(sql0);

            int id = Convert.ToInt32(e.CommandArgument.ToString());

            string sql1 = "select * from Dynamic where id= '" + id + "'";

            DataTable dt = mycenter.select(sql1);

            string sql3 = "select * from Dynamic where id='" + id + "' and username='" + Session["name"] + "'";

            DataTable dt1 = mycenter.select(sql3);

            string reprint = Session["nickname"].ToString() + "转载" + dt.Rows[0][2].ToString();

            if (dt1.Rows.Count == 0)
            {
                string sql2 = "insert into Dynamic values('" + Session["name"].ToString() + "','"+ Session["nickname"].ToString() + "','" + dt3.Rows[0][0].ToString() + "','" + dt.Rows[0][4].ToString() + "','"+dt.Rows[0][5].ToString()+"','"+dt.Rows[0][6].ToString()+"','"+dt.Rows[0][7].ToString()+"','"+dt.Rows[0][8].ToString()+"','say',0,'"+DateTime.Now.ToString()+"',',','"+dt.Rows[0][13].ToString()+"','"+dt.Rows[0][14].ToString()+"','"+reprint+"')";

                mycenter.store_change(sql2);

                Response.Write("<script>window.location='Personal_center.aspx'</script>");
            }
            else
                Response.Write("<script>alert('不可转载自己的动态！')</script>");
        }
        if(e.CommandName=="Collect")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());

            string sql1 = "select * from Dynamic where id= '" + id + "'";

            DataTable dt = mycenter.select(sql1);

            string sql3 = "select * from Collection_dynamic where say='" + dt.Rows[0][4].ToString() + "' and myusername='" + Session["name"] + "' and title='" + dt.Rows[0][5].ToString() + "'and dairy='" + dt.Rows[0][6].ToString() + "'and album='" + dt.Rows[0][7].ToString() + "' and photo='" + dt.Rows[0][8].ToString() + "'and printdairy='" + dt.Rows[0][13].ToString() + "' and printphoto='" + dt.Rows[0][14].ToString() + "'";

            DataTable dt1 = mycenter.select(sql3);
            //之前没收藏过
            if (dt1.Rows.Count == 0)
            {
                string sql2 = "insert into Collection_dynamic values('" + Session["name"].ToString() + "','" + dt.Rows[0][1].ToString() + "','" + dt.Rows[0][4].ToString() + "','" + dt.Rows[0][5].ToString() + "','" + dt.Rows[0][6].ToString() + "','" + dt.Rows[0][7].ToString() + "','" + dt.Rows[0][8].ToString() + "','" + dt.Rows[0][13].ToString() + "','" + dt.Rows[0][14].ToString() + "')";

                mycenter.store_change(sql2);

                Response.Write("<script>alert('收藏成功！');location='Homepage.aspx'</script>");

            }
            else
                Response.Write("<script>alert('不可重复收藏！')</script>");
        }
        if (e.CommandName == "Friendspace")
        {
            Session["tourist"] = e.CommandArgument.ToString();

            string sql = "select nickname from Login where username='" + Session["tourist"].ToString() + "'";

            DataTable dt = mycenter.select(sql);

            Session["touristnickname"] = dt.Rows[0][0].ToString();

            string sql1 = "select * from Authority where username='" + Session["tourist"].ToString() + "' and otherusername='" + Session["name"].ToString() + "'";

            int a = mycenter.visit(sql1, Session["tourist"].ToString(), Session["touristnickname"].ToString(), Session["name"].ToString(), Session["nickname"].ToString());
            if (a == 1)
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
        if (e.CommandName=="Album")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());

            string sql = "select * from Dynamic where id='" + id + "'";

            DataTable dt = mycenter.select(sql);

            Session["photoname"] = dt.Rows[0][7].ToString();
            //查看是否是转载的
           //是则替换dt
            if (dt.Rows[0][15].ToString() != "")
            {
                string sql4 = "select * from Dynamic where album='" + Session["photoname"].ToString() + "' and reprint=''";

                dt = mycenter.select(sql4);
            }
               
                
                //不是则进行一下步骤
                if (dt.Rows[0][1].ToString() == Session["name"].ToString())
                {
                    Response.Write("<script>window.location='Photo_list.aspx'</script>");
                }
                else
                {
                    string sql1 = "select * from Authority where username='" + dt.Rows[0][1].ToString() + "' and otherusername='" + Session["name"].ToString() + "'";

                    DataTable dt1 = mycenter.select(sql1);

                    if (dt1.Rows.Count == 0)

                    {
                        Session["tourist"] = dt.Rows[0][1].ToString();

                        Session["touristnickname"] = dt.Rows[0][2].ToString();


                        string sql2 = "select * from Login where username='" + Session["name"].ToString() + "'";

                        DataTable dt2 = mycenter.select(sql2);

                        string visitway = dt2.Rows[0][9].ToString() + "访问你的空间查看了相册:" + dt.Rows[0][7].ToString();

                        string sql3 = "insert into Tourist values('" + Session["tourist"].ToString() + "','" + dt2.Rows[0][8].ToString() + "','" + Session["name"].ToString() + "','" + Session["nickname"].ToString() + "','" + visitway + "','" + DateTime.Now.ToString() + "')";

                        mycenter.store_change(sql3);


                        Response.Write("<script>window.location='Photo_list.aspx'</script>");
                    }
                    else
                        Response.Write("<script>alert('你没有访问权限！')</script>");
                }
            }
      
            
            
                


            
        
        if(e.CommandName=="Dairy")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());

            string sql = "select * from Dynamic where id='" + id + "'";

            DataTable dt = mycenter.select(sql);

            string sql1 = "";
            //点击时根据是否是自己的动态来选择

            //查看是否是转载的
            //是则替换dt
            if (dt.Rows[0][15].ToString() != "")
            {
                string sql4 = "select * from Dynamic where title='" + dt.Rows[0][5].ToString() + "' and dairy='" + dt.Rows[0][6].ToString() + "' and reprint=''";

                dt = mycenter.select(sql4);
            }

            if (dt.Rows[0][1].ToString() == Session["name"].ToString())
            {
                sql1 = "select id from Dairy where username='" + Session["name"].ToString() + "' and dairy='" + dt.Rows[0][6].ToString() + "' and title='" + dt.Rows[0][5].ToString() + "'";
            }
            else
            {
                sql1 = "select id from Dairy where username='" + dt.Rows[0][1].ToString() + "'  and dairy='" + dt.Rows[0][6].ToString() + "' and title='" + dt.Rows[0][5].ToString() + "'";

                Session["tourist"] = dt.Rows[0][1].ToString();

                Session["touristnickname"] = dt.Rows[0][2].ToString();
            }
            DataTable dt1 = mycenter.select(sql1);

            Session["dairy_id"] = Convert.ToInt32(dt1.Rows[0][0].ToString());

            string sql0 = "select * from Authority where username='" + dt.Rows[0][1].ToString() + "' and otherusername='" + Session["name"].ToString() + "'";

            DataTable dt0 = mycenter.select(sql0);

            if (dt0.Rows.Count == 0)
            {
                string sql2 = "select * from Login where username='" + Session["name"].ToString() + "'";

                DataTable dt2 = mycenter.select(sql2);

                string visitway = dt2.Rows[0][9].ToString() + "访问你的空间查看了日志:" + dt.Rows[0][5].ToString();

                string sql3 = "insert into Tourist values('" + Session["tourist"].ToString() + "','" + dt2.Rows[0][8].ToString() + "','" + Session["name"].ToString() + "','" + Session["nickname"].ToString() + "','" + visitway + "','" + DateTime.Now.ToString() + "')";

                mycenter.store_change(sql3);

                Response.Write("<script>window.location='Dairy_content.aspx'</script>");
            }
            else
            {
                Session["dairy_id"] = null;

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

            DataTable dt = mycenter.select(sql);

            Session["touristnickname"] = dt.Rows[0][0].ToString();

            string sql1 = "select * from Authority where username='" + Session["tourist"].ToString() + "' and otherusername='" + Session["name"].ToString() + "'";

            int a = mycenter.visit(sql1, Session["tourist"].ToString(), Session["touristnickname"].ToString(), Session["name"].ToString(), Session["nickname"].ToString());
            if (a == 1)
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
    }
}