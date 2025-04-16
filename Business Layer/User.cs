using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Business_Layer
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Name must not be over 20 symbols!")]
        public string Name { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Surname must not be over 20 symbols!")]
        public string Surname { get; set; }

        [Required]
        [Range(10, 80, ErrorMessage = "Age must be between 10 and 80!")]
        public int Age { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Username must not be over 20 symbols!")]
        public string Username { get; set; }

        [Required]
        [MaxLength(70, ErrorMessage = "Password must not be over 70 symbols!")]
        public string Password { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Email must not be over 20 symbols!")]
        public string Email { get; set; }

        public List<User> Friends { get; set; }
        public List<Game> Games { get; set; }

        public User()
        {

        }

        public User(string name, string surname, int age, string username, string password, string email)
        {
            Name = name;
            Surname = surname;
            Age = age;
            Username = username;
            Password = password;
            Email = email;
            Friends = new List<User>();
            Games = new List<Game>();
        }

    }
}
