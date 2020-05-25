using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace SelfMonitoringApp.Views
{
    /// <summary>
    /// inverterBoolConverterinooooo its a fun name no?
    /// </summary>
    public class BoolInverterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
    }

    public class GraphListBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "List Of Items" : "Grid";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return  value.ToString() == "List Of Items";
        }
    }

}
