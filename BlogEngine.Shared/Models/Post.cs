using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Shared.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string Content { get; set; }
        public DateTime? PublishedDate { get; set; }
        public int Status { get; set; }
        public bool SeeComments { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
        public Post()
        {
            Comments = new HashSet<Comment>();
        }
    }
}
