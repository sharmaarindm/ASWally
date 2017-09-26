/*  
*  FILE          : Order.cs 
*  PROJECT       : RelationalDatabase - Assignment 4
*  PROGRAMMER    : Arindm Sharma
*  DESCRIPTION   : The purpose for this file is to provide a structure for the order object.
*/


namespace ASWally
{
    class Order
    {
        private string orderID;
        private string orderDate;
        private string branchID;
        private string customerID;
        private string status;

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

        public string OrderDate
        {
            get
            {
                return orderDate;
            }

            set
            {
                orderDate = value;
            }
        }

        public string BranchID
        {
            get
            {
                return branchID;
            }

            set
            {
                branchID = value;
            }
        }

        public string CustomerID
        {
            get
            {
                return customerID;
            }

            set
            {
                customerID = value;
            }
        }

        public string Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }
    }
}
