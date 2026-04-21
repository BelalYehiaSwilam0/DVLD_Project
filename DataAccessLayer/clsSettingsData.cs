using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class clsSettingsData
    {
        public static bool FindSettingInfoByID(int SettingID, ref string SettingName, ref short SettingValue)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_FindSettingInfoByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SettingID", SettingID);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            SettingName = reader["Name"].ToString();
                            SettingValue = Convert.ToInt16(reader["Value"]);
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Handle exceptions if needed
            }
            return isFound;
        }

        public static bool FindSettingInfoByName(string SettingName, ref int SettingID, ref short SettingValue)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_FindSettingInfoByName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", SettingName);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            SettingID = Convert.ToInt32(reader["SettingID"]);
                            SettingValue = Convert.ToInt16(reader["Value"]);
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Handle exceptions if needed
            }
            return isFound;
        }

        public static int AddNewSetting(string SettingName, short SettingValue)
        {
            int newSettingID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_AddNewSetting", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", SettingName);
                    command.Parameters.AddWithValue("@Value", SettingValue);

                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        newSettingID = insertedID;
                    }
                }
            }
            catch (Exception)
            {
                // Handle exceptions if needed
            }
            return newSettingID;
        }

        public static bool UpdateSetting(int SettingID, string SettingName, short SettingValue)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_UpdateSetting", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SettingID", SettingID);
                    command.Parameters.AddWithValue("@Name", SettingName);
                    command.Parameters.AddWithValue("@Value", SettingValue);

                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int affected))
                    {
                        rowsAffected = affected;
                    }
                }
            }
            catch (Exception)
            {
                // Handle exceptions if needed
            }
            return (rowsAffected > 0);
        }

        public static DataTable GetAllSettings()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetAllSettings", connection))
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
            catch (Exception)
            {
                // Handle exceptions if needed
            }
            return dt;
        }

        public static bool DeleteSettingByID(int SettingID)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_DeleteSettingByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SettingID", SettingID);

                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int affected))
                    {
                        rowsAffected = affected;
                    }
                }
            }
            catch (Exception)
            {
                // Handle exceptions if needed
            }
            return (rowsAffected > 0);
        }
    }
}
