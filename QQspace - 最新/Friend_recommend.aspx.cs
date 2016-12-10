using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Friend_recommend : System.Web.UI.Page
{
    Class1 myfriend = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["name"] != null)
        {
            DataBindToRepeater(1);
            
        }
        else
            Response.Write("<script>alert('尚未登录！');location='Login.aspx'</script>");
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
        string sql1 = "select * from Hobby where username='" + Session["name"].ToString() + "'";

        DataTable dt1 = myfriend.select(sql1);

        DataTable dt2 = new DataTable();

        string sql3 = "select * from Hobby where username='" + Session["name"].ToString() + "'";

        DataTable dt3 = myfriend.select(sql3);

        int numble;

        string sql2 = "";

        for (numble = 0; numble < dt1.Rows.Count ; numble++)
        {
            sql2 = "select * from Hobby where hobby='" + dt1.Rows[numble][1].ToString() + "' ";

            dt2 = myfriend.select(sql2);

            dt3.Merge(dt2);
        }
        //排序，按username大小进行排序
        DataView dv = new DataView(dt3);

        dv.Sort = "username desc";

        dt3 = dv.ToTable();
        //删除重复行

       int i, j;

        for(i = 0; i < dt3.Rows.Count-1; i++)
        {
            if (dt3.Rows[i][0].ToString() == dt3.Rows[i + 1][0].ToString())
            {
                for (j = i; j < dt3.Rows.Count-1; j++)
                {
                    if (dt3.Rows[i][0].ToString() == dt3.Rows[j + 1][0].ToString())
                    {
                 
                        dt3.Rows.RemoveAt(j + 1);
                        j--;//索引会变，注意减一
                    }
                    else
                    {      
                        break;
                    }
                }
                
            }
        }


        PagedDataSource pds = new PagedDataSource();

        pds.AllowPaging = true;

        pds.PageSize = 5;

        pds.DataSource = dt3.DefaultView;

        lbTotal.Text = pds.PageCount.ToString();

        pds.CurrentPageIndex = currentPage - 1;//当前页数从零开始，故把接受的数减一

        rptfriendcommend.DataSource = pds;

        rptfriendcommend.DataBind();

    }

    protected void rptfriendcommend_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Friendfind")
        {
            string otherusername = e.CommandArgument.ToString();

            string sql1 = "select * from Friend where myusername='" + Session["name"].ToString() + "' and otherusername='" + otherusername + "'";

            string sql2 = "insert into Friend_make values('" + Session["name"].ToString() + "','" + otherusername + "','" + Session["nickname"].ToString() + "')";

            string sql4 = "select * from Friend_make where myusername='" + Session["name"].ToString() + "' and otherusername='" + otherusername + "'";

            DataTable dt1 = myfriend.select(sql1);

            DataTable dt2 = myfriend.select(sql4);

            if (dt1.Rows.Count > 0)
            {
                Response.Write("<script>alert('他已经是你的好友！')</script>");
            }
            else
            {
                if (Session["name"].ToString() == otherusername)
                {
                    Response.Write("<script>alert('不可以添加自己为好友！');location='Friend_find.aspx'</script>");
                }
                else
                {
                    if (dt2.Rows.Count == 0)
                    {
                        myfriend.store_change(sql2);

                        Response.Write("<script>alert('发送好友申请成功！');location='Friend_find.aspx'</script>");
                    }
                    else
                        Response.Write("<script>alert('已经发送申请，请耐心等待对方回应！');location='Friend_find.aspx'</script>");
                }
            }
        }
        if (e.CommandName == "Friendspace")
        {
            Session["tourist"] = e.CommandArgument.ToString();

            string sql = "select nickname from Login where username='" + Session["tourist"].ToString() + "'";

            DataTable dt = myfriend.select(sql);

            Session["touristnickname"] = dt.Rows[0][0].ToString();

            string sql1 = "select * from Authority where username='" + Session["tourist"].ToString() + "' and otherusername='" + Session["name"].ToString() + "'";

            DataTable dt1 = myfriend.select(sql1);

            if (dt1.Rows.Count == 0)
            {
                //访问陌生人权限查看
                string sql4 = "select authority from Login where username='" + Session["tourist"].ToString() + "'";

                DataTable dt4 = myfriend.select(sql4);
                //等于零允许陌生人访问
                if (Convert.ToInt32(dt4.Rows[0][0].ToString()) == 0)
                {
                    if (Session["name"].ToString() != Session["tourist"].ToString())
                    {
                        string sql2 = "select * from Login where username='" + Session["name"].ToString() + "'";

                        DataTable dt2 = myfriend.select(sql2);

                        string visitway = dt2.Rows[0][9].ToString() + "访问你的空间";

                        string sql3 = "insert into Tourist values('" + Session["tourist"].ToString() + "','" + dt2.Rows[0][8].ToString() + "','" + Session["name"].ToString() + "','" + Session["nickname"].ToString() + "','" + visitway + "','" + DateTime.Now.ToString() + "')";

                        myfriend.store_change(sql3);
                    }
                    Response.Write("<script>window.location='Homepage.aspx'</script>");
                }
                //否则判断是否为好友或者自己
                else
                {
                    string sql5 = "select * from Friend where myusername='" + Session["tourist"].ToString() + "' and otherusername='" + Session["name"].ToString() + "'";

                    DataTable dt5 = myfriend.select(sql5);

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

                        DataTable dt6 = myfriend.select(sql6);

                        string visitway = dt6.Rows[0][9].ToString() + "访问你的空间";

                        string sql7 = "insert into Tourist values('" + Session["tourist"].ToString() + "','" + dt6.Rows[0][8].ToString() + "','" + Session["name"].ToString() + "','" + Session["nickname"].ToString() + "','" + visitway + "','" + DateTime.Now.ToString() + "')";

                        myfriend.store_change(sql7);

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