using System.Data;
using System.Data.SqlClient;
using System.Net.Configuration;
using System.Web.Services;

namespace IDELicenses
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        //Beyrek
        DataTable dt;
        SqlDataReader dr;
        string connectionString = "Server=SRVSQL;Database=Miqra_settings;uid=mmqk;pwd=sa321,sa321;";

        [WebMethod]
        public void AuthLicenseControl(string mac, string license)
        {
            string sql_query = "select distinct *  from TB_MMQK_LICENSE where MAC_ADRESS='" + mac + "'";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand com = new SqlCommand(sql_query, conn);
                dr = com.ExecuteReader();
                dt = new DataTable();
                dt.Load(dr);
                dt.TableName = "license";
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                conn.Close();
                if (dt.Rows.Count > 0)
                {
                    bool has_license = false;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["LICENSE_KEY"].ToString() == license && dt.Rows[i]["MAC_ADRESS"].ToString()==mac) {
                            has_license = true;
                        }
                    }

                    if (has_license)
                    {
                        Context.Response.Write("has_license," + dt.Rows[0]["USERS_COUNT"].ToString() + "," + dt.Rows[0]["STATUS"].ToString() + "," + dt.Rows[0]["FIRM_NAME"].ToString() + "," + dt.Rows[0]["TIMEOUT"].ToString());
                        return;
                    }
                    else {
                        Context.Response.Write("wrong_license"); return;
                    }
                    //foreach (var item in dt.Rows)
                    //{
                    //    if (item["LICENSE_KEY"] == license)
                    //    {

                    //    }
                    //    else { 
                            
                    //    }
                    //}
                    //if (license == dt.Rows[0]["LICENSE_KEY"].ToString())
                    //{
                    //    Context.Response.Write("has_license,"+ dt.Rows[0]["USERS_COUNT"].ToString()+","+ dt.Rows[0]["STATUS"].ToString()+","+dt.Rows[0]["FIRM_NAME"].ToString()); return;
                    //}
                    //else
                    //{
                    //    Context.Response.Write("wrong_license"); return;
                    //}
                }
                else {

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = "INSERT INTO TB_MMQK_LICENSE (MAC_ADRESS,LICENSE_KEY) VALUES (@mac,@license)";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.Add("@mac", mac);
                            command.Parameters.Add("@license", license);
                            connection.Open();
                            int result = command.ExecuteNonQuery();

                            // Check Error
                            if (result < 0) { Context.Response.Write("insert_error_license"); return; }
                            else { Context.Response.Write("insert_license"); }
               
                        }
                    }
                    ////string insert_license = "insert into IDE_LICENSE(MAC_ADRESS,LICANSE_KEY,STATUS) values('"+mac+"','"+license+"',0)";
                   
                    //using (SqlConnection conn_insert = new SqlConnection(connectionString)) {
                    //    SqlCommand command = new SqlCommand(query, conn_insert);
                    //    command.Parameters.Add("@mac", mac);
                    //    command.Parameters.Add("@license", license);
                    //    command.ExecuteNonQuery();
                    //}
                    //Context.Response.Write("has_license," + dt.Rows[0]["USERS_COUNT"].ToString() + "," + dt.Rows[0]["STATUS"].ToString()); return;
                }
            }


        }

    }
}

