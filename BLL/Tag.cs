using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    /// <summary>
    /// התגית השלמה
    /// </summary>
   public class Tag
    {
        public Tag(string fullTag, List<DWord> informTags)
        {
            FullTag = fullTag;
            InformTags = informTags;
        }
        /// <summary>
        /// התגית : יתכנו כמה מילים
        /// </summary>
        public string FullTag { get; set; }
        /// <summary>
        /// רשימת 'מילים מנותחות' : המכילה עבור כל מילה בתגית את הנתוח והמילה עצמה
        /// </summary>
        public List<DWord> InformTags { get; set; }
    }
}
