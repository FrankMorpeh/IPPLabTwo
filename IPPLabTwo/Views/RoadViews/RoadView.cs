using IPPLabTwo.Controllers.RoadControllers;
using IPPLabTwo.Views.RoadViews.TrafficLightStates;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace IPPLabTwo.Views.RoadViews
{
    public class RoadView
    {
        private RoadController itsRoadController;
        private Grid itsRoad;
        private TrafficLightState itsTrafficLightState;

        public RoadView(RoadController roadController, Grid road)
        {
            itsRoadController = roadController;
            itsRoad = road;
            itsTrafficLightState = new GreenTrafficLightState(itsRoadController, (Ellipse)itsRoad.FindName("redLight")
                , (Ellipse)itsRoad.FindName("greenLight"));
        }


        // Cars display
        public void StartMovement()
        {
            itsRoadController.StartMovement();
        }
        public void ShowNewCarOnRoad(Rectangle car)
        {
            itsRoad.Dispatcher.BeginInvoke(new Action(() => { itsRoad.Children.Add(car); }));
        }
        public void HideCarOnRoad(Rectangle car)
        {
            itsRoad.Dispatcher.BeginInvoke(new Action(() => { itsRoad.Children.Remove(car); }));
        }


        // Traffic lights
        public void SwitchLight()
        {
            itsTrafficLightState.SwitchLight(ref itsTrafficLightState);
        }
    }
}