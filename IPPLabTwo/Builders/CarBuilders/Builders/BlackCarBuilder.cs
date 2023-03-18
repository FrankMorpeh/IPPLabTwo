using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace IPPLabTwo.Builders.CarBuilders
{
    public class BlackCarBuilder : NormalCarBuilder
    {
        private static int itsCarNumber;

        static BlackCarBuilder()
        {
            itsCarNumber = 1;
        }
        protected override string GetName()
        {
            return "BlackCar" + itsCarNumber++;
        }
        protected override ImageBrush GetColor()
        {
            return new ImageBrush() { ImageSource = new BitmapImage(new System.Uri(MainWindow.initialLocation + "\\SystemMedia\\Images\\BlackCar.png")) };
        }
        protected override Thickness GetMargin()
        {
            return new Thickness(690, 100, 10, 30);
        }
    }
}