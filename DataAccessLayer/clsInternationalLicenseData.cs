using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class clsInternationalLicenseData
    {
        public static int _AddNewInternationalLicense(int applicationID, int driverID, int issuedUsingLocalLicenseID, DateTime issueDate, DateTime expirationDate, bool isActive, int createdByUserID)
        {
            int internationalLicenseID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewInternationalLicense", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ApplicationID", applicationID);
                        command.Parameters.AddWithValue("@DriverID", driverID);
                        command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", issuedUsingLocalLicenseID);
                        command.Parameters.AddWithValue("@IssueDate", issueDate);
                        command.Parameters.AddWithValue("@ExpirationDate", expirationDate);
                        command.Parameters.AddWithValue("@IsActive", isActive);
                        command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);

                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            internationalLicenseID = insertedID;
                        }
                    }
                }
            }
            catch (Exception)
            {
                //throw;
            }
            return internationalLicenseID;
        }

        public static bool UpdateInternationalLicense(int internationalLicenseID, int applicationID, int driverID, int issuedUsingLocalLicenseID, DateTime issueDate, DateTime expirationDate, bool isActive, int createdByUserID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateInternationalLicense", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@InternationalLicenseID", internationalLicenseID);
                        command.Parameters.AddWithValue("@ApplicationID", applicationID);
                        command.Parameters.AddWithValue("@DriverID", driverID);
                        command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", issuedUsingLocalLicenseID);
                        command.Parameters.AddWithValue("@IssueDate", issueDate);
                        command.Parameters.AddWithValue("@ExpirationDate", expirationDate);
                        command.Parameters.AddWithValue("@IsActive", isActive);
                        command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);

                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                //throw;
            }

            return rowsAffected > 0;
        }

        public static bool DeleteInternationalLicenseByID(int internationalLicenseID)
        {
            bool isDeleted = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_DeleteInternationalLicenseByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@InternationalLicenseID", internationalLicenseID);

                        connection.Open();
                        isDeleted = command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception)
            {
                //throw;
            }

            return isDeleted;
        }

        public static bool FindInternationalLicenseByID(int internationalLicenseID, ref int applicationID, ref int driverID, ref int issuedUsingLocalLicenseID, ref DateTime issueDate, ref DateTime expirationDate, ref bool isActive, ref int createdByUserID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_FindInternationalLicenseByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@InternationalLicenseID", internationalLicenseID);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                applicationID = (int)reader["ApplicationID"];
                                driverID = (int)reader["DriverID"];
                                issuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                                issueDate = (DateTime)reader["IssueDate"];
                                expirationDate = (DateTime)reader["ExpirationDate"];
                                isActive = (bool)reader["IsActive"];
                                createdByUserID = (int)reader["CreatedByUserID"];
                            }
                            else
                            {
                                isFound = false;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                //throw;
            }

            return isFound;
        }

        public static bool IsInternationalLicenseActive(int driverID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_IsInternationalLicenseActive", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DriverID", driverID);

                        connection.Open();

                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            isFound = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                //throw;
            }

            return isFound;
        }

        public static DataTable InternationalLicenses(int driverID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetInternationalLicensesByDriverID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DriverID", driverID);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                //throw;
            }

            return dt;
        }

        public static DataTable GetAllInternationalLicenses()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetAllInternationalLicenses", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                //throw;
            }

            return dt;
        }
    }
}
