﻿using System;
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
            if (!IsPostBack)
            {
                string sql = "select * from Login where username='" + Session["name"].ToString() + "'";

                DataTable dt = myperson.select(sql);

                lbnickname.Text = dt.Rows[0][3].ToString();

                lbsex.Text = dt.Rows[0][4].ToString();

                lbage.Text = dt.Rows[0][5].ToString();

                lblocation.Text = dt.Rows[0][6].ToString();

                txtnickname.Text = dt.Rows[0][3].ToString();

                txtsex.Text = dt.Rows[0][4].ToString();

                txtage.Text = dt.Rows[0][5].ToString();

                txtlocation.Text = dt.Rows[0][6].ToString();
            }
        }
        else
            Response.Write("<script>alert('尚未登录！');location='Login.aspx'</script>");
    }

    protected void change_data_Click(object sender, EventArgs e)
    {
        pndata.Visible = false;

        pndata_change.Visible = true;
    }

    protected void yes_Click(object sender, EventArgs e)
    {
        string nickname = txtnickname.Text;

        string sex = txtsex.Text;

        string age = txtage.Text;

        string location = txtlocation.Text;

        string sql = "update Login set nickname='" + nickname + "',sex='" + sex + "',age='" + age + "',location='" + location + "' where username='" + Session["name"].ToString() + "'";

        myperson.store_change(sql);

        Response.Write("<script>alert('修改成功！');location='Person.aspx'</script>");
    }

    protected void no_Click(object sender, EventArgs e)
    {
        pndata.Visible = true; 

        pndata_change.Visible = false;
    }

}