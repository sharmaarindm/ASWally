/*  
*  FILE          : Form1.cs 
*  PROJECT       : RelationalDatabase - Assignment 4
*  PROGRAMMER    : Arindm Sharma
*  DESCRIPTION   : The purpose for this file is to provide the funcitonality for the startup form.
*/
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ASWally
{
    public partial class Form1 : Form
    {
        public static string Branch;
        public Form1()
        {
            InitializeComponent();


            ConnectDB obj = new ConnectDB();
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            dictionary = obj.getBranches();
            int i = 0;
            string buffer;
            foreach (KeyValuePair<int, string> kvp in dictionary)
            {
                // reding from dictionary the branches and adding it to the combo box.
                dictionary.TryGetValue(i, out buffer);
                comboBox1.Items.Add(buffer);
                i++;
            }

            
        }

        // spawning respective forms depending on the button selected.

        private void button3_Click(object sender, EventArgs e)
        {
            Branch = comboBox1.Text;
            currectInventory click3 = new currectInventory();
            click3.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Branch = comboBox1.Text;
            PendingOrders click2 = new PendingOrders();
            click2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Branch = comboBox1.Text;
            CompleteRec click1 = new CompleteRec();
            click1.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true; // enable buttons only when the brach in selected.
            button2.Enabled = true;
            button3.Enabled = true;
        }
    }
}
