using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Tastiera_Fonetica
{
    public class BrushToColorConverter : IValueConverter
    {
        private double defaultOpacity = 1;
        public double DefaultOpacity
        {
            set { defaultOpacity = value; }
        }

        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            SolidColorBrush brush = value as SolidColorBrush;
            double opacity;

            if (!Double.TryParse((string)parameter, out opacity))
                opacity = defaultOpacity;

            if (brush == null)
                return Colors.Transparent;
            else
                return Color.FromArgb((byte)(255.0 * brush.Opacity * opacity),
                                      brush.Color.R,
                                      brush.Color.G,
                                      brush.Color.B
                                     );
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            return null;
        }
    }
}
