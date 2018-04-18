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
        public Station selectedStation;

        private List<Station> stations;

        public SearchComboBox()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ITransport t = new Transport();

            if (!string.IsNullOrEmpty(cboxSearch.Text))
            {
                stations = t.GetStations(cboxSearch.Text).StationList; //Liste mit Vorschlägen

                List<string> stationNames = new List<string>();

                stations.ForEach (delegate (Station station) 
                {
                    stationNames.Add(station.Name);
                });
                cboxSearch.ItemsSource = stationNames;

                
            }
        }

        private void cboxSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (stations != null && cboxSearch.SelectedIndex > 0)
            {
                Station stationToCheck = stations[cboxSearch.SelectedIndex];
                selectedStation = stationToCheck;
            }
        }
    }
}
