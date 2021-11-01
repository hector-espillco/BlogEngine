using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlogEngine.Dto.Request
{
    public class EditPostRequestDto
    {
        [JsonPropertyName("post-id")]
        public int PostId { get; set; }

        [JsonPropertyName("author-id")]
        public int AuthorId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}
