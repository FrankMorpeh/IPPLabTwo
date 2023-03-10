using IPPLabTwo.Builders.CarBuilders;
using IPPLabTwo.Controllers.CarControllers;
using IPPLabTwo.Views.RoadViews;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace IPPLabTwo.Controllers.RoadControllers
{
    public class RoadController
    {
        private RoadView itsRoadView;
        private List<CarController> itsCarControllers;
       
        public RoadView RoadView { set { itsRoadView = value; } }

        public RoadController()
        {
            itsCarControllers = new List<CarController>() { new CarController(new OrangeCarBuilder())
                , new CarController(new RedCarBuilder()) };
        }
        public void SetEventHandlers()
        {
            foreach (CarController carController in itsCarControllers)
                carController.SetRoadControllerEvents(this);
        }


        // Movement
        public void StartMovement()
        {
            foreach (CarController carController in itsCarControllers)
                carController.StartMovementAsParallel();
        }
        public void ContinueMovement()
        {
            foreach (CarController carController in itsCarControllers)
                carController.AllowMovement();
        }
        public void StopMovement()
        {
            foreach (CarController carController in itsCarControllers)
                carController.StopMovement();
        }


        // Event handlers
        public void CarEnteredRoad(Rectangle car)
        {
            itsRoadView.ShowNewCarOnRoad(car);
        }
        public void CarLeftRoad(Rectangle car)
        {
            itsRoadView.HideCarOnRoad(car);
        }
    }
}