﻿using GUI_20212202_BV3N92.Logic;
using GUI_20212202_BV3N92.Windows.Rules;
using System;
using System.Collections.Generic;
using System.IO;
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
        RulesWindow rules;

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
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(20);
            dt.Tick += Dt_Tick;
            dt.Start();
            display.Resize(new Size(canvas.ActualWidth, canvas.ActualHeight));
            display.InvalidateVisual();

            if (!File.Exists("save.sav"))
            {
                rules = new RulesWindow();
                rules.Show();
            }
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            logic.TimeStep();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    logic.Control(Controls.moveUp);
                    break;
                case Key.S:
                    logic.Control(Controls.moveDown);
                    break;
                case Key.A:
                    logic.Control(Controls.moveLeft);
                    break;
                case Key.D:
                    logic.Control(Controls.moveRight);
                    break;                
                case Key.Space:
                     logic.Control(Controls.shoot);
                    break;
                case Key.Escape:
                    logic.Control(Controls.menu);
                    break;
            }
        }
    }
}
