using LibCfg;
using MySql.Data.MySqlClient;
using System;

//System
//MySql.Data
//System.Data

namespace Program
{
    public class DBConnect : IDisposable
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public DBConnect()
        {
            Initialize();
            OpenConnection();
        }

        //Initialize values
        private void Initialize()
        {
            Cfg config = new Cfg(SCHEnvironment.CfgDir + "db.xml");

            server = config.GetValue("server"); 
            database = config.GetValue("database"); 
            uid = config.GetValue("uid"); 
            password = config.GetValue("password"); 

            string connectionString;
            connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";

            connection = new MySqlConnection(connectionString);
        }

        public string ErrString;

        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        ErrString = "Cannot connect to server.  Contact administrator";
                        break;

                    case 1045:
                        ErrString = "Invalid username/password, please try again";
                        break;
                }
                return false;
            }
        }

        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                ErrString = ex.Message;
                return false;
            }
        }

        public void NonResultQuery(string query)
        {
            MySqlCommand cmd = new MySqlCommand
            {
                CommandText = query,
                Connection = connection
            };

            cmd.ExecuteNonQuery();
        }

        public void MultipleResultQuery(string query, Action<MySqlDataReader> dataProcess)
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);

            MySqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                dataProcess.Invoke(dataReader);
            }

            dataReader.Close();
        }

        public void Dispose()
        {
            CloseConnection();
        }
    }
}
