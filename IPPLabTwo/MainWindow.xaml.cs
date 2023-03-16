using IPPLabTwo.Builders.CarBuilders;
using IPPLabTwo.Controllers.RoadControllers;
using IPPLabTwo.Views.RoadViews;
using System;
using System.Windows;
using System.Windows.Shapes;

namespace IPPLabTwo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RoadController roadController;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MainWindow_Loaded(object sender, EventArgs e)
        {
            roadController = new RoadController(this);

            roadController.StartMovementAsParallel();
            road.Focus();
        }

        private void Road_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Space)
                roadController.SwitchLight();
        }
    }
}