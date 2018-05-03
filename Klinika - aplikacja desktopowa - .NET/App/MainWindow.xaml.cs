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
using Doktor_i_Pacjent_2017_XD_Pro;
using Doktor_i_Pacjent_2017_XD_Pro.DataLogic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Doktor_i_Pacjent_2017_XD_Pro.FrontWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Logic AppLogic = new Logic();
        public MainWindow()
        {
            InitializeComponent();
            Menu m = new Menu(AppLogic);
            Content = m;
        }
        public void SetContent(Page p)
        {
            Content = p;
        }
    }
}
