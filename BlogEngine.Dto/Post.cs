using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlogEngine.Dto
{
    public class Post
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("author-id")]
        public int AuthorId { get; set; }
        [JsonPropertyName("author-name")]
        public string AuthorName { get; set; }

        [JsonPropertyName("published-date")]
        public DateTime? PublishedDate { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("comments")]
        public IEnumerable<Comment> Comments { get; set; }

        public Post()
        {
            Comments = new HashSet<Comment>();
        }
    }
}
