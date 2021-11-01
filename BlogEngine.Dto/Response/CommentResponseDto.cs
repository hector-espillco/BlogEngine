using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlogEngine.Dto.Response
{
    public class CommentResponseDto : ResponseDto
    {
        [JsonPropertyName("comment")]
        public Comment Comment { get; set; }
    }
}
