using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class BanksEntities
    {
        [Key]
        [JsonProperty("bic")]
        public string Bic { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

    }
}
