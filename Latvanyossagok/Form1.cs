using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Latvanyossagok
{
    public partial class Form1 : Form
    {
        MySqlConnection conn;
        public Form1()
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=latvanyossagokdb;Uid=root;Pwd=;");
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = @"CREATE TABLE IF NOT EXISTS `latvanyossagokdb`.`varosok`
            ( `id` INT NOT NULL AUTO_INCREMENT , `nev` VARCHAR(70) NOT NULL ,
            PRIMARY KEY (`id`), UNIQUE (`nev`)) ENGINE = InnoDB,
            ALTER TABLE `varosok` ADD `lakossag` INT NOT NULL AFTER `nev`;
            ) ";
            command.CommandText = @"CREATE TABLE IF NOT EXISTS `latvanyossagokdb`.`latvanyossagok` ( `id` INT NOT NULL AUTO_INCREMENT ,
            `nev` VARCHAR(70) NOT NULL ,
            `leiras` VARCHAR(70) NOT NULL ,  `ar` INT NOT NULL ,
            `varos_id` INT NOT NULL ,    PRIMARY KEY  (`id`)) ENGINE = InnoDB;)";

        }

        public void varoslistazas()
        {
            
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id, nev, lakossag from varosok";
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var id = reader.GetInt32("id");
                    var nev = reader.GetString("nev");
                    var lakossag = reader.GetInt32("lakossag");
                    
                    listBox1.Items.Add(id, nev, lakossag);
                }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void buttonkuldes_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(textBox1.Text))
                {
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = @"INSERT INTO varosok(nev,lakossag) VALUES (@nev, @lakossag)";
                    cmd.Parameters.AddWithValue("@nev", textBox1.Text);
                    cmd.Parameters.AddWithValue("@lakossag", lakossagnumeric.Value);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("nem toltott ki mindent");
                }
               
            }
            catch (MySqlException ex )
            {
                if (ex.Number == 1062)
                {
                    MessageBox.Show("mar van ilyen nevu varos");
                }
                
            }
            



        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click_1(object sender, EventArgs e)//latvanyossaggomb
        {
            if (!String.IsNullOrWhiteSpace(textboxnev.Text)&& !String.IsNullOrWhiteSpace(textBoxleiras.Text)&& latvanyossagarnumeric.Value <1 && comboBoxvaros.SelectedIndex == -1)
            {
                var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO latvanyossagok(nev,leiras,ar,varos_id) VALUES (@nev, @leiras,@ar,@varos_id)";
            cmd.Parameters.AddWithValue("@nev", textboxnev.Text);
            cmd.Parameters.AddWithValue("@leiras", textBoxleiras.Text);
            cmd.Parameters.AddWithValue("@ar", latvanyossagarnumeric.Value);
            cmd.Parameters.AddWithValue("@varos_id", comboBoxvaros.SelectedItem);

                cmd.ExecuteNonQuery();
            }
            else
            {
                MessageBox.Show("nem toltott ki mindent");
            }



        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void listatorlesbutton_Click(object sender, EventArgs e)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id, nev, lakossag from varosok";
            if (varosok)
            {

            }

        }
    }
}
