using Newtonsoft.Json;
using System.Collections.Generic;

namespace Umbraco.Forms.Integrations.Crm.Hubspot.Models.Responses
{
    public class Property
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "options")]
        public List<PropertyOption> Options { get; set; } = new List<PropertyOption>();
    }
}
