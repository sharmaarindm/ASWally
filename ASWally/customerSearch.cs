/*  
*  FILE          : customerSearch.cs 
*  PROJECT       : RelationalDatabase - Assignment 4
*  PROGRAMMER    : Arindm Sharma
*  DESCRIPTION   : The purpose for this file is to provide functionality for the form that provides the customer search results.
*/
using System.Windows.Forms;

namespace ASWally
{
    public partial class customerSearch : Form
    {
        public customerSearch(Customer obj)
        {
            InitializeComponent();
            // formatting the customer details.
            textBox1.Text = "customerID | FirstName | LastName | PhoneNumber\r\n";
            textBox1.Text += obj.CustomerID + " | " + obj.Fname + " | " + obj.Lname +" | "+ obj.Number;
        }
    }
}
