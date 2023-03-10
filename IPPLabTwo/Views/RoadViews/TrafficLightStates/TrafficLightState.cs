using IPPLabTwo.Controllers.RoadControllers;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace IPPLabTwo.Views.RoadViews.TrafficLightStates
{
    public abstract class TrafficLightState
    {
        protected RoadController itsRoadController;
        protected Ellipse itsRedLight;
        protected Ellipse itsGreenLight;

        public TrafficLightState(RoadController roadController, Ellipse redLight, Ellipse greenLight)
        {
            itsRoadController = roadController;
            itsRedLight = redLight;
            itsGreenLight = greenLight;
        }

        public abstract void SwitchLight(ref TrafficLightState trafficLightState);
    }
}