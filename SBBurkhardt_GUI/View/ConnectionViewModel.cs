using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwissTransport;

namespace SBBurkhardt_GUI.View
{
    class ConnectionViewModel : INotifyPropertyChanged
    {
        Transport t = new Transport();
        public ObservableCollection<Connection> connectionsList { get; set; }
        

        public ConnectionViewModel()
        {
            connectionsList = new ObservableCollection<Connection>();
            connectionPoints = new ObservableCollection<Station>();
        }


        public void showConnections(Station fromStation, Station toStation)
        {
            //connectionsList.Clear();

            Connections connectionList = t.GetConnections(fromStation.Id, toStation.Id);
            foreach (Connection connection in connectionList.ConnectionList)
            {
                connectionsList.Add(connection);
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public Connection selectedConnection;
        public ObservableCollection<Station> connectionPoints;

        public void showConnectionPoints()
        {
            connectionPoints.Add(selectedConnection.To.Station);
            connectionPoints.Add(selectedConnection.From.Station);
        }
    }
}