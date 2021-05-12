using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    /// <summary>
    /// עצם טקסטי
    /// </summary>
   public class Text
    {
        public Text()
        {
        }

        public Text(string texts)
        {
            Texts = texts;
        }

        public string Texts { get; set; }
    }
}
