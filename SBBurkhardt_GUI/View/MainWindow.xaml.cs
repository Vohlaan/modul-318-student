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
        ConnectionViewModel Cvm { get; set; }
        BoardViewModel Bvm { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Cvm = new ConnectionViewModel();
            Bvm = new BoardViewModel();
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
                this.DataContext = Cvm;
            }
            else if (tabStation.IsSelected)
            {
                this.DataContext = Bvm;
            }
            else if (tabCloseStations.IsSelected)
            {

            }
        }





        private void btnShowMap_Click(object sender, RoutedEventArgs e)
        {
            (this.DataContext as BoardViewModel).showMap();
        }

        private void lvConnections_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((Connection)lvConnections.SelectedItem != null)
            {
                (this.DataContext as ConnectionViewModel).selectedConnection = (Connection)lvConnections.SelectedItem;
                (this.DataContext as ConnectionViewModel).showConnectionPoints();
            }
            
        }

        private void lvConnectionPoints_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }

        private void lvStationBoard_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }

        private void btnSendMail_Click(object sender, RoutedEventArgs e)
        {
            (this.DataContext as ConnectionViewModel).sendMail();
        }
    }
}
