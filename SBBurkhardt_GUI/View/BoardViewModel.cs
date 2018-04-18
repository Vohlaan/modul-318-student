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


        public ObservableCollection<string> entryTable { get; set; }

        public BoardViewModel()
        {
            entryTable = new ObservableCollection<string>();
        }

        public void showBoard(Station station)
        {
            entryTable.Clear();

            Transport t = new Transport();
            currentStation = t.GetStationBoard(station.Name, station.Id);

            foreach(StationBoard entry in currentStation.Entries)
            {
                string x = entry.Name;
                entryTable.Add(entry.To);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
