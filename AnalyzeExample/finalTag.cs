using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyzeExample
{
    /// <summary>
    /// תגית משיר
    /// </summary>
    class FinalTag
    {
        public FinalTag(string fullTag, List<Tagit> informTags)
        {
            FullTag = fullTag;
            InformTags = informTags;
        }
        /// <summary>
        /// התגית (יתכנו כמה מילים
        /// </summary>
        public string FullTag { get; set; }
        /// <summary>
        /// רשימת תגים המכילים עבור כל מילה בתגית את הנתוח והמילה עצמה
        /// </summary>
        public List<Tagit> InformTags{ get; set; }
    }
}
