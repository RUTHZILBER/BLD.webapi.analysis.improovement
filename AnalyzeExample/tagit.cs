using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyzeExample
{
    /// <summary>
    /// אינם בינארי לנתוח הטקסט
    /// </summary>
    enum Analizing:long
    {
        /// <summary>
        /// 
        /// </summary>
        zero=0, ///<summary>A == B</summary>
        doo = 1,mem=2,shin=4,he= 8//צווי__ מ__ש__ה //1
            , vav=16,vavCon=32,kaf=64,lamed= 128//ו__וחבור__כ__ל//2
            , bet=256,kshe=512,lekshe=1024,q= 2048//ב__כש__לכש__שאלה//3
            , sheha=4096,verb=8192,noun=16384,adj= 32768//4//שה__פועל__עצם__תואר
            , num=65536,yachas=131072,pnoun=262144,con= 524288//מספר__יחס__מילת גוף__מילת חבור//5
            , model=1048576,name=2097152,ben=4194304,bat= 8388608//רציתי ללכת__שם__בן__בת//6
            , first=16777216,second=33554432,third=67108864,nismach= 134217728//7//גוף ראשון__שני__שלישי__נסמך
            , future=268435456,past=536870912,present=1073741824,paul= 2147483648//8//עתיד__עבר__הווה__פעול
            , makor=4294967296,adverb=8589934592,b=17179869184,c= 34359738368//9//מקור__תואר__הפועל__סתם
            , e=68719476736,all= 268435455//מספרי רזרבות

          //  0000 0000 0100 0000 0000 0000
          //                           8421
          
    }
    /// <summary>
    /// מילה מהתגית
    /// </summary>
    class Tagit
    {
        public Tagit(Analizing ana, string word)
        {
            this.Ana = ana;
            Word = word;
        }

        /// <summary>
        /// הניתוח
        /// </summary>
        public Analizing Ana { get; set; }
        /// <summary>
        /// המילה עצמה
        /// </summary>
        public string Word { get; set; }
    }
}
