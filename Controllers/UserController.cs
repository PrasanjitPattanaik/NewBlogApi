using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using NewBlog.API.Models;

namespace NewBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        SqlConnection con = new SqlConnection("Server=PRASANJIT;Database=TheDevBlogDb;Trusted_Connection=true");
        //SqlConnection con = new SqlConnection("Server=database-1.chlvtiacmc2b.ap-south-1.rds.amazonaws.com;Database=blog; User=admin; Password=Prasanjit123Pattanaik; Trusted_Connection=true");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;

        [HttpGet("/{username}")]
        public IActionResult GetUser(string username)
        {

            con.Open();
            cmd = new SqlCommand("SELECT * FROM Users WHERE UserName=@Name", con);
            cmd.Parameters.AddWithValue("@Name", username);
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            con.Close();
            User user = new User();
            foreach (DataRow row in dt.Rows)
            {
                user.UserId = row["UserId"].ToString();
                user.UserName = row["UserName"].ToString();
                user.Email = row["Email"].ToString();
                user.Password = row["Password"].ToString();
                user.Phone = row["Phone"].ToString();
            }
            if (user.UserId != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        public IActionResult PostUser([FromBody] User user)
        {
            Guid UserId = Guid.NewGuid();
            int password = user.Password.GetHashCode();
            con.Open();
            cmd = new SqlCommand("INSERT INTO Users (UserId, UserName, Email, Password, Phone) VALUES (@UserId, @UserName, @Email, @Password, @Phone)", con);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@UserName", user.UserName);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", password);
            cmd.Parameters.AddWithValue("@Phone", user.Phone);

            cmd.ExecuteNonQuery();
            con.Close();
            return Ok(user);
        }

        [HttpGet("/{username}/{password}")]

        public IActionResult GetUserLogin(string username, string password)
        {
            int passwords = password.GetHashCode();
            string passwordss = passwords.ToString();
            con.Open();
            cmd = new SqlCommand("SELECT * FROM Users WHERE UserName=@Name AND Password=@Password", con);
            cmd.Parameters.AddWithValue("@Name", username);
            cmd.Parameters.AddWithValue("@Password", passwordss);
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            con.Close();
            User user = new User();
            foreach (DataRow row in dt.Rows)
            {
                user.UserId = row["UserId"].ToString();
                user.UserName = row["UserName"].ToString();
                user.Email = row["Email"].ToString();
                user.Phone = row["Phone"].ToString();
            }
            if (user.UserId != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }

        }

    }
}
