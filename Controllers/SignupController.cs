using ldl4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.RulesetToEditorconfig;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SQLite;

namespace ldl4.Controllers
{
    public class SignupController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddUser(Data newuser)
        {
            string connectionString = "Data Source=C:/sqlite/data.db;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                
                string query = "INSERT INTO Users (Username, PasswordHash, Email) VALUES (@Username, @PasswordHash, @Email)";

                
                string passwordHash = GetMD5Hash(newuser.Password);

                
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", newuser.Username);
                    command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                    command.Parameters.AddWithValue("@Email", newuser.Mail);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        
                        return Redirect("/");
                    }
                    else
                    {
                        return View("Index");
                    }
                }
            }



            


        }
        private string GetMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                return string.Join("", hashBytes.Select(b => b.ToString("x2")));
            }
        }
    } 
}
