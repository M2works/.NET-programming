using Doktor_i_Pacjent_2017_XD_Pro.DataLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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



namespace Doktor_i_Pacjent_2017_XD_Pro.FrontWPF
{
    /// <summary>
    /// Interaction logic for PersonPage.xaml
    /// </summary>
    public partial class PersonPage : Page
    {
        bool DEBUG = false;
        Logic AppLogic;
        ObservableCollection<Visit> list;
        private Patient currpatient;
        private Patient CurrentPatient
        {
            get
            {
                return currpatient;
            }
            set
            {
                DataContext = currpatient = value;
            }
        }
        public PersonPage(Logic l, Patient p = null)
        {
            InitializeComponent();
            EnableTextBoxes(false);
            AppLogic = l;
            if(p == null)
            {
                p = new Patient();
                AppointmentsGrid.Visibility = Visibility.Hidden;
                PatientDataStackPanel.Visibility = Visibility.Hidden;
                NewPatientStackPanel.Visibility = Visibility.Visible;
                i.Visibility = Visibility.Visible;
                EnableTextBoxes(true);
                PeselTextBox.IsEnabled = true;
                NameTextBox.Focus();
            }
            else if(!DEBUG)
            {
                try
                {
                    list = new ObservableCollection<Visit>(AppLogic.ShowPatientVisits(p));
                    VisitsList.ItemsSource = list;
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowUnknownError(ex);
                }
            }
            CurrentPatient = p;
        }      
        private void EnableTextBoxes(bool v)
        {
            NameTextBox.IsEnabled = v;
            SurnameTextbox.IsEnabled = v;
            //PeselTextBox.IsEnabled = v;
            PhoneTextBox.IsEnabled = v;
            CityTextBox.IsEnabled = v;
            PostcodeTextBox.IsEnabled = v;
            StreetTextBox.IsEnabled = v;
            HouseNrTextBox.IsEnabled = v;
            FlatNumberTextBox.IsEnabled = v;
        }

        #region Events
        private void RandomPatientButton_Click(object sender, RoutedEventArgs e)
        {
            using (ExampleData data = new ExampleData())
            {
                CurrentPatient = data.ExamplePatient();
            }
        }
        private void EditPatientData_Click(object sender, RoutedEventArgs e)
        {
            EnableTextBoxes(true);
            NameTextBox.Focus();
        }
        private void VisitAplliementButton_Click(object sender, RoutedEventArgs e)
        {
            (Parent as MainWindow).SetContent(new ApplicationPage(AppLogic, CurrentPatient));
        }
        private void UpdatePatienPatientButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AppLogic.UpdatePatient(CurrentPatient))
                {
                    MessageBoxes.ShowSuccess();
                    EnableTextBoxes(false);
                    CurrentPatient.NotifyFullName();
                }
                else MessageBoxes.ShowFail();
            }
            catch (NotImplementedException)
            {
                MessageBoxes.ShowNotImplemented();
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowUnknownError(ex);
            }
        }
        private void AddNewPatienPatientButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AppLogic.CreatePatient(CurrentPatient))
                {
                    MessageBoxes.ShowSuccess();
                    AppointmentsGrid.Visibility = Visibility.Visible;
                    PatientDataStackPanel.Visibility = Visibility.Visible;
                    NewPatientStackPanel.Visibility = Visibility.Hidden;
                    i.Visibility = Visibility.Hidden;
                    EnableTextBoxes(false);
                    PeselTextBox.IsEnabled = false;
                }

                else MessageBoxes.ShowFail();
            }
            catch (NotImplementedException)
            {
                MessageBoxes.ShowNotImplemented();
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowUnknownError(ex);
            }
        }
        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            (Parent as MainWindow).SetContent(new Menu(AppLogic));
        }
        private void DeletePatientButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AppLogic.DeletePatient(CurrentPatient))
                {
                    MessageBoxes.ShowSuccess();
                    ReturnButton_Click(null, null);
                }
                else MessageBoxes.ShowFail();
            }
            catch(Exception ex)
            {
                MessageBoxes.ShowUnknownError(ex );
            }
        }
        private void VisitsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //MessageBoxes.ShowNotImplemented();
            if (VisitsList.SelectedItem == null) return;
            Visit v = VisitsList.SelectedItem as Visit;
            if (v.Patient.PESELNumber == CurrentPatient.PESELNumber)
            {
                try
                {
                    if (AppLogic.DeleteVisit(v.DT, v.Doctor, v.Patient))
                    {
                        MessageBoxes.ShowSuccess();
                        //v.Patient = null;
                        list.Remove(v);
                    }
                    else MessageBoxes.ShowFail();
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowUnknownError(ex);
                }
            }
        }
        #endregion

    }
}
