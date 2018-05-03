using Doktor_i_Pacjent_2017_XD_Pro.DataLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for SeatchPage.xaml
    /// </summary>
    public partial class SearchPage : Page, INotifyPropertyChanged
    {
        Logic AppLogic;
        private int page;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Page
        {
            get
            {
                return page;
            }
            set
            {
                if (value != this.page)
                {
                    page = value;
                    NotifyPropertyChanged("Page");
                }
            }
        }
        public SearchPage(Logic l)
        {
            InitializeComponent();
            AppLogic = l;
            DataContext = this;
            Page = 1;
            DisableNext();
            PeselTextBox_Search.Focus();            
        }
        private void Search(int page)
        {
                int count = 10;
                IEnumerable<Patient> list = null;
                try
                {
                    count = int.Parse((PatientsNumberCombo.SelectedItem as ComboBoxItem).Content.ToString());
                    list = AppLogic.SearchPatient(PeselTextBox_Search.Text, SurnameTextBox_Search.Text, NameTextBox_Search.Text, CityTextBox_Search.Text, page - 1, int.Parse((PatientsNumberCombo.SelectedItem as ComboBoxItem).Content.ToString()));
                }
                catch (NotImplementedException)
                {
                    MessageBoxes.ShowNotImplemented();
                    DisableNext();
                    return;
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowUnknownError(ex);
                    DisableNext();
                    return;
                }
                if (list == null)
                {
                    MessageBoxes.ShowWrongData();
                    DisableNext();
                    return;
                }
                FoundPatientsListView.ItemsSource = list;
                if (FoundPatientsListView.Items.Count == 0) MessageBoxes.ShowNoSuchPatients();
                if (FoundPatientsListView.Items.Count < count)
                    DisableNext();
                else
                    EnableNext();
                if (FoundPatientsListView.Items.Count == 0) MessageBoxes.ShowNoSuchPatients();
        }
        private void DisableNext()
        {
            PlusSearchTextBlock.Foreground = new SolidColorBrush(Colors.Gray);
            PlusSearchTextBlock.Cursor = Cursors.Arrow;
        }
        private void EnableNext()
        {
            PlusSearchTextBlock.Foreground = new SolidColorBrush(Colors.Blue);
            PlusSearchTextBlock.Cursor = Cursors.Hand;
        }

        #region Events
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            EnableNext();
            Page = 1;
            Search(Page);
        }
        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            (Parent as MainWindow).SetContent(new Menu(AppLogic));
        }
        private void FoundPatientsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (FoundPatientsListView.SelectedItem != null) (Parent as MainWindow).SetContent(new PersonPage(AppLogic, FoundPatientsListView.SelectedItem as Patient));
        }
        private void RandomButton_Click(object sender, RoutedEventArgs e)
        {
            using (ExampleData data = new ExampleData())
            {
                FoundPatientsListView.ItemsSource = data.ExamplePatient(int.Parse((PatientsNumberCombo.SelectedItem as ComboBoxItem).Content.ToString()));
            }

                
        }
        private void TextBlockPlus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (((sender as TextBlock).Foreground as SolidColorBrush).Color == Colors.Blue)
                Search(++Page);
        }
        private void TextBlockMinus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Page > 1)
            {
                EnableNext();
                Search(--Page);
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        #endregion

        protected void NotifyPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
