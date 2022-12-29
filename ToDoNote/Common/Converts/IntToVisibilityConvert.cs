using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ToDoNote.Common.Converts
{
    /// <summary>
    /// 转换前端的数据，可以这个接口可以把数据转换成想要的类型，自己写方法
    /// </summary>
    public class IntToVisibilityConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && int.TryParse(value.ToString(), out int res))
            {
                if (res == 0)
                    return Visibility.Visible;
            }
            return Visibility.Hidden;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
