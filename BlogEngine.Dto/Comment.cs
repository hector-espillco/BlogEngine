using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlogEngine.Dto
{
    public class Comment
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("post-id")]
        public int PostId { get; set; }

        [JsonPropertyName("author-id")]
        public int AuthorId { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}
