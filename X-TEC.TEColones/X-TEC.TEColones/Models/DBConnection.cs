using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X_TEC.TEColones.Models
{
    public class DBConnection
    {
        private static readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=miBaseDatos;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private static SqlConnection connection = new SqlConnection(connectionString);

        public static bool InsertStudent(int param1, int param2, int param3, int param4, int param5)
        {
            try
            {
                connection.Close();

                SqlCommand command = new SqlCommand("miProcedure", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };


                command.Parameters.AddWithValue("param1", 1);
                command.Parameters.AddWithValue("param2", param1);
                command.Parameters.AddWithValue("param3", param2);
                command.Parameters.AddWithValue("param4", param3);
                command.Parameters.AddWithValue("param5", param4);
                command.Parameters.AddWithValue("param6", param5);
               
                

                connection.Open();
                command.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error SP_Insert_User " + ex.Message);
            }
            return false;

        }
    }
}


