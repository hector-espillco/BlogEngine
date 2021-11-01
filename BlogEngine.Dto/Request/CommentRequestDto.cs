using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlogEngine.Dto.Request
{
    public class CommentRequestDto
    {
        [Required]
        [JsonPropertyName("post-id")]
        public int PostId { get; set; }

        [Required]
        [JsonPropertyName("author-id")]
        public int AuthorId { get; set; }

        [Required]
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}
