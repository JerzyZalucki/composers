using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp10
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        class Gatunek
        {
            public int id;
            public string dane3;

            public override string ToString()
            {
                return dane3;
            }
        }
        bool czyNawigacja = false;

        private void button2_Click(object sender, EventArgs e)
        {
            czyNawigacja = true;
            Form2 f = new Form2();
            Application.OpenForms[0].Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            f.Show();
            this.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            string connectionString = "User Id=TEST10;Password=puszek;Data Source=localhost:1521/xe";
            string queryString = "select id_gatunek,nazwa2||' : '||opis as dane3 FROM gatunek";
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                connection.Open();
                OracleDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Gatunek g = new Gatunek();
                        g.id = reader.GetInt32(0);
                        g.dane3 = reader["dane3"].ToString();
                        comboBox1.Items.Add(g);
                        // Console.WriteLine(reader.GetInt32(0) + ", " + reader.GetString(1));
                        Console.WriteLine(g);
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
            Gatunek g = (Gatunek)((ComboBox)sender).SelectedItem;
            string connectionString = "User Id=TEST10;Password=puszek;Data Source=localhost:1521/xe";
            string queryString = "select id_gatunek,nazwa2,opis FROM gatunek where id_gatunek =:id_gatunek";
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                command.BindByName = true;
                OracleParameter op = new OracleParameter(":id_gatunek",g.id);
                command.Parameters.Add(op);
                connection.Open();
                OracleDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        textBox1.Text = reader["nazwa2"].ToString();
                        textBox2.Text = reader["opis"].ToString();
                        
                        // Console.WriteLine(reader.GetInt32(0) + ", " + reader.GetString(1));
                        Console.WriteLine(g);
                    }
                }
                finally
                {
                    // always call Close when done reading.
                    reader.Close();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Gatunek g = (Gatunek)comboBox1.SelectedItem;
            string connectionString = "User Id=TEST10;Password=puszek;Data Source=localhost:1521/xe";
            string queryString = "update gatunek set nazwa2 =:nazwa2, opis =:opis where id_gatunek = :id_gatunek";
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                command.BindByName = true;
                OracleParameter op = new OracleParameter(":id_gatunek", g.id);
                command.Parameters.Add(op);
                op = new OracleParameter(":nazwa2", textBox1.Text);
                command.Parameters.Add(op);
                op = new OracleParameter(":opis", textBox2.Text);
                command.Parameters.Add(op);
                connection.Open();
                int wynik = command.ExecuteNonQuery();

            }
        }
    }
}
