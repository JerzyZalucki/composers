using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp10
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        class Ojczyzna
        {
            public int id;
            public string dane;

            public override string ToString()
            {
                return dane;
            }
        }
        bool czyNawigacja = false;

        private void button1_Click(object sender, EventArgs e)
        {
            czyNawigacja = true;
            Form4 f = new Form4();
            f.Show();
            this.Close();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            string connectionString = "User Id=TEST10;Password=puszek;Data Source=localhost:1521/xe";
            string queryString = "SELECT ojczyzna.id_ojczyzna,nazwa||' '||miasto||':  '||imie||' '||nazwisko AS dane FROM ojczyzna JOIN kompozytorzy on ojczyzna.id_ojczyzna=kompozytorzy.ojczyzna_id_ojczyzna";
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                connection.Open();
                OracleDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Ojczyzna o = new Ojczyzna();
                        o.id = reader.GetInt32(0);
                        o.dane = reader["dane"].ToString();
                        comboBox1.Items.Add(o);
                        // Console.WriteLine(reader.GetInt32(0) + ", " + reader.GetString(1));
                        Console.WriteLine(o);
                    }
                }
                finally
                {
                    // always call Close when done reading.
                    reader.Close();
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ojczyzna o = (Ojczyzna)((ComboBox)sender).SelectedItem;
            string connectionString = "User Id=TEST10;Password=puszek;Data Source=localhost:1521/xe";
            string queryString = "SELECT ojczyzna.id_ojczyzna,nazwa,miasto,imie,nazwisko FROM ojczyzna JOIN kompozytorzy on ojczyzna.id_ojczyzna=kompozytorzy.ojczyzna_id_ojczyzna where id_ojczyzna =: id_ojczyzna";
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                command.BindByName = true;
                OracleParameter op = new OracleParameter(":id_ojczyzna", o.id);
                command.Parameters.Add(op);
                connection.Open();
                OracleDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        textBox1.Text = reader["imie"].ToString();
                        textBox2.Text = reader["nazwisko"].ToString();
                        textBox3.Text = reader["nazwa"].ToString();
                        textBox4.Text = reader["miasto"].ToString();

                        // Console.WriteLine(reader.GetInt32(0) + ", " + reader.GetString(1));
                        Console.WriteLine(o);
                    }
                }
                finally
                {
                    // always call Close when done reading.
                    reader.Close();
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.google.pl");

        }
    }
}
