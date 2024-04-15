using ldl4.Models;
using Microsoft.AspNetCore.Mvc;
using FluentEmail.Core;
using FluentEmail.Smtp;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace ldl4.Controllers
{
    public class PasswordController : Controller
    {
        private readonly SmtpSender _smtpSender;
        private readonly string _connectionString;

        public PasswordController()
        {
            _connectionString = "Data Source=C:/sqlite/data.db;";

            _smtpSender = new SmtpSender(new SmtpClient()
            {
                Host = "smtp-mail.outlook.com", 
                Port = 587, 
                EnableSsl = true, 
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("Ldl4.DenisSaliuc@outlook.com", "Ldl4.SaliucDenis")
            });

            Email.DefaultSender = _smtpSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Reset(Recovery mail)
        {
            if (ModelState.IsValid)
            {
                Task.Run(() => SendPasswordResetEmail(mail.Email));
                ViewBag.ShowPopup = true;
                return Redirect("/");
            }

            return RedirectToAction("Index");
        }

        private void SendPasswordResetEmail(string email)
        {
            string tempPassword = GetTempPasswordFromDatabase(email);

            
            Email
                .From("Ldl4.DenisSaliuc@outlook.com")
                .To(email)
                .Subject("Password Reset")
                .Body("Here is your temporary password: " + tempPassword)
                .SendAsync();
        }

        private string GetTempPasswordFromDatabase(string email)
        {
            string tempPassword = "0"; 

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                
                string query = "SELECT PasswordHash FROM Users WHERE Email = @Email";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tempPassword = reader["PasswordHash"].ToString();
                        }
                    }
                }
            }

            return tempPassword;
        }
    }
}
