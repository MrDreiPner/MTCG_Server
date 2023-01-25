using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.MTCG.Models
{
    public class UserContent
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Bio { get; set; }

        public UserContent(string name, string image, string bio) { 
            Name = name;
            Image = image;
            Bio = bio;
        }
    }
}
