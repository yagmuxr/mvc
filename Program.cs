using System;
using System.Data.SqlClient;
using udemy;

namespace MyApp
{
    interface IProductDal
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        void Create(Product p);
        void Update(Product p);
        void Delete(int productId);
    }
    public class SqlProductDal : IProductDal
    {
        private SqlConnection GetSqlConnection()
        {
            string connectionString = @"Data Source=YAGMURSMATEBOOK\SQLEXPRESS; Initial Catalog=deneme; Integrated Security=SSPI";

            return new SqlConnection(connectionString);
        }
        public void Create(Product p)
        {
            throw new NotImplementedException();
        }

        public void Delete(int productId)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = null;
            using (var connection = GetSqlConnection())
            {
                try
                {
                    connection.Open();

                    string sql = "select * from products";
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    products = new List<Product>();
                    while (reader.Read())
                    {
                        products.Add(
                        new Product
                        {
                            ProductId=int.Parse(reader["id"].ToString()),
                            Name = reader["name"]?.ToString(),
                            Price = double.Parse(reader["price"]?.ToString()),
                        }
                        );
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }

            }
            return products;
        }

        public Product GetProductById(int id)
        {
            List<Product> products = null;
            using (var connection = GetSqlConnection())
            {
                try
                {
                    connection.Open();

                    string sql = "select * from products where id=@productid";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.Add("@productid", System.Data.SqlDbType.Int,id);
                    SqlDataReader reader = command.ExecuteReader();
                    products = new List<Product>();
                    if(reader.HasRows)
                    {
                        product = new List<Product>();
                    }
                    while (reader.Read())
                    {
                        products.Add(
                        new Product
                        {
                            ProductId=int.Parse(reader["id"].ToString()),
                            Name = reader["name"]?.ToString(),
                            Price = double.Parse(reader["price"]?.ToString()),
                        }
                        );
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }

            }
            return products;
        }

        public void Update(Product p)
        {
            throw new NotImplementedException();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlProductDal productDal = new SqlProductDal();
            var products = productDal.GetAllProducts();
            foreach (var p in products)
            {
                System.Console.WriteLine($"name: {p.Name}, price: {p.Price}");
            }
        }

       

    
    }
}

