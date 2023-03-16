using System.Windows.Shapes;
using System.Windows;
using IPPLabTwo.Controllers.RoadControllers;

namespace IPPLabTwo.Views.RoadViews.TrafficLightStates
{
    public class GreenTrafficLightState : TrafficLightState
    {
        public GreenTrafficLightState(RoadController roadController, Ellipse redLight, Ellipse greenLight) 
            : base(roadController, redLight, greenLight) { }

        public override void SwitchLight(ref TrafficLightState trafficLightState)
        {
            itsGreenLight.Visibility = Visibility.Hidden;
            itsRedLight.Visibility = Visibility.Visible;
            itsRoadController.StopMovement();
            trafficLightState = new RedTrafficLightState(itsRoadController, itsRedLight, itsGreenLight);
        }
    }
}