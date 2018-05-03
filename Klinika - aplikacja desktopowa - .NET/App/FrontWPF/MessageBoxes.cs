using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Doktor_i_Pacjent_2017_XD_Pro.FrontWPF
{
    public class MessageBoxes
    {
        public static void ShowNotImplemented()
        {
            MessageBox.Show("Not implemented yet.");
        }
        public static void ShowNoSuchPatients()
        {
            MessageBox.Show("Nie znaleziono takich pacjentów");
        }
        public static void ShowNoSuchDoctors()
        {
            MessageBox.Show("Nie znaleziono takich lekarzy.");
        }
        public static void ShowUnknownError(Exception e)
        {
            MessageBox.Show("Wystąpił nieznany błąd, aplikacja moze nie działać poprawnie.\n" + e.Message, "Krytyczny błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public static void ShowWrongData()
        {
            MessageBox.Show("Niepoprawne dane");
        }
        public static void ShowSuccess()
        {
            MessageBox.Show("Operacja zakończona sukcesem.", "Sukces!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public static void ShowFail()
        {
            MessageBox.Show("Operacja zakończona niepowodzeniem.", "Wystąpił błąd.", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        
    }
}
