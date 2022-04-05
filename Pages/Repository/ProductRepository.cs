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
            "Database=exercises;" +
            "Uid=root;Pwd=;"
        );
    }    
    // get all products in list
    public List<Product> GetAll()
    {
        using (IDbConnection dbConnection = Connect())
        {
            return dbConnection.Query<Product>("SELECT * FROM product").ToList();
        }
    }

    // get product by id
    public Product? GetById(int productId)
    {
        using (IDbConnection dbConnection = Connect())
        {
            string sQuery = "SELECT * FROM product WHERE ProductId = @ProductId"; 
            return dbConnection.Query<Product>(sQuery, new {ProductId = productId }).FirstOrDefault();
        }
    }
    
    // add product
    public void Add(string name, int categoryId, double price)
    {
        using (IDbConnection dbConnection = Connect())
        {
            string sQuery = "INSERT INTO product (Name, CategoryId, Price) VALUES(@Name, @CategoryId, @Price)";
            dbConnection.Execute(sQuery, new {Name = name, CategoryId = categoryId, Price = price });
        }
    }

    // update product
    public void Update(int productId, string name, int categoryId, double price)
    {
        using (IDbConnection dbConnection = Connect())
        {
            string sQuery = "UPDATE product SET Name = @Name, CategoryId = @CategoryId, Price = @Price WHERE ProductId = @ProductId";
            dbConnection.Execute(sQuery, new {ProductId = productId, Name = name, CategoryId = categoryId, Price = price });
        }
    }

    // delete product
    public void Delete(int productId)
    {
        using (IDbConnection dbConnection = Connect())
        {
            string sQuery = "DELETE FROM product WHERE ProductId = @ProductId";
            dbConnection.Execute(sQuery, new {ProductId = productId });
        }
    }
    
    // get all products by category
    public List<Product> GetByCategory(int categoryId)
    {
        using (IDbConnection dbConnection = Connect())
        {
            string sQuery = "SELECT * FROM product WHERE CategoryId = @CategoryId";
            return dbConnection.Query<Product>(sQuery, new {CategoryId = categoryId }).ToList();
        }
    }
}