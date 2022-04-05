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
        public CafeUser GetById(int UserId)
        {
            using (IDbConnection db = Connect())
            {
                return db.Query<CafeUser>("SELECT * FROM user WHERE UserId = @UserId", new { UserId }).FirstOrDefault();
            }
        }

        public void AddUser(string username, string password)
        {
            using var connection = Connect();
            var users = connection
                .Query("INSERT INTO user (username, password) VALUES (@username,@password)",
                    new
                    {
                        username = username,
                        password = BCrypt.Net.BCrypt.HashPassword(password),
                    });
            return;
        }

        public List<authresult> Auth(string username, string password)
        {
            using var connection = Connect();
            var user = connection
                .QuerySingleOrDefault("SELECT * FROM user WHERE username = @username",
                    new
                    {
                        username = username
                    });
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user?.Password))
            {
                return new List<authresult> { new authresult { auth = true, userid = user?.UserId } };
            }
            else
            {
                return new List<authresult> { new authresult { auth = false } };
            }
        }
    }
}

public class authresult
{
    public bool auth { get; set; }
    public int userid { get; set; }
}
