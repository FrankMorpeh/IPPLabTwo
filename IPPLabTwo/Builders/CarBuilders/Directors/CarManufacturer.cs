using System.Windows.Shapes;

namespace IPPLabTwo.Builders.CarBuilders
{
    public static class CarManufacturer
    {
        public static Rectangle BuildCar(CarBuilder carBuilder)
        {
            carBuilder.CreateCar();
            carBuilder.SetColor();
            carBuilder.SetHeight();
            carBuilder.SetWidth();
            carBuilder.SetInitialLocation();
            return carBuilder.BuildCar();
        }
    }
}