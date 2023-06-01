using System.Globalization;
using System.Security.Cryptography.Xml;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RateMyProfessors.Models
{
    public class Professor
    {
        public string? Id { get; set; }  

        public string? Name { get; set; }

        [JsonPropertyName("photo")]
        public string? Photo { get; set; }

        public string? Position { get; set; }

        public string? Phone { get; set; }

        public string? Office { get; set; }

        public int[]? Ratings { get; set; }
        
        // enhancement
        public string []? Comments{ get; set; }

        public override string ToString() => JsonSerializer.Serialize<Professor>(this);
        
    }
}
