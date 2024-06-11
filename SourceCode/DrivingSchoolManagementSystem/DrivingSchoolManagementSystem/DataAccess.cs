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

        public static int SendData(string sql, string? atCarId = null, string? atPickupLocation = null)
        {
            int rowsAffected;

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (atCarId != null)
                    {
                        cmd.Parameters.AddWithValue(atCarId, DBNull.Value);
                    }
                    if (atPickupLocation != null)
                    {
                        cmd.Parameters.AddWithValue(atPickupLocation, DBNull.Value);
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
    }
}
