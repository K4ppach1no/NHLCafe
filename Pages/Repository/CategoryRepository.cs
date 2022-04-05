using System.Data;
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
        using IDbConnection dbConnection = Connect();
        return dbConnection.Query<Category>("SELECT * FROM category").ToList();
    }
    
    // get a category by id
    public Category? GetById(int id)
    {
        using IDbConnection dbConnection = Connect();
        string sQuery = "SELECT * FROM category WHERE CategoryId = @Id";
        return dbConnection.Query<Category>(sQuery, new { Id = id }).FirstOrDefault();
    }
    
    // get a category by name
    public Category? GetByName(string name)
    {
        using IDbConnection dbConnection = Connect();
        string sQuery = "SELECT * FROM category WHERE Name = @Name";
        return dbConnection.Query<Category>(sQuery, new { Name = name }).FirstOrDefault();
    }
    
    // delete a category by id
    public void Delete(int id)
    {
        using IDbConnection dbConnection = Connect();
        string sQuery = "DELETE FROM category WHERE CategoryId = @Id";
        dbConnection.Execute(sQuery, new { Id = id });
    }
    
    // add a new category
    public bool Add(string name)
    {
        using IDbConnection dbConnection = Connect();
        string sQuery = "INSERT INTO category (Name) VALUES(@Name)";
        dbConnection.Execute(sQuery, new { Name = name });
        return true;
    }

    // update a category
    public void Update(string name, int id)
    {
        using IDbConnection dbConnection = Connect();
        string sQuery = "UPDATE category SET Name = @Name WHERE CategoryId = @Id";
        dbConnection.Execute(sQuery, new { Name = name, Id = id });
    }
}