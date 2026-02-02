using Newtonsoft.Json;

namespace Umbraco.Forms.Integrations.Crm.Hubspot.Models.Responses
{
    public class PropertyOption
    {
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }
    }
}
