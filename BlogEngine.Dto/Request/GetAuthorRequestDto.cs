using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlogEngine.Dto.Request
{
    public class GetAuthorRequestDto
    {
        [JsonPropertyName("user-name")]
        public string UserName { get; set; }
    }
}
