using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace InvoiceApp.WPF.Converters;

/// <summary>Converts a non-empty string to Visibility.Visible</summary>
public class StringToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => string.IsNullOrEmpty(value?.ToString()) ? Visibility.Collapsed : Visibility.Visible;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}

/// <summary>Inverts a bool value</summary>
public class BoolInverseConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => value is bool b ? !b : value;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => value is bool b ? !b : value;
}

/// <summary>Returns one of two strings based on bool. Param = "TrueValue|FalseValue"</summary>
public class LoadingConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isLoading && parameter is string parts)
        {
            var split = parts.Split('|');
            return isLoading ? split[0] : split.Length > 1 ? split[1] : parts;
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
