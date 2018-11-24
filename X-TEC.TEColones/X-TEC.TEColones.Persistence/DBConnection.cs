using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Web.Configuration;
using X_TEC.TEColones.Models.StudentModels;
using X_TEC.TEColones.Models.SCMModels;
using X_TEC.TEColones.Models.AdminModels;
using Tweetinvi.Core.Events;

namespace X_TEC.TEColones.Persistence
{
    public class DBConnection
    {
        public static Configuration rootWebConfig =
                WebConfigurationManager.OpenWebConfiguration("/X-TEC.TEColones");
        public static ConnectionStringSettings connString = rootWebConfig.ConnectionStrings.ConnectionStrings["DBXTEColones"];

        private static SqlConnection Connection = new SqlConnection(connString.ConnectionString);

        #region Student

        /// <summary>
        /// 
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public static bool InsertStudent(CreateUser student)
        {
            try
            {
                Connection.Close();
                Connection.Open();

                SqlCommand command = new SqlCommand("SP_Insert_Student", Connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("Identification", student.Id);
                command.Parameters.AddWithValue("Description", student.Description);
                command.Parameters.AddWithValue("Skills", student.Skills);
                command.Parameters.AddWithValue("PhoneNumber", student.PhoneNumber);
                command.Parameters.AddWithValue("FirstName", student.FirstName);
                command.Parameters.AddWithValue("LastName", student.LastName);
                command.Parameters.AddWithValue("University", student.University);
                command.Parameters.AddWithValue("Headquarter", student.Headquarter);
                command.Parameters.AddWithValue("Email", student.Email);
                command.Parameters.AddWithValue("Password", student.Password);
                SqlParameter photo = new SqlParameter("PhotoData", SqlDbType.VarBinary)
                {
                    Value = student.ImageByte
                };
                command.Parameters.Add(photo);
                var returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
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
                Connection.Close();
                Connection.Open();

                SqlCommand command = new SqlCommand("SP_Exist_User", Connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("Identification", identification);
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
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool ShareTCS(StudentModel user)
        {
            try
            {
                Connection.Close();
                Connection.Open();

                SqlCommand command = new SqlCommand("SP_Share_TCS", Connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("Identification", user.Id);
                command.Parameters.AddWithValue("IdentificationToShare", user.ShareTCS.UserToShareId);
                command.Parameters.AddWithValue("TCStoShare", user.ShareTCS.TCSToShare);
                var returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                command.ExecuteNonQuery();
                var result = returnParameter.Value;
                if (result.Equals(1))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ShareTCS " + ex.Message);
            }
            return false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetNameUser(string id)
        {
            string name = string.Empty;
            try
            {
                Connection.Close();
                Connection.Open();

                SqlCommand command = new SqlCommand("SELECT CONCAT(U.FirstName + ' ' , U.LastName) FROM [User] U WHERE U.Id = @idUser", Connection)
                {
                    CommandType = CommandType.Text
                };
                command.Parameters.AddWithValue("idUser", id);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    name = reader[0].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error GetNameUser " + ex.Message);
            }
            return name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="student"></param>
        public static void GetBenefit(StudentModel student)
        {
            try
            {
                Connection.Close();
                Connection.Open();

                SqlCommand command = new SqlCommand("SELECT B.Type, B.ExchangeRate FROM Benefit B", Connection)
                {
                    CommandType = CommandType.Text
                };
                SqlDataReader reader = command.ExecuteReader();
                student.AssignTCS.Benefits = new List<Benefit>();
                while (reader.Read())
                {
                    Benefit benefit = new Benefit
                    {
                        Type = reader["Type"].ToString(),
                        ExchangeRate = float.Parse(reader["ExchangeRate"].ToString(), CultureInfo.InvariantCulture.NumberFormat)
                    };
                    student.AssignTCS.Benefits.Add(benefit);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error GetBenefit " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="student"></param>
        public static void GetMaterial(StudentModel student)
        {
            try
            {
                Connection.Close();
                Connection.Open();

                SqlCommand command = new SqlCommand("SELECT M.Type, M.ValueTCS FROM Material M", Connection)
                {
                    CommandType = CommandType.Text
                };
                SqlDataReader reader = command.ExecuteReader();
                student.HomeVM.Materials = new List<Material>();
                while (reader.Read())
                {
                    Material material = new Material
                    {
                        Type = reader["Type"].ToString(),
                        ValueTCS = float.Parse(reader["ValueTCS"].ToString(), CultureInfo.InvariantCulture.NumberFormat)
                    };
                    student.HomeVM.Materials.Add(material);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error GetMaterial " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="benefit"></param>
        /// <param name="tcs"></param>
        /// <param name="cs"></param>
        /// <param name="exrDate"></param>
        /// <returns></returns>
        public static bool InsertLogAssign(int id, string benefit, int tcs, float cs, float exrDate)
        {
            try
            {
                Connection.Close();
                Connection.Open();

                SqlCommand command = new SqlCommand("SP_Insert_Log_Assign", Connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("Identification", id);
                command.Parameters.AddWithValue("Benefit", benefit);
                command.Parameters.AddWithValue("TCS", tcs);
                command.Parameters.AddWithValue("CS", cs);
                command.Parameters.AddWithValue("ExRtoDate", exrDate);

                var returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                command.ExecuteNonQuery();
                var result = returnParameter.Value;
                if (result.Equals(1))
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error InsertLogAssign " + ex.Message);
            }
            return false;
        }
        #endregion

        #region  StoregeCenterManager

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="idSCM"></param>
        /// <param name="material"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static bool InsertRegister(string idUser, int idSCM, string material, string amount)
        {
            try
            {
                Connection.Close();
                Connection.Open();

                SqlCommand command = new SqlCommand("SP_Insert_Log_Register", Connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("IdUser", idUser);
                command.Parameters.AddWithValue("IdSCM", idSCM);
                command.Parameters.AddWithValue("Material", material);
                command.Parameters.AddWithValue("Amount", amount);

                var returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                command.ExecuteNonQuery();
                var result = returnParameter.Value;
                if (result.Equals(1))
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error InsertRegister " + ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Get materials 
        /// </summary>
        /// <param name="scm"></param>
        public static void GetMaterial(SCM scm)
        {
            try
            {
                Connection.Close();
                Connection.Open();

                SqlCommand command = new SqlCommand("SELECT M.Type, M.ValueTCS FROM Material M", Connection)
                {
                    CommandType = CommandType.Text
                };
                SqlDataReader reader = command.ExecuteReader();
                scm.Materials = new List<string>();
                while (reader.Read())
                {
                    scm.Materials.Add(reader["Type"].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error GetMaterial " + ex.Message);
            }
        }

        /// <summary>
        /// Get email student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetEmailUser(string id)
        {
            string email = string.Empty;
            try
            {
                Connection.Close();
                Connection.Open();

                SqlCommand command = new SqlCommand("SP_Get_Email", Connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("IdUser", id);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    email = reader[0].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error GetNameUser " + ex.Message);
            }
            return email;
        }

        #endregion

        #region LogIn
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
                Connection.Close();
                Connection.Open();

                SqlCommand command = new SqlCommand("SP_Verify_Get_Student", Connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("Identification", identification);
                command.Parameters.AddWithValue("PasswordVerify", password);
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
                        student.Id = Int32.Parse(identification);
                        student.FirstName = reader["FirstName"].ToString();
                        student.LastName = reader["LastName"].ToString();
                        student.University = reader["University"].ToString();
                        student.Headquarter = reader["University"].ToString();
                        student.Email = reader["University"].ToString();
                        student.PhotoBytes = (byte[])reader["Photo"];
                        student.Description = reader["Description"].ToString();
                        student.Skills = reader["Skills"].ToString();
                        student.TCS = (int)reader["TCS"];
                        student.PhoneNumber = reader["PhoneNumber"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error VerifyStudent " + ex.Message);
            }
            return student;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="identification"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static Tuple<int, bool> VerifyAdminSCM(string identification, string password)
        {
            Tuple<int, bool> user;
            try
            {
                Connection.Close();
                Connection.Open();

                SqlCommand command = new SqlCommand("SP_Verify_Admin_SCM", Connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("Identification", identification);
                command.Parameters.AddWithValue("PasswordVerify", password);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    //incorrect password or identification
                    if (reader[0].ToString().Equals("0"))
                    {
                        user = Tuple.Create(0, false);
                        return user;
                    }
                    else
                    {
                        bool isAdmin = (bool)reader["Admin"];
                        user = Tuple.Create(Int32.Parse(identification), isAdmin);
                        return user;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error VerifyStudent " + ex.Message);
            }
            return Tuple.Create(0, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static SCM GetSCM(int id)
        {
            SCM scm = new SCM();
            try
            {
                Connection.Close();
                Connection.Open();

                SqlCommand command = new SqlCommand("SP_Get_AdminSCM", Connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("Identification", id);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    //not return data
                    if (reader[0].ToString().Equals("0"))
                    {
                        scm.Id = 0;
                        return scm;
                    }
                    else
                    {
                        scm.Id = id;
                        scm.FirstName = reader["FirstName"].ToString();
                        scm.LastName = reader["LastName"].ToString();
                        scm.University = reader["University"].ToString();
                        scm.Headquarter = reader["University"].ToString();
                        scm.Email = reader["University"].ToString();
                        scm.PhotoBytes = (byte[])reader["Photo"];
                        scm.Department = reader["Department"].ToString();
                        scm.PhoneNumber = reader["PhoneNumber"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error GetSCM " + ex.Message);
            }
            return scm;
        }
        #endregion

        #region Configuration

        /// <summary>
        /// Get the value in TEColones of the Materials from the database.
        /// </summary>
        /// <param name="Config"></param>
        public static void GetMaterialTCSValue(ConfigurationViewModel Config)
        {
            try
            {
                Connection.Close();
                Connection.Open();

                SqlCommand command = new SqlCommand("SELECT Type, ValueTCS FROM Material", Connection)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader reader = command.ExecuteReader();
                Config.ValuesTCS = new List<float>();

                while (reader.Read())
                {
                    float value = float.Parse(reader["ValueTCS"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                    Config.ValuesTCS.Add(value);
                }
                Config.SetValues();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Getting Data" + ex.Message);
            }
            Connection.Close();

        }

        /// <summary>
        /// Edit the TCS values of the materials globally on the database.  
        /// </summary>
        /// <param name="PlasticNewValue"></param>
        /// <param name="GlassNewValue"></param>
        /// <param name="PaperNewValue"></param>
        /// <param name="AluminumNewValue"></param>
        public static void InsertNewMaterialTCSValue(float PlasticNewValue, float GlassNewValue, float PaperNewValue, float AluminumNewValue)
        {
            try
            {
                Connection.Close();
                Connection.Open();

                SqlCommand command = new SqlCommand("SP_Insert_NewMaterialValues", Connection)
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

        /// <summary>
        /// Get the benefits value in colones in each account from the database.
        /// </summary>
        /// <param name="Config"></param>
        public static void GetBenefitsValue(ConfigurationViewModel Config)
        {
            try
            {
                Connection.Close();
                Connection.Open();

                SqlCommand command = new SqlCommand("SELECT Type, ExchangeRate FROM Benefit", Connection)
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
            Connection.Close();
        }

        /// <summary>
        /// Edit benefits in colones in each account globally from the database.  
        /// </summary>
        /// <param name="NewDinningExchange"></param>
        /// <param name="NewStudyExchange"></param>
        public static void InsertNewBenefitsValue(float NewDinningExchange, float NewStudyExchange)
        {
            try
            {
                Connection.Close();

                SqlCommand command = new SqlCommand("SP_Insert_NewBenefitsValue", Connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("ExchangeRateComedor", NewDinningExchange);
                command.Parameters.AddWithValue("ExchangeRateMatricula", NewStudyExchange);

                Connection.Open();
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
        public static void GetTwitterData()
        {
            try
            {
                Connection.Close();
                Connection.Open();

                SqlCommand command = new SqlCommand("SP_Get_TwitterData", Connection)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TwitterConnection.CONSUMER_KEY = reader[0].ToString();
                    TwitterConnection.CONSUMER_SECRET = reader[1].ToString();
                    TwitterConnection.ACCESS_TOKEN = reader[2].ToString();
                    TwitterConnection.ACCESS_TOKEN_SECRET = reader[3].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Inserting Data" + ex.Message);
            }
            Connection.Close();
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
                Connection.Close();

                SqlCommand command = new SqlCommand("SP_Insert_TwitterData", Connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("CONSUMER_KEY", ConsumerKey);
                command.Parameters.AddWithValue("CONSUMER_SECRET", ConsumerSecret);
                command.Parameters.AddWithValue("ACCESS_TOKEN", AcessToken);
                command.Parameters.AddWithValue("ACCESS_TOKEN_SECRET", AccessTokenSecret);

                Connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Inserting Data" + ex.Message);
            }
        }
        #endregion

        #region Promotion

        /// <summary>
        /// Get the types (names) of the Materials of the database.
        /// </summary>
        /// <param name="Config"></param>
        public static void GetMaterialType(PromotionViewModel Promo)
        {
            try
            {
                Connection.Close();
                Connection.Open();

                SqlCommand command = new SqlCommand("SELECT Type, ValueTCS FROM Material", Connection)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader reader = command.ExecuteReader();
                Promo.ListMaterials = new List<string>();
                while (reader.Read())
                {
                    Promo.ListMaterials.Add(reader["Type"].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Getting Data" + ex.Message);
            }
            Connection.Close();
        }

        /// <summary>
        /// Inserts a new single promotion into the database. 
        /// </summary>
        /// <param name="AdminModeId"></param>
        /// <param name="materialType"></param>
        /// <param name="amountKg"></param>
        /// <param name="ValueTCS"></param>
        /// <param name="FinishDate"></param>
        /// <param name="ActiveValue"></param>
        public static void InsertNewPromotion(int AdminModeId, float ValueTCS, string FinishDate, int ActiveValue, int SingleProm)
        {
            try
            {
                Connection.Close();
                Connection.Open();

                SqlCommand command = new SqlCommand("SP_InsertNewPromotion", Connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("AdminModeId", AdminModeId);
                command.Parameters.AddWithValue("ValueTCS", ValueTCS);
                command.Parameters.AddWithValue("FinishDate", FinishDate);
                command.Parameters.AddWithValue("ActiveValue", ActiveValue);
                command.Parameters.AddWithValue("SingleProm", SingleProm);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error InsertNewComboPromotion " + ex.Message);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Promo"></param>
        public static void GetNewestIdPromotion(PromotionViewModel Promo)
        {
            try
            {
                Connection.Close();
                Connection.Open();

                SqlCommand command = new SqlCommand("SELECT TOP 1 Id, IdAdmin FROM Promotion ORDER BY Id DESC", Connection)
                {
                    CommandType = CommandType.Text
                };

                SqlDataReader reader = command.ExecuteReader();

                string IdPromoString = string.Empty;

                while (reader.Read())
                {
                    IdPromoString = reader["Id"].ToString();
                }

                Promo.LatestIdPromotion = int.Parse(IdPromoString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Getting Data" + ex.Message);
            }
            Connection.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdPromo"></param>
        /// <param name="Material"></param>
        /// <param name="AmountKg"></param>
        public static void InsertPromosMaterial(int IdPromo, string Material, int AmountKg) 
        {
            try
            {
                Connection.Close();
                Connection.Open();

                SqlCommand command = new SqlCommand("SP_InsertNewPromotionsMaterials", Connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("IdPromo", IdPromo);
                command.Parameters.AddWithValue("Material", Material);
                command.Parameters.AddWithValue("AmountKg", AmountKg);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error InsertNewComboPromotion " + ex.Message);
            }
            Connection.Close();
        }
        #endregion
    }
}