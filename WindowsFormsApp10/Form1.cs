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
// USER ID = TEST10; DATA SOURCE = localhost:1521 / xe; PERSIST SECURITY INFO=True

namespace WindowsFormsApp10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        class Osoba
        {
            public int id;
            public string dane;

            public override string ToString()
            {
                return dane;
            }
        }
       

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
            Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            string connectionString = "User Id=TEST10;Password=puszek;Data Source=localhost:1521/xe";
            string queryString = "select id_kompozytor,imie||' '|| nazwisko||':  '||data_ur as dane from kompozytorzy";
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                connection.Open();
                OracleDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Osoba o = new Osoba();
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
            Osoba o = (Osoba)((ComboBox)sender).SelectedItem;
            string connectionString = "User Id=TEST10;Password=puszek;Data Source=localhost:1521/xe";
            string queryString = "select id_kompozytor,imie,nazwisko,data_ur FROM kompozytorzy where id_kompozytor =:id_kompozytor";
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                command.BindByName = true;
                OracleParameter op = new OracleParameter(":id_kompozytor", o.id);
                command.Parameters.Add(op);
                connection.Open();
                OracleDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        textBox1.Text = reader["Imie"].ToString();
                        textBox2.Text = reader["Nazwisko"].ToString();
                        textBox3.Text = reader ["data_ur"].ToString();

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
            Process.Start(((LinkLabel)sender).Tag.ToString());
        }

        private void zakończToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void oProgramieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 a = new AboutBox1();
            a.ShowDialog();
        }

        private void zakończToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void kolorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = this.BackColor;
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.BackColor = colorDialog1.Color;
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = menuStrip1.Font;
            if(fontDialog1.ShowDialog() == DialogResult.OK)
            {
                menuStrip1.Font = fontDialog1.Font;
            }
        }
    }
}
