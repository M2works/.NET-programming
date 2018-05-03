using Doktor_i_Pacjent_2017_XD_Pro.DataLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Doktor_i_Pacjent_2017_XD_Pro.FrontWPF
{
    public class VisitBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if (!(value is Patient)) return new SolidColorBrush(Colors.Gold);
            return new SolidColorBrush((value  == null) ? Color.FromRgb(35,104,208) : Color.FromRgb(70,70,70));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class VisitInvertedBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if (!(value is Patient)) return new SolidColorBrush(Colors.Gold);
            return new SolidColorBrush((value != null) ? Color.FromRgb(35, 104, 208) : Color.FromRgb(70, 70, 70));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class SpecializationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if (!(value is Patient)) return new SolidColorBrush(Colors.Gold);

            return (SpecializationPL)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class PreviousFontColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if (!(value is Patient)) return new SolidColorBrush(Colors.Gold);

            return new SolidColorBrush((int)value > 1 ? Colors.Blue : Colors.Gray);
            //return (int)value > 1 ? Colors.Blue : Colors.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class PreviousCursorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if (!(value is Patient)) return new SolidColorBrush(Colors.Gold);

            return (int)value > 1 ? Cursors.Hand : Cursors.Arrow;
            //return (int)value > 1 ? Colors.Blue : Colors.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
