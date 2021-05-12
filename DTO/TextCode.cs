using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public class TextCode
    {
        public TextCode()
        {
        }

        public TextCode(string text, int songId)
        {
            Text = text;
            SongId = songId;
        }

        public string Text { get; set; }
        public int SongId { get; set; }

    }
}
