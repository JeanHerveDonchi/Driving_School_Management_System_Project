using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchoolManagementSystem
{
    public static class DataAccess
    {
        public static DataTable GetData(string sqlQuery)
        {
            sqlQuery = SQLCleaner(sqlQuery);

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public static DataSet GetData(params string[] sqlQueries)
        {
            string sqlQuery = string.Join(";", sqlQueries);
            sqlQuery = SQLCleaner(sqlQuery);

            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }
            return ds;
        }

        public static int SendData(string sql, string? atCarId = null)
        {
            sql = SQLCleaner(sql);

            int rowsAffected;

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (atCarId != null)
                    {
                        cmd.Parameters.AddWithValue(atCarId, DBNull.Value);
                    }
                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            return rowsAffected;
        }

        public static object GetValue(string sql)
        {
            sql = SQLCleaner(sql);
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();

                    object returnValue = cmd.ExecuteScalar();

                    conn.Close();

                    return returnValue;
                }
            }
        }

        private static string GetConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            return connectionString;
        }

        public static string SQLFix(string str)
        {
            return str.Replace("'", "''");
        }

        public static string SQLCleaner(string sqlStatement)
        {
            while (sqlStatement.Contains("  "))
                sqlStatement = sqlStatement.Replace("  ", " ");

            return sqlStatement.Replace(Environment.NewLine, "");
        }
    }
}
