using SBBurkhardt_GUI.View;
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
            
        }

        private void btnShowBoard_Click(object sender, RoutedEventArgs e)
        {
            (this.DataContext as BoardViewModel).showBoard(searchBoard.selectedStation);
            
        }

        private void btnShowConnections_Click(object sender, RoutedEventArgs e)
        {
            (this.DataContext as ConnectionViewModel).showConnections(fromStation.selectedStation, toStation.selectedStation);
        }

        private void tabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
            if (tabVerbindung.IsSelected)
            {
                this.DataContext = new ConnectionViewModel();
            }
            else if (tabStation.IsSelected)
            {
                this.DataContext = new BoardViewModel();
            }
            else if (tabCloseStations.IsSelected)
            {

            }
        }

        private void lbConnections_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
            (this.DataContext as ConnectionViewModel).selectedConnection = (Connection)lbConnections.SelectedItem;
            (this.DataContext as ConnectionViewModel).showConnectionPoints();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }

        private void btnShowMap_Click(object sender, RoutedEventArgs e)
        {
            (this.DataContext as BoardViewModel).showMap();
        }
    }
}
