using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BLL.Models.Requests.Ner
{
    public class NerCitiesRequest : BaseRequest
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string text;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string[] sentences;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string sentence;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string[] words;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string[] categories;
    }
}
