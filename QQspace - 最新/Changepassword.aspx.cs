using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Changepassword : System.Web.UI.Page
{
    Class1 mychange = new Class1();
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnchangpassword_Click(object sender, EventArgs e)
    {//满足限制条件后进行下一步
        if(RequiredFieldValidator1.IsValid==true && RequiredFieldValidator2.IsValid==true && RequiredFieldValidator3.IsValid==true && CompareValidator1.IsValid==true )
        {
            string oldpassword = mychange.md5(password.Text, 16);

            string newpassword = mychange.md5(txtPwd.Text, 16);

            string sql = "select * from Login where username='" + Session["name"].ToString() + "' and password='" + oldpassword + "'";

            string sql1 = "update Login set password='" + newpassword + "'";

            DataTable dt = mychange.select(sql);

            if (dt.Rows.Count > 0)
            {
                mychange.store_change(sql1);
                //发送邮件至注册时的邮箱
                Library.DAL.Send.Sendemails("17806282596@163.com", dt.Rows[0][12].ToString(), "修改密码", "你的密码修改了,如果不是本人操作请迅速使用密码问题或手机更换密码");

                Session["name"] = null;

                Response.Write("<script>alert('登录失效！');location='Login.aspx'</script>");

            }
            else
                Response.Write("<script>alert('原密码错误！')</script>");

        }
    }
}