using Doktor_i_Pacjent_2017_XD_Pro.DataLogic;
using Doktor_i_Pacjent_2017_XD_Pro.DBConnectionLogic.SurgeryModel;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AppliementPage.xaml
    /// </summary>
    public partial class ApplicationPage : Page
    {
        private Logic AppLogic;
        private DataLogic.Patient patient;
        public int Page { get; set; }
        public ApplicationPage(Logic l, DataLogic.Patient p = null)
        {
            InitializeComponent();
            AppLogic = l;
            patient = p;
            SpecializationCombo.ItemsSource = Enum.GetValues(typeof(Specialization)).Cast<Specialization>();
            SpecializationCombo.SelectedIndex = 0;
            SurnameTextBox_Search.Focus();
            VisitDatePicker.DisplayDateStart = DateTime.Today;
        }

        #region Events
        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            (Parent as MainWindow).SetContent(new Menu(AppLogic));
        }
        private void AppliementListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Visit v = VisitsListView.SelectedItem as Visit;
            if (v.Patient == null)
            {

                try
                {
                    if (AppLogic.AddVisit(v.DT, patient, v.Doctor))
                    {
                        MessageBoxes.ShowSuccess();
                        v.Patient = patient;
                    }
                    else MessageBoxes.ShowFail();
                }
                catch(Exception ex )
                {
                    MessageBoxes.ShowUnknownError(ex);
                }
            }
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<Visit> list = null;
            
            try
            {
                DateTime dt = VisitDatePicker.SelectedDate != null ? VisitDatePicker.SelectedDate.Value : DateTime.Today;
                string startingHour = (HoursFromCombo.SelectedItem as ComboBoxItem).Content.ToString();
                string endingHour = (HoursToCombo.SelectedItem as ComboBoxItem).Content.ToString();
                string surname = SurnameTextBox_Search.Text;
                string city = CityTextBox_Search.Text;
                Specialization spec = (Specialization)SpecializationCombo.SelectedItem;
                bool freeOnly = FreeOnlyCheckbox.IsChecked.Value;
                int itemsPerPage = int.Parse((VisitsNumberCombo.SelectedItem as ComboBoxItem).Content.ToString());
                list = AppLogic.ShowVisits(dt, startingHour, endingHour, surname, city, spec, freeOnly, Page, itemsPerPage);
            }
            catch (NotImplementedException)
            {
                MessageBoxes.ShowNotImplemented();
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowUnknownError(ex);
            }
            if (list == null)
            {
                MessageBoxes.ShowWrongData();
            }
            else
            {
                VisitsListView.Items.Clear();
                foreach(var x in list)
                    VisitsListView.Items.Add(x);
            }
            if (VisitsListView.Items.Count == 0) MessageBoxes.ShowNoSuchPatients();
        }
        private void PatientPageReturnButton_Click(object sender, RoutedEventArgs e)
        {
            (Parent as MainWindow).SetContent(new PersonPage(AppLogic, patient));
        }
        #endregion
    }
}
