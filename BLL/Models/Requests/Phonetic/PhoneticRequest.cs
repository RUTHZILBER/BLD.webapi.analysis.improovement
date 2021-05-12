using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BLL.Models.Requests.Phonetic
{
    public class PhoneticRequest : BaseRequest
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public UIPhoneticType type;

        public string[] words;
    }
}
