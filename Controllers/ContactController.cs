using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewBlog.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace NewBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        SqlConnection con = new SqlConnection("Server=PRASANJIT;Database=TheDevBlogDb;Trusted_Connection=true");
        //SqlConnection con = new SqlConnection("Server=database-1.chlvtiacmc2b.ap-south-1.rds.amazonaws.com;Database=blog; User=admin; Password=Prasanjit123Pattanaik; Trusted_Connection=true");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;

        // Get Function from Database
        [HttpGet]
        public IEnumerable<Contact> Get()
        {
            List<Contact> contacts = new List<Contact>();
            con.Open();
            cmd = new SqlCommand("SELECT * FROM Posts", con);
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            con.Close();
            foreach (DataRow row in dt.Rows)
            {
                contacts.Add(
                    new Contact
                    {
                        Id = row["Id"].ToString(),
                        Title = row["Title"].ToString(),
                        Content = row["Content"].ToString(),
                        Summary = row["Summary"].ToString(),
                        UrlHandel = row["UrlHandel"].ToString(),
                        FeaturedImageUrl = row["FeaturedImageUrl"].ToString(),
                        Visible = row["Visible"].ToString(),
                        Author = row["Author"].ToString(),
                        PublishDate = row["PublishDate"].ToString(),
                        UpdatedDate = row["UpdatedDate"].ToString()
                    });
            }
            return contacts;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Contact contact)
        {
            Guid Id = Guid.NewGuid();
            con.Open();
            cmd = new SqlCommand("INSERT INTO Posts (Id, Title, Content, Summary, UrlHandel, FeaturedImageUrl, Visible, Author, PublishDate, UpdatedDate) VALUES (@Id, @Title, @Content, @Summary, @UrlHandel, @FeaturedImageUrl, @Visible, @Author, @PublishDate, @UpdatedDate)", con);
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@Title", contact.Title);
            cmd.Parameters.AddWithValue("@Content", contact.Content);
            cmd.Parameters.AddWithValue("@Summary", contact.Summary);
            cmd.Parameters.AddWithValue("@UrlHandel", contact.UrlHandel);
            cmd.Parameters.AddWithValue("@FeaturedImageUrl", contact.FeaturedImageUrl);
            cmd.Parameters.AddWithValue("@Visible", contact.Visible);
            cmd.Parameters.AddWithValue("@Author", contact.Author);
            cmd.Parameters.AddWithValue("@PublishDate", contact.PublishDate);
            cmd.Parameters.AddWithValue("@UpdatedDate", contact.UpdatedDate);
            cmd.ExecuteNonQuery();

            con.Close();
            return Ok(contact);

        }
        [HttpPut("{id}")]
        
        public IActionResult Put(string id, [FromBody] Contact contact)
        {
            con.Open();
            cmd = new SqlCommand("UPDATE Posts SET Title=@Title, Content=@Content, Summary=@Summary, UrlHandel=@UrlHandel, FeaturedImageUrl=@FeaturedImageUrl, Visible=@Visible, Author=@Author, PublishDate=@PublishDate, UpdatedDate=@UpdatedDate WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Title", contact.Title);
            cmd.Parameters.AddWithValue("@Content", contact.Content);
            cmd.Parameters.AddWithValue("@Summary", contact.Summary);
            cmd.Parameters.AddWithValue("@UrlHandel", contact.UrlHandel);
            cmd.Parameters.AddWithValue("@FeaturedImageUrl", contact.FeaturedImageUrl);
            cmd.Parameters.AddWithValue("@Visible", contact.Visible);
            cmd.Parameters.AddWithValue("@Author", contact.Author);
            cmd.Parameters.AddWithValue("@PublishDate", contact.PublishDate);
            cmd.Parameters.AddWithValue("@UpdatedDate", contact.UpdatedDate);
            cmd.ExecuteNonQuery();
            con.Close();
            return Ok(contact);
        }

        // Get function by id
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            con.Open();
            cmd = new SqlCommand("SELECT * FROM Posts WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            con.Close();
            Contact contact = new Contact();
            foreach (DataRow row in dt.Rows)
            {
                contact.Id = row["Id"].ToString();
                contact.Title = row["Title"].ToString();
                contact.Content = row["Content"].ToString();
                contact.Summary = row["Summary"].ToString();
                contact.UrlHandel = row["UrlHandel"].ToString();
                contact.FeaturedImageUrl = row["FeaturedImageUrl"].ToString();
                contact.Visible = row["Visible"].ToString();
                contact.Author = row["Author"].ToString();
                contact.PublishDate = row["PublishDate"].ToString();
                contact.UpdatedDate = row["UpdatedDate"].ToString();
            }
            return Ok(contact);
        }
        // Delete from Database
        [HttpDelete("{id}")]

        public IActionResult Delete(string id)
        {
            con.Open();
            cmd = new SqlCommand("DELETE FROM Posts WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
            con.Close();
            return Ok();
        }



        // Get users details from table Users
        //[HttpGet("users")]
        //public IEnumerable<User> GetUsers()
        //{
        //    List<User> users = new List<User>();
        //    con.Open();
        //    cmd = new SqlCommand("SELECT * FROM Users", con);
        //    da = new SqlDataAdapter(cmd);
        //    dt = new DataTable();
        //    da.Fill(dt);
        //    con.Close();
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        users.Add(
        //            new User
        //            {
        //                Id = row["Id"].ToString(),
        //                Name = row["Name"].ToString(),
        //                Email = row["Email"].ToString(),
        //                Password = row["Password"].ToString(),
        //                Role = row["Role"].ToString(),
        //                CreatedDate = row["CreatedDate"].ToString(),
        //                UpdatedDate = row["UpdatedDate"].ToString()
        //            });
        //    }
        //    return users;
        //}

        

    }
}
 