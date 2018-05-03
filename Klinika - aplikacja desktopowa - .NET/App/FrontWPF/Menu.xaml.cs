using Doktor_i_Pacjent_2017_XD_Pro.DataLogic;
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
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        private Logic AppLogic;

        public Menu(Logic l)
        {
            InitializeComponent();
            AppLogic = l;
        }

        #region Events
        private void SearchScreenButton_Click(object sender, RoutedEventArgs e)
        {
            (Parent as MainWindow).SetContent(new SearchPage(AppLogic));
        }
        private void AddPatientScreenButton_Click(object sender, RoutedEventArgs e)
        {
            (Parent as MainWindow).SetContent(new PersonPage(AppLogic));
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            (Parent as MainWindow).Close();
        }
        private void SearchDoctor_Click(object sender, RoutedEventArgs e)
        {
            (Parent as MainWindow).SetContent(new SearchDoctorPage(AppLogic));
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int added = 0;
            for (int i = 0; i < 100; i++)
                using (ExampleData data = new ExampleData())
                    if (AppLogic.CreatePatient(data.ExamplePatient()))
                        added++;
            MessageBox.Show($"Dodadno {added} pacjentów.");
            
        }
        #endregion

    }
}
