using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public class TextUserId
    {
        public TextUserId()
        {
        }

        public TextUserId(int usrId, string text, string state)
        {
            UsrId = usrId;
            Text = text;
            State = state;
        }

        public int UsrId { get; set; }
        public string Text { get; set; }
        public string State { get; set; }
    }
}
