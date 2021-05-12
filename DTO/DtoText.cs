using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    class DtoText
    {
        public DtoText()
        {
        }

        public DtoText(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
    }
}
