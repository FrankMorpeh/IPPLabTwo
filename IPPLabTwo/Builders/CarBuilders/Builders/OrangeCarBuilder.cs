using System.Windows;
using System.Windows.Media;

namespace IPPLabTwo.Builders.CarBuilders
{
    public class OrangeCarBuilder : NormalCarBuilder
    {
        protected override Brush GetColor()
        {
            return Brushes.Orange;
        }
        protected override Thickness GetMargin()
        {
            return new Thickness(690, 100, 10, 30);
        }
    }
}