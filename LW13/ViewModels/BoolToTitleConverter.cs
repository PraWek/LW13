using System.Globalization;

namespace CellularCompany.ViewModels;

/// <summary>
/// converts boolean to page title
/// </summary>
public class BoolToTitleConverter : IValueConverter
{
    /// <summary>
    /// converts boolean to title string
    /// </summary>
    /// <param name="value">boolean value</param>
    /// <param name="targetType">target type</param>
    /// <param name="parameter">converter parameter</param>
    /// <param name="culture">culture info</param>
    /// <returns>title string</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (bool)value ? "New Client" : "Edit Client";

    /// <summary>
    /// converts back from string to boolean
    /// </summary>
    /// <param name="value">string value</param>
    /// <param name="targetType">target type</param>
    /// <param name="parameter">converter parameter</param>
    /// <param name="culture">culture info</param>
    /// <returns>not implemented</returns>
    /// <exception cref="NotImplementedException">method not supported</exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}
