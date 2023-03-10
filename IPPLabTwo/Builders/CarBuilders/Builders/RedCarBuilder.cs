using System.Windows;
using System.Windows.Media;

namespace IPPLabTwo.Builders.CarBuilders
{
    public class RedCarBuilder : NormalCarBuilder
    {
        protected override Brush GetColor()
        {
            return Brushes.DarkRed;
        }
        protected override Thickness GetMargin()
        {
            return new Thickness(690, 354, 10, 30);
        }
    }
}