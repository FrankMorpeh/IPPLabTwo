namespace IPPLabTwo.Builders.CarBuilders
{
    public abstract class NormalCarBuilder : CarBuilder
    {
        sealed protected override double GetHeight()
        {
            return 50;
        }
        sealed protected override double GetWidth()
        {
            return 100;
        }
    }
}