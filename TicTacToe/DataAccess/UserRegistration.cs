using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Cassandra;

namespace TicTacToe.DataAccess
{
    public class UserRegistration
    {
      

        //updating registered details in database
        public bool UpdateNewUser(User newUser)
        {
            var _Connection = Cluster.Builder()
            .AddContactPoint("127.0.0.1").Build();
            if (ValidateUserName(newUser.UserName))
            {
                var session = _Connection.Connect("gaming");
                var InsertQuery = session.Prepare("Insert into UserDetails (FirstName,LastName,UserName)values(?,?,?)");
                var command = InsertQuery.Bind(newUser.FirstName,newUser.LastName,newUser.UserName);
                session.Execute(command);
            }
              else
              return false;
            return true;
        }

        public bool ValidateUserName(string userName)
        {

            var _Connection = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            var session = _Connection.Connect("gaming");
            var SelectQuery = session.Prepare("Select * from UserDetails where UserName= ?");
            var command = SelectQuery.Bind(userName);
            var result = session.Execute(command);
            var rows = result.GetRows();
            if(rows.Count() > 0)
              return false;

            return true;
        }
          public bool GenerateToken(string userName)
          {
            var _Connection = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            var session = _Connection.Connect("gaming");
            var UpdateQuery = session.Prepare("update UserDetails set AccessToken=? where UserName=?");
            var SelectQuery = session.Prepare("select AccessToken from UserDetails Where UserName=?");
            var selectCommand = SelectQuery.Bind(userName);
            var result = session.Execute(selectCommand).First();
            if(result["accesstoken"]!=null)
            return false;

            var updateCommand = UpdateQuery.Bind(userName+10,userName);
            session.Execute(updateCommand);
            return true;
          }
       

        public bool ValidateAccessToken(string accessToken)
        {

            var _Connection = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            var session = _Connection.Connect("gaming");
            var SelectQuery = session.Prepare("select * from UserDetails where AccessToken=? allow filtering");
            var command = SelectQuery.Bind(accessToken);
            var result = session.Execute(command);
            var rows = result.GetRows();
            if (rows.Count() == 0)
                return false;
            return true;
            
        }
    }
}
//using (_Connection = new SqlConnection(_Connector.ConnectionString))
//{
//    _Connection.Open();
//    string InsertQuery = "Insert into UserDetails (FirstName,LastName,UserName)values(@firstName,@lastName,@userName)";
//    using (SqlCommand command = new SqlCommand(InsertQuery, _Connection))
//    {
//        command.Parameters.AddWithValue("@firstName", newUser.FirstName);
//        command.Parameters.AddWithValue("@lastName", newUser.LastName);
//        command.Parameters.AddWithValue("@userName", newUser.UserName);
//        command.ExecuteNonQuery();
//    }
//}
//using (_Connection = new SqlConnection(_Connector.ConnectionString))
//{
//    _Connection.Open();
//    string SelectQuery = "select * from UserDetails where UserName=(@newUserName)";
//    using (SqlCommand command = new SqlCommand(SelectQuery, _Connection))
//    {
//        command.Parameters.AddWithValue("@newUserName", userName);
//        SqlDataReader reader = command.ExecuteReader();
//        if (reader.HasRows)
//            return false;

//    }
//    return true;
//}
//    using (_Connection = new SqlConnection(_Connector.ConnectionString))
//    {
//        _Connection.Open();
//        string updateQuery = "update UserDetails set AccessToken=@accessKey where UserName=@uName";
//        string SelectQuery = "select AccessToken from UserDetails where UserName=@uName";
//        using (SqlCommand command = new SqlCommand(SelectQuery, _Connection))
//        {
//            command.Parameters.AddWithValue("@uName", userName);
//            string alreadyGenerated = command.ExecuteScalar().ToString();
//          if (alreadyGenerated!="")
//                return false;
//        }
//        using (SqlCommand command = new SqlCommand(updateQuery, _Connection))
//        {
//            command.Parameters.AddWithValue("@accessKey", userName + 10);
//            command.Parameters.AddWithValue("@uName", userName);
//            command.ExecuteNonQuery();
//            return true;
//        }
//    }
//}
//using (_Connection = new SqlConnection(_Connector.ConnectionString))
//{
//    _Connection.Open();
//    string SelectQuery = "select * from UserDetails where AccessToken=@accessKey";
//    using (SqlCommand command = new SqlCommand(SelectQuery, _Connection))
//    {
//        command.Parameters.AddWithValue("@accesskey", accessToken);
//        SqlDataReader reader = command.ExecuteReader();
//        if (reader.HasRows)
//            return true;
//    }
//    return false;
//}