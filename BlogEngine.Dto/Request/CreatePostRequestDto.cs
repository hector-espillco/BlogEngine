using System.Text.Json.Serialization;

namespace BlogEngine.Dto.Request
{
    public class CreatePostRequestDto
    {
        [JsonPropertyName("author-id")]
        public int AuthorId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}
