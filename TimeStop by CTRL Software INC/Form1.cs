using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Diagnostics;

namespace TimeStop_by_CTRL_Software_INC
{
    public partial class Form1 : Form
    {
        List<ProgList> Programs = new List<ProgList>();

        private int[] Usage = new int[7];
        private int Limit = 180;


         List<ProgList> LoadPrograms(string path = @"TimeStop.csv")
        {
            List<ProgList> data = new List<ProgList>();
            if (File.Exists(path))
            {
                string[] lines = System.IO.File.ReadAllLines(path);
                // Display the file contents by using a foreach loop.
                foreach (string line in lines)
                {
                    string[] settings = line.Split(';');
                    ProgList item = new ProgList();
                    item.Name = settings[0];
                    item.Path = settings[1];
                    item.Version = settings[2];
                    item.Timer = bool.Parse(settings[3]);
                    item.Warn = bool.Parse(settings[4]);
                    item.Stop = bool.Parse(settings[5]);
                    data.Add(item);
                }
            }
            
            return data;
        }
        void SavePrograms(List<ProgList> listofa, string path= @"TimeStop.csv")
        {
            string data = "";
            foreach (ProgList item in listofa)
            {
                data+=item.Name+";"+item.Path+";"+item.Version + ";"+item.Timer + ";" + item.Warn + ";" + item.Stop + "\n";
            }
            //MessageBox.Show(data);
            System.IO.File.WriteAllText(path, data);
            SaveSettings();
        }

