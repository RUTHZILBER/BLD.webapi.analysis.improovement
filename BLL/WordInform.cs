using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    class WordInform
    {
        public WordInform()
        {
        }

        public WordInform(int songIndex, string word, int tagIndex, int tagCountWords)
        {
            SongIndex = songIndex;
            Word = word;
            TagIndex = tagIndex;
            TagCountWords = tagCountWords;
        }




        /// <summary>
        /// אינדקס המילה בשיר
        /// </summary>
        public int SongIndex { get; set; }
        /// <summary>
        /// המילה
        /// </summary>
        public string Word { get; set; }
        /// <summary>
        /// אינדקס התגית שאליה המילה שיכת
        /// </summary>
        public int TagIndex { get; set; }
        /// <summary>
        /// מספר המילים בתגית אליה המילה שיכת
        /// </summary>
        public int TagCountWords { get; set; }
    }
}
