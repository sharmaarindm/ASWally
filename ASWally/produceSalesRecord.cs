/*  
*  FILE          : produceSalesRecord.cs 
*  PROJECT       : RelationalDatabase - Assignment 4
*  PROGRAMMER    : Arindm Sharma
*  DESCRIPTION   : The purpose for this file is to provide functionality for the form that acts as the sales record.
*/
using System;
using System.Windows.Forms;

namespace ASWally
{
    public partial class produceSalesRecord : Form
    {

        public produceSalesRecord(string ID)
        {
            InitializeComponent();
            ConnectDB obj = new ConnectDB();
            obj.getUserNameForID(CompleteRec.userID);
            

            // variable declaration.
            double pay;
            textBox1.Text = "Thank you for shopping at\r\nWally’s World "+Form1.Branch;

            textBox1.Text += "\r\nOn " + CompleteRec.Dates +"," + ConnectDB.retValF+" " + ConnectDB.retValL+"!";
            textBox1.Text += "\r\nOrder ID:" + obj.getLastOrderID();
            
            
            string price;
            string Name;
            double subtotal = 0;
            // if one of the productID 1 product was selected.
            if (NewOrder.proid0 != 0)
            {
                textBox1.Text += "\r\n";
                price = obj.getPriceForID("1");
                Name = obj.getProdNameForID("1");
                textBox1.Text += Name;
                textBox1.Text += NewOrder.proid0 + "X $" + price;
                textBox1.Text += "= $";
                pay = NewOrder.proid0 * double.Parse(price);
                textBox1.Text += pay.ToString();
                subtotal += pay;
            }
            // if one of the productID 2 product was selected.
            if (NewOrder.proid1 != 0)
            {
                textBox1.Text += "\r\n";
                price = obj.getPriceForID("2");
                Name = obj.getProdNameForID("2");
                textBox1.Text += Name;
                textBox1.Text += NewOrder.proid1 + "X $" + price;
                textBox1.Text += "= $";
                pay = NewOrder.proid1 * double.Parse(price);
                textBox1.Text += pay.ToString();
                subtotal += pay;
            }
            // if one of the productID 3 product was selected.
            if (NewOrder.proid2 != 0)
            {
                textBox1.Text += "\r\n";
                price = obj.getPriceForID("3");
                Name = obj.getProdNameForID("3");
                textBox1.Text += Name;
                textBox1.Text += NewOrder.proid2 + "X $" + price;
                textBox1.Text += "= $";
                pay = NewOrder.proid2 * double.Parse(price);
                textBox1.Text += pay.ToString();
                subtotal += pay;
            }
            // if one of the productID 4 product was selected.
            if (NewOrder.proid3 != 0)
            {
                textBox1.Text += "\r\n";
                price = obj.getPriceForID("4");
                Name = obj.getProdNameForID("4");
                textBox1.Text += Name;
                textBox1.Text += NewOrder.proid3 + "X $" + price;
                textBox1.Text += "= $";
                pay = NewOrder.proid3 * double.Parse(price);
                textBox1.Text += pay.ToString();
                subtotal += pay;
            }
            // if one of the productID 5 product was selected.
            if (NewOrder.proid4 != 0)
            {
                textBox1.Text += "\r\n";
                price = obj.getPriceForID("5");
                Name = obj.getProdNameForID("5");
                textBox1.Text += Name;
                textBox1.Text += NewOrder.proid4 + "X $" + price;
                textBox1.Text += "= $";
                pay = NewOrder.proid4 * double.Parse(price);
                textBox1.Text += pay.ToString();
                subtotal += pay;

            }
            // if one of the productID 6 product was selected.
            if (NewOrder.proid5 != 0)
            {
                textBox1.Text += "\r\n";
                price = obj.getPriceForID("6");
                Name = obj.getProdNameForID("6");
                textBox1.Text += Name;
                textBox1.Text += NewOrder.proid5 + "X $" + price;
                textBox1.Text += "= $";
                pay = NewOrder.proid5 * double.Parse(price);
                textBox1.Text += pay.ToString();
                subtotal += pay;
            }

            //formating the sales record and calculating the HST and final price.
            textBox1.Text += "\r\nSubtotal = $ " + subtotal.ToString();
            double HST = (13 * subtotal) / 100;
            textBox1.Text += "\r\nHST(13%) = $ " + HST.ToString();
            textBox1.Text += "\r\nSale Total = = $ " + (HST + subtotal).ToString();
            Order ord = new Order();

            ord = obj.SelectOrderByID(obj.getLastOrderID());
            string status = ord.Status;
            textBox1.Text += "\r\n";
            //printing status detail in th end.
            if (status == "PAID")
            {
                textBox1.Text += "Paid - Thank you!";
            }
            else if (status == "PEND")
            {
                textBox1.Text += "Pending - We'll contact you soon!";
            }
            else if(status == "RFND")
            {
                textBox1.Text += "Refunded - Please come again!";
            }

        }
        
    }
}
