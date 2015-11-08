using Newtonsoft.Json;

namespace BecomeSolid.Refactoring.Day1.Models
{
    public class Wind
    {

        [JsonProperty("speed")]
        public double Speed { get; set; }

        [JsonProperty("deg")]
        public double Deg { get; set; }
    }
}