/*  
*  FILE          : Order.cs 
*  PROJECT       : RelationalDatabase - Assignment 4
*  PROGRAMMER    : Arindm Sharma
*  DESCRIPTION   : The purpose for this file is to provide a structure for the orderList object.
*/


namespace ASWally
{
    public class OrderList
    {
        private string orderID;
        private string productID;
        private string quantity;

        public string OrderID
        {
            get
            {
                return orderID;
            }

            set
            {
                orderID = value;
            }
        }

        public string ProductID
        {
            get
            {
                return productID;
            }

            set
            {
                productID = value;
            }
        }

        public string Quantity
        {
            get
            {
                return quantity;
            }

            set
            {
                quantity = value;
            }
        }
    }
}
