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


        public static bool UpdatePassword(int identification, string password)
        {
            try
            {
                Connection.Close();
                Connection.Open();

                SqlCommand command = new SqlCommand("SP_UpdatePassword_User", Connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("Identification", identification);
                command.Parameters.AddWithValue("NewPassword", password);
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
                Console.WriteLine("Error UpdatePassword " + ex.Message);
            }
            return false;
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



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static AdminModel GetAdmin(int id)
        {
            AdminModel admin = new AdminModel();
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
                        admin.Id = 0;
                        return admin;
                    }
                    else
                    {
                        admin.Id = id;
                        admin.FirstName = reader["FirstName"].ToString();
                        admin.LastName = reader["LastName"].ToString();
                        admin.University = reader["University"].ToString();
                        admin.Headquarter = reader["University"].ToString();
                        admin.Email = reader["University"].ToString();
                        admin.PhotoBytes = (byte[])reader["Photo"];
                        admin.Department = reader["Department"].ToString();
                        admin.PhoneNumber = reader["PhoneNumber"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error GetAdmin " + ex.Message);
            }
            return admin;
        }

        #endregion


        #region Configuration


        /// <summary>
        /// 
        /// </summary>
        /// <param name="identification"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public static string VerifyEmail(string identification, string email )
        {
            string DBEmail = "0";
            try
            {
                Connection.Close();
                Connection.Open();

                SqlCommand command = new SqlCommand("SP_Verify_Email", Connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("Identification", identification);
                command.Parameters.AddWithValue("EmailVerify", email);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    DBEmail = reader[0].ToString();
                    //incorrect email or identification if DBEmail == "0"
                    return DBEmail;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error VerifyEmailStudent " + ex.Message);
            }
            return DBEmail;
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
                Config.Materials = new Dictionary<string, float>();

                while (reader.Read())
                {
                    string name = reader["Type"].ToString();
                    float value = float.Parse(reader["ValueTCS"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                    Config.Materials.Add(name, value);
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
        public static void InsertNewMaterialTCSValue(string typeMaterial, float newValue)
        {
            try
            {
                Connection.Close();
                Connection.Open();

                SqlCommand command = new SqlCommand("SP_Insert_NewMaterialValues", Connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("Type", typeMaterial);
                command.Parameters.AddWithValue("NewValue", newValue);

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
        public static void InsertNewBenefitsValue(float newExchange, string typeExchange)
        {
            try
            {
                Connection.Close();

                SqlCommand command = new SqlCommand("SP_Insert_NewBenefitsValue", Connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Type", typeExchange);
                command.Parameters.AddWithValue("@ExchangeRate", newExchange);

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
        /// Gets the id of the newest promotion inserted in the database.
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
        /// Inserts the materials and the amount of kg of the materials in association to the promotion, into the database. 
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
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Promo"></param>
        /// <param name="TypePromo"></param>
        public static void GetPromotion(PromotionViewModel Promo, string TypePromo)
        {
            // if combo promotion
            if (TypePromo.Equals("single"))
            {
                try
                {
                    Connection.Close();
                    Connection.Open();

                    SqlCommand command = new SqlCommand("SP_Get_SinglePromotionData", Connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataReader reader = command.ExecuteReader();

                    Promo.SinglePromoData = new List<List<string>>();

                    while (reader.Read())
                    {
                        List<string> listPromo = new List<string>();

                        string id = reader["Id"].ToString();
                        string type = reader["Type"].ToString();
                        string valueTCS = reader["ValueTCS"].ToString();
                        string finishDate = reader["FinishDate"].ToString();
                        string active = reader["Active"].ToString();
                        string amountMaterial = reader["AmountMaterial"].ToString();

                        listPromo.Add(id);
                        listPromo.Add(type);
                        listPromo.Add(amountMaterial);
                        listPromo.Add(valueTCS);
                        listPromo.Add(finishDate);
                        listPromo.Add(active);

                        Promo.SinglePromoData.Add(listPromo);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Getting Data" + ex.Message);
                }
                Connection.Close();
            }
            
            // if combo promotion
            if (TypePromo.Equals("combo"))
            {
                try
                {
                    Connection.Close();
                    Connection.Open();

                    SqlCommand command = new SqlCommand("SP_Get_ComboPromotionData", Connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataReader reader = command.ExecuteReader();

                    Promo.ComboPromoData = new List<List<string>>();

                    while (reader.Read())
                    {
                        List<string> listPromo = new List<string>();

                        string id = reader["Id"].ToString();
                        string type = reader["Type"].ToString();
                        string valueTCS = reader["ValueTCS"].ToString();
                        string finishDate = reader["FinishDate"].ToString();
                        string active = reader["Active"].ToString();
                        string amountMaterial = reader["AmountMaterial"].ToString();

                        listPromo.Add(id);
                        listPromo.Add(type);
                        listPromo.Add(amountMaterial);
                        listPromo.Add(valueTCS);
                        listPromo.Add(finishDate);
                        listPromo.Add(active);

                        Promo.ComboPromoData.Add(listPromo);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Getting Data" + ex.Message);
                }
                Connection.Close();
            }
            else
            {
                Console.WriteLine("Error Getting Data");
            }
        }
        
        #endregion
          
          #region Create New User Admin or SCM
         public static bool InsertAdminSCM(NewAdminSCM user, int isAdmin)
        {
            try
            {
                Connection.Close();
                Connection.Open();
                 SqlCommand command = new SqlCommand("SP_Insert_AdminSCM", Connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("Identification", user.Identification);
                command.Parameters.AddWithValue("Department", user.Department);
                command.Parameters.AddWithValue("PhoneNumber", user.PhoneNumber);
                command.Parameters.AddWithValue("Admin", isAdmin);
                command.Parameters.AddWithValue("FirstName", user.FirstName);
                command.Parameters.AddWithValue("LastName", user.LastName);
                command.Parameters.AddWithValue("University", user.University);
                command.Parameters.AddWithValue("Headquarter", user.Headquarter);
                command.Parameters.AddWithValue("Email", user.Email);
                command.Parameters.AddWithValue("Password", user.Password);
                SqlParameter photo = new SqlParameter("PhotoData", SqlDbType.VarBinary)
                {
                    Value = new byte[] {0x0}
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
                Console.WriteLine("Error SP_Insert_AdminSCM " + ex.Message);
            }
            return false;
         }
         #endregion
         #region Dashboard Admin
         /// <summary>
        /// 
        /// </summary>
        /// <param name="dashboard"></param>
        public static void GetTonsMonth(DashboardModel dashboard)
        {
            dashboard.ToneladasXmes = new List<float>();
            try
            {
                Connection.Close();
                Connection.Open();
                 SqlCommand command = new SqlCommand("SP_Get_TonsMonth", Connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                 var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Dictionary<string, float> valueTonsMonth = new Dictionary<string, float>
                        {
                            { reader["Mes"].ToString(), float.Parse(reader["Toneladas"].ToString()) }
                        };
                    while (reader.Read())
                    {
                        float valueTon = float.Parse(reader["Toneladas"].ToString());
                        string month = reader["Mes"].ToString();
                        valueTonsMonth.Add(month, valueTon);
                    }
                    for (int i = 1; i <= 12; i++)
                    {
                        if (valueTonsMonth.ContainsKey(i.ToString()))
                        {
                            dashboard.ToneladasXmes.Add(valueTonsMonth[i.ToString()]);
                        }
                        else
                        {
                            dashboard.ToneladasXmes.Add(0.0f);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error GetTonsMonth " + ex.Message);
            }            
        }
         /// <summary>
        /// 
        /// </summary>
        /// <param name="dashboard"></param>
        public static void GetUsersMonth(DashboardModel dashboard)
        {
            dashboard.UsuariosXmes = new List<int>();
            try
            {
                Connection.Close();
                Connection.Open();
                 SqlCommand command = new SqlCommand("SP_Get_UsersMonth", Connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                 var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Dictionary<string, int> valueTonsMonth = new Dictionary<string, int>
                        {
                            { reader["Mes"].ToString(), int.Parse(reader["Cantidad_Usuarios"].ToString()) }
                        };
                    while (reader.Read())
                    {
                        int valueTon = int.Parse(reader["Cantidad_Usuarios"].ToString());
                        string month = reader["Mes"].ToString();
                        valueTonsMonth.Add(month, valueTon);
                    }
                    for (int i = 1; i <= 12; i++)
                    {
                        if (valueTonsMonth.ContainsKey(i.ToString()))
                        {
                            dashboard.UsuariosXmes.Add(valueTonsMonth[i.ToString()]);
                        }
                        else
                        {
                            dashboard.UsuariosXmes.Add(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error GetUsersMonth " + ex.Message);
            }
        }
         /// <summary>
        /// 
        /// </summary>
        /// <param name="dashboard"></param>
        public static void GetMoneyMonth(DashboardModel dashboard)
        {
            dashboard.DineroXbeneficio = new List<float>();
            try
            {
                Connection.Close();
                Connection.Open();
                 SqlCommand command = new SqlCommand("SP_Get_MoneyMonth", Connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                 var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Dictionary<string, float> valueTonsMonth = new Dictionary<string, float>
                        {
                            { reader["Mes"].ToString(), float.Parse(reader["Dinero"].ToString()) }
                        };
                    while (reader.Read())
                    {
                        float valueTon = float.Parse(reader["Dinero"].ToString());
                        string month = reader["Mes"].ToString();
                        valueTonsMonth.Add(month, valueTon);
                    }
                    for (int i = 1; i <= 12; i++)
                    {   
                        if (valueTonsMonth.ContainsKey(i.ToString()))
                        {
                            dashboard.DineroXbeneficio.Add(valueTonsMonth[i.ToString()]);
                        }
                        else
                        {
                            dashboard.DineroXbeneficio.Add(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error GetMoneyMonth " + ex.Message);
            }
        }
         /// <summary>
        /// 
        /// </summary>
        /// <param name="dashboard"></param>
        public static void GetTonsPeriod(DashboardModel dashboard)
        {
            dashboard.ToneladasAnuales = 0.0f;
            try
            {
                Connection.Close();
                Connection.Open();
                 SqlCommand command = new SqlCommand("SP_Get_TonsPeriod", Connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                 var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    dashboard.ToneladasAnuales = float.Parse(reader["Toneladas"].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error GetTonsPeriod " + ex.Message);
            }
        }
         /// <summary>
        /// 
        /// </summary>
        /// <param name="dashboard"></param>
        public static void GetTopStudents(DashboardModel dashboard)
        {
            dashboard.Top10 = new List<StudentModel>();
            try
            {
                Connection.Close();
                Connection.Open();
                 SqlCommand command = new SqlCommand("SP_Get_TopStudents", Connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                 var reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    string firstName = reader[0].ToString();
                    string lastName = reader[1].ToString();
                    int id = int.Parse(reader[2].ToString());
                    float kg = float.Parse(reader[3].ToString());
                    StudentModel student = new StudentModel()
                    {
                       FirstName = firstName,
                       LastName = lastName,
                       Id = id,
                       KgRecicled = kg
                    };
                    
                    dashboard.Top10.Add(student);
                }
                while (dashboard.Top10.Count <= 10)
                {
                    dashboard.Top10.Add(new StudentModel() { FirstName = " ", LastName = " ", Id = 0, KgRecicled = 0});
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error GetTopStudents " + ex.Message);
            }
        }
         public static void GetTonsCampuses(DashboardModel dashboard)
        {
            dashboard.TxS = "[";
            string start = "{";
            string end = "},";
             try
            {
                Connection.Close();
                Connection.Open();
                
                SqlCommand command = new SqlCommand("SP_Get_TonsCampuses", Connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                 var reader = command.ExecuteReader();
                 //"[ {'Sede':'CA','Tons':16} , {'Sede':'SJ','Tons':12}, {'Sede':'SC','Tons':345}]";
                while (reader.Read())
                {                    
                    string Tons = reader[0].ToString();
                    string Campus = reader[1].ToString();
                    string item = String.Format("'Sede':'{0}','Tons':{1}", Campus, Tons);
                    dashboard.TxS += start + item + end;
                }
                dashboard.TxS += "]";
             }
            catch (Exception ex)
            {
                Console.WriteLine("Error GetTopStudents " + ex.Message);
            }
        }
        #endregion
        
    }
}