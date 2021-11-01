using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlogEngine.Dto.Request
{
    public class SubmitPostRequestDto
    {
        [JsonPropertyName("post-id")]
        public int PostId { get; set; }

        [JsonPropertyName("author-id")]
        public int AuthorId { get; set; }
    }
}
