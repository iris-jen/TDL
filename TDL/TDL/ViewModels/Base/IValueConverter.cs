using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels.Base
{
    public abstract partial class BaseViewModel: IValueConverter
    {
        private static object GenericConverter(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (targetType == typeof(int))
                {
                    switch (value)
                    {
                        case string _ when int.TryParse(value.ToString(), out var intRet):
                            return intRet;
                        case double d:
                            return (int)d;
                        default:
                            return -999;
                    }
                }
                if (targetType == typeof(double))
                {
                    switch (value)
                    {
                        case string _ when double.TryParse(value.ToString(), out var doubleRet):
                            return doubleRet;
                        case int i:
                            return (double)i;
                        case string _:
                            return -999;

                    }
                }
                if (targetType == typeof(string))
                    return value.ToString();

                throw new Exception("No matching parsing logic in converter");
            }
            catch (Exception)
            {
                return null;
            }
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return GenericConverter(value, targetType, parameter, culture);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return GenericConverter(value, targetType, parameter, culture);
        }
    }
}
