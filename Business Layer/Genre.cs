using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Business_Layer
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Name must not be over 20 symbols!")]
        public string Name { get; set; }

        public List<User> Users { get; set; }

        public Genre()
        {

        }

        public Genre(string name)
        {
            Name = name;
            Users = new List<User>();
        }

    }
}
