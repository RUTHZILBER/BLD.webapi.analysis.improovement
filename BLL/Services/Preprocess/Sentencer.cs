using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models.Requests.PreProcess;

namespace BLL.Services.Preprocess
{
    public class Sentencer
    {
        public const string PREPROCESS_SENTENCER_ENDPOINT = "/service/preprocess/sentencer";

        private static readonly Service<SentencerRequest> Service =
            new Service<SentencerRequest>(PREPROCESS_SENTENCER_ENDPOINT);

        public static List<string> Sentences(string text)
        {
            return Service.GetService<List<string>>(new SentencerRequest() { text = text, token = HebrewNLP.Password });
        }
    }
}
