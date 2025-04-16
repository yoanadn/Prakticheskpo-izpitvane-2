using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Business_Layer
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Name must not be over 20 symbols!")]
        public string Name { get; set; }

        public List<User> Users { get; set; }
        public List<Genre> Genres { get; set; }

        public Game()
        {

        }

        public Game(string name)
        {
            Name = Name;
            Users = new List<User>();
            Genres = new List<Genre>();
        }

    }
}
