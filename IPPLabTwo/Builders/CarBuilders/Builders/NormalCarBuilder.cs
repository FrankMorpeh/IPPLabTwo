namespace IPPLabTwo.Builders.CarBuilders
{
    public abstract class NormalCarBuilder : CarBuilder
    {
        sealed protected override double GetHeight()
        {
            return 25;
        }
        sealed protected override double GetWidth()
        {
            return 50;
        }
    }
}