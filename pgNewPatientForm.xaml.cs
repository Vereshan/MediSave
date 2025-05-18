using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Medisave
{
    /// <summary>
    /// Interaction logic for pgNewPatientForm.xaml
    /// </summary>
    public partial class pgNewPatientForm : Page
    {
        public pgNewPatientForm()
        {
            InitializeComponent();
            //prefix population in combo box
            List<string> Prefixes = new List<String> { "Mr", "Ms", "Mrs" };
            cmbPrefix.ItemsSource = Prefixes;
            //Sex population in combo box
            List<string> Sex = new List<String> { "Male", "Female", "Other" };
            cmbSex.ItemsSource = Sex;

        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Build CoreInfo object from user input
                CoreInfo coreInfo = new CoreInfo
                {
                    Prefix = cmbPrefix.SelectedItem?.ToString(),
                    FullName = txtFullName.Text,
                    Address = txtAddress.Text,
                    TelNo = txtTeleNo.Text,
                    Gender = cmbSex.SelectedItem?.ToString(),
                    DateOfBirth = dtpDateOfBirth.SelectedDate,
                    DateModified = DateTime.Now
                };

                // Insert into DB
                DatabaseHelper db = new DatabaseHelper();
                int newId = db.InsertCoreInfo(coreInfo);

                if (newId > 0)
                {
                    // 2. Create Medical_Info object using new ID
                    Medical_Info medicalInfo = new Medical_Info
                    {
                        ID = newId,  // foreign key
                        MedicalAid = txtMedicalAid.Text,
                        Occupation = txtOccupation.Text,
                        Employer = txtEmployer.Text,
                        Allergies = txtAllergies.Text,
                        SpecialFeatures = txtSpecialFeatures.Text,
                        DateModified = DateTime.Now
                    };

                    bool medicalInserted = db.InsertMedicalInfo(medicalInfo);
                    if (newId > 0)
                    {
                        MessageBox.Show($"Patient inserted successfully! New ID: {newId}", "Success");
                    }
                    else
                    {
                        MessageBox.Show("Failed to insert patient.", "Error");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Something went wrong: {ex.Message}", "Error");
            }
        }



    }
}
