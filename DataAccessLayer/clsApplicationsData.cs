using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsApplicationsData
    {
        public static bool GetApplicationInfoByID(int applicationID, ref int applicantPersonID, ref DateTime applicationDate,
        ref int applicationTypeID, ref byte applicationStatus, ref DateTime lastStatusDate,
        ref decimal paidFees, ref int createdByUserID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                {
                    string query = "SP_GetApplicationInfoByID";

                    using (SqlCommand command = new SqlCommand(query, connection)) 
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ApplicationID", applicationID);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                applicantPersonID = (int)reader["ApplicantPersonID"];
                                applicationDate = (DateTime)reader["ApplicationDate"];
                                applicationTypeID = (int)reader["ApplicationTypeID"];
                                applicationStatus = (byte)reader["ApplicationStatus"];
                                lastStatusDate = (DateTime)reader["LastStatusDate"];
                                paidFees = (decimal)reader["PaidFees"];
                                createdByUserID = (int)reader["CreatedByUserID"];
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

        public static int AddNewApplication( int applicantPersonID, DateTime applicationDate,
    int applicationTypeID, byte applicationStatus, DateTime lastStatusDate,
    decimal paidFees, int createdByUserID)
        {
            int ApplicantID = -1;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                {

                    string query = "SP_AddNewApplication";

                    using(SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ApplicantPersonID", applicantPersonID);
                        command.Parameters.AddWithValue("@ApplicationDate", applicationDate);
                        command.Parameters.AddWithValue("@ApplicationTypeID", applicationTypeID);
                        command.Parameters.AddWithValue("@ApplicationStatus", applicationStatus);
                        command.Parameters.AddWithValue("@LastStatusDate", lastStatusDate);
                        command.Parameters.AddWithValue("@PaidFees", paidFees);
                        command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);

                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int newApplicantID))
                        {
                            ApplicantID = newApplicantID;
                        }
                    }
                }

            }
            catch (Exception)
            {

                //throw;
            }
         
            return ApplicantID;
        }

        public static bool UpdateApplication(int applicationID, int applicantPersonID, DateTime applicationDate,
     int applicationTypeID, byte applicationStatus, DateTime lastStatusDate,
     decimal paidFees, int createdByUserID)
        {
            int rowsAffected = 0;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                {
                    string query = "SP_UpdateApplication";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ApplicationID", applicationID);
                        command.Parameters.AddWithValue("@ApplicantPersonID", applicantPersonID);
                        command.Parameters.AddWithValue("@ApplicationDate", applicationDate);
                        command.Parameters.AddWithValue("@ApplicationTypeID", applicationTypeID);
                        command.Parameters.AddWithValue("@ApplicationStatus", applicationStatus);
                        command.Parameters.AddWithValue("@LastStatusDate", lastStatusDate);
                        command.Parameters.AddWithValue("@PaidFees", paidFees);
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
          
            return (rowsAffected > 0);
        }
        public static bool DeleteApplicationIDByID(int ApplicationID)
        {
            int rowsAffected = 0;
            try
            {
                using(SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                {
                    string query = "SP_DeleteApplicationIDByID";

                    using(SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);


                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception)
            {

                //throw;
            }

            return (rowsAffected > 0);
        }
        public static bool IsApplicationActivating(string NationalNo,string ClassName)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                {
                    string query = "SP_IsApplicationActivating";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType= CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NationalNo", NationalNo);
                        command.Parameters.AddWithValue("@ClassName", ClassName);

                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null)
                            IsFound = true;
                    }
                }

            }
            catch (Exception)
            {

               // throw;
            }
            return IsFound;
        }

        public static bool IsApplicationExist(int ApplicationID)
        {
            bool isFound = false;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                {
                    string query = "SP_IsApplicationExist";

                    using(SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        connection.Open();

                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            isFound = reader.HasRows;
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
        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {

            //incase the ActiveApplication ID !=-1 return true.
            return (GetActiveApplicationID(PersonID, ApplicationTypeID) != -1);
        }

        public static int GetActiveApplicationID(int ApplicantPersonID, int ApplicationTypeID)
        {
            int ActiveApplicationID = -1;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                {
                    string query = "SP_GetActiveApplicationID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType= CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
                        command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

                        connection.Open();
                        object result = command.ExecuteScalar();


                        if (result != null && int.TryParse(result.ToString(), out int AppID))
                        {
                            ActiveApplicationID = AppID;
                        }
                    }    
                }
            }
            catch (Exception)
            {

                //throw;
            }

            return ActiveApplicationID;
        }

        public static int GetActiveApplicationIDForLicenseClass(int ApplicantPersonID, int ApplicationTypeID, int LicenseClassID)
        {
            int ActiveApplicationID = -1;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                {
                    string query = @"SP_GetActiveApplicationIDForLicenseClass";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
                        command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                        command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);


                        connection.Open();
                        object result = command.ExecuteScalar();


                        if (result != null && int.TryParse(result.ToString(), out int AppID))
                        {
                            ActiveApplicationID = AppID;
                        }
                    }
                }
            }
            catch (Exception)
            {

                //throw;
            }
            return ActiveApplicationID;
        }
        public static bool UpdateStatus(int ApplicationID, short NewStatus)
        {

            int rowsAffected = 0;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDatabaseAccessSettings.ConnectionString))
                {
                    string query = @"SP_UpdateStatus";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure; 
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        command.Parameters.AddWithValue("@NewStatus", NewStatus);
                        command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);


                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

                //throw;
            }
            return (rowsAffected > 0);
        }


    }
}
