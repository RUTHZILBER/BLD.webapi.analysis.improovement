using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models.Requests.Names;
using BLL.Models.Respone.Names;

namespace BLL.Services.Names
{
    public class NameAnalyzer
    {
        public const string NAME_TRANSLATION_ENDPOINT = "/service/names/translation";

        private static readonly Service<NamesAnalyzeRequest> Service =
            new Service<NamesAnalyzeRequest>(NAME_TRANSLATION_ENDPOINT);

        public static List<NameInfo> Analyze(string[] names)
        {
            return Service.GetService<List<NameInfo>>(new NamesAnalyzeRequest { names = names });
        }
    }
}
