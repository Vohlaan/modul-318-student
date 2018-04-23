using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
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

        public void showConnections(Station fromStation, Station toStation)
        {
            connectionsList.Clear();

            Connections connectionList = t.GetConnections(fromStation.Id, toStation.Id , connectionDate, isArrivalTime);
            foreach (Connection connection in connectionList.ConnectionList)
            {
                connection.From.Departure = toHours(connection.From.Departure);
                connection.To.Arrival = toHours(connection.To.Arrival);
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
        public ObservableCollection<ConnectionPoint> connectionPoints { get; set; }

        public void showConnectionPoints()
        {
            connectionPoints.Clear();
            connectionPoints.Add(selectedConnection.To);
            connectionPoints.Add(selectedConnection.From);

            connectionPoints[0].Arrival = toHours(connectionPoints[0].Arrival);
            connectionPoints[1].Departure = toHours(connectionPoints[1].Departure);
        }

        private string toHours(string isoTime)
        {
            DateTime newTime = DateTime.Parse(isoTime);
            return newTime.ToString("HH:mm");
        }

        private string _eMail;
        public string eMail
        {
            get { return _eMail; }
            set { _eMail = value; OnPropertyChanged("eMail"); }
        }

        public void sendMail()
        {
            // Email Token: 29699272cf006618d96a39420cededc4
            // Easy Free Mail From:
            // https://www.emailondeck.com/eod.php
            try
            {
                MailMessage msg = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                msg.From = new MailAddress("teapatricktestmail@gmail.com");
                
                msg.To.Add(eMail);
                msg.Subject = "Verbindung | SBBurkhardt";
                msg.Body = "Von: " + connectionPoints[0].Station.Name + " Um: " + connectionPoints[0].Arrival +
                            " \n Nach: " + connectionPoints[1].Station.Name + " Um: " + connectionPoints[1].Departure;
                            

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("teapatricktestmail", "patricktestmailpasswort1");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(msg);
            }

            catch 
            {
                
            }
        }
    }
}