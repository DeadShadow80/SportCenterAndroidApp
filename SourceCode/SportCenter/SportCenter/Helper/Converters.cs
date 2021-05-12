using System;
using System.Globalization;
using Xamarin.Forms;

namespace SportCenter.Helper {
    public class ReturnMultiConverter : IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            if (values == null) {
                return null;
            }
            return values;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
             throw new NotImplementedException();
        }
    }
}
