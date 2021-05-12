using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BLL.Models.Requests.Morphology
{
    public class AnalyzeRequest : BaseRequest
    {

        public bool readable = false;

        public bool best = false;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string paragraph;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string[] sentences;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string sentence;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string[] words;

    }
}
