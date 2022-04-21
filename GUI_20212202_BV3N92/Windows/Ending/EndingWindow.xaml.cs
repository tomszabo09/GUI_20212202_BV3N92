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
using System.Windows.Shapes;

namespace GUI_20212202_BV3N92.Windows.Ending
{
    /// <summary>
    /// Interaction logic for EndingWindow.xaml
    /// </summary>
    public partial class EndingWindow : Window
    {
        public EndingWindow()
        {
            InitializeComponent();
        }

        private void b_restart_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void b_exit_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
