using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Net;
using System.Security.Policy;
using System.Data;

namespace DataAccessLayer
{
    public class clsDataAccessPeople
    {
        
        public static bool FindPersonByID(ref int PersonID , ref string NationalNo , ref string FirstName, ref string SecondName,ref string ThirdName,ref string LastName,
            ref DateTime DateOfBirth, ref byte Gender, ref string Address,ref string Phone, ref string Email, ref int NationalityCountryID,ref string ImagePath)
        {
            bool IsFound = false;
            try
            {
                using(SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                {
                    string query = "SP_FindPersonByID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType= CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", PersonID);

                        connection.Open();

                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                NationalNo = (string)reader["NationalNo"];
                                FirstName = (string)reader["FirstName"];
                                SecondName = (string)reader["SecondName"];
                                ThirdName = reader.IsDBNull(reader.GetOrdinal("ThirdName")) ? string.Empty : reader.GetString(reader.GetOrdinal("ThirdName"));
                                LastName = (string)reader["LastName"];
                                DateOfBirth = (DateTime)reader["DateOfBirth"];
                                Gender = (Byte)reader["Gender"];
                                Address = (string)reader["Address"];
                                Phone = (string)reader["Phone"];
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? string.Empty : reader.GetString(reader.GetOrdinal("Email"));
                                NationalityCountryID = (int)reader["NationalityCountryID"];
                                ImagePath = reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? string.Empty : reader.GetString(reader.GetOrdinal("ImagePath"));
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                //throw;
            }

            return IsFound;
        }

        public static bool FindPersonByNationalNo(ref int PersonID, ref string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName,
            ref DateTime DateOfBirth, ref byte Gender, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool IsFound = false;
            try
            {
                using(SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                {
                    string query = "SP_FindPersonByNationalNo";

                    using(SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NationalNo", NationalNo);

                        connection.Open(); 

                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                PersonID = (int)reader["PersonID"];
                                NationalNo = (string)reader["NationalNo"];
                                FirstName = (string)reader["FirstName"];
                                SecondName = (string)reader["SecondName"];
                                ThirdName = reader.IsDBNull(reader.GetOrdinal("ThirdName")) ? string.Empty : reader.GetString(reader.GetOrdinal("ThirdName"));
                                LastName = (string)reader["LastName"];
                                DateOfBirth = (DateTime)reader["DateOfBirth"];
                                Gender = (Byte)reader["Gender"];
                                Address = (string)reader["Address"];
                                Phone = (string)reader["Phone"];
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? string.Empty : reader.GetString(reader.GetOrdinal("Email"));
                                NationalityCountryID = (int)reader["NationalityCountryID"];
                                ImagePath = reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? string.Empty : reader.GetString(reader.GetOrdinal("ImagePath"));
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                //throw;
            }
            return IsFound;
        }

        public static bool IsPersonExistByID(int PersonID)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                {
                    string query = "SP_IsPersonExist";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", PersonID);

                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null)
                            IsFound = true;
                    }
                }
            }
            catch (Exception)
            {

                //throw;
            }
            return IsFound;
        }

        public static bool IsPersonExistByNationalNo(string NationalNo)
        {
            bool IsFound = false;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                {
                    string query = "SP_IsPersonExistByNationalNo";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NationalNo", NationalNo);

                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null)
                            IsFound = true;
                    }
                }    
            }
            catch (Exception)
            {

                //throw;
            }
            return IsFound;
        }

        public static int AddNewPerson(string nationalNo, string firstName, string secondName, string thirdName,
          string lastName, DateTime dateOfBirth, byte gender, string address, string phone,
          string email, int nationalityCountryID, string imagePath)
        {
            int personID = -1;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                {
                    string query = "SP_AddPerson";

                    using(SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType= CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NationalNo", nationalNo);
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@SecondName", secondName);
                        command.Parameters.AddWithValue("@ThirdName", string.IsNullOrEmpty(thirdName) ? (object)DBNull.Value : thirdName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                        command.Parameters.AddWithValue("@Gender", gender);
                        command.Parameters.AddWithValue("@Address", address);
                        command.Parameters.AddWithValue("@Phone", phone);
                        command.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email);
                        command.Parameters.AddWithValue("@NationalityCountryID", nationalityCountryID);
                        command.Parameters.AddWithValue("@ImagePath", string.IsNullOrEmpty(imagePath) ? (object)DBNull.Value : imagePath);

                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int newPerson))
                        {
                            personID = newPerson;
                        }
                    }
                }
            }
            catch (Exception)
            {

                //throw;
            }
            return personID;

        }

        public static bool UpdatePersonInfoByID(int personID, string nationalNo, string firstName, string secondName, string thirdName,
   string lastName, DateTime dateOfBirth, byte gender, string address, string phone,
   string email, int nationalityCountryID, string imagePath)
        {
            bool isUpdated = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                {
                    string query = "SP_UpdatePersonByID";

                    using(SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", personID);
                        command.Parameters.AddWithValue("@NationalNo", nationalNo);
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@SecondName", secondName);
                        command.Parameters.AddWithValue("@ThirdName", string.IsNullOrEmpty(thirdName) ? (object)DBNull.Value : thirdName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                        command.Parameters.AddWithValue("@Gender", gender);
                        command.Parameters.AddWithValue("@Address", address);
                        command.Parameters.AddWithValue("@Phone", phone);
                        command.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email);
                        command.Parameters.AddWithValue("@NationalityCountryID", nationalityCountryID);
                        command.Parameters.AddWithValue("@ImagePath", string.IsNullOrEmpty(imagePath) ? (object)DBNull.Value : imagePath);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                            isUpdated = true;
                    }
                }    
            }
            catch (Exception)
            {

                //throw;
            }
            return isUpdated;
        }

        public static bool DeletePersonByID(int personID)
        {
            bool IsDeleted = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                {
                    string query = "SP_DeletePersonByID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", personID);

                        connection.Open();
                        IsDeleted = command.ExecuteNonQuery() > 0;
                    }
                }

            }
            catch (Exception)
            {

                //throw;
            }
            return IsDeleted;
        }

        public static DataTable GetAllPeople()
        {
            DataTable result = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                {
                    string query = "SP_GetAllPeople";
                    using(SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();

                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                result.Load(reader);
                            }
                            reader.Close();
                        }
                    }
                }
            }
            catch (Exception)
            {

                //throw;
            }
            return result;
        }
    }
}
