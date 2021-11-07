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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        class Nagrody
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
            Form5 f = new Form5();
            f.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            czyNawigacja = true;
            Form2 f = new Form2();
            Application.OpenForms[0].Show();
            this.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            string connectionString = "User Id=TEST10;Password=puszek;Data Source=localhost:1521/xe";
            string queryString = "SELECT nagrody.id_nagrody,nazwisko||': '||nazwa as dane from laureat JOIN nagrody on laureat.nagrody_id_nagrody = nagrody.id_nagrody  join kompozytorzy on laureat.kompozytorzy_id_kompozytor = kompozytorzy.id_kompozytor ";


            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                connection.Open();
                OracleDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Nagrody n = new Nagrody();
                        n.id = reader.GetInt32(0);
                        n.dane = reader["dane"].ToString();
                        comboBox1.Items.Add(n);
                        // Console.WriteLine(reader.GetInt32(0) + ", " + reader.GetString(1));
                        Console.WriteLine(n);
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
            Nagrody n = (Nagrody)((ComboBox)sender).SelectedItem;
            string connectionString = "User Id=TEST10;Password=puszek;Data Source=localhost:1521/xe";
            string queryString = "SELECT nagrody.id_nagrody,nazwisko,nazwa from laureat JOIN nagrody on laureat.nagrody_id_nagrody = nagrody.id_nagrody  join kompozytorzy on laureat.kompozytorzy_id_kompozytor = kompozytorzy.id_kompozytor where id_nagrody =:id_nagrody ";


            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                command.BindByName = true;
                OracleParameter op = new OracleParameter(":id_nagrody", n.id);
                command.Parameters.Add(op);
                connection.Open();
                OracleDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        textBox1.Text = reader["nazwisko"].ToString();
                        textBox2.Text = reader["nazwa"].ToString();
                        // Console.WriteLine(reader.GetInt32(0) + ", " + reader.GetString(1));
                        Console.WriteLine(n);
                    }
                }
                finally
                {
                    // always call Close when done reading.
                    reader.Close();
                }
            }
        }
    }
}
