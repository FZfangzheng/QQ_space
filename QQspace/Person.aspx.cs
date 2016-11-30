using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Person2 : System.Web.UI.Page
{
    Class1 myperson = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["name"] != null)
        {
            string sql = "select * from Login where username='" + Session["name"].ToString() + "'";

            DataTable dt = myperson.select(sql);

            lbnickname.Text = dt.Rows[0][3].ToString();
        }
        else
            Response.Write("<script>alert('尚未登录！');location='Login.aspx'</script>");
    }

    protected void change_data_Click(object sender, EventArgs e)
    {

    }
}