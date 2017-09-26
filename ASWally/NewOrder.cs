/*  
*  FILE          : NewOrder.cs 
*  PROJECT       : RelationalDatabase - Assignment 4
*  PROGRAMMER    : Arindm Sharma
*  DESCRIPTION   : The purpose for this file is to provide functionality for the form that provides the user the ability to place a new order.
*/
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ASWally
{
    public partial class NewOrder : Form
    {
        // class level variables.
        string dateOfOrder;
        string CustomerID;
        string BranchID;
        string Status;

        string orderIDs;

        int want = -1;

        public static int proid0 = 0;
        public static int proid1 = 0;
        public static int proid2 = 0;
        public static int proid3 = 0;
        public static int proid4 = 0;
        public static int proid5 = 0;

        public static bool statusFlag = false;

        int i = 0;

        Dictionary<int, productUpdate> prolist = new Dictionary<int, productUpdate>();

        ConnectDB obj = new ConnectDB();

        string thisOrder;

        public NewOrder(string date, string membership)
        {
            InitializeComponent();

            // inserting orders.
            dateOfOrder = date;
            CustomerID = membership;
            BranchID = "2";
            Status = "";

            string tosend = "insert into orders(OrderDate, BranchID, CustomerID, OrderStatus) values('" + dateOfOrder + "', '" + BranchID + "', '" + CustomerID + "', '" + Status + "')";
            i = 0;
            ConnectDB obj = new ConnectDB();
            obj.Insert(tosend);

            orderIDs = obj.getLastOrderID();

            textBox3.Text = "Product | Quantity\r\n";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label4.Text = "";
            textBox1.Text = button1.Text;
            want = 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label4.Text = "";
            textBox1.Text = button2.Text;
            want = 2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label4.Text = "";
            textBox1.Text = button3.Text;
            want = 3;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label4.Text = "";
            textBox1.Text = button4.Text;
            want = 4;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label4.Text = "";
            textBox1.Text = button5.Text;
            want = 5;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label4.Text = "";
            textBox1.Text = button6.Text; // check which button they pressed.
            want = 6;// set the pid of the product.
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                label4.Text = "please provide the quantity!";
            }
            else
            {
                int quantity = int.Parse(textBox2.Text);
                string productName = textBox1.Text;
                int have = 0;
                
                string orderID = obj.getLastOrderID();

                string tosend = want.ToString();

                string available = obj.getQuantity(tosend);
                have = int.Parse(available);


                if (have < quantity)
                {
                    var result = MessageBox.Show("we do not have enough " + productName + " available, press yes to add the order as pending or no to not add it to the cart!", "Quantity Alert!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        Status = "PEND";
                        statusFlag = true;

                        // updating quantity depending on the product choosen.
                        if (want == 1)
                        {
                            proid0 = proid0 + quantity;
                        }
                        else if (want == 2)
                        {
                            proid1 = proid1 + quantity;
                        }
                        else if (want == 3)
                        {
                            proid2 = proid2 + quantity;
                        }
                        else if (want == 4)
                        {
                            proid3 = proid3 + quantity;
                        }
                        else if (want == 5)
                        {
                            proid4 = proid4 + quantity;
                        }
                        else if (want == 6)
                        {
                            proid5 = proid5 + quantity;
                        }
                    }

                }
                else
                {
                    
                    obj.Insert("insert into orderline(OrderID, ProductID, Quantity) values('" + orderID + "', '" + tosend + "', '" + textBox2.Text + "')");
                    textBox3.Text += textBox1.Text + " | " + textBox2.Text + "\r\n";
                    Status = "PAID";

                    productUpdate list = new productUpdate();
                    list.PID = want;
                    list.quanity = quantity;

                    prolist.Add(i, list);
                    i++;

                    // updating quantity depending on the product choosen.
                    if (want == 1)
                    {
                        proid0 = proid0 + quantity;
                    }
                    else if (want == 2)
                    {
                        proid1 = proid1 + quantity;
                    }
                    else if (want == 3)
                    {
                        proid2 = proid2 + quantity;
                    }
                    else if (want == 4)
                    {
                        proid3 = proid3 + quantity;
                    }
                    else if (want == 5)
                    {
                        proid4 = proid4 + quantity;
                    }
                    else if (want == 6)
                    {
                        proid5 = proid5 + quantity;
                    }
                }
                tosend = "UPDATE orders SET OrderStatus ='" + Status + "' WHERE OrderID =" + orderID;
                obj.Update(tosend);

                thisOrder = orderID;

                textBox2.Text = "";
                textBox1.Text = "";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // check if the order was pending.
            if (statusFlag == true)
            {
                Status = "PEND";
                string tosend = "UPDATE orders SET OrderStatus ='" + Status + "' WHERE OrderID =" + thisOrder;
                obj.Update(tosend);
            }
            else
            {

                foreach (KeyValuePair<int, productUpdate> kvp in prolist)
                {

                    string tosend = "UPDATE product SET Quantity = quantity - '" + kvp.Value.quanity.ToString() + "' WHERE ProductID =" + kvp.Value.PID.ToString();
                    obj.Update(tosend);

                }
            }
            produceSalesRecord sale = new produceSalesRecord(orderIDs);
            sale.Show();
        }

    }
}
