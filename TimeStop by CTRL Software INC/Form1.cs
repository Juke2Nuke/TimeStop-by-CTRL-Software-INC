using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeStop_by_CTRL_Software_INC
{
    public partial class Form1 : Form
    {
        List<ProgList> Programs = new List<ProgList>();

        private int[] Usage = { 90, 120, 180, 120, 20, 160,60};
        private int Limit = 180;
        private void Load_Stats()
        {
            DateTime today = DateTime.Today;

            label5.Text = ((int)(Usage[0]/60)).ToString() + " h " + (Usage[0] % 60).ToString() + " min";
            label6.Text = ((int)(Usage[1] / 60)).ToString() + " h " + (Usage[1] % 60).ToString() + " min";
            label8.Text = ((int)(Usage[2] / 60)).ToString() + " h " + (Usage[2] % 60).ToString() + " min";
            label10.Text = ((int)(Usage[3] / 60)).ToString() + " h " + (Usage[3] % 60).ToString() + " min";
            label12.Text = ((int)(Usage[4] / 60)).ToString() + " h " + (Usage[4] % 60).ToString() + " min";
            label14.Text = ((int)(Usage[5] / 60)).ToString() + " h " + (Usage[5] % 60).ToString() + " min";
            label16.Text = ((int)(Usage[6] / 60)).ToString() + " h " + (Usage[6] % 60).ToString() + " min";
            toolStripStatusLabel2.Text = "Today Time Spent: "+((int)(Usage[0] / 60)).ToString() + " h " + (Usage[0] % 60).ToString() + " min";

            label17.Text = today.ToString("MM-dd");
            label15.Text = today.AddDays(-1).ToString("MM-dd");
            label13.Text = today.AddDays(-2).ToString("MM-dd");
            label11.Text = today.AddDays(-3).ToString("MM-dd");
            label9.Text = today.AddDays(-4).ToString("MM-dd");
            label7.Text = today.AddDays(-5).ToString("MM-dd");
            label4.Text = today.AddDays(-6).ToString("MM-dd");

            progressBar1.Maximum = Limit;
            progressBar2.Maximum = Limit;
            progressBar3.Maximum = Limit;
            progressBar4.Maximum = Limit;
            progressBar5.Maximum = Limit;
            progressBar6.Maximum = Limit;
            progressBar7.Maximum = Limit;
            toolStripProgressBar1.Maximum = Limit;

            progressBar1.Value = Usage[0];
            toolStripProgressBar1.Value = Usage[0];
            progressBar2.Value = Usage[1];
            progressBar3.Value = Usage[2];
            progressBar4.Value = Usage[3];
            progressBar5.Value = Usage[4];
            progressBar6.Value = Usage[5];
            progressBar7.Value = Usage[6];

        }

        public Form1()
        {
            InitializeComponent();



            Programs.Add(new ProgList("CS:GO", "C:\\Program Files\\CSGO\\csgo.exe", "4.20.11.69",true,true,false));
            Programs.Add(new ProgList("League Of Legends", "C:\\Program Files\\Salt Mine\\lolXD.exe", "6.6.6"));

            foreach (var item in Programs)
            {
                listBox1.Items.Add(item.Name);
            }

                Load_Stats();
            listBox1.SetSelected(0,true);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem.ToString() == "ANY")
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                groupBox1.Enabled = false;
                checkBox1.Checked = false;
                checkBox2.Checked = true;
                checkBox3.Checked = false;
            }
            else
            {
                groupBox1.Enabled = true;
                foreach (var item in Programs)
                {
                    if (listBox1.SelectedItem.ToString() == item.Name)
                    {
                        textBox1.Text = item.Name;
                        textBox2.Text = item.Path;
                        textBox3.Text = item.Version;

                        checkBox1.Checked = item.Timer;
                        checkBox2.Checked = item.Warn;
                        checkBox3.Checked = item.Stop;
                    }
                }
            }
            
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            label23.Enabled = !checkBox4.Checked;
            label22.Enabled = !checkBox4.Checked;
            label21.Enabled = !checkBox4.Checked;
            numericUpDown3.Enabled = !checkBox4.Checked;
            numericUpDown4.Enabled = !checkBox4.Checked;
        }
    }
}
