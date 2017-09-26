/*  
*  FILE          : PendingOrders.cs 
*  PROJECT       : RelationalDatabase - Assignment 4
*  PROGRAMMER    : Arindm Sharma
*  DESCRIPTION   : The purpose for this file is to provide the functionality for the form that prints the pending orders
*/

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ASWally
{

    public partial class PendingOrders : Form
    {
        ConnectDB obj = new ConnectDB();
        Order toedit = new Order();
        string tosend;
        public PendingOrders()
        {
            InitializeComponent();
        }
       
        private void button4_Click(object sender, EventArgs e)
        {
            string orderID = textBox2.Text;

            Order reply = new Order();
            reply = obj.SelectOrderByID(orderID);

            toprint(reply);
           

            toedit= reply;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // to paid.
            tosend = "update orders set OrderStatus = 'PAID' where orderID =";
            tosend = tosend + "'" +toedit.OrderID+ "'";
            obj.Update(tosend);
            Dictionary<int, OrderList> ret = new Dictionary<int, OrderList>();

            ret = obj.getOrderListByID(toedit.OrderID);

            foreach (KeyValuePair<int, OrderList> kvp in ret)
            {
                tosend = "update product set Quantity = Quantity -"+ kvp.Value.Quantity+" where ProductID = " + kvp.Value.ProductID;
                obj.Update(tosend);
               
            }

            Order reply = new Order();
            reply = obj.SelectOrderByID(toedit.OrderID);

            toprint(reply);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // to refund
            tosend = "update orders set OrderStatus = 'RFND' where orderID =";
            tosend = tosend + "'" + toedit.OrderID + "'";
            obj.Update(tosend);

            Dictionary<int, OrderList> ret = new Dictionary<int, OrderList>();

            ret = obj.getOrderListByID(toedit.OrderID);

            foreach (KeyValuePair<int, OrderList> kvp in ret)
            {
                tosend = "update product set Quantity = Quantity +" + kvp.Value.Quantity + " where ProductID = " + kvp.Value.ProductID;
                obj.Update(tosend);

            }
            Order reply = new Order();
            reply = obj.SelectOrderByID(toedit.OrderID);

            toprint(reply);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // to cancel
            tosend = "update orders set OrderStatus = 'CNCL' where orderID =";
            tosend = tosend + "'" + toedit.OrderID + "'";
            obj.Update(tosend);

            Order reply = new Order();
            reply = obj.SelectOrderByID(toedit.OrderID);

            toprint(reply);
        }

        private void toprint(Order obj)
        {
            textBox1.Text = "orderID | order Date | customer ID | Status\r\n";
            textBox1.Text += obj.OrderID + " | " + obj.OrderDate + " | " + obj.CustomerID + " | " + obj.Status;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Dictionary<int,Order > reply = new Dictionary<int, Order>();
            reply = obj.SelectOrderByCustID(textBox4.Text);
            textBox1.Text = "orderID | order Date | customer ID | Status\r\n";
            foreach (KeyValuePair<int, Order> kvp in reply)
            {
                textBox1.Text += kvp.Value.OrderID + " | " + kvp.Value.OrderDate + " | " + kvp.Value.CustomerID + " | " + kvp.Value.Status+"\r\n";
            }
        }
    }
}
