using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogEngine.Domain.Entities
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Role { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public Author()
        {
            Comments = new HashSet<Comment>();
            Posts = new HashSet<Post>();
        }
    }
}
