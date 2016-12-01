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
    {
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

                Session["name"] = null;

                Response.Write("<script>alert('登录失效！');location='Login.aspx'</script>");

            }
            else
                Response.Write("<script>alert('原密码错误！')</script>");

        }
    }
}