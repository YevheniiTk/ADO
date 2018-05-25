using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_HW
{
    class Program
    {
        private static readonly string ConnectionString =
            @"Server=DESKTOP-D5VI0OE;Database=Northwind;Trusted_Connection=True;";

        static void Main(string[] args)
        {
            var totalCountOrders = SalesByYear(new DateTime(1997, 01, 01), new DateTime(1998, 01, 01)).Count;

            var totalPriceOfMostExpensiveTenProducts = TenMostExpensiveProducts()
                                                       .Select(_ => _.UnitPrice)
                                                       .Sum();
        }

        private static List<Order> SalesByYear(DateTime BeginningDate, DateTime EndingDate)
        {
            var result = new List<Order>();
            
            string storedProcedure = @"Sales by Year";

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(storedProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Beginning_Date",BeginningDate );
                command.Parameters.AddWithValue("@Ending_Date", EndingDate);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var order = new Order
                            {
                                ShippedDate = (DateTime)reader["ShippedDate"],
                                OrderID = (int)reader["OrderID"],
                                Subtotal = (decimal)reader["Subtotal"],
                                Year = (string)reader["Year"]
                            };
                            result.Add(order);
                        }
                    }
                }
            };
            return result;
        }

        private static List<Product> TenMostExpensiveProducts()
        {
            var result = new List<Product>();

            string storedProcedure = @"Ten Most Expensive Products";

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(storedProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var order = new Product
                            {
                              ProductName = (string)reader["TenMostExpensiveProducts"],
                              UnitPrice = (decimal)reader["UnitPrice"]
                            };
                            result.Add(order);
                        }
                    }
                }
            };
            return result;
        }
    }
}
