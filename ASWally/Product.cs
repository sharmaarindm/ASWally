/*  
*  FILE          : Product.cs 
*  PROJECT       : RelationalDatabase - Assignment 4
*  PROGRAMMER    : Arindm Sharma
*  DESCRIPTION   : The purpose for this file is to provide a structure for the Product object.
*/

namespace ASWally
{
    class Product
    {
        private string productID;
        private string productName;
        private string productPrice;
        private string productQuantity;

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

        public string ProductName
        {
            get
            {
                return productName;
            }

            set
            {
                productName = value;
            }
        }

        public string ProductPrice
        {
            get
            {
                return productPrice;
            }

            set
            {
                productPrice = value;
            }
        }

        public string ProductQuantity
        {
            get
            {
                return productQuantity;
            }

            set
            {
                productQuantity = value;
            }
        }
    }
}
