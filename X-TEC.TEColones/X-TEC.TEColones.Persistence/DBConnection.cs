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
using System.Globalization;

namespace X_TEC.TEColones.Persistence
{
    public class DBConnection
    {
        private static readonly string connectionString = @"Data Source=projectce.database.windows.net;Initial Catalog = XTEColones; User ID = mustang; Password=randyCE09!;Connect Timeout = 30; Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private static SqlConnection connection = new SqlConnection(connectionString);

        #region Configuration

        /// <summary>
        /// Get the value in TEColones of the Materials
        /// </summary>
        /// <param name="Config"></param>
        public static void GetMaterialTCSValue(ConfigurationModel Config)
        {
            try
            {
                connection.Close();
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT TOP 4 ValueTCS FROM Material", connection)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Config.PlasticValue = float.Parse(reader["ValueTCS"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                    Config.PaperValue = float.Parse(reader["ValueTCS"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                    Config.GlassValue = float.Parse(reader["ValueTCS"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                    Config.AluminumValue = float.Parse(reader["ValueTCS"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Getting Data" + ex.Message);
            }
            connection.Close();

        }

        /// <summary>
        /// Edit the TCS values of the materials globally.  
        /// </summary>
        /// <param name="PlasticNewValue"></param>
        /// <param name="GlassNewValue"></param>
        /// <param name="PaperNewValue"></param>
        /// <param name="AluminumNewValue"></param>
        public static void InsertNewMaterialTCSValue(float PlasticNewValue, float GlassNewValue, float PaperNewValue, float AluminumNewValue)
        {
            try
            {
                connection.Close();

                SqlCommand command = new SqlCommand("SP_Insert_NewMaterialValues", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("PlasticNewValue", PlasticNewValue);
                command.Parameters.AddWithValue("PaperNewValue", PaperNewValue);
                command.Parameters.AddWithValue("GlassNewValue", GlassNewValue);
                command.Parameters.AddWithValue("AluminumNewValue", AluminumNewValue);

                connection.Open();
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error SP_Insert_NewMaterialValues " + ex.Message);
            }
        }

        public static void GetBenefitsValue(ConfigurationModel Config)
        {
            try
            {
                connection.Close();
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT ExchangeRate FROM Benefit" , connection)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Config.DinningExchange = float.Parse(reader["ExchangeRate"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                    Config.StudyExchange = float.Parse(reader["ExchangeRate"].ToString(), CultureInfo.InvariantCulture.NumberFormat);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Getting Data" + ex.Message);
            }
            connection.Close();  
        }
        

        public static void InsertNewBenefitsValue(float NewDinningExchange, float NewStudyExchange)
        {
            try
            {
                connection.Close();

                SqlCommand command = new SqlCommand("SP_Insert_NewBenefits", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("Comedor", NewDinningExchange);
                command.Parameters.AddWithValue("Matricula", NewStudyExchange);
               
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Inserting Data" + ex.Message);
            }
        }


        /// <summary>
        /// Get the the two keys and two tokens of the twitter account from the database. 
        /// </summary>
        /// <param name="Config"></param>
        public static void GetTwitterData(ConfigurationModel Config)
        {
            try
            {
                connection.Close();
                connection.Open();

                SqlCommand command = new SqlCommand("SP_Insert_NewTwitterData", connection)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Config.CONSUMER_KEY = reader[0].ToString();
                    //Console.WriteLine(Config.CONSUMER_KEY);

                    Config.CONSUMER_SECRET = reader[1].ToString();
                    //Console.WriteLine(Config.CONSUMER_SECRET);

                    Config.ACCESS_TOKEN = reader[2].ToString();
                    //Console.WriteLine(Config.ACCESS_TOKEN);

                    Config.ACCESS_TOKEN_SECRET = reader[3].ToString();
                    //Console.WriteLine(Config.ACCESS_TOKEN_SECRET);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Inserting Data" + ex.Message);
            }
            connection.Close();
        }

        /// <summary>
        /// Updates the two keys and two tokens of the twitter account into the database.
        /// </summary>
        /// <param name="ConsumerKey"></param>
        /// <param name="ConsumerSecret"></param>
        /// <param name="AcessToken"></param>
        /// <param name="AccessTokenSecret"></param>
        public static void InsertNewTwitterData(string ConsumerKey, string ConsumerSecret, string AcessToken, string AccessTokenSecret)
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
        #endregion
    }


}
