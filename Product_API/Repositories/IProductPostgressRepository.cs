using Product_API.Models;

namespace Product_API.Repositories
{
    public class ProductPostgressRepository : IProductRepository
    {
        public string connectionstring = "Server=localhost;Port=5432;Database=Homework;User Id=postgres;Password=root";
        public NpgsqlConnection connection;
        public Product Add(Product product)
        {
            connection = new NpgsqlConnection(connectionstring);
            connection.Open();
            using (var command = new NpgsqlCommand("INSERT INTO products (name, description, photopath) VALUES (@Name, @Description, @PhotoPath);", connection))
            {
                command.Parameters.AddWithValue("Name", product.Name);
                command.Parameters.AddWithValue("Description", product.Description);
                command.Parameters.AddWithValue("PhotoPath", product.PhotoPath);
                command.ExecuteNonQuery();
            }
            connection.Close();
            return product;
        }

        public List<Product> GetAll()
        {
            connection = new NpgsqlConnection(connectionstring);
            connection.Open();
            List<Product> products = new List<Product>();
            using (var command = new NpgsqlCommand("select * from products;", connection))
            {
                NpgsqlDataReader? reader = command.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(new Product()
                    {
                        Id = (int)reader["id"],
                        Name = (string)reader["name"],
                        Description = (string)reader["description"],
                        PhotoPath = (string)reader["photopath"]
                    });
                }
                reader.Close();
            }
            connection.Close();
            return products;
        }
    }
}
