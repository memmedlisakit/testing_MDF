using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Data.SQLite;

namespace MDF_Database
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string connection = "Data Source="+getPath()+ @"App_Data\SQLiteDB.db;Version=3;";

         


        public static string getPath()
        {
            string path = Application.StartupPath;

            List<string> splited = Regex.Split(path, "bin").ToList();

            return splited[0];

        }



        public void fillAll()
        {
            this.textBox3.Text = "";
            SQLiteDataAdapter da = new SQLiteDataAdapter();
            DataTable dt = new DataTable();
            string SQLite = "SELECT * FROM [Users]";
            SQLiteConnection con = new SQLiteConnection(connection);
            SQLiteCommand com = new SQLiteCommand(SQLite, con);
            da.SelectCommand = com;
            da.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                this.textBox3.Text += item["name"] + " -- " + item["surname"] + "\r\n";
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var name = textBox1.Text;
            var surname = textBox2.Text;

            SQLiteConnection con = new SQLiteConnection(connection);
            con.Open();
            string SQLite = "INSERT INTO [Users](name, surname) VALUES('" + name + "', '" + surname + "')";

            SQLiteCommand com = new SQLiteCommand(SQLite, con);
            int effected = com.ExecuteNonQuery();
            con.Close();
            MessageBox.Show(effected + "");
            this.fillAll();
        }
    }
}
