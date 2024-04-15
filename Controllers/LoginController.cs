using ldl4.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;
using System.Text;
using System.Security.Cryptography;
using System.Linq;

namespace ldl4.Controllers
{
    public class LoginController : Controller
    {
        string connectionString = "Data Source=C:/sqlite/data.db;";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Check(Data user)
        {
            if (ModelState.IsValid)
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Prepare SQL query to retrieve user's hashed password by username
                    string query = "SELECT PasswordHash FROM Users WHERE Username = @Username";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", user.Username);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string hashedPasswordFromDB = reader.GetString(0);
                                string hashedPassword = GetMD5Hash(user.Password);

                                
                                if (hashedPasswordFromDB == hashedPassword || hashedPasswordFromDB == user.Password)
                                {
                                    ViewBag.Success = true;


                                }

                                else { ViewBag.Fail = true; }
                            }
                        }
                    }
                }
            }

            
            return View("Index");
        }

        [HttpPost]
        public IActionResult ForgotPassword()
        {
            return Redirect("/Password");
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