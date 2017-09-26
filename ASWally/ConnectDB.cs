/*  
*  FILE          : ConnectDB.cs 
*  PROJECT       : RelationalDatabase - Assignment 4
*  PROGRAMMER    : Arindm Sharma
*  DESCRIPTION   : The purpose for this file is to connect a client from a different PC/ or the same PC that has a database created on it.
*                  and update/insert and get data out of the database.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;



namespace ASWally
{
    class ConnectDB
    {
        //declare private variables
        private static string server;
        private static string database;
        private static string userID;
        private static string password;
        private static MySqlConnection connection;

        public static string retValF;
        public static string retValL;

        Dictionary<int, Customer> cust = new Dictionary<int, Customer>();

        public ConnectDB()
        {
            //initialize the private vars
            server = "localhost";
            database = "aswally";
            userID = "root";
            password = "Conestoga1";

            //store the requered info into the connection string
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + userID + ";" + "PASSWORD=" + password + ";";
            //create new connection with DB
            connection = new MySqlConnection(connectionString);
        }


        /* 
        *  FUNCTION      : Connect 
        * 
        *  DESCRIPTION   : This function connects to the database
        * 
        *  PARAMETERS    : N/A
        * 
        *  RETURNS       : bool
        */
        private static bool Connect()
        {
            //try to connect
            try
            {
                //open the connection to the database
                connection.Open();
                //if connects return true
                return true;
            }
            //catch if connection fails
            catch (MySqlException ex)
            {
                //if the connection doenst occur, reply with error message and return false
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }
        /* 
*  FUNCTION      : Insert
* 
*  DESCRIPTION   : This function acts as a generic insert funciton which takes in a string as a parameter.
* 
*  PARAMETERS    : string ins
* 
*  RETURNS       : void
*/
        public void Insert(string ins)
        {
            string query = ins;


            //open connection
            if (Connect() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                Disconnect();
            }
        }
        /* 
*  FUNCTION      : Insert
* 
*  DESCRIPTION   : This function acts as a generic update funciton which takes in a string as a parameter.
* 
*  PARAMETERS    : string ins
* 
*  RETURNS       : void
*/
        public void Update(string ins)
        {
            string query = ins;

            //Open connection
            if (Connect() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                Disconnect();
            }
        }

        /* 
*  FUNCTION      : SelectOrderByCustID
* 
*  DESCRIPTION   : This function returns a dictionary filled with orders given a customer ID.
* 
*  PARAMETERS    : string CustomerID
* 
*  RETURNS       : Dictionary<int, Order>
*/
        public Dictionary<int, Order> SelectOrderByCustID(string customerID)
        {
            string query = "SELECT * FROM orders";
            query = query + " WHERE CustomerID ='" + customerID + "'";

            Dictionary<int, Order> dictionary = new Dictionary<int, Order>();

            //Open connection
            if (Connect() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                int i = 0;
                //Read the data and store them in the list

                while (dataReader.Read())
                {
                    Order ob = new Order();
                    ob.OrderID = dataReader["OrderID"].ToString();
                    ob.OrderDate = dataReader["OrderDate"].ToString();
                    ob.BranchID = dataReader["BranchID"].ToString();
                    ob.CustomerID = dataReader["CustomerID"].ToString();
                    ob.Status = dataReader["OrderStatus"].ToString();

                    dictionary.Add(i, ob);


                    i++;

                }


                //close Data Reader
                dataReader.Close();

                //close Connection
                Disconnect();


            }

            return dictionary;
        }


        /* 
*  FUNCTION      : SelectCustomerPhone
* 
*  DESCRIPTION   : This function returns a Customer object with customer details give the phone number.
* 
*  PARAMETERS    : string number
* 
*  RETURNS       : Customer
*/
        public Customer SelectCustomerPhone(string number)
        {
            string query = "SELECT * FROM customer";
            query = query + " WHERE PhoneNo ='" + number + "'";
            Customer ob = new Customer();


            //Open connection
            if (Connect() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
        
                //Read the data and store them in the list

                while (dataReader.Read())
                {

                    ob.CustomerID = dataReader["CustomerID"].ToString();
                    ob.Fname = dataReader["FirstName"].ToString();
                    ob.Lname = dataReader["LastName"].ToString();
                    ob.Number = dataReader["PhoneNo"].ToString();

                }


                //close Data Reader
                dataReader.Close();

                //close Connection
                Disconnect();


            }

            return ob;
        }


        /* 
*  FUNCTION      : SelectCustomerLastName
* 
*  DESCRIPTION   : This function returns a Customer object with customer details give the last name.
* 
*  PARAMETERS    : string number
* 
*  RETURNS       : Customer
*/
        public Customer SelectCustomerLastName(string name)
        {
            string query = "SELECT * FROM customer";
            query = query + " WHERE LastName ='" + name + "'";
            Customer ob = new Customer();


            //Open connection
            if (Connect() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
              
                //Read the data and store them in the list

                while (dataReader.Read())
                {

                    ob.CustomerID = dataReader["CustomerID"].ToString();
                    ob.Fname = dataReader["FirstName"].ToString();
                    ob.Lname = dataReader["LastName"].ToString();
                    ob.Number = dataReader["PhoneNo"].ToString();

                }


                //close Data Reader
                dataReader.Close();

                //close Connection
                Disconnect();


            }

            return ob;
        }

        /* 
*  FUNCTION      : getProductQuantity
* 
*  DESCRIPTION   : This function returns a string with quantity of a particular product given a product ID and order ID.
* 
*  PARAMETERS    : string PID, string OID
* 
*  RETURNS       : string
*/
        public string getProductQuantity(string PID, string OID)
        {
            string query = "SELECT * FROM orderline";
            query = query + " WHERE ProductID ='" + PID + "'and OrderID ='" + OID + "'";

            OrderList pro = new OrderList();

            //Open connection
            if (Connect() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                int i = 0;
                //Read the data and store them in the list

                while (dataReader.Read())
                {
                    pro.Quantity = dataReader["Quantity"].ToString();

                    //ob.OrderID = dataReader["OrderID"].ToString();
                    //ob.OrderDate = dataReader["OrderDate"].ToString();
                    //ob.BranchID = dataReader["BranchID"].ToString();
                    //ob.CustomerID = dataReader["CustomerID"].ToString();
                    //ob.Status = dataReader["OrderStatus"].ToString();




                    i++;

                }


                //close Data Reader
                dataReader.Close();

                //close Connection
                Disconnect();


            }

            return pro.Quantity;
        }

        /* 
*  FUNCTION      : SelectProduct
* 
*  DESCRIPTION   : This function returns a dictionary of product objects with all of product deatils.
* 
*  PARAMETERS    : None
* 
*  RETURNS       : Dictionary<int, Product> 
*/
        public Dictionary<int, Product> SelectProduct()
        {
            string query = "SELECT * FROM product";

            Dictionary<int, Product> dictionary = new Dictionary<int, Product>();

            //Open connection
            if (Connect() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                int i = 0;
                //Read the data and store them in the list

                while (dataReader.Read())
                {
                    Product obj = new Product();
                    obj.ProductID = dataReader["ProductID"].ToString();
                    obj.ProductName = dataReader["ProductName"].ToString();
                    obj.ProductPrice = dataReader["Price"].ToString();
                    obj.ProductQuantity = dataReader["Quantity"].ToString();

                    dictionary.Add(i, obj);


                    i++;

                }


                //close Data Reader
                dataReader.Close();

                //close Connection
                Disconnect();


            }

            return dictionary;
        }
        /* 
*  FUNCTION      : getOrderListByID
* 
*  DESCRIPTION   : This function returns a dictionary of orderlist objects with all oforderlist details.
* 
*  PARAMETERS    : string ID
* 
*  RETURNS       : Dictionary<int, Orderlist> 
*/
        public Dictionary<int, OrderList> getOrderListByID(string ID)
        {
            string query = "SELECT * FROM orderline WHERE OrderID =";
            query = query + "'" + ID + "'";

            Dictionary<int, OrderList> dictionary = new Dictionary<int, OrderList>();

            //Open connection
            if (Connect() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                int i = 0;
                //Read the data and store them in the list

                while (dataReader.Read())
                {
                    OrderList obj = new OrderList();
                    obj.OrderID = dataReader["OrderID"].ToString();
                    obj.ProductID = dataReader["ProductID"].ToString();
                    obj.Quantity = dataReader["Quantity"].ToString();


                    dictionary.Add(i, obj);


                    i++;

                }


                //close Data Reader
                dataReader.Close();

                //close Connection
                Disconnect();


            }

            return dictionary;
        }
        /* 
*  FUNCTION      : SelectOrderByID
* 
*  DESCRIPTION   : This function returns order objects with all of order details given an orderiD.
* 
*  PARAMETERS    : string orderID
* 
*  RETURNS       : Order
*/
        public Order SelectOrderByID(string orderID)
        {
            string query = "SELECT * FROM orders WHERE OrderID = ";

            query = query + "'" + orderID + "'";

            Order ob = new Order();

            //Open connection
            if (Connect() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list

                while (dataReader.Read())
                {
                    ob.OrderID = dataReader["OrderID"].ToString();
                    ob.OrderDate = dataReader["OrderDate"].ToString();
                    ob.BranchID = dataReader["BranchID"].ToString();
                    ob.CustomerID = dataReader["CustomerID"].ToString();
                    ob.Status = dataReader["OrderStatus"].ToString();

                }


                //close Data Reader
                dataReader.Close();

                //close Connection
                Disconnect();


            }

            return ob;
        }
        /* 
*  FUNCTION      : getQuantity
* 
*  DESCRIPTION   : This function returnsa string with quantity of a particular product.
* 
*  PARAMETERS    : string pid
* 
*  RETURNS       : string
*/
        public string getQuantity(string pid)
        {
            string query = "select * from product where ProductID = " + pid + "";

            string retVal = "";

            //Open connection
            if (Connect() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list

                while (dataReader.Read())
                {

                    retVal = dataReader["Quantity"].ToString();
                }


                //close Data Reader
                dataReader.Close();

                //close Connection
                Disconnect();

            }

            return retVal;
        }
        /* 
*  FUNCTION      : getLastOrderID
* 
*  DESCRIPTION   : This function returns a string with the latest orderID stored.
* 
*  PARAMETERS    : NOne
* 
*  RETURNS       : string
*/
        public string getLastOrderID()
        {
            string query = "SELECT OrderID FROM orders ORDER BY OrderID DESC LIMIT 1";

            string retVal = "";

            //Open connection
            if (Connect() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
   
                //Read the data and store them in the list

                while (dataReader.Read())
                {

                    retVal = dataReader["OrderID"].ToString();
                }


                //close Data Reader
                dataReader.Close();

                //close Connection
                Disconnect();

            }

            return retVal;
        }
        /* 
*  FUNCTION      : getLastCustomerID
* 
*  DESCRIPTION   : This function returns a string with the latest CustomerID stored.
* 
*  PARAMETERS    : NOne
* 
*  RETURNS       : string
*/
        public string getLastCustomerID()
        {
            string query = "SELECT CustomerID FROM customer ORDER BY CustomerID DESC LIMIT 1";

            string retVal = "";

            //Open connection
            if (Connect() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list

                while (dataReader.Read())
                {

                    retVal = dataReader["CustomerID"].ToString();
                }


                //close Data Reader
                dataReader.Close();

                //close Connection
                Disconnect();

            }

            return retVal;
        }
        /* 
*  FUNCTION      : getPriceForID
* 
*  DESCRIPTION   : This function returns a string with the price of a particular product provided the productID.
* 
*  PARAMETERS    : string productID
*  *
*  RETURNS       : string
*/
        public string getPriceForID(string productID)
        {
            string query = "SELECT * FROM product WHERE ProductID = '";
            query = query + productID + "'";

            string retVal = "";

            //Open connection
            if (Connect() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list

                while (dataReader.Read())
                {

                    retVal = dataReader["Price"].ToString();
                }


                //close Data Reader
                dataReader.Close();

                //close Connection
                Disconnect();

            }

            return retVal;
        }
        /* 
*  FUNCTION      : getProdNameForID
* 
*  DESCRIPTION   : This function returns a string with the product name provided the productID.
* 
*  PARAMETERS    : string productID
* 
*  RETURNS       : string
*/
        public string getProdNameForID(string productID)
        {
            string query = "SELECT * FROM product WHERE ProductID = '";
            query = query + productID + "'";

            string retVal = "";

            //Open connection
            if (Connect() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list

                while (dataReader.Read())
                {

                    retVal = dataReader["ProductName"].ToString();
                }


                //close Data Reader
                dataReader.Close();

                //close Connection
                Disconnect();

            }

            return retVal;
        }
        /* 
*  FUNCTION      : getUserNameForID
* 
*  DESCRIPTION   : This function returns a string with the users name provided the USerID.
* 
*  PARAMETERS    : string UserID
* 
*  RETURNS       : void
*/
        public void getUserNameForID(string UserID)
        {
            string query = "SELECT * FROM customer WHERE CustomerID = '";
            query = query + UserID + "'";



            //Open connection
            if (Connect() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list

                while (dataReader.Read())
                {

                    retValF = dataReader["FirstName"].ToString();
                    retValL = dataReader["LastName"].ToString();
                }


                //close Data Reader
                dataReader.Close();

                //close Connection
                Disconnect();

            }


        }
        /* 
        *  FUNCTION      : Disconnect 
        * 
        *  DESCRIPTION   : This function disconnects from the database
        * 
        *  PARAMETERS    : N/A
        * 
        *  RETURNS       : bool
        */
        private static bool Disconnect()
        {
            //try to close connection
            try
            {
                //close connection
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /* 
*  FUNCTION      : getProdDetailsByPID
* 
*  DESCRIPTION   : This function returns product details provided the productID.
* 
*  PARAMETERS    : none
* 
*  RETURNS       : Product
*/
        public Product getProdDetailsByPID(string pId)
        {
            string query = "SELECT * FROM product WHERE ProductID =";
            query = query + "'" + pId + "'";


            Product retVal = new Product();

            //Open connection
            if (Connect() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                retVal.ProductID = pId;
                while (dataReader.Read())
                {
                    retVal.ProductName = dataReader["ProductName"].ToString();
                    retVal.ProductPrice = dataReader["Price"].ToString();
                    //retVal = dataReader["OrderID"].ToString();
                }


                //close Data Reader
                dataReader.Close();

                //close Connection
                Disconnect();

            }

            return retVal;
        }
        /* 
*  FUNCTION      : getBranches
* 
*  DESCRIPTION   : This function returns a dictionary containing branch name of all the branches
* 
*  PARAMETERS    : none
* 
*  RETURNS       : Dictionary<int, string>
*/
        public Dictionary<int, string> getBranches()
        {
            string query = "SELECT * FROM branch";



            Product retVal = new Product();
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            //Open connection
            if (Connect() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                int i = 0;
                //Read the data and store them in the list
                // retVal.ProductID = pId;
                while (dataReader.Read())
                {
                    dictionary.Add(i, dataReader["BranchName"].ToString());
                    i++;
                    //retVal = dataReader["OrderID"].ToString();
                }


                //close Data Reader
                dataReader.Close();

                //close Connection
                Disconnect();

            }

            return dictionary;
        }
        /* 
*  FUNCTION      : getAllCustomers
* 
*  DESCRIPTION   : This function returns a dictionary containing customer details of all the customers.
* 
*  PARAMETERS    : none
* 
*  RETURNS       : Dictionary<int, Customer>
*/
        public Dictionary<int, Customer> getAllCustomers()
        {
            string query = "SELECT * FROM customer";
            //Customer ob = new Customer();

            Dictionary<int, Customer> obj = new Dictionary<int, Customer>();
            //Open connection
            if (Connect() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                int i = 0;
                //Read the data and store them in the list

                while (dataReader.Read())
                {
                    Customer ob = new Customer();

                    ob.CustomerID = dataReader["CustomerID"].ToString();
                    ob.Fname = dataReader["FirstName"].ToString();
                    ob.Lname = dataReader["LastName"].ToString();
                    ob.Number = dataReader["PhoneNo"].ToString();

                    obj.Add(i, ob);
                    i++;

                }


                //close Data Reader
                dataReader.Close();

                //close Connection
                Disconnect();


            }
            return obj;

        }
    }
}
