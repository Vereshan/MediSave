using Medisave;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;

public class DatabaseHelper
{
    private readonly string _connectionString;

    public DatabaseHelper()
    {
        _connectionString = ConfigurationManager.ConnectionStrings["MedisaveDB"].ConnectionString;
    }

    public SqlConnection GetConnection()
    {
        return new SqlConnection(_connectionString);
    }

    public bool TestConnection()
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                return true;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Connection failed: {ex.Message}", "DB Error");
            return false;
        }
    }

    public int InsertCoreInfo(CoreInfo coreInfo)
    {
        string query = @"
        INSERT INTO Core_Info (Prefix, Full_Name, Address, Tel_No, Gender, Date_Of_Birth, Date_Modified)
        VALUES (@Prefix, @FullName, @Address, @TelNo, @Gender, @DateOfBirth, @DateModified);
        SELECT CAST(SCOPE_IDENTITY() AS int);";

        try
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Prefix", (object)coreInfo.Prefix ?? DBNull.Value);
                command.Parameters.AddWithValue("@FullName", (object)coreInfo.FullName ?? DBNull.Value);
                command.Parameters.AddWithValue("@Address", (object)coreInfo.Address ?? DBNull.Value);
                command.Parameters.AddWithValue("@TelNo", coreInfo.TelNo);
                command.Parameters.AddWithValue("@Gender", (object)coreInfo.Gender ?? DBNull.Value);
                command.Parameters.AddWithValue("@DateOfBirth", (object)coreInfo.DateOfBirth ?? DBNull.Value);
                command.Parameters.AddWithValue("@DateModified", (object)coreInfo.DateModified ?? DBNull.Value);

                connection.Open();
                int newId = (int)command.ExecuteScalar(); // gets the new auto-generated ID
                return newId;
            }
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show($"Error inserting record: {ex.Message}");
            return -1; // or throw
        }
    }

    public bool InsertMedicalInfo(Medical_Info info)
    {
        string query = @"INSERT INTO medical_Info (ID, Medical_Aid, Occupation, Employer, Allergies, Special_Features, Date_Modified)
                     VALUES (@ID, @MedicalAid, @Occupation, @Employer, @Allergies, @SpecialFeatures, @DateModified)";

        using (SqlConnection conn = GetConnection())
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@ID", info.ID);
            cmd.Parameters.AddWithValue("@MedicalAid", info.MedicalAid ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Occupation", info.Occupation ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Employer", info.Employer ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Allergies", info.Allergies ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@SpecialFeatures", info.SpecialFeatures ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@DateModified", info.DateModified ?? (object)DBNull.Value);

            conn.Open();
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }
    }


}

