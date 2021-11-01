using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlogEngine.Dto.Response
{
    public class PostsResponseDto : ResponseDto
    {
        [JsonPropertyName("posts")]
        public IEnumerable<Post> Posts { get; set; }
        public PostsResponseDto()
        {
            Posts = new HashSet<Post>();
        }
    }
}
