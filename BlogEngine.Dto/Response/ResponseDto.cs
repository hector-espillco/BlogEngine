using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlogEngine.Dto.Response
{
    public class ResponseDto
    {
        [JsonPropertyName("success")]
        public bool IsSucces { get; set; }

        [JsonPropertyName("errors")]
        public IEnumerable<string> Errors { get; set; }

        public ResponseDto()
        {
            IsSucces = false;
            Errors = new HashSet<string>();
        }
    }
}
