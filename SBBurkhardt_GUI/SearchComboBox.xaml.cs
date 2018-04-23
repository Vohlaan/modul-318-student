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
using SwissTransport;

namespace SBBurkhardt_GUI
{
    /// <summary>
    /// Interaktionslogik für SearchComboBox.xaml
    /// </summary>
    public partial class SearchComboBox : UserControl
    {
        
        //Sobald eine Station ausgewählt ist, wird selectedStation festgelegt.
        //selectedStation ist ein Objekt der Klasse Station.
        public Station selectedStation;

        private List<Station> stations;

        public SearchComboBox()
        {
            InitializeComponent();
        }

        private void cboxSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (stations != null && cboxSearch.SelectedIndex > 0)
            {
                selectedStation = stations[cboxSearch.SelectedIndex];
            }
        }

        private void cboxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            cboxSearch.IsDropDownOpen = true;
            getStationList();
        }

        ITransport t = new Transport();
        private void getStationList()
        {
            stations = t.GetStations(cboxSearch.Text).StationList; //Liste mit Vorschlägen

            List<string> stationNames = new List<string>();

            stations.ForEach(delegate (Station station)
            {
                stationNames.Add(station.Name);
            });
            cboxSearch.ItemsSource = stationNames;
            if (stations != null && cboxSearch.SelectedIndex > -1)
            {
                selectedStation = stations[cboxSearch.SelectedIndex];
            }
        }
    }
}
