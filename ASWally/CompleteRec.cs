/*  
*  FILE          : CompleteRec.cs 
*  PROJECT       : RelationalDatabase - Assignment 4
*  PROGRAMMER    : Arindm Sharma
*  DESCRIPTION   : The purpose for this file is to provide the funcitonality for the form that prvides the functionality to complete a record.
*/
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ASWally
{
    public partial class CompleteRec : Form
    {
        public static string Dates;
        public static string userID;
        public CompleteRec()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConnectDB obj = new ConnectDB();
            string fname;
            string lname;
            string phone;

            fname = textBox1.Text;
            lname = textBox2.Text;
            phone = textBox3.Text;

            bool ret = numberValid(phone);
            if (ret == true)
            {
                string tosend = "insert into customer(FirstName, LastName, PhoneNo) values('" + fname + "', '" + lname + "', '" + phone + "')";
                obj.Insert(tosend);

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";

                string orderiD = obj.getLastCustomerID();

                label5.Text = "customer added to the database succesfully! YOUR Customer ID -" + orderiD;
            }
            else
            {
                label5.Text = "invalid phone number!";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NewOrder.proid0 = 0;
            NewOrder.proid1 = 0;
            NewOrder.proid2 = 0;
            NewOrder.proid3 = 0;
            NewOrder.proid4 = 0;
            NewOrder.proid5 = 0;

            bool IDflag = false;
            ConnectDB obj = new ConnectDB();
            Dictionary<int, Customer> ob = new Dictionary<int, Customer>();
            ob = obj.getAllCustomers();

            foreach (KeyValuePair<int, Customer> kvp in ob)
            {
                if (textBox5.Text == kvp.Value.CustomerID)
                {
                    IDflag = true;
                }
                
            }
            if (IDflag)
            {
                bool retval = dateValid(textBox4.Text);
                if (retval == true)
                {
                    Dates = textBox4.Text;
                    userID = textBox5.Text;

                    NewOrder click = new NewOrder(textBox4.Text, textBox5.Text);
                    click.Show();
                }
                else
                {
                    label5.Text = "incorrect date provided!";
                }
            }
            else
            {
                label12.Text = "this customer doesnt exist in the Database!";
            }
        }

        public bool numberValid(string n)
        {
            bool retval = false;
            if (n.Length == 12)
            {
                if ((n[3] == '-') && (n[7] == '-'))
                {
                    string temp;
                    temp = n.Substring(0, 3);
                    temp = temp + n.Substring(4, 3);
                    temp = temp + n.Substring(8, 4);

                    retval = numCharValid(temp);
                }
            }

            return retval;
        }
        public bool numCharValid(string format)
        {
            string allowableLetters = "0123456789";

            if (format.Length == 10)
            {
                foreach (char c in format)
                {
                    // This is using String.Contains for .NET 2 compat.,
                    //   hence the requirement for ToString()
                    if (!allowableLetters.Contains(c.ToString()))
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }
        public bool dateValid(string d)
        {
            bool retVal1;
            if ((d == "") || (d == "N/A"))
            {
                return true;
            }
            else
            {
                d = Regex.Replace(d, @"\s+", ""); // removing spaces.
                d = Regex.Replace(d, @"[-]", ""); // removing spaces.
                retVal1 = dateCharValid(d); // checks if thee date only contians numbers.

                if (retVal1 == true)
                {


                    d = d.Insert(4, "-");
                    d = d.Insert(7, "-");

                    DateTime temp;
                    if (DateTime.TryParse(d, out temp))
                    {
                        if (temp <= DateTime.Now)
                        {
                            return true;
                        }
                        return false;

                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }
            }
        }

        public bool dateCharValid(string format)
        {
            string allowableLetters = "0123456789";

            if (format.Length == 8)
            {
                foreach (char c in format)
                {
                    // This is using String.Contains for .NET 2 compat.,
                    //   hence the requirement for ToString()
                    if (!allowableLetters.Contains(c.ToString()))
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string number = textBox6.Text;
            ConnectDB obj = new ConnectDB();
            Customer result = new Customer();
            result = obj.SelectCustomerPhone(number);

            customerSearch form = new customerSearch(result);
            form.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string lastname = textBox7.Text;
            ConnectDB obj = new ConnectDB();
            Customer result = new Customer();
            result = obj.SelectCustomerLastName(lastname);

            customerSearch form = new customerSearch(result);
            form.Show();
        }
    }
}
