using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using NHLCafe.Pages.Models;

namespace NHLCafe.Pages.Repository;

public class ProductRepository
{
    // dbconnection
    private IDbConnection Connect()
    {
        return new MySqlConnection(
            "Server=127.0.0.1;Port=3306;" +
            "Database=nhlcafe;" +
            "Uid=root;Pwd=;"
        );
    }    
    // get all products in list
    public List<Product> GetAll()
    {
        using (IDbConnection dbConnection = Connect())
        {
            return dbConnection.Query<Product>("SELECT * FROM products").ToList();
        }
    }

    // get product by id
    public Product GetById(int id)
    {
        using (IDbConnection dbConnection = Connect())
        {
            string sQuery = "SELECT * FROM products WHERE Id = @Id";
            return dbConnection.Query<Product>(sQuery, new { Id = id }).FirstOrDefault();
        }
    }
    
    // add product
    public void Add(Product product)
    {
        using (IDbConnection dbConnection = Connect())
        {
            string sQuery = "INSERT INTO products (Name, Price, Description, Image, Category) VALUES(@Name, @Price, @Description, @Image, @Category)";
            dbConnection.Execute(sQuery, product);
        }
    }

    // update product
    public void Update(Product product)
    {
        using (IDbConnection dbConnection = Connect())
        {
            string sQuery = "UPDATE products SET Name = @Name, Price = @Price, Description = @Description, Image = @Image WHERE Id = @Id";
            dbConnection.Execute(sQuery, product);
        }
    }
    
    // delete product
    public void Delete(int id)
    {
        using (IDbConnection dbConnection = Connect())
        {
            string sQuery = "DELETE FROM products WHERE Id = @Id";
            dbConnection.Execute(sQuery, new { Id = id });
        }
    }
    
    // get all products by category
    public List<Product> GetByCategory(int categoryId)
    {
        using (IDbConnection dbConnection = Connect())
        {
            string sQuery = "SELECT * FROM products WHERE CategoryId = @CategoryId";
            return dbConnection.Query<Product>(sQuery, new { CategoryId = categoryId }).ToList();
        }
    }
}