using System.Windows.Shapes;
using System.Windows;
using IPPLabTwo.Controllers.RoadControllers;

namespace IPPLabTwo.Views.RoadViews.TrafficLightStates
{
    public class RedTrafficLightState : TrafficLightState
    {
        public RedTrafficLightState(RoadController roadController, Ellipse redLight, Ellipse greenLight) 
            : base(roadController, redLight, greenLight) { }

        public override void SwitchLight(ref TrafficLightState trafficLightState)
        {
            itsRedLight.Visibility = Visibility.Hidden;
            itsGreenLight.Visibility = Visibility.Visible;
            itsRoadController.AllowMovement();
            trafficLightState = new GreenTrafficLightState(itsRoadController, itsRedLight, itsGreenLight);
        }
    }
}