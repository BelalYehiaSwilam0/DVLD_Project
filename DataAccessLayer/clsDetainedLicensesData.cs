using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class clsDetainedLicensesData
    {
        public static bool GetDetainedLicenseInfoByID(int detainID, ref int licenseID, ref DateTime detainDate, ref decimal fineFees,
                              ref int createdByUserID, ref bool isReleased,
                              ref DateTime? releaseDate, ref int? releasedByUserID, ref int? releaseApplicationID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetDetainedLicenseInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DetainID", detainID);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                licenseID = (int)reader["LicenseID"];
                                detainDate = (DateTime)reader["DetainDate"];
                                fineFees = (decimal)reader["FineFees"];
                                createdByUserID = (int)reader["CreatedByUserID"];
                                isReleased = (bool)reader["IsReleased"];

                                releaseDate = reader["ReleaseDate"] != DBNull.Value ? (DateTime?)reader["ReleaseDate"] : null;
                                releasedByUserID = reader["ReleasedByUserID"] != DBNull.Value ? (int?)reader["ReleasedByUserID"] : null;
                                releaseApplicationID = reader["ReleaseApplicationID"] != DBNull.Value ? (int?)reader["ReleaseApplicationID"] : null;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Handle exception as needed
            }

            return isFound;
        }

        public static bool GetDetainedLicenseByLicenseID(ref int detainID, int licenseID, ref DateTime detainDate, ref decimal fineFees,
                             ref int createdByUserID, ref bool isReleased,
                             ref DateTime? releaseDate, ref int? releasedByUserID, ref int? releaseApplicationID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetDetainedLicenseByLicenseID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@LicenseID", licenseID);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                detainID = (int)reader["DetainID"];
                                detainDate = (DateTime)reader["DetainDate"];
                                fineFees = (decimal)reader["FineFees"];
                                createdByUserID = (int)reader["CreatedByUserID"];
                                isReleased = (bool)reader["IsReleased"];

                                releaseDate = reader["ReleaseDate"] != DBNull.Value ? (DateTime?)reader["ReleaseDate"] : null;
                                releasedByUserID = reader["ReleasedByUserID"] != DBNull.Value ? (int?)reader["ReleasedByUserID"] : null;
                                releaseApplicationID = reader["ReleaseApplicationID"] != DBNull.Value ? (int?)reader["ReleaseApplicationID"] : null;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Handle exception as needed
            }

            return isFound;
        }

        public static int AddNewDetainedLicense(int licenseID, DateTime detainDate, decimal fineFees, int createdByUserID)
        {
            int detainID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewDetainedLicense", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@LicenseID", licenseID);
                        command.Parameters.AddWithValue("@DetainDate", detainDate);
                        command.Parameters.AddWithValue("@FineFees", fineFees);
                        command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);

                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int newID))
                        {
                            detainID = newID;
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Handle exception as needed
            }

            return detainID;
        }

        public static bool UpdateByID(int detainID, int licenseID, DateTime detainDate, decimal fineFees, int createdByUserID)
        {
            bool isUpdated = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateDetainedLicenseByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DetainID", detainID);
                        command.Parameters.AddWithValue("@LicenseID", licenseID);
                        command.Parameters.AddWithValue("@DetainDate", detainDate);
                        command.Parameters.AddWithValue("@FineFees", fineFees);
                        command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            isUpdated = true;
                    }
                }
            }
            catch (Exception)
            {
                // Handle exception as needed
            }

            return isUpdated;
        }

        public static bool ReleaseDetainedLicense(int detainID, int releasedByUserID, ref int releaseApplicationID)
        {
            bool isReleased = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_ReleaseDetainedLicense", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DetainID", detainID);
                        command.Parameters.AddWithValue("@ReleasedByUserID", releasedByUserID);
                        command.Parameters.AddWithValue("@ReleaseApplicationID", releaseApplicationID);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        isReleased = (rowsAffected > 0);
                    }
                }
            }
            catch (Exception)
            {
                // Handle exception as needed
            }

            return isReleased;
        }

        public static bool IsLicenseDetainedByID(int licenseID)
        {
            bool isDetained = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_IsLicenseDetainedByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@LicenseID", licenseID);

                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int detained))
                        {
                            isDetained = (detained == 1);
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Handle exception as needed
            }

            return isDetained;
        }

        public static DataTable GetAllDetainedLicenses()
        {
            DataTable result = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetAllDetainedLicenses", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                result.Load(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Handle exception as needed
            }

            return result;
        }
    }
}
