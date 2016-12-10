using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    Class1 mycenter = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["name"] != null)
        {
            if (myhomepage.SelectedValue == "个人中心")
            {
                Session["tourist"] = null;

                Session["touristnickname"] = null;

                Response.Write("<script>window.location='Personal_center.aspx'</script>");
            }
            if (myhomepage.SelectedValue == "主页")
            {
                Session["tourist"] = null;

                Session["touristnickname"] = null;

                Response.Write("<script>window.location='Homepage.aspx'</script>");
            }
            if (myhomepage.SelectedValue == "日志")
            {
                Session["tourist"] = null;

                Session["touristnickname"] = null;

                Response.Write("<script>window.location='Dairy.aspx'</script>");
            }
            if (myhomepage.SelectedValue == "相册")
            {
                Session["tourist"] = null;

                Session["touristnickname"] = null;

                Response.Write("<script>window.location='Photo.aspx'</script>");
            }
            if (myhomepage.SelectedValue == "说说")
            {
                Session["tourist"] = null;

                Session["touristnickname"] = null;

                Response.Write("<script>window.location='Say.aspx'</script>");
            }
            if (myhomepage.SelectedValue == "个人档")
            {
                Session["tourist"] = null;

                Session["touristnickname"] = null;

                Response.Write("<script>window.location='Person.aspx'</script>");
            }

            string sql = "";

            string sql1 = "";

            if (Session["tourist"] != null)
            {
                personal_center1.Visible = false;

                personal_center2.Visible = true;

                friend.Visible = false;

                friendmessage.Visible = false;

                special.Visible = false;

                set.Visible = false;

                collect.Visible = false;

                FileUpload1.Visible = false;

                Button1.Visible = false;

                alltourist.Visible = false;

                todaytourist.Visible = false;

                lballtourist.Visible = false;

                lbtodaytourist.Visible = false;

                sql = "select photo from Login where username='" + Session["tourist"].ToString() + "'";

                sql1 = "select * from Friend_make where otherusername='" + Session["tourist"].ToString() + "'";

                lbname.Text = Session["touristnickname"].ToString();
            }
            else
            {
                personal_center1.Visible = true;

                personal_center2.Visible = false;

                lbname.Text = Session["nickname"].ToString();

                sql = "select photo from Login where username='" + Session["name"].ToString() + "'";

                sql1 = "select * from Friend_make where otherusername='" + Session["name"].ToString() + "'";
            }
            lable.Text = "QQ空间";



            DataTable dt = new DataTable();

            dt = mycenter.select(sql);

            if (dt.Rows.Count > 0)
            {
                string sql0 = "update Dynamic set head_portrait='" + dt.Rows[0][0].ToString() + "' where username='" + Session["name"].ToString() + "'";

                mycenter.store_change(sql0);

                Image1.ImageUrl = dt.Rows[0][0].ToString();
            }
            //读取加好友信息


            DataTable dt1 = mycenter.select(sql1);

            if (dt1.Rows.Count > 0)
            {
                friendmessage.Text = dt1.Rows.Count.ToString();
            }

            //获取今日和历史访问人数
            string sql2 = "select * from Tourist where username='" + Session["name"].ToString() + "'";

            DataTable dt2 = mycenter.select(sql2);

            lballtourist.Text = dt2.Rows.Count.ToString();

            string time = DateTime.Now.ToString().Substring(0, 9);

            string sql3 = "select * from Tourist where username='" + Session["name"].ToString() + "' and time like'%" + time + "%'";

            DataTable dt3 = mycenter.select(sql3);

            lbtodaytourist.Text = dt3.Rows.Count.ToString();
            //获取等级
            string sql4 = "select rank from Login where username='" + Session["name"].ToString() + "'";

            DataTable dt4 = mycenter.select(sql4);

            lbspacerank.Text = dt4.Rows[0][0].ToString();

            if (Convert.ToInt32(dt4.Rows[0][0].ToString()) > 25 && Convert.ToInt32(dt4.Rows[0][0].ToString()) < 100)
            {
                spacerank.ImageUrl = "~/Images/2.jpg";
            }
            if (Convert.ToInt32(dt4.Rows[0][0].ToString()) >= 100)
            {
                spacerank.ImageUrl = "~/Images/3.jpg";
            }
        }
        else
            Response.Write("<script>alert('尚未登录！');location='Login.aspx'</script>");
    }
    protected void btnupload_Click(object sender, EventArgs e)
    {
        
        Boolean fileOk = false;
        //指定文件路径，pic是项目下的一个文件夹；～表示当前网页所在的文件夹
        String path = Server.MapPath("~/Images/");//物理文件路径

        int length = this.FileUpload1.PostedFile.ContentLength;//获取图片大小，以字节为单位

        
                                                               //文件上传控件中如果已经包含文件
        if (FileUpload1.HasFile)
        {
            //得到文件的后缀
             String fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();

             //允许文件的后缀
             String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg", ".bmp" };

             //看包含的文件是否是被允许的文件的后缀
             for (int i = 0; i < allowedExtensions.Length; i++)
             {
                 if (fileExtension == allowedExtensions[i])
                 {
                     fileOk = true;
                 }
             }
            /* string a = mycenter.CheckTrueFileName(path);
             if(a=="6677"||a== "255216"||a=="13780")
                 fileOk = true;*/
            /*HttpRequest request = System.Web.HttpContext.Current.Request;
            HttpFileCollection FileCollect = request.Files;
            foreach (string str in FileCollect)
            {
                HttpPostedFile FileSave = FileCollect[str];
                fileOk = mycenter.IsAllowedExtension(FileSave);
            }*/
            // string fullPath = Path.GetFullPath(FileUpload1.PostedFile.FileName);
           /* string fileNameNo = Path.GetFileName(FileUpload1.PostedFile.FileName); //获取文件名和扩展名
            string DirectoryName = Path.GetDirectoryName(FileUpload1.PostedFile.FileName);
            string fullPath = DirectoryName + fileNameNo;
            fileOk = mycenter.IsPicture(fullPath);*/

        }
        if (fileOk)
        {
            try
            {

                //文件另存在服务器的指定目录下     
                string name =  FileUpload1.FileName;//获取上传的文件名

                path = "~/Images/" + name;

                string sql = "update Login set photo ='" + path + "' where username='" + Session["name"].ToString() + "'";

                Image1.ImageUrl = path;

                FileUpload1.SaveAs(System.Web.HttpContext.Current.Server.MapPath(path));

                mycenter.store_change(sql);//保存文件路径数据到数据库

                panel.Visible = false;

                Response.Write("<script>alert('文件上传成功！');</script>");
            }
            catch
            {
                Response.Write("<script>alert('文件上传失败！');</script>");
            }
        }
        else
        {
            Response.Write("<script>alert('只能上传png,jpg,bmp图象文件！');</script>");
        }
    }

    protected void Image1_Click(object sender, ImageClickEventArgs e)
    {
        panel.Visible = true;
    }

    protected void personal_center_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.location='Personal_center.aspx'</script>");
    }

    protected void out_Click(object sender, EventArgs e)
    {
        Session["name"] = null;
        Session["nickname"] = null;

        Session["tourist"] = null;
        Session["touristnickname"] = null;
        Response.Write("<script>window.location='Login.aspx'</script>");
    }

    protected void personal_center2_Click(object sender, EventArgs e)
    {
        Session["tourist"] = null;

        Session["touristnickname"] = null;

        Response.Write("<script>window.location='Personal_center.aspx'</script>");
    }
}
