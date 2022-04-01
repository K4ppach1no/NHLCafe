using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using MySql.Data.MySqlClient;
using NHLCafe.Pages.Models;

namespace NHLCafe.Pages.Repository;

public class CategoryRepository
{
    // create dbconnection
    private IDbConnection Connect()
    {
        return new MySqlConnection(
            "Server=127.0.0.1;Port=3306;" +
            "Database=exercises;" +
            "Uid=root;Pwd=;"
        );
    }
    // get all categories in a list
    public List<Category> GetAll()
    {
        using (IDbConnection dbConnection = Connect())
        {
            return dbConnection.Query<Category>("SELECT * FROM category").ToList();
        }
    }
    
    // add a new category
    public void Add(Category category)
    {
        using (IDbConnection dbConnection = Connect())
        {
            string query = "INSERT INTO category (Name) VALUES (@Name)";
            dbConnection.Execute(query, category);
        }
    }

}