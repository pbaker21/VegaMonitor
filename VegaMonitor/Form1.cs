using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Xml;




namespace VegaMonitor
{
    public partial class Form1 : Form
    {
        public bool status = false;
        public Timer timer1;
        int sleep_time = 5;

        DBConnect db;

        


        public Form1()
        {
            
            if (File.Exists(@"config_monitor.xml"))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("config_monitor.xml");

                XmlNode node1 = doc.DocumentElement.SelectSingleNode("db_option");
                XmlNode node2 = doc.DocumentElement.SelectSingleNode("sleep_seconds");

                string status_set = node1.InnerText.ToLower();
                sleep_time = Convert.ToInt32(node2.InnerText);

                status = (status_set == "live") ? true : false;

                string status_label = (status_set == "live") ? "Live" : "Test";

                //Console.WriteLine("status_set: " + status_set + " -- sleep_time: " + sleep_time + " -- status: " + status);

                // get current version number
                string fn = System.Reflection.Assembly.GetExecutingAssembly().Location;
                System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(fn);
                Version version = assembly.GetName().Version;
                Console.WriteLine("Version: " + version);
                
                db = new DBConnect(status);

                InitializeComponent();

                this.Text = "Vega Site Monitor - v" + version;

                monitor_label_status.Text = "Viewing Status : " + status_label;                
            }
            else
            {

                Console.WriteLine("Could not find 'config_monitor.xml' file.");
                
                Form1_FormClosing();
            }
        }






        private void Form1_FormClosing()
        {
            
            MessageBox.Show("Cannot find 'config_monitor.xml' file.", "Alert!");

            System.Environment.Exit(1);
        }



        



        public void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(monitorTable);
            timer1.Interval = (sleep_time * 1000); // in miliseconds
            timer1.Start();
        }
        


        private void Form1_Load(object sender, EventArgs e)
        {
//            Console.WriteLine("start timer");
            InitTimer();
        }




        private void monitorTable(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            

            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Site", typeof(string));
            table.Columns.Add("Ping", typeof(string));
            table.Columns.Add("Last Ping", typeof(string));
            table.Columns.Add("Localhost", typeof(string));
                        

            var lst = db.getMonitorResults();

            int ix = 0;

            foreach (var item in lst)
            {
                //   Console.WriteLine(ix + " -- " + item.site_id + ", " + item.ping + ", " + item.last_trigger + ", " + item.localdown);

                table.Rows.Add(ix + 1, item.site_id, item.ping, item.last_trigger, item.localdown);

                ix++;
            }

            monitorGridView.DataSource = table;

            table.DefaultView.Sort = "Ping" + " DESC";


            for (int i = 0; i < ix; i++) { 

                DataGridViewRow row = monitorGridView.Rows[i];// get you required index
                
                string siteStatus = row.Cells[2].Value.ToString();
                string hostStatus = row.Cells[4].Value.ToString();

                //Console.WriteLine(RowType);

                row.DefaultCellStyle.BackColor = System.Drawing.Color.White;


                if (hostStatus != "Running ↑")
                {                    
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.Orange;
                }

                if (siteStatus != "Active")
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.Red;

                    row.Cells[4].Value = "?"; // sites not pining any more so we're not sure if the host is up or down, hence '?'
                }

            }


            
            monitorGridView.Columns["Id"].Width = 50;
            monitorGridView.Columns["Ping"].Width = 100;
            monitorGridView.Columns["Last Ping"].Width = 170;
        }






        /*
        private void siteOption_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            if (siteOption.Text == "Test Monitor")
            {
                siteOption.Text = "Live Monitor";
                status = true;
            }
            else if (siteOption.Text == "Live Monitor")
            {
                siteOption.Text = "Test Monitor";
                status = false;
            }

            Console.WriteLine(status);

            db = new DBConnect(status);

            this.Enabled = true;
            this.Cursor = Cursors.Default;
        }
        */

    }
}
