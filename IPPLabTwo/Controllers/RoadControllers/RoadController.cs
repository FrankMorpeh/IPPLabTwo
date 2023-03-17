using IPPLabTwo.Builders.CarBuilders;
using IPPLabTwo.Views.RoadViews;
using IPPLabTwo.Views.RoadViews.TrafficLightStates;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace IPPLabTwo.Controllers.RoadControllers
{
    public class RoadController
    {
        private MainWindow itsContent;
        private TrafficLightState itsTrafficLightState;
        private CarBuilder itsCarBuilder;
        private System.Timers.Timer itsCarMoveTimer;
        private Thread itsCarMoveTimerThread;
        private Mutex itsCarsMutex;
        private ManualResetEvent itsMovementManualResetEvent;
        private bool itsStopRequested; // used for the cases when the traffic light is read, but the cars haven't reached the stop point
        private bool addNew = false;
        private bool itsResetMovementEvent = false;

        public RoadController(MainWindow content)
        {
            itsContent = content;

            itsTrafficLightState = new GreenTrafficLightState(this, (Ellipse)itsContent.road.FindName("redLight")
                , (Ellipse)itsContent.road.FindName("greenLight"));

            itsCarBuilder = new OrangeCarBuilder();
            itsCarBuilder.Car = new Rectangle();
            itsCarMoveTimer = new System.Timers.Timer(1000);
            itsCarMoveTimer.Elapsed += CarMoveTimer_Elapsed;
            itsCarMoveTimerThread = new Thread(itsCarMoveTimer.Start);
            itsCarsMutex = new Mutex();

            itsMovementManualResetEvent = new ManualResetEvent(false);
            itsMovementManualResetEvent.Set();

            itsStopRequested = false;
        }

        // Data handling
        public void AddCar()
        {
            itsCarsMutex.WaitOne();
            Rectangle newCar = CarManufacturer.BuildCar(itsCarBuilder);
            itsContent.Dispatcher.BeginInvoke(new Action(() => 
            {
                itsContent.road.Children.Add(newCar);
                itsContent.RegisterName(newCar.Name, newCar);
            }));
            itsCarsMutex.ReleaseMutex();
        }
        public void RemoveFirstCar()
        {
            itsCarsMutex.WaitOne();
            itsContent.Dispatcher.BeginInvoke(new Action(() =>
            {
                itsContent.road.Children.RemoveAt(2);
            }));
            itsCarsMutex.ReleaseMutex();
        }


        // Movement
        public void StartMovementAsParallel()
        {
            itsCarMoveTimerThread.Start();
        }
        public void StartMovement()
        {
            itsCarMoveTimer.Start();
        }
        private void CarMoveTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            itsCarsMutex.WaitOne();

            itsContent.Dispatcher.BeginInvoke(new Action(() => 
            {
                itsMovementManualResetEvent.WaitOne();

                Thickness carLocation = default;
                bool removeFirstCar = false;
                bool someCarIsInFrontOfTrafficLight = SomeCarIsInFrontOfTrafficLight();

                foreach (object child in itsContent.road.Children)
                {
                    //carLocation = car.Margin; 
                    if (child is Rectangle)
                    {
                        Rectangle car = (Rectangle)child;
                        carLocation = car.Margin;
                        if (!itsStopRequested) // if the traffic light is green, then just let it go
                        {
                            carLocation.Left -= 100;
                            car.Margin = carLocation;
                            if (carLocation.Left == -1010)
                                removeFirstCar = true;
                        }
                        else
                        {
                            if (carLocation.Left <= -810 || !someCarIsInFrontOfTrafficLight)
                            {
                                carLocation.Left -= 100;
                                car.Margin = carLocation;
                                if (carLocation.Left == -1010)
                                    removeFirstCar = true;
                            }
                            else
                            {
                                if (!removeFirstCar)
                                {
                                    itsResetMovementEvent = true;
                                    break;
                                }
                            }
                        }
                    }
                }

                if (addNew == false && itsResetMovementEvent == false)
                    addNew = true;
                else if (addNew == true)
                {
                    AddCar(); // add a car in the end 
                    addNew = false;
                }
                if (removeFirstCar)
                    RemoveFirstCar(); // remove the first car if it's passed through the traffic light
                if (itsResetMovementEvent)
                {
                    itsCarMoveTimer.Stop();
                    itsResetMovementEvent = false;
                    for (int i = 1; i <= 3; i++)
                    {
                        Rectangle lastCar = ((Rectangle)(itsContent.road.Children[2]));
                        Thickness lastCarLocation = lastCar.Margin;
                        lastCarLocation.Left -= 100;
                        lastCar.Margin = lastCarLocation;
                        Thread.Sleep(1000);
                    }
                    RemoveFirstCar();
                }
            }));

            itsCarsMutex.ReleaseMutex();
        }
        private bool SomeCarIsInFrontOfTrafficLight()
        {
            bool someCarIsInFrontOfTrafficLight = false;
            foreach (object roadObject in itsContent.road.Children)
            {
                if (roadObject is Rectangle)
                {
                    Rectangle car = (Rectangle)roadObject;
                    if (car.Margin.Left == -710)
                    {
                        someCarIsInFrontOfTrafficLight = true;
                        break;
                    }
                }
            }
            return someCarIsInFrontOfTrafficLight;
        }
        public void StopMovement()
        {
            //if (((Rectangle)(itsContent.road.Children[2])).Margin.Left > -490)
            //    itsStopRequested = true;
            //else
            //    itsMovementManualResetEvent.Reset();
            itsStopRequested = true;
        }
        public void AllowMovement()
        {
            itsStopRequested = false;
            itsCarMoveTimer.Start();
            //itsMovementManualResetEvent.Set();
        }

        public void SwitchLight()
        {
            itsTrafficLightState.SwitchLight(ref itsTrafficLightState);
        }
    }
}