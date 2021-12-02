using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;

namespace DataKodePosWeb.API
{
    public class DBHelper
    {
        static string connStr = ConfigurationManager.ConnectionStrings["KodeposConn"].ConnectionString;
        public static Paging Page<T>(string cmdText)
        {
            Paging retval = new Paging();

            retval.Data = new List<dynamic>();

            DataTable dt = new DataTable();
            using (SqlConnection sql = new SqlConnection(connStr))
            {
                sql.Open();
                using (SqlCommand cmd = new SqlCommand(cmdText, sql))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandTimeout = 0;
                    dt.Load(cmd.ExecuteReader());
                }
                sql.Close();
            }

            foreach (DataRow dr in dt.Rows)
            {
                T _type = (T)Activator.CreateInstance<T>();
                foreach (DataColumn dc in dt.Columns)
                {
                    string memberName = dc.ColumnName;
                    string val = dr[dc].ToString();
                    if (memberName != "PageCount")
                    {
                        _type.GetType().GetProperty(memberName).SetValue(_type, val);
                    }
                }
                retval.Data.Add(_type);
            }
            if (dt.Rows.Count > 0)
            {
                retval.PageCount = int.Parse(dt.Rows[0]["PageCount"].ToString());
            }
            return retval;
        }
        public static string Scalar(string cmdText)
        {
            string retval = string.Empty;

            using (SqlConnection sql = new SqlConnection(connStr))
            {
                sql.Open();
                using (SqlCommand cmd = new SqlCommand(cmdText, sql))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandTimeout = 0;
                    object obj = cmd.ExecuteScalar();
                    if (obj != null)
                    {
                        retval = obj.ToString();
                    }
                }
                sql.Close();
            }

            return retval;
        }
        public static dynamic Execute(string cmdText)
        {
            dynamic retval = null;

            using (SqlConnection sql = new SqlConnection(connStr))
            {
                sql.Open();
                SqlTransaction tran = sql.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = new SqlCommand(cmdText, sql, tran))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandTimeout = 0;
                        int res = cmd.ExecuteNonQuery();
                        retval = res;
                        tran.Commit();
                    }
                }
                catch(Exception ex)
                {
                    tran.Rollback();
                }
                sql.Close();
            }

            return retval;
        }
    }
}