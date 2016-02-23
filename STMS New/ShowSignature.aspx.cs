using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;

using System.Drawing.Imaging;
using System.Drawing.Text;

public partial class ShowSignature : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string signString = "";
            string qryStr = "SELECT SignatureData from " + Connection.tbl_ChkInOutDetailsName + " where CheckInOutID=" + Session["CheckInOutIDForSign"];
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(qryStr, Connection.conn);
            Connection.conn.Open();
            MySql.Data.MySqlClient.MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                signString = dr[0].ToString();
            }
            dr.Close();
            Connection.conn.Close();


            byte[] bytes = Convert.FromBase64String(signString);
            string signPoint = System.Text.Encoding.UTF8.GetString(bytes);

            MemoryStream ms = new MemoryStream(bytes);
            StreamReader sr = new StreamReader(ms);
            if (signString == null || signString=="")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('Signature not availble');</script>");
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>JavaScript:window.close();</script>");
            }
            else
            {
                float[][] points = new float[1][];

                float[] tempPoints = new float[3];

                string temp;
                int i = 0, cnt1 = 0, cnt2 = 2;
                while (!((temp = sr.ReadLine()) == "EOF"))
                {
                    if (temp == "FreeLine")
                    {
                        points = new float[++cnt1][];
                        points[cnt1 - 1] = new float[3];
                        i = 0;

                    }

                    if (temp == "X" + i)
                    {
                        points[cnt1 - 1][cnt2++] = float.Parse(sr.ReadLine());
                        Array.Copy(points[cnt1 - 1], tempPoints, points[cnt1 - 1].Length);
                        points[cnt1 - 1] = new float[points[cnt1 - 1].Length + 1];
                        Array.Copy(tempPoints, points[cnt1 - 1], tempPoints.Length);
                        tempPoints = new float[points[cnt1 - 1].Length];



                    }

                    if (temp == "Y" + i)
                    {
                        points[cnt1 - 1][cnt2++] = float.Parse(sr.ReadLine());
                        Array.Copy(points[cnt1 - 1], tempPoints, points[cnt1 - 1].Length);
                        points[cnt1 - 1] = new float[points[cnt1 - 1].Length + 1];
                        Array.Copy(tempPoints, points[cnt1 - 1], tempPoints.Length);
                        tempPoints = new float[points[cnt1 - 1].Length];
                        i++;


                    }

                    if (temp == "FreeLine InitialPointX")
                    {
                        points[cnt1 - 1][0] = float.Parse(sr.ReadLine());

                        //Array.Copy(points[cnt1-1], tempPoints, points[cnt1-1].Length);
                        //points[cnt1-1] = new float[points[cnt1-1].Length + 1];
                        //Array.Copy(tempPoints, points[cnt1-1], tempPoints.Length);
                        //tempPoints = new float[points[cnt1-1].Length];

                    }
                    if (temp == "FreeLine InitialPointY")
                    {
                        points[cnt1 - 1][1] = float.Parse(sr.ReadLine());
                        //Array.Copy(points[cnt1 - 1], tempPoints, points[cnt1 - 1].Length);
                        //points[cnt1 - 1] = new float[points[cnt1 - 1].Length + 1];
                        //Array.Copy(tempPoints, points[cnt1 - 1], tempPoints.Length);
                        //tempPoints = new float[points[cnt1 - 1].Length];


                    }





                }



                Bitmap objBitmap;
                Graphics objGraphics;
                objBitmap = new Bitmap(800, 1000);
                objGraphics = Graphics.FromImage(objBitmap);
                objGraphics.Clear(Color.White);
                Pen p = new Pen(Color.Black, 1);
                int k;
                for (k = 0; k < points[0].Length - 1; k += 2)
                {
                    if ((k + 3) >= points[0].Length - 1)
                        break;
                    objGraphics.DrawLine(p, points[0][k], points[0][k + 1], points[0][k + 2], points[0][k + 3]);

                }
                objBitmap.Save(Server.MapPath("Sign.jpg"), ImageFormat.Jpeg);
                imgSign.ImageUrl = "~/Sign.jpg";
            }
        }
        catch
        {
        }
    }
}