using System.Windows;
using System.Windows.Media;

namespace IPPLabTwo.Builders.CarBuilders
{
    public class OrangeCarBuilder : NormalCarBuilder
    {
        private static int itsCarNumber;

        static OrangeCarBuilder()
        {
            itsCarNumber = 1;
        }
        protected override string GetName()
        {
            return "OrangeCar" + itsCarNumber++;
        }
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