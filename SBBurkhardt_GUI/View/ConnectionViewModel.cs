using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwissTransport;

namespace SBBurkhardt_GUI.View
{
    class ConnectionViewModel
    {
        //List<Connection> tableConnectionList;

        public void showConnections(Station fromStation, Station toStation)
        {
            
            Transport t = new Transport();
            Connections connectionList = t.GetConnections(fromStation.Id, toStation.Id);
            foreach(Connection connection in connectionList.ConnectionList)
            {
                //tableConnectionList.Add(connection);
            }
        }
    }
}
