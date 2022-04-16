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

namespace GUI_20212202_BV3N92.Windows
{
    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        }

        private void b_resume_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void b_save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void b_resetart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void b_exit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
