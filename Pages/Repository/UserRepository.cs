using System.Data;
using MySql.Data.MySqlClient;
using Dapper;
using NHLCafe.Pages.Models;

namespace NHLCafe.Pages.Repository
{
    public class UserRepository
    {
        private IDbConnection Connect()
        {
            return new MySqlConnection(
                "Server=127.0.0.1;Port=3306;" +
                "Database=exercises;" +
                "Uid=root;Pwd=;"
            );
        }

        // get cafeuser by id
        public CafeUser? GetById(int userId)
        {
            using (IDbConnection db = Connect())
            {
                return db.Query<CafeUser>("SELECT * FROM user WHERE UserId = @UserId", new {UserId = userId }).FirstOrDefault();
            }
        }

        public void AddUser(string username, string password)
        {
            using var connection = Connect();
            connection
                .Query("INSERT INTO user (username, password) VALUES (@username,@password)",
                    new
                    {
                        username,
                        password = BCrypt.Net.BCrypt.HashPassword(password),
                    });
        }

        public List<Authresult> Auth(string username, string password)
        {
            using var connection = Connect();
            var user = connection
                .QuerySingleOrDefault("SELECT * FROM user WHERE username = @username",
                    new
                    {
                        username
                    });
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user?.Password))
            {
                return new List<Authresult> { new Authresult { Auth = true, Userid = user?.UserId } };
            }
            else
            {
                return new List<Authresult> { new Authresult { Auth = false } };
            }
        }
    }
}

public class Authresult
{
    public bool Auth { get; set; }
    public int Userid { get; set; }
}
