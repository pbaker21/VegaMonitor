using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.IO;





class DBConnect
{
    private MySqlConnection connection;
    private string server;
    private string database;
    private string uid;
    private string password;


    //Constructor
    public DBConnect(bool status)
    {
        Initialize(status);
    }




    //Initialize values
    private void Initialize(bool status)
    {
        if (status) {
            database = "vegastats_live";
            uid = "vegalive";
        }else
        {
            database = "vegastats_test";
            uid = "vegatest";
        }

        server = "217.172.143.116";
        password = "1964Pib3";
        string connectionString;
        connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

        connection = new MySqlConnection(connectionString);
    }



    //open connection to database
    private bool OpenConnection()
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
                    Console.WriteLine("Cannot connect to server.  Contact administrator");
                    break;

                case 1045:
                    Console.WriteLine("Invalid username/password, please try again");
                    break;
            }
            return false;
        }
    }



    //Close connection
    private bool CloseConnection()
    {
        try
        {
            connection.Close();
            return true;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }



    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public class FileNameHash
    {
        public string site_id { get; set; }
        public string ping { get; set; }
        public string last_trigger { get; set; }
        public string localdown { get; set; }        
    }



    public List<FileNameHash> getMonitorResults(){

        var sites = new List<FileNameHash>();

        var thedate = DateTime.Today.ToString("yyyy-MM-dd");

        //Open connection
        if (this.OpenConnection() == true)
        {
            string query = "SELECT photos_taken.pk_id, photos_taken.site_id, sites.site_name, photos_taken.last_trigger, photos_taken.wait_for, photos_taken.webservice_status, -sites.timezone AS timezone, IF(photos_alert.offline_alert > 0, 'No Pulse', 'Active') AS offline, IF(photos_alert.localhost_alert > 0, 'Down ↓', 'Running ↑') AS localdown " + 
"FROM `photos_taken` " +
"JOIN sites ON sites.site_id = photos_taken.site_id " +
"JOIN photos_alert ON photos_alert.fk_id = photos_taken.pk_id " +
"WHERE photos_taken.photo_date = '"+ thedate + "'  " + 
"ORDER BY offline, localdown, photos_taken.site_id";
            
            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);

            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            
            while (dataReader.Read())
            {
                sites.Add(new FileNameHash
                {
                    site_id = dataReader["site_id"].ToString(),
                    ping = dataReader["offline"].ToString(),
                    last_trigger = dataReader["last_trigger"].ToString(),
                    localdown = dataReader["localdown"].ToString()
                });
            }

            //close Data Reader
            dataReader.Close();
        }

        //close Connection
        this.CloseConnection();

        return sites;
    }


    


}// end of class
