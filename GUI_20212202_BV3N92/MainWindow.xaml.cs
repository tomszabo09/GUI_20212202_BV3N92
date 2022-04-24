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
using System.Windows.Threading;

namespace GUI_20212202_BV3N92
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameLogic logic;
        DispatcherTimer gameTimer = new DispatcherTimer();
        bool goLeft, goRight, goUp, goDown;
        bool noLeft, noRight, noUp, noDown;
        int speed = 8;
        Rect playerhitbox;
        public MainWindow()
        {
            InitializeComponent();
            logic = new GameLogic(this);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            display.Resize(new Size(canvas.ActualWidth, canvas.ActualHeight));
            display.InvalidateVisual();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {           
            display.SetupModel(logic);
            display.Resize(new Size(canvas.ActualWidth, canvas.ActualHeight));
            display.InvalidateVisual();
            gameTimer.Interval = TimeSpan.FromSeconds(20);
            //gameTimer.Tick += Gameloop;
            
        }

        private void GameLoop()
        {
            
        }

        private void canvas_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void canvas_KeyUp(object sender, KeyEventArgs e)
        {

        }

        
    }
}
