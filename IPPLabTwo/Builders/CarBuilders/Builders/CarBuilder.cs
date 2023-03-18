using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace IPPLabTwo.Builders.CarBuilders
{
    public abstract class CarBuilder
    {
        protected Rectangle itsCar;

        public Rectangle Car { set { itsCar = value; } }

        public void CreateCar()
        {
            itsCar.Dispatcher.BeginInvoke(new Action(() => { itsCar = new Rectangle(); }));
            //itsCar = new Rectangle();
        }

        public void SetName()
        {
            itsCar.Name = GetName();
            //itsCar.Dispatcher.BeginInvoke(new Action(() => { itsCar.Name = GetName(); }));
        }
        protected abstract string GetName();

        public void SetColor()
        {
            //itsCar.Fill = GetColor();
            itsCar.Dispatcher.BeginInvoke(new Action(() => { itsCar.Fill = GetColor(); }));
        }
        protected abstract ImageBrush GetColor();

        public void SetHeight()
        {
            itsCar.Height = GetHeight();
            //itsCar.Dispatcher.BeginInvoke(new Action(() => { itsCar.Height = GetHeight(); }));
        }
        protected abstract double GetHeight();

        public void SetWidth()
        {
            itsCar.Width = GetWidth();
            //itsCar.Dispatcher.BeginInvoke(new Action(() => { itsCar.Width = GetWidth(); }));
        }
        protected abstract double GetWidth();

        public void SetInitialLocation()
        {
            itsCar.Margin = GetMargin();
            //itsCar.Dispatcher.BeginInvoke(new Action(() => { itsCar.Margin = GetMargin(); }));
        }
        protected abstract Thickness GetMargin();

        public Rectangle BuildCar()
        {
            return itsCar;
        }
    }
}