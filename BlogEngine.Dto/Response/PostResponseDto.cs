using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlogEngine.Dto.Response
{
    public class PostResponseDto : ResponseDto
    {
        [JsonPropertyName("post")]
        public Post Post { get; set; }
    }
}
