using System.Windows;
using System.Windows.Media;

namespace IPPLabTwo.Builders.CarBuilders
{
    public class RedCarBuilder : NormalCarBuilder
    {
        private static int itsCarNumber;

        static RedCarBuilder()
        {
            itsCarNumber = 1;
        }
        protected override string GetName()
        {
            return "RedCar" + itsCarNumber++;
        }
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