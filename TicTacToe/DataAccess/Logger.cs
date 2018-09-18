using Cassandra;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.DataAccess
{
    public class Logger
    {
        public void LogException(string request, string response, string exception, string comment)
        {
            var Connection = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            var session = Connection.Connect("gaming");
            var InsertQuery = session.Prepare("Insert into statuslogger (id,request,response,comment)values(now(),?,?,?)");
            var command = InsertQuery.Bind(request,response,comment);
            var result = session.Execute(command);

          
        }

    }
}
