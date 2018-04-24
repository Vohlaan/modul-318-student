using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using SwissTransport;
using SBBurkhardt_GUI.Common;
using System.Net.NetworkInformation;

namespace SBBurkhardt_GUI.View
{
    class ConnectionViewModel : INotifyPropertyChanged
    {

        Transport t = new Transport();
        public ObservableCollection<Connection> connectionsList { get; set; }


        public ConnectionViewModel()
        {
            bool pingable = false;
            Ping pinger = new Ping();
            try
            {
                PingReply reply = pinger.Send("transport.opendata.ch");
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                MessageBox.Show("Can't connect to Opendata. I suggest you try again later.");
            }
            connectionsList = new ObservableCollection<Connection>();
            connectionPoints = new ObservableCollection<ConnectionPoint>();
            connectionDate = DateTime.Now;
        }

        private DateTime _connectionDate;
        public DateTime connectionDate
        {
            get { return _connectionDate; }
            set { _connectionDate = value; OnPropertyChanged("connectionDate"); }
        }

        private bool _isArrivalTime;
        public bool isArrivalTime
        {
            get { return _isArrivalTime; }
            set { _isArrivalTime = value; OnPropertyChanged("isArrivalTime"); }
        }

        private string _connectionTime;
        public string connectionTime
        {
            get { return _connectionTime; }
            set { _connectionTime = value; OnPropertyChanged("connectionTime"); }
        }

        public Connection selectedConnection;
        public ObservableCollection<ConnectionPoint> connectionPoints { get; set; }

        private string _eMail;
        public string eMail
        {
            get { return _eMail; }
            set { _eMail = value; OnPropertyChanged("eMail"); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        // Show Connections between the two selected Stations.
        public void showConnections(Station fromStation, Station toStation)
        {
            connectionsList.Clear();

            if (!string.IsNullOrEmpty(connectionTime))
            {
                string regex = "^([0-1][0-9]|2[0-3]):[0-5][0-9]$";
                Match match = Regex.Match(connectionTime, regex);

                if (match.Success)
                {
                    DateTime newTime = DateTime.Parse(connectionTime);
                    DateTime newDateTime = new DateTime(connectionDate.Year, connectionDate.Month, connectionDate.Day, newTime.Hour, newTime.Minute, 0);
                    connectionDate = newDateTime;

                    MessageBox.Show(connectionDate.ToString("yyyy-MM-dd"));
                }
            }
            else
            {
                connectionDate = DateTime.Now;
            }
            Connections connectionList = t.GetConnections(fromStation.Id, toStation.Id , connectionDate, isArrivalTime);
            foreach (Connection connection in connectionList.ConnectionList)
            {
                connection.From.Departure = Tools.toHours(connection.From.Departure);
                connection.To.Arrival = Tools.toHours(connection.To.Arrival);
                connectionsList.Add(connection);
            }

        }

        // Show Points in a Connection
        public void showConnectionPoints()
        {
            connectionPoints.Clear();
            connectionPoints.Add(selectedConnection.To);
            connectionPoints.Add(selectedConnection.From);

            connectionPoints[0].Arrival = Tools.toHours(connectionPoints[0].Arrival);
            connectionPoints[1].Departure = Tools.toHours(connectionPoints[1].Departure);
        }

        
        // Send Selected Connection to Entered E-Mail-Adress.
        public void sendMail()
        {
            if (connectionPoints != null)
            {
                try
                {
                    MailMessage msg = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    msg.From = new MailAddress("teapatricktestmail@gmail.com");

                    msg.To.Add(eMail);
                    msg.Subject = "Verbindung | SBBurkhardt";
                    msg.Body = "Von: " + connectionPoints[1].Station.Name + " Um: " + connectionPoints[1].Departure +
                                " \n Nach: " + connectionPoints[0].Station.Name + " Um: " + connectionPoints[0].Arrival;

                     
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("teapatricktestmail", "patricktestmailpasswort1");
                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(msg);
                    MessageBox.Show("Email Versandt!");
                    eMail = "";
                }

                catch
                {

                }
            }
        }

        // Open Google Maps with Connections between both selected stations.
        public void showConnectionMap()
        {
            if (selectedConnection != null)
            {
                double latFrom = connectionPoints[1].Station.Coordinate.XCoordinate;
                double lngFrom = connectionPoints[1].Station.Coordinate.YCoordinate;
                double latTo = connectionPoints[0].Station.Coordinate.XCoordinate;
                double lngTo = connectionPoints[0].Station.Coordinate.YCoordinate;


                System.Diagnostics.Process.Start("https://www.google.ch/maps/dir/'" + latFrom.ToString() + "," + lngFrom.ToString() + "'/'" + latTo.ToString() + "," + lngTo.ToString() + "'");

            }
        }


    }
}