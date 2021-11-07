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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        class Utwory
        {
            public int id;
            public string dane2;

            public override string ToString()
            {
                return dane2;
            }
        }

        bool czyNawigacja = false;

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            czyNawigacja = true;
            Form1 f = new Form1();
            Application.OpenForms[0].Show();
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            string connectionString = "User Id=TEST10;Password=puszek;Data Source=localhost:1521/xe";
            string queryString = "select id_utworu,nazwa||' : '||forma as dane2 FROM utwory";
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                connection.Open();
                OracleDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Utwory u = new Utwory();
                        u.id = reader.GetInt32(0);
                        u.dane2 = reader["dane2"].ToString();
                        comboBox1.Items.Add(u);
                        // Console.WriteLine(reader.GetInt32(0) + ", " + reader.GetString(1));
                        Console.WriteLine(u);
                    }
                }
                finally
                {
                    // always call Close when done reading.
                    reader.Close();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Utwory u = (Utwory)((ComboBox)sender).SelectedItem;
            string connectionString = "User Id=TEST10;Password=puszek;Data Source=localhost:1521/xe";
            string queryString = "select id_utworu,nazwa,forma FROM utwory where id_utworu =:id_utworu";
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                command.BindByName = true;
                OracleParameter op = new OracleParameter(":id_utworu", u.id);
                command.Parameters.Add(op);
                connection.Open();
                OracleDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        textBox1.Text = reader["nazwa"].ToString();
                        textBox2.Text = reader["forma"].ToString();

                        // Console.WriteLine(reader.GetInt32(0) + ", " + reader.GetString(1));
                        Console.WriteLine(u);
                    }
                }
                finally
                {
                    // always call Close when done reading.
                    reader.Close();
                }
            }
        }

        private void zakończToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
