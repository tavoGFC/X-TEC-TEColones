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
        private static readonly string connectionString = "Server=tcp:projectce.database.windows.net,1433;Initial Catalog=XTEColones;Persist Security Info=False;User ID=mustang;Password=randyCE09!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

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

                SqlCommand command = new SqlCommand("SELECT Type, ValueTCS FROM Material", connection)
                {
                    CommandType = CommandType.Text
                };
            
                SqlDataReader reader = command.ExecuteReader();
                Config.ValuesTCS =  new List<float>();
               
                while (reader.Read())
                {
                    float value =  float.Parse(reader["ValueTCS"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                    Config.ValuesTCS.Add(value);
                }
                Config.SetValues();
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
                connection.Open();

                SqlCommand command = new SqlCommand("SP_Insert_NewMaterialValues", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("PlasticNewValue", PlasticNewValue);
                command.Parameters.AddWithValue("PaperNewValue", PaperNewValue);
                command.Parameters.AddWithValue("GlassNewValue", GlassNewValue);
                command.Parameters.AddWithValue("AluminumNewValue", AluminumNewValue);

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

                SqlCommand command = new SqlCommand("SELECT Type, ExchangeRate FROM Benefit" , connection)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader reader = command.ExecuteReader();

                List<float> values = new List<float>();
                while (reader.Read())
                {
                    values.Add(float.Parse(reader["ExchangeRate"].ToString(), CultureInfo.InvariantCulture.NumberFormat));
                }
                Config.DinningExchange = values[0];
                Config.StudyExchange = values[1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Getting Data" + ex.Message);
            }
            connection.Close();  
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="NewDinningExchange"></param>
        /// <param name="NewStudyExchange"></param>
        public static void InsertNewBenefitsValue(float NewDinningExchange, float NewStudyExchange)
        {
            try
            {
                connection.Close();

                SqlCommand command = new SqlCommand("SP_Insert_NewBenefitsValue", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("ExchangeRateComedor", NewDinningExchange);
                command.Parameters.AddWithValue("ExchangeRateMatricula", NewStudyExchange);
               
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

                SqlCommand command = new SqlCommand("SP_Get_TwitterData", connection)
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
