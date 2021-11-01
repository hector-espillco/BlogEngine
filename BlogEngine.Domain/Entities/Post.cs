using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Domain.Entities
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public DateTime? PublishedDate { get; set; }
        public int Status { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public Post()
        {
            Comments = new HashSet<Comment>();
        }
    }
}
