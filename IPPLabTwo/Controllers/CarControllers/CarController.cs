using IPPLabTwo.Builders.CarBuilders;
using IPPLabTwo.Controllers.RoadControllers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Shapes;

namespace IPPLabTwo.Controllers.CarControllers
{
    public class CarController
    {
        private LinkedList<Rectangle> itsCars;
        private CarBuilder itsCarBuilder;
        private System.Timers.Timer itsCarMoveTimer;
        private Thread itsCarMoveTimerThread;
        private ManualResetEvent itsMovementManualResetEvent;
        private bool itsStopRequested; // used for the cases when the traffic light is read, but the cars haven't reached the stop point
        private event Action<Rectangle> itsCarAddedEvent;
        private event Action<Rectangle> itsCarRemovedEvent;

        public CarController(CarBuilder carBuilder)
        {
            itsCars = new LinkedList<Rectangle>();

            itsCarBuilder = carBuilder;

            itsCarMoveTimer = new System.Timers.Timer(1000);
            itsCarMoveTimer.Elapsed += CarMoveTimer_Elapsed;
            itsCarMoveTimerThread = new Thread(itsCarMoveTimer.Start);

            itsMovementManualResetEvent = new ManualResetEvent(false);
            itsMovementManualResetEvent.Set();

            itsStopRequested = false;
        }
        public void SetRoadControllerEvents(RoadController roadController)
        {
            itsCarAddedEvent += roadController.CarEnteredRoad;
            itsCarRemovedEvent += roadController.CarLeftRoad;
        }


        // Data handling
        public void AddCar()
        {
            itsCars.AddLast(CarManufacturer.BuildCar(itsCarBuilder));
            itsCarAddedEvent(itsCars.Last.Value);
        }
        public void RemoveFirstCar()
        {
            itsCarRemovedEvent(itsCars.First.Value);
            itsCars.RemoveFirst();
        }


        // Movement
        public void StartMovementAsParallel()
        {
            itsCarMoveTimerThread.Start();
        }
        private void CarMoveTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            itsMovementManualResetEvent.WaitOne();

            Thickness carLocation = default;
            bool removeFirstCar = false;
            foreach (Rectangle car in itsCars)
            {
                carLocation = car.Margin;
                if (itsStopRequested && carLocation.Left > -490)
                {
                    carLocation.Left -= 100;
                    if (carLocation.Left == -590)
                        removeFirstCar = true;
                }
                else
                    itsMovementManualResetEvent.Reset();
            }

            AddCar(); // add a car in the end 
            if (removeFirstCar)
                RemoveFirstCar(); // remove the first car if it's passed through the traffic light
        }
        public void StopMovement()
        {
            if (itsCars.First.Value.Margin.Left > -490)
                itsStopRequested = true;
            else
                itsMovementManualResetEvent.Reset();
        }
        public void AllowMovement()
        {
            itsStopRequested = false;
            itsMovementManualResetEvent.Set();
        }
    }
}