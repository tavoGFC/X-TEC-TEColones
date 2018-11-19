using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using X_TEC.TEColones.Models.StudentModels;
using X_TEC.TEColones.Models.AdminModels;

namespace X_TEC.TEColones.Persistence
{
    public class DBConnection
    {
        private static readonly string connectionString = @"Data Source=projectce.database.windows.net;User ID=mustang;Password=********;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private static SqlConnection connection = new SqlConnection(connectionString);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public static bool InsertStudent(CreateUser student)
        {
            try
            {           
                connection.Close();

                SqlCommand command = new SqlCommand("SP_Insert_Student", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                
                command.Parameters.AddWithValue("Identification", student.Identification);
                command.Parameters.AddWithValue("FirstName", student.FirstName);
                command.Parameters.AddWithValue("LastName", student.LastName);
                command.Parameters.AddWithValue("Email", student.Email);
                command.Parameters.AddWithValue("University", student.University);
                command.Parameters.AddWithValue("Headquarter", student.Headquarter);
                command.Parameters.AddWithValue("PhoneNumber", student.PhoneNumber);
                command.Parameters.AddWithValue("Password", student.Password);
                command.Parameters.AddWithValue("Description", student.Description);
                command.Parameters.AddWithValue("Skills", student.Skills);
                command.Parameters.AddWithValue("PhotoData", student.Photo);

                var returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                connection.Open();
                command.ExecuteNonQuery();

                var result = returnParameter.Value;


                if (result.Equals(1))
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error SP_Insert_User " + ex.Message);
            }
            return false;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="identification"></param>
        /// <returns></returns>
        public static int ExistUser(string identification)
        {
            int result = 0;
            try
            {
                connection.Close();

                SqlCommand command = new SqlCommand("SP_Exist_User", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("Identification", identification);
                
                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result = (int)reader[0];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Exist_Usr " + ex.Message);
            }
            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="identification"></param>
        /// <returns></returns>
        public static StudentModel VerifyStudent(string identification, string password)
        {
            StudentModel student = new StudentModel();
            try
            {
                connection.Close();

                SqlCommand command = new SqlCommand("SP_Verify_Student", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("Identification", identification);
                command.Parameters.AddWithValue("PasswordVerify", password);

                connection.Open();
                var reader = command.ExecuteReader();

                if (reader.Read())
                {  
                    //incorrect password or identification
                    if (reader[0].ToString().Equals("0"))
                    {
                        student.Id = 0;
                        return student;
                    }
                    else
                    {                       
                        student.FirstName = reader["FirstName"].ToString();
                        student.LastName = reader["LastName"].ToString();
                        student.Photo = reader["Photo"].ToString();
                        student.Id = (int)reader["Id"];
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Exist_Usr " + ex.Message);
            }
            return student;
        }

        /// <summary>
        /// Insert new keys and tokens of twitter into the data base.
        /// </summary>
        /// <param name="ConsumerKey"></param>
        /// <param name="ConsumerSecret"></param>
        /// <param name="AcessToken"></param>
        /// <param name="AccessTokenSecret"></param>
        public static void InsertTwitterData(string ConsumerKey, string ConsumerSecret, string AcessToken, string AccessTokenSecret)
        {
            try
            {
                connection.Close();

                SqlCommand command = new SqlCommand("SP_Insert_TwitterData", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("CONSUMER_KEY", ConsumerKey);
                command.Parameters.AddWithValue("CONSUMER_SECRET", ConsumerSecret);
                command.Parameters.AddWithValue("ACCESS_TOKEN", AcessToken);
                command.Parameters.AddWithValue("ACCESS_TOKEN_SECRET", AccessTokenSecret);

                connection.Open();
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Inserting Data" + ex.Message);
            }
        }

        public static void GetTwitterData(ConfigurationModel Config)
        {
            try
            {
                connection.Close();
                connection.Open();

                SqlCommand command = new SqlCommand("SP_Insert_TwitterData", connection)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader reader = command.ExecuteReader();
            
                while (reader.Read())
                {
                    Config.CONSUMER_KEY = reader[0].ToString();
                    Config.CONSUMER_SECRET = reader[1].ToString();
                    Config.ACCESS_TOKEN = reader[2].ToString();
                    Config.ACCESS_TOKEN_SECRET = reader[3].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Inserting Data" + ex.Message);
            }
        }

        #region Operaciones
       public int Suma(int a, int b)
        {
            int c = a + b;
            return c;
        }
        #endregion
    }


}
