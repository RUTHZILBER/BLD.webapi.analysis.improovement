using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

namespace BLL.Models.Requests.Morphology

   

   
    {
        public class NormalizeRequest : BaseRequest
        {
            [JsonConverter(typeof(StringEnumConverter))]
            public NormalizationType type = NormalizationType.SEARCH;

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string text;

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string[] sentences;

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string sentence;

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string[] words;
        }
    }

