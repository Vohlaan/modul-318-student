﻿using SBBurkhardt_GUI.View;
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

namespace SwissTransport
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new BoardViewModel();
            this.DataContext = new ConnectionViewModel();
        }

        private void showBoard_Click(object sender, RoutedEventArgs e)
        {
            (this.DataContext as BoardViewModel).showBoard(searchBoard.selectedStation);
        }

        private void btnShowConnections_Click(object sender, RoutedEventArgs e)
        {
            (this.DataContext as ConnectionViewModel).showConnections(fromStation.selectedStation, toStation.selectedStation);
        }
    }
}