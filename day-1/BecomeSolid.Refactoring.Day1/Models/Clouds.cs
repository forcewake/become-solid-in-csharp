using Newtonsoft.Json;

namespace BecomeSolid.Refactoring.Day1.Models
{
    public class Clouds
    {

        [JsonProperty("all")]
        public int All { get; set; }
    }
}