        void LoadSettings()
        {
            string path = @"TimeStop.settings.csv";
            string lines = System.IO.File.ReadAllText(path);
            string[] data = lines.Split(';');

            checkBox7.Checked = bool.Parse(data[0]); //conts any
            checkBox6.Checked = bool.Parse(data[1]); //warn 
            checkBox5.Checked = bool.Parse(data[2]); //lock
            checkBox4.Checked = bool.Parse(data[3]); //identical to workdays
            numericUpDown1.Value = int.Parse(data[4]); //workday h
            numericUpDown2.Value = int.Parse(data[5]); //workday m
            numericUpDown4.Value = int.Parse(data[6]); //weekend h
            numericUpDown3.Value = int.Parse(data[7]); //weekend m

            checkBox8.Checked = bool.Parse(data[8]); //restrict only workdays
            numericUpDown8.Value = int.Parse(data[9]); //workday start h
            numericUpDown7.Value = int.Parse(data[10]); //workday start m
            numericUpDown6.Value = int.Parse(data[11]); //workday end h
            numericUpDown5.Value = int.Parse(data[12]); //workday end m

            if (DateTime.Today.ToString("yyyy-MM-dd") == data[13])
            {
                for (int i = 14; i < 14+7; i++)
                {
                    Usage[i - 14] = int.Parse(data[i]);
                }
            }
            else
            {
                int difference = (int)((DateTime.Today - DateTime.ParseExact(data[13], "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture)).TotalDays);
                if (difference<7)
                {
                    Usage[6] = 0;
                    for (int i = 14+difference, j=0; j < 7-difference; i++,j++)
                    {
                        Usage[j] = int.Parse(data[i]);
                    }
                }
                
            }

            //DateTime.Today.ToString("MM-dd") + ";"; //today
            //foreach (int usageday in Usage)
            //{
             //   data += usageday + ";";
            //}
        }


        void SaveSettings()
        {
            string path = @"TimeStop.settings.csv";
            string data = "";
            data += checkBox7.Checked+";"; //conts any
            data += checkBox6.Checked + ";"; //warn 
            data += checkBox5.Checked + ";"; //lock
            data += checkBox4.Checked + ";"; //identical to workdays
            data += numericUpDown1.Value + ";"; //workday h
            data += numericUpDown2.Value + ";"; //workday m
            data += numericUpDown4.Value + ";"; //weekend h
            data += numericUpDown3.Value + ";"; //weekend m
            data += checkBox8.Checked + ";"; //restrict only workdays
            data += numericUpDown8.Value + ";"; //workday start h
            data += numericUpDown7.Value + ";"; //workday start m
            data += numericUpDown6.Value + ";"; //workday end h
            data += numericUpDown5.Value + ";"; //workday end m
            data += DateTime.Today.ToString("yyyy-MM-dd") + ";"; //today
            foreach (int usageday in Usage)
            {
                data += usageday + ";";
            }
            //MessageBox.Show(data);
            System.IO.File.WriteAllText(path, data);
        }

        private void Load_Stats()
        {
            DateTime today = DateTime.Today;

            

            label17.Text = today.ToString("yyyy-MM-dd");
            label15.Text = today.AddDays(-1).ToString("yyyy-MM-dd");
            label13.Text = today.AddDays(-2).ToString("yyyy-MM-dd");
            label11.Text = today.AddDays(-3).ToString("yyyy-MM-dd");
            label9.Text = today.AddDays(-4).ToString("yyyy-MM-dd");
            label7.Text = today.AddDays(-5).ToString("yyyy-MM-dd");
            label4.Text = today.AddDays(-6).ToString("yyyy-MM-dd");

            if ((numericUpDown1.Value * 60 + numericUpDown2.Value) > (numericUpDown4.Value * 60 + numericUpDown3.Value))
            {
                Limit = (int)(numericUpDown1.Value * 60 + numericUpDown2.Value);
            }
            else
            {
                Limit = (int)(numericUpDown4.Value * 60 + numericUpDown3.Value);
            }
            Update_stats();

        }

        void Update_stats()
        {
            label5.Text = ((int)(Usage[0] / 60)).ToString() + " h " + (Usage[0] % 60).ToString() + " min";
            label6.Text = ((int)(Usage[1] / 60)).ToString() + " h " + (Usage[1] % 60).ToString() + " min";
            label8.Text = ((int)(Usage[2] / 60)).ToString() + " h " + (Usage[2] % 60).ToString() + " min";
            label10.Text = ((int)(Usage[3] / 60)).ToString() + " h " + (Usage[3] % 60).ToString() + " min";
            label12.Text = ((int)(Usage[4] / 60)).ToString() + " h " + (Usage[4] % 60).ToString() + " min";
            label14.Text = ((int)(Usage[5] / 60)).ToString() + " h " + (Usage[5] % 60).ToString() + " min";
            label16.Text = ((int)(Usage[6] / 60)).ToString() + " h " + (Usage[6] % 60).ToString() + " min";
            toolStripStatusLabel2.Text = "Today Time Spent: " + ((int)(Usage[6] / 60)).ToString() + " h " + (Usage[0] % 60).ToString() + " min";
            if ((numericUpDown1.Value * 60 + numericUpDown2.Value) > (numericUpDown4.Value * 60 + numericUpDown3.Value))
            {
                Limit = (int)(numericUpDown1.Value * 60 + numericUpDown2.Value);
            }
            else
            {
                Limit = (int)(numericUpDown4.Value * 60 + numericUpDown3.Value);
            }
            if (Limit!= progressBar1.Maximum)
            {
                


                progressBar1.Maximum = Limit;
                progressBar2.Maximum = Limit;
                progressBar3.Maximum = Limit;
                progressBar4.Maximum = Limit;
                progressBar5.Maximum = Limit;
                progressBar6.Maximum = Limit;
                progressBar7.Maximum = Limit;
                toolStripProgressBar1.Maximum = Limit;
            }
            if (Usage[0] <= Limit)
            {
                progressBar1.Value = Usage[0];
            }
            else
            {
                progressBar1.Style = ProgressBarStyle.Marquee;
            }

            if (Usage[1] <= Limit)
            {
                progressBar2.Value = Usage[1];
            }
            else
            {
                progressBar2.Style = ProgressBarStyle.Marquee;
            }

            if (Usage[2] <= Limit)
            {
                progressBar3.Value = Usage[2];
            }
            else
            {
                progressBar3.Style = ProgressBarStyle.Marquee;
            }

            if (Usage[3] <= Limit)
            {
                progressBar4.Value = Usage[3];
            }
            else
            {
                progressBar4.Style = ProgressBarStyle.Marquee;
            }

            if (Usage[4] <= Limit)
            {
                progressBar5.Value = Usage[4];
            }
            else
            {
                progressBar5.Style = ProgressBarStyle.Marquee;
            }

            if (Usage[5] <= Limit)
            {
                progressBar6.Value = Usage[5];
            }
            else
            {
                progressBar6.Style = ProgressBarStyle.Marquee;
            }

            if (Usage[6] <= Limit)
            {
                progressBar7.Value = Usage[6];
                toolStripProgressBar1.Value = Usage[6];
            }
            else
            {
                progressBar7.Style = ProgressBarStyle.Marquee;
                toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            }

            //progressBar2.Value = Usage[1];
            //progressBar3.Value = Usage[2];
            //progressBar4.Value = Usage[3];
            //progressBar5.Value = Usage[4];
            //progressBar6.Value = Usage[5];
            //progressBar7.Value = Usage[6];
            //toolStripProgressBar1.Value = Usage[6];

            toolStripStatusLabel3.Text = "Limit: "+Limit.ToString()+" min";


        }

        void UpdateProgList() {
            listBox1.Items.Clear();
            foreach (ProgList item in Programs)
            {
                listBox1.Items.Add(item.Name);
            }
        }

        void enableEdit()
        {
            textBox1.ReadOnly = false;
            textBox2.ReadOnly = false;
            textBox3.ReadOnly = false;




            checkBox1.AutoCheck = true;
            checkBox2.AutoCheck = true;
            checkBox3.AutoCheck = true;

            groupBox1.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = true;
        }

        void disableEdit()
        {
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;

            button1.Enabled = false;
            button2.Enabled = false;


            checkBox1.AutoCheck = false;
            checkBox2.AutoCheck = false;
            checkBox3.AutoCheck = false;

        }

        public Form1()
        {
            InitializeComponent();


            Programs = LoadPrograms();

            LoadSettings();
            Load_Stats();
            UpdateProgList();
            //Programs.Add(new ProgList("CS:GO", "C:\\Program Files\\CSGO\\csgo.exe", "4.20.11.69",true,true,false));
            //Programs.Add(new ProgList("League Of Legends", "C:\\Program Files\\Salt Mine\\lolXD.exe", "6.6.6"));



            //listBox1.SetSelected(0,true);
            //SavePrograms(Programs);
            //MessageBox.Show(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)+"\\TimeStop.xml","test");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (listBox1.SelectedItem.ToString() == "ANY")
            //{
            //    textBox1.Text = "";
            //    textBox2.Text = "";
            //    textBox3.Text = "";
            //    groupBox1.Enabled = false;
            //    checkBox1.Checked = false;
            //    checkBox2.Checked = true;
            //    checkBox3.Checked = false;
            //}
            //else
            //{

            if (button2.Enabled)
            {
                disableEdit();
            }

            if (listBox1.SelectedIndex != -1)
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
            
            //}
            
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            label23.Enabled = !checkBox4.Checked;
            label22.Enabled = !checkBox4.Checked;
            label21.Enabled = !checkBox4.Checked;
            numericUpDown3.Enabled = !checkBox4.Checked;
            numericUpDown4.Enabled = !checkBox4.Checked;
            if (checkBox4.Checked)
            {
                numericUpDown4.Value = numericUpDown1.Value;
                numericUpDown3.Value = numericUpDown2.Value;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SavePrograms(Programs);
        }


        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (listBox1.Items.Count>0)
            {
                listBox1.SetSelected(listBox1.FindString(textBox4.Text), true);
            }
            
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown5.Enabled = checkBox8.Checked;
            numericUpDown6.Enabled = checkBox8.Checked;
            numericUpDown7.Enabled = checkBox8.Checked;
            numericUpDown8.Enabled = checkBox8.Checked;

            label25.Enabled = checkBox8.Checked;
            label26.Enabled = checkBox8.Checked;
            label27.Enabled = checkBox8.Checked;
            label28.Enabled = checkBox8.Checked;
            label29.Enabled = checkBox8.Checked;
            label24.Enabled = checkBox8.Checked;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
            {
                Usage[6]++;
                Update_stats();
                if (checkBox6.Checked && Limit-Usage[6]==5)
                {
                    MessageBox.Show("Time left: 5 min", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if(checkBox5.Checked && Limit - Usage[6] <=0) {
                    MessageBox.Show("Locking User off!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                var allProcesses = Process.GetProcesses();
                bool found = false;
                foreach (Process item in allProcesses)
                {
                    foreach (ProgList item2 in Programs)
                    {
                        if (item2.Timer==true)
                        {
                            if (item.ProcessName + ".exe" == item2.Path)
                            {
                                Usage[6]++;
                                Update_stats();
                                found = true;
                                if (item2.Warn && Limit - Usage[6] == 5)
                                {
                                    MessageBox.Show("Time left: 5 min", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else if (item2.Stop && Limit - Usage[6] <= 0)
                                {
                                    item.Kill();
                                    MessageBox.Show("Stopping program!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }

                                break;
                            }
                        }
                    }
                    if (found)
                    {
                        break;
                    }
                }
            }

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                numericUpDown4.Value = numericUpDown1.Value;
            }
            if (numericUpDown1.Value == 0 && numericUpDown2.Value==0)
            {
                numericUpDown2.Value = 1;
            }
            else if (numericUpDown1.Value == 24)
            {
                numericUpDown2.Value = 0;
            }
            Update_stats();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                numericUpDown3.Value = numericUpDown2.Value;
            }
            if (numericUpDown1.Value==24)
            {
                numericUpDown2.Value = 0;
            }
            else if (numericUpDown1.Value ==0 && numericUpDown2.Value == 0)
            {
                numericUpDown2.Value = 1;
            }
            Update_stats();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {

            if (numericUpDown4.Value == 0 && numericUpDown3.Value == 0)
            {
                numericUpDown3.Value = 1;
            }
            else if (numericUpDown4.Value == 24)
            {
                numericUpDown3.Value = 0;
            }
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown4.Value == 24)
            {
                numericUpDown3.Value = 0;
            }
            else if (numericUpDown4.Value == 0 && numericUpDown3.Value == 0)
            {
                numericUpDown3.Value = 1;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDiag = new OpenFileDialog();
            OpenFileDiag.Title = "Chose CSV file to import";
            if (OpenFileDiag.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show(OpenFileDiag.FileName);
                List<ProgList> temp = LoadPrograms(OpenFileDiag.FileName);

                foreach (ProgList item in temp)
                {
                    bool found = false;
                    foreach (ProgList searcher in Programs)
                    {
                        if (item.Path==searcher.Path)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        Programs.Add(item);
                    }
                    //if (Programs.Contains(item,new IEqualityComparer<ProgList>)
                    //{
                    //    //Programs.Add(item);
                    //    MessageBox.Show(item.Name);
                    //}
                }
                UpdateProgList();
                SavePrograms(Programs);

            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog SafeFileDiag = new SaveFileDialog();
            SafeFileDiag.Title = "Chose CSV file save location";
            if (SafeFileDiag.ShowDialog() == DialogResult.OK)
            {
                SavePrograms(Programs,SafeFileDiag.FileName);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void removeItemToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (listBox1.Items.Count > 0)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";

                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                groupBox1.Enabled = false;

                for (int i = 0; i < Programs.Count; i++)
                {
                    if (listBox1.SelectedItem.ToString() == Programs[i].Name)
                    {
                        Programs.RemoveAt(i);
                        break;
                    }
                }

                UpdateProgList();
                SavePrograms(Programs);
                //}
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            listBox1.SelectedIndex = -1;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

            enableEdit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                if (listBox1.SelectedIndex<0)
                {
                    bool found = false;
                    foreach (ProgList searcher in Programs)
                    {
                        if (textBox2.Text == searcher.Path)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        ProgList item = new ProgList(textBox1.Text, textBox2.Text, textBox3.Text, checkBox1.Checked, checkBox2.Checked, checkBox3.Checked);
                        Programs.Add(item);
                        SavePrograms(Programs);
                        UpdateProgList();
                    }
                    else
                    {
                        MessageBox.Show("Program with same executable already found! Skipping!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    Programs[listBox1.SelectedIndex] = new ProgList(textBox1.Text, textBox2.Text, textBox3.Text, checkBox1.Checked, checkBox2.Checked, checkBox3.Checked);
                    //Programs.Add(item);
                    SavePrograms(Programs);
                    UpdateProgList();
                        
                       // MessageBox.Show("Program with same executable already found! Skipping!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                }
                

                disableEdit();

            }
            else
            {
                MessageBox.Show("Please Enter Program Name and Executable Name!", "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Usage[6] = 0;
            Update_stats();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            enableEdit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDiag = new OpenFileDialog();
            OpenFileDiag.Title = "Chose program executable";
            if (OpenFileDiag.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = OpenFileDiag.SafeFileName;
            }
        }
    }
}
