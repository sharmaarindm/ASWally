/*  
*  FILE          : currectInventory.cs 
*  PROJECT       : RelationalDatabase - Assignment 4
*  PROGRAMMER    : Arindm Sharma
*  DESCRIPTION   : The purpose for this file is to provide functionality for the form that provides currentinventory.
*/
using System.Collections.Generic;
using System.Windows.Forms;

namespace ASWally
{
    public partial class currectInventory : Form
    {
        
        public currectInventory()
        {
            InitializeComponent();

            Dictionary<int, Product> dictionary = new Dictionary<int, Product>();

            ConnectDB Inventory = new ConnectDB();
            dictionary = Inventory.SelectProduct();

            textBox1.Text = "ProductID | Product Name | Product Price | Product Quantity\r\n";

            // reading from dictionary and formatting the values.
            foreach (KeyValuePair<int, Product> kvp in dictionary)
            {
                
                textBox1.Text += string.Format("{0} | {1} | {2} | {3}\r\n", kvp.Key, kvp.Value.ProductName,kvp.Value.ProductPrice, kvp.Value.ProductQuantity);
            }
        }
        
    }
}
