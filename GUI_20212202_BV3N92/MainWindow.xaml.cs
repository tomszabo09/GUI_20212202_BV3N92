using GUI_20212202_BV3N92.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI_20212202_BV3N92
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameLogic logic;
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            display.Resize(new Size(canvas.ActualWidth, canvas.ActualHeight));
            display.InvalidateVisual();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            logic = new GameLogic(this, display.size);
            display.SetupModel(logic);
            display.Resize(new Size(canvas.ActualWidth, canvas.ActualHeight));
            display.InvalidateVisual();           
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    logic.Control(GameLogic.Controls.moveUp);
                    break;
                case Key.S:
                    logic.Control(GameLogic.Controls.moveDown);
                    break;
                case Key.A:
                    logic.Control(GameLogic.Controls.moveLeft);
                    break;
                case Key.D:
                    logic.Control(GameLogic.Controls.moveRight);
                    break;                
                case Key.Space:
                    logic.Control(GameLogic.Controls.shoot);
                    break;

            }
        }
    }
}
