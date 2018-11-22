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
        /// 
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



    }
}
