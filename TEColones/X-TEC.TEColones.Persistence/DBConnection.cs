using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using X_TEC.TEColones.Persistence.Entity;
using System.Data;

namespace X_TEC.TEColones.Persistence
{
    public class DBConnection
    {

        public DBConnection()
        {
        }


        static SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBXTEC"].ToString());

        

        public static bool VerifyStudent(Student user)
        {
            try
            {
                connection.Close();
                connection.Open();

                SqlCommand command = new SqlCommand("SP_Exist_User", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("Id", user.NumberId);
                command.Parameters.AddWithValue("Name", user.User.FirstName);

                var returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                command.ExecuteNonQuery();
                var result = returnParameter.Value;
                if (result.Equals("1"))
                {
                    return true;
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Conexion SP_Exist_User " + ex.Message);
            }
            return false;
        }


    }
}
