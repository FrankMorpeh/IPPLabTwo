using IPPLabTwo.Controllers.RoadControllers;
using IPPLabTwo.Views.RoadViews;
using System.Windows;

namespace IPPLabTwo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RoadController roadController;
        RoadView roadView;
        public MainWindow()
        {
            InitializeComponent();
            roadController = new RoadController();
            roadView = new RoadView(roadController, road);
            roadController.RoadView = roadView;
            roadController.SetEventHandlers();

            roadView.StartMovement();
        }

        private void Road_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Space)
                roadView.SwitchLight();
        }
    }
}