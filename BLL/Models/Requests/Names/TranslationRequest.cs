using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BLL.Models.Requests.Names
{
    public class TranslationRequest : BaseRequest
    {

        [JsonConverter(typeof(StringEnumConverter))]
        public Language type;

        public string[] words;

        public int threshold;

    }
}
