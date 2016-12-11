using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Class1 的摘要说明
/// </summary>
public class Class1
{
    string str = @"server=DESKTOP-34MDJJN;Integrated Security=SSPI;database=QQ_space;";
    public Class1()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    public DataTable select(string sql)//用于查询并输出查询结果
    {

        SqlConnection conn = new SqlConnection(str);
        DataTable dt = new DataTable();
        conn.Open();
        SqlDataAdapter da = new SqlDataAdapter(sql, conn);
        da.Fill(dt);
        conn.Close();
        return dt;
    }
    public void store_change(string sql)//用于增删改等作用
    {
        SqlConnection conn = new SqlConnection(str);
        conn.Open();
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.ExecuteNonQuery();
        conn.Close();
    }
    public string md5(string str, int code)  //code 16 或 32  用于哈希加密
    {
        if (code == 16) //16位MD5加密（取32位加密的9~25字符）  
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16);
        }

        if (code == 32) //32位加密  
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
        }

        return "00000000000000000000000000000000";
    }
    public void rank(string username, int a)//计算空间等级分
    {
        string sql = "select rank from Login where username='" + username + "'";

        DataTable dt = select(sql);

        int rank = Convert.ToInt32(dt.Rows[0][0].ToString()) + a;

        string sql1 = "update Login set rank='" + rank + "' where username='" + username + "'";

        store_change(sql1);
    }
    public DataTable  store_change_parameterization(string sql,string a,string b,string thisvalue1,string thisvalue2)//参数化sql,nvarchar型
    {
        SqlConnection conn = new SqlConnection(str);
        conn.Open();
        SqlCommand comm = new SqlCommand(sql,conn);

      
        comm.Parameters.Add(new SqlParameter(a, SqlDbType.NVarChar) { Value = thisvalue1 });
        comm.Parameters.Add(new SqlParameter(b, SqlDbType.NVarChar) { Value = thisvalue2 });
   
        SqlDataAdapter da = new SqlDataAdapter(comm);
        DataTable dt = new DataTable();
        da.Fill(dt);
        conn.Close();
        return dt;
       

    }
   public int  visit(string sql1,string tourist,string touristnickname, string name,string nickname)
    {

        DataTable dt1 = select(sql1);

        if (dt1.Rows.Count == 0)
        {
            //访问陌生人权限查看
            string sql4 = "select authority from Login where username='" + tourist + "'";

            DataTable dt4 = select(sql4);
            //等于零允许陌生人访问
            if (Convert.ToInt32(dt4.Rows[0][0].ToString()) == 0)
            {
                if (name != tourist)
                {
                    string sql2 = "select * from Login where username='" + name + "'";

                    DataTable dt2 = select(sql2);

                    string visitway = dt2.Rows[0][9].ToString() + "访问你的空间";

                    string sql3 = "insert into Tourist values('" + tourist + "','" + dt2.Rows[0][8].ToString() + "','" + name + "','" + nickname + "','" + visitway + "','" + DateTime.Now.ToString() + "')";

                    store_change(sql3);
                }
                return 1;//1表示访问成功
                //Response.Write("<script>window.location='Homepage.aspx'</script>");
            }
            //否则判断是否为好友或者自己
            else
            {
                string sql5 = "select * from Friend where myusername='" + tourist + "' and otherusername='" + name + "'";

                DataTable dt5 = select(sql5);

                if (dt5.Rows.Count == 0)
                {
                    //是不是自己
                    if (name == tourist)
                    {
                        return 1;
                       // Response.Write("<script>window.location='Homepage.aspx'</script>");
                    }
                    else
                    {

                        return 0;
                        //Response.Write("<script>alert('你没有访问权限！')</script>");
                    }
                }
                else
                {
                    string sql6 = "select * from Login where username='" + name + "'";

                    DataTable dt6 = select(sql6);

                    string visitway = dt6.Rows[0][9].ToString() + "访问你的空间";

                    string sql7 = "insert into Tourist values('" + tourist + "','" + dt6.Rows[0][8].ToString() + "','" + name + "','" + nickname + "','" + visitway + "','" + DateTime.Now.ToString() + "')";

                    store_change(sql7);

                    return 1;
                   // Response.Write("<script>window.location='Homepage.aspx'</script>");
                }
            }
        }
        else
        {

            return 0;
            //Response.Write("<script>alert('你没有访问权限！')</script>");
        }
    }
    

}