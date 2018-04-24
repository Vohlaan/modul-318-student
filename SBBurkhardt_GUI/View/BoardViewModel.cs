using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SwissTransport;

namespace SBBurkhardt_GUI.View
{
    class BoardViewModel : INotifyPropertyChanged
    {

        private StationBoardRoot _currentStation;
        public StationBoardRoot currentStation
        {
            get { return _currentStation; }
            set { _currentStation = value; OnPropertyChanged("currentStation"); }
        }

        private ObservableCollection<StationBoard> _entryTable = new ObservableCollection<StationBoard>();
        public ObservableCollection<StationBoard> entryTable
        {
            get
            {
               return _entryTable;
            }
            private set { _entryTable = value; OnPropertyChanged("entryTable"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public BoardViewModel()
        {
            entryTable = new ObservableCollection<StationBoard>() {  };
        }
        
        // Show Stationboard of selected Station
        public void showBoard(Station station)
        {
            if ( station!= null)
            {
                entryTable.Clear();

                Transport t = new Transport();
                currentStation = t.GetStationBoard(station.Name, station.Id);

                foreach (StationBoard entry in currentStation.Entries)
                {
                    entryTable.Add(entry);
                }
            }
        }

        // Show Google-Map with marker on selected Location.
        public void showMap()
        {
            if (currentStation != null)
            {
                double lat = currentStation.Station.Coordinate.XCoordinate;
                double lng = currentStation.Station.Coordinate.YCoordinate;
                System.Diagnostics.Process.Start("http://www.google.com/maps/place/" + lat.ToString() + "," + lng.ToString());
            }
            
        }
    }
}
