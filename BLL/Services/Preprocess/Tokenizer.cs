using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models.Requests.PreProcess;

namespace BLL.Services.Preprocess
{
    public class Tokenizer
    {
        public const string PREPROCESS_TOKENIZER_ENDPOINT = "/service/preprocess/tokenizer";

        static readonly Service<TokenizerRequest>
            Service = new Service<TokenizerRequest>(PREPROCESS_TOKENIZER_ENDPOINT);

        public static List<List<string>> TokenizeText(string text)
        {
            return Service.GetService<List<List<string>>>(new TokenizerRequest() { text = text });
        }

        public static List<List<string>> TokenizeSentences(string[] sentences)
        {
            return Service.GetService<List<List<string>>>(new TokenizerRequest() { sentences = sentences });
        }

        public static List<List<string>> TokenizeSentence(string sentence)
        {
            return Service.GetService<List<List<string>>>(new TokenizerRequest() { sentence = sentence });
        }
    }
}
