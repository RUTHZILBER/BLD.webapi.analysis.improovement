using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using HebrewNLP.Morphology;
using System.Text.RegularExpressions;
using System.Collections;

namespace AnalyzeExample
{
    /// <summary>
    /// enum for information about the tags, how to cut or join...
    /// </summary>
    //enum Demand
    //{
    //    zero = 0, first = 1, last = 2, between = 4, alone = 8, vav = 16, vavCon = 32, kaf = 64, lamed = 128, bet = 256
    //}
    //בכל שיר בממוצע 75 מילים, וכל מילה בת שלש וחצי תווים +רוח

    class Program
    {
        public int day;
        public int maybe;
        //רשימות של מילים היכולות להופיע רק בסוף תגית, בתחילתה ובאמצעה
        public static List<string> achar = new List<string> { "אחרי", "אחריכם", "אחריכן", "אחריהם", "אחריהן", "אחריך", "אחריך", "אחריו", "אחריה", "אחרינו" };//9
        public static List<string> el = new List<string> { "אל", "אליכם", "אליכן", "אליהם", "אליהן", "אלי", "אליך", "אליך", "אליו", "אליה", "אלינו" };//9
        public static List<string> ezel = new List<string> { "אצל", "אצלכם", "אצלכן", "אצלם", "אצלן", "אצלי", "אצלך", "אצלך", "אצלו", "אצלה", "אצלנו" };//9
        public static List<string> et = new List<string> { "את", "אתכם", "אתכן", "אותם", "אותן", "אותי", "אותך", "אותך", "אותו", "אותה", "אותנו" };//9
        public static List<string> tachat = new List<string> { "תחתיכם", "תחתיכן", "תחתיהם", "תחת", "תחתי", "תחתיך", "תחתיך", "תחתיו", "תחתיה", "תחתינותחתיכם", "תחתיכן", "תחתיהם", "תחתיהן," };//9
        public static List<string> bishvil = new List<string> { "בשבילכם", "בשבילכן", "בשבילם", "בשבילן", "בשביל", "בשבילי", "בשבילך", "בשבילך", "בשבילו", "בשבילה", "בשבילנו" };//9
        public static List<string> meachor = new List<string> { "אחוריn", "מאחוריך", "מאחוריך", "מאחוריו", "מאחוריה", "מאחורינו", "מאחוריכם", "מאחוריכן", "מאחוריהם", "מאחוריהן" };//9
        public static List<string> shel = new List<string> { "שלי", "שלו", "שלה", "שלך", "שלכם", "שלנו", "שלהם", "שלהן" };//9
        public static List<string> lefanay = new List<string> { "לפניכם", "לפניכן", "לפניהם", "לפניהן", "לפנילפני", "לפניך", "לפניך", "לפניו", "לפניה", "לפנינו" };//9
        public static List<string> be = new List<string> { "בכם", "בכן", "בהם", "בהן", "בי", "בך", "בך", "בו", "בה", "בנו" };//9+

        public static List<string> q = new List<string> { "היכן", "האם", "במי", "במה", "מי", "מה", "מתי", "למי", "למה", "לאן", "למה", "איך", "איפה", "איה", "איזה", "אילו", "איזו", "מדוע", "מאין", "מנין", "מתי", "ממי", "ממה", "כמה", "כיצד" };//1
        public static List<string> bein = new List<string> { "ביניכם", "ביניכן", "ביניהם", "ביני", "בינך", "בינך", "בינו", "בינה", "בינינו","יהיה","היה","יהא" };//9+
        public static List<string> me = new List<string> { "מן", "מכם", "מכן", "מהם", "מהן", "ממנה", "ממנו", "ממני", "ממך", "ממך", "ממנו","איתי","איתך","איתו","איתה","איתנו","איתכם","איתכן","איתם","איתן" };//9+
        public static List<string> lach = new List<string> { "לי", "לך", "לה", "לו", "לנו", "להם", "להן", "לכם", "להן" };//+9

        public static List<string> ani = new List<string> { "ואני", "והיא", "והוא", "וזה", "ואנחנו", "ואתם", "ואת", "וזו", "וזאת", "ואתה", "ואתן", "והם", "והן", "והנה", "אני", "היא", "הוא", "זה", "אנחנו", "אתם", "את", "זו", "זאת", "אתה", "אתן", "הם", "הן", "הנה" };//1
        public static List<string> split = new List<string> { "ואני", "והיא", "והוא", "וזה", "ואנחנו", "ואתם", "ואת", "וזו", "וזאת", "ואתה", "ואתן", "והם", "והן", "והנה", "אני", "היא", "הוא", "זה", "אנחנו", "אתם", "את", "זו", "זאת", "אתה", "אתן", "הם", "הן", "הנה" };//1
        public static List<string> legabey = new List<string> { "לגביכם", "לגביכן", "לגביהם", "לגביהן", "לגבי", "לגביך", "לגביך", "לגביו", "לגביה", "לגבינו" };//1


        public static List<string> alay = new List<string> { "עליכם", "עליכן", "עליהם", "עליהם", "עלי", "עליך", "עליך", "עליו", "עליה", "עלינו" };//+
        public static List<string> eini = new List<string> { "איני","אינך","אינו","אינה","אינם","אינן","איננו","אינכם","אינכן","כי"};//+


        public static List<string> leyad = new List<string> { "לידכם", "לידכן", "לידם", "לידן", "ליד", "לידי", "לידך", "לידך", "לידו", "לידה", "לידינו" };//91
        public static List<string> biglal = new List<string> { "בגללכם", "בגללכן", "בגללכם", "בגללן", "בגלל", "בגללי", "בגללך", "בגללך", "בגללו", "בגללה", "בגללנו" };
        public static List<string> bilad = new List<string> { "בלעדיכם", "בלעדיכן", "בלעדיהם", "בלעדיהן", "בלעדי", "בלעדי", "בלעדיך", "בלעדיך", "בלעדיו", "בלעדיה", "בלעדינו" };//91
        public static List<string> betoch = new List<string> { "בתוככם", "בתוככן", "בתוכם", "בתוכן", "בתוך", "בתוכי", "בתוכך", "בתוכך", "בתוכו", "בתוכה", "בתוכנו" };//9+1 

        public static List<string> middle = new List<string> { "אל", "אשר", "ליד", "תחת", "בתוך", "את", "על", "אצל", "ללא", "הכל", "מכל", "בכל", "לכל", "אל", "כבר", "את", "אשר", "רק", "על", "כן", "לא", "כל", "של", "בתוך", "ליד" };
        public static List<string> middleEnd = new List<string> { "הזה", "הזו", "ההוא", "הלז", "ההיא", "ההם", "ההן", "האלה", "האלו", "הזו", "הזאת", "מעל" };
        public static List<string> middleStart = new List<string> { "בין", "בשביל", "אחר", "מן", "כמו", "המה" };
        public static List<string> start1 = new List<string> { "אנוכי", "אלו", "אלה", "גם","אז","אך" };
        public static List<List<string>> canStart_1_1p_19 = new List<List<string>> { betoch, leyad, ani, legabey, q, start1, middleStart };//מילים שיכולות לפתח ולא חיבות
        public static List<List<string>> canEnd_9_9p_19 = new List<List<string>> {achar, el, ezel, et, tachat, bishvil, meachor, shel, lefanay, be, bein, me, lach, leyad, bilad, betoch, middleEnd };//מילים שיכולות לסגר ולא חייבות
        public static List<List<string>> end_ = new List<List<string>>
            { achar, el, ezel, et,tachat,bishvil,shel,lefanay,meachor};//מילים החיבות להיות בסוף
        public static List<List<string>> middle_ = new List<List<string>>
            {be, bein,me,lach,alay,eini,middleEnd,middle,middleStart};//מילים היכולות להיות באמצע
        public static List<List<string>> middle_must = new List<List<string>>
            {alay,middle};//מילים החיבות להיות באמצע

        public static List<List<string>> start_ = new List<List<string>> { start1, ani, legabey, q };//מילים חיבות להיות בהתחלה

        /// <summary>
        /// פונקציה המריצה את פיצול התגיות שבשיר
        /// </summary>
        /// <param name="song">השיר</param>
        /// <returns></returns>
        public static List<FinalTag> start(string song1)
        {

            string song = cleanSong(song1);//טהרת השיר מתווים אסורים ומרווחים מיותרים 
            string firstWordDitty1, firstWordDitty2, lastWordDitty1, lastWordDitty2;

            dittyWords(song, out firstWordDitty1, out firstWordDitty2, out lastWordDitty1, out lastWordDitty2);//קבלת מילים פותחות וסוגרות עבור שני הפזמונים הראשיים
            List<List<Tagit>> a = getMorphList(song);//הפעלת ניתוח של הספריה
            List<FinalTag> b = getWords(a);//הפעלת הניתוח לפי האינם שלי
            b = isVavCon(b);//זמון פונקציה למציאת וו החיבור שלא מפרידה

            int minus;
            for (int i = 0; i < b.Count; i++)//איגוד תגיות של מילים מאחדות
            {
               
                b = firstJoiner(b, i, out minus, firstWordDitty1, firstWordDitty2, lastWordDitty1, lastWordDitty2);

                {
                    i -= minus;
                }


            }

            int minus0 = 0;
            for (int i = 0; i < b.Count; i++)//איגוד של מילים בודדות
            {
                if (i == 7)
                    i = i;
                b = secondJoinerNotAlone(b, i, out minus0, firstWordDitty1, firstWordDitty2, lastWordDitty1, lastWordDitty2);
                i = i - minus0;

            }

            int min = 0;

            //for (int i = 0; i < b.Count; i++)//לא שימושי
            //{
            //    b = thirdSplit(b, i, out min, firstWordDitty1, firstWordDitty2, lastWordDitty1, lastWordDitty2);
            //}
            for (int i = 0; i < b.Count; i++)//פיצול תגיות במקום בו אפשר, ואם אי אפשר והתגית מעל 8 תווים, גמישות עד 10 ואז חתך שרירותי
            {
               
                b = thirdSplit8Letters(b, i, out min, firstWordDitty1, firstWordDitty2, lastWordDitty1, lastWordDitty2);
                if (min == 1)
                {
                    i--;
                }
                if (i == 5)
                    i = i;
                
            }
            for (int i = 0; i < b.Count; i++)
            {
                Console.WriteLine(b[i].FullTag);
            }
            return b;



        }
       /// <summary>
       /// הרצת פיצול התגיות והחזרת רשימת התגיות הסופית
       /// </summary>
       /// <param name="song"></param>
       /// <returns></returns>
       public static List<string> startUp(string song)
        {
            List<string> tags = new List<string>();
            List<FinalTag> a = start(song);
            for (int i = 0; i < a.Count; i++)
            {
                tags.Add(a[i].FullTag);
            }
            return tags;
        }
        /// <summary>
        /// החזרת מילים פותחות וסוגרות לשני הפזמונים הראשונים
        /// </summary>
        /// <param name="song"></param>
        /// <param name="firstD1"></param>
        /// <param name="lastD1"></param>
        /// <param name="firstD2"></param>
        /// <param name="lastD2"></param>
        public static void dittyWords(string song, out string firstD1, out string lastD1, out string firstD2, out string lastD2)
        {
            string lastWordDitty1 = "", firstWordDitty1 = "", lastWordDitty2 = "", firstWordDitty2 = "";
            //המילה הראשונה והאחרונה בפזמון חייבת לפתוח או לסגר תווית
            string ditty = LongestWords(song);//קבלת הפזמון הראשון
            if (ditty != "")
            {
                if (ditty[0] == ' ')
                    ditty = ditty.Substring(1, ditty.Length - 1);
                if (ditty != "")
                {


                    if (ditty[ditty.Length - 1] == ' ')
                        ditty = ditty.Substring(0, ditty.Length - 1);
                }
            }
            if (ditty[0] == ' ')//מחיקת רווח אם קים בהתחלה
                ditty = ditty.Substring(1, ditty.Length - 1);
            if (ditty[ditty.Length - 1] == ' ')//מחיקת רווח אם קים בסוף
                ditty = ditty.Substring(0, ditty.Length - 1);
            if (ditty.IndexOf(" ", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                int len = ditty.IndexOf(' ');//קבלת מיקום ראשון של רווח
                firstWordDitty1 = ditty.Substring(0, len);//קבלת המילה הראשונה
                int from = ditty.LastIndexOf(' ') + 1;// קבלת מיקום אחרון של רווח 
                len = ditty.Length - 1 - ditty.LastIndexOf(' ');//אורך המילה הארונה
                lastWordDitty1 = ditty.Substring(from, len);//קבלת המילה האחרונה
                if (ditty.Split(' ').Length < 3)//אם הפזמון קצר מידי
                {
                    lastWordDitty2 = "";
                    firstWordDitty2 = "";

                    firstD1 = firstWordDitty1;
                    firstD2 = firstWordDitty2;
                    lastD1 = lastWordDitty1;
                    lastD2 = lastWordDitty2;
                    return;
                }

                if (!Regex.IsMatch(song, @"\b" + lastWordDitty1 + "\b"))//אם ה'מילה' האחרונה בפזמון חתוכה, בחירת המילה שלפניה
                {
                    string str = ditty;
                    int lastIndex = str.LastIndexOf(' ');
                    ditty = str.Substring(0, lastIndex);
                    lastWordDitty1 = ditty.Split(' ').Last();
                }

                if (!Regex.IsMatch(song, @"\b" + firstWordDitty1 + "\b"))//אם ה'מילה' הראשונה בפזמון חתוכה, בחירת המילה שאחריה
                {
                    int i = ditty.IndexOf(" ") + 1;
                    ditty = ditty.Substring(i);
                    firstWordDitty1 = ditty.Split(' ').First();

                }
            }


            //כנ"ל גם בתת הפזמון
            string ditty2 = LongestWordsLevelIndex(song, 2);
            if (ditty2 != "")
            {
                if (ditty2[0] == ' ')
                    ditty2 = ditty2.Substring(1, ditty2.Length - 1);
                    if (ditty2 != "")
                    {
                        

                        if (ditty2[ditty2.Length - 1] == ' ')
                            ditty2 = ditty2.Substring(0, ditty2.Length - 1);
                    }
            }
            if (ditty2.IndexOf(" ", StringComparison.OrdinalIgnoreCase) >= 0)//האם מופיע רווח במילה
            {
               

                firstWordDitty2 = ditty2.Substring(0, song.IndexOf(' '));
                lastWordDitty2 = ditty2.Substring(ditty2.LastIndexOf(' ') + 1, ditty2.Length - 1 - ditty2.LastIndexOf(' '));
                if (ditty2.Split(' ').Length < 3)//אם הפזמון קצר מידי
                {
                    lastWordDitty2 = "";
                    firstWordDitty2 = "";

                    firstD1 = firstWordDitty1;
                    firstD2 = firstWordDitty2;
                    lastD1 = lastWordDitty1;
                    lastD2 = lastWordDitty2;
                    return;
                }
                if (!Regex.IsMatch(song, @"\b" + lastWordDitty2 + "\b"))//אם ה'מילה' האחרונה בפזמון חתוכה, בחירת המילה שלפניה
                {
                    string str = ditty2;
                    int lastIndex = str.LastIndexOf(' ');
                    ditty2 = str.Substring(0, lastIndex);
                    lastWordDitty2 = ditty2.Split(' ').Last();
                }

                if (!Regex.IsMatch(song, @"\b" + firstWordDitty2 + "\b"))//אם ה'מילה' הראשונה בפזמון חתוכה, בחירת המילה שאחריה
                {
                    int i = ditty2.IndexOf(" ") + 1;
                    ditty2 = ditty2.Substring(i);
                    firstWordDitty2 = ditty2.Split(' ').First();

                }
            }
            firstD1 = firstWordDitty1;
            firstD2 = firstWordDitty2;
            lastD1 = lastWordDitty1;
            lastD2 = lastWordDitty2;


        }

        /// <summary>
        /// האם המילה יכולה ולא חיבת להיות באמצע
        /// </summary>
        /// <param name="str">המילה</param>
        /// <returns></returns>
        public static bool getListMiddle(string str)
        {


            if (middle_.Any(sublist => sublist.Contains(str)))
                return true;
            return false;
        }
        /// <summary>
        /// האם המילה  חיבת להיות באמצע
        /// </summary>
        /// <param name="str">המילה</param>
        /// <returns></returns>
        public static bool getListMiddleMust(string str)
        {


            if (middle_must.Any(sublist => sublist.Contains(str)))
                return true;
            return false;
        }
        /// <summary>
        /// האם המילה חותכת
        /// </summary>
        /// <param name="str">המילה</param>
        /// <returns></returns>
        public static bool getListSplit(string str)
        {


            if (split.Any(sublist => sublist.Contains(str)))
                return true;
            return false;
        }
        /// <summary>
        /// האם המילה יכולה ולא חיבת להיות בהתחלה
        /// </summary>
        /// <param name="str">המילה</param>
        /// <returns></returns>
        public static bool getListMayStart(string str)
        {


            if (canStart_1_1p_19.Any(sublist => sublist.Contains(str)))
                return true;
            return false;
        }
        /// <summary>
        /// האם המילה יכולה להיות באמצע
        /// </summary>
        /// <param name="str">המילה</param>
        /// <returns></returns>
        public static bool getListMayEnd(string str)
        {

            //אם המילה ברשימה הסטטית של המילים היכולות להיות באמצע יוחזר אמת
            if (canEnd_9_9p_19.Any(sublist => sublist.Contains(str)))
                return true;
            return false;
        }
        /// <summary>
        /// החזרת רשימת תגיות לשיר לפי האינם שהוגדר במחלקת תגית
        /// </summary>
        /// <param name="song">השיר</param>
        /// <returns>התגיות</returns>
        public static List<List<Tagit>> getMorphList(string song)
        {
            long value;
            long hhhh = 1099511627775;//בזמן הנתוח, התיחסות רק בפעם הראשונה

            List<List<List<MorphInfo>>> morphInfos3 = new List<List<List<MorphInfo>>>() { HebrewMorphology.AnalyzeSentence(song) };
            String[] strgs = song.Split(" ");//פיצול השיר למילים
            ArrayList arrayList = new ArrayList();
            List<List<Tagit>> tagits = new List<List<Tagit>>();
            for (int i = 0; i < strgs.Length; i++)
            {
                tagits.Add(new List<Tagit>());
                tagits[i].Add(new Tagit(Analizing.zero, strgs[i]));
            }
            long subordination, prepositionchars, partofspeech, gender, person, constructstate, tense;//עבור תוצאות הנתוח ופינוחם לפי האינם שהוגדר
            for (int i = 0; i < strgs.Length; i++)
            {

                for (int j = 0; j < morphInfos3.Count; j++)
                {
                    
                    List<MorphInfo> lm = morphInfos3[j][i];
                    foreach (MorphInfo item in lm)
                    {
                        
                        value = 0;
                        subordination = (long)item.Subordination;
                        prepositionchars = (long)item.PrepositionChars;
                        partofspeech = (long)item.PartOfSpeech;
                        gender = (long)item.Gender;
                        person = (long)item.Person;
                        constructstate = (long)item.ConstructState;
                        tense = (long)item.Tense;

                        //שמוש בנתוחים של הספריה לפענוח סוג התגית לפי האינם שהוגדר כאמור במחלקת תגית
                        if (item.Vav == true)//אם יש וו של משה וכלב
                            value = (long)Analizing.vav | value | (long)tagits[i][0].Ana;//האינם יסומן כוו, קימת פונקציה בהמשך לבדיקת אפיון הוו: מחבר או מפריד
                        switch (subordination)
                        {

                            case 1:
                                value = (long)Analizing.shin | (long)tagits[i][0].Ana | value;
                                break;
                            case 2:
                                value = (long)Analizing.kshe | (long)tagits[i][0].Ana | value;
                                break;
                            case 3:
                                value = (long)Analizing.lekshe | (long)tagits[i][0].Ana | value;
                                break;
                            case 7:
                                value = (long)Analizing.he | (long)tagits[i][0].Ana | value;
                                break;
                            case 8:
                                value = (long)Analizing.he | (long)Analizing.sheha | (long)tagits[i][0].Ana | value;
                                break;


                            default:
                                break;
                        }
                        switch (prepositionchars)
                        {
                            case 1:
                                value = (long)Analizing.bet | (long)tagits[i][0].Ana | value;
                                break;
                            case 8:
                                value = (long)Analizing.mem | (long)Analizing.kaf | (long)tagits[i][0].Ana | value;
                                break;
                            case 4:
                                value = (long)Analizing.kaf | (long)tagits[i][0].Ana | value;
                                break;
                            case 5:
                                value = (long)Analizing.kaf | (long)Analizing.bet | (long)tagits[i][0].Ana | value;
                                break;
                            case 6:
                                value = (long)Analizing.lamed | (long)Analizing.bet | (long)tagits[i][0].Ana | value;
                                break;
                            case 7:
                                value = (long)Analizing.kaf | (long)Analizing.mem | (long)tagits[i][0].Ana | value;
                                break;
                            case 3:
                                value = (long)Analizing.lamed | (long)tagits[i][0].Ana | value;
                                break;
                            case 12:
                                value = (long)Analizing.lamed | (long)Analizing.kaf | (long)tagits[i][0].Ana | value;
                                break;
                            case 2:
                                value = (long)Analizing.mem | (long)tagits[i][0].Ana | value;
                                break;
                            case 10:
                                value = (long)Analizing.bet | (long)Analizing.mem | (long)tagits[i][0].Ana | value;
                                break;
                            case 9:
                                value = (long)Analizing.kaf | (long)Analizing.mem | (long)tagits[i][0].Ana | value;
                                break;
                            case 11:
                                value = (long)Analizing.lamed | (long)Analizing.mem | (long)tagits[i][0].Ana | value;
                                break;


                            default:
                                break;
                        }

                        switch (partofspeech)
                        {

                            case 1:
                                value = (long)Analizing.verb | (long)tagits[i][0].Ana | value;
                                break;
                            case -1 + 3:
                                value = (long)Analizing.noun | (long)tagits[i][0].Ana | value;
                                break;
                            case -1 + 4:
                                value = (long)Analizing.adj | (long)tagits[i][0].Ana | value;
                                break;
                            case -1 + 5:
                                value = (long)Analizing.num | (long)tagits[i][0].Ana | value;
                                break;
                            case -1 + 6:
                                value = (long)Analizing.yachas | (long)tagits[i][0].Ana | value;
                                break;
                            case -1 + 7:
                                value = (long)Analizing.pnoun | (long)tagits[i][0].Ana | value;
                                break;
                            case -1 + 8:
                                value = (long)Analizing.q | (long)tagits[i][0].Ana | value;
                                break;
                            case 8:
                                value = (long)Analizing.con | (long)tagits[i][0].Ana | value;
                                break;
                            case 10:
                                value = (long)Analizing.adverb | (long)tagits[i][0].Ana | value;
                                break;
                            case -1 + 12:
                                value = (long)Analizing.model | (long)tagits[i][0].Ana | value;
                                break;
                            case -1 + 13:
                                value = (long)Analizing.name | (long)tagits[i][0].Ana | value;
                                break;


                            default:
                                break;
                        }
                        switch (gender)
                        {
                            case 1:
                                value = (long)Analizing.ben | (long)tagits[i][0].Ana | value;
                                break;
                            case 2:
                                value = (long)Analizing.bat | (long)tagits[i][0].Ana | value;
                                break;
                            case 3:
                                value = (long)Analizing.ben | (long)Analizing.bat | (long)tagits[i][0].Ana | value;
                                break;

                            default:
                                break;
                        }
                        switch (person)
                        {
                            case 1:
                                value = (long)Analizing.first | (long)tagits[i][0].Ana | value;
                                break;
                            case 2:
                                value = (long)Analizing.second | (long)tagits[i][0].Ana | value;
                                break;
                            case 3:
                                value = (long)Analizing.third | (long)tagits[i][0].Ana | value;
                                break;
                            default:
                                break;
                        }

                        switch (constructstate)
                        {

                            case 1:
                                { 
                                   
                                    {
                                        value = (long)Analizing.nismach | (long)tagits[i][0].Ana | value&hhhh;
                                          break;
                                    }

                               }
                                
//                            case 3:
//                                {
//value = (long)Analizing.nismach | (long)tagits[i][0].Ana | value&hhhh;
//                                break;
//                                }
                                

                            default:
                                break;
                        }
                        

                        switch (tense)
                        {
                            case 1:
                                value = (long)Analizing.past | (long)tagits[i][0].Ana | value;
                                break;
                            case 2:
                                value = (long)Analizing.present | (long)tagits[i][0].Ana | value;
                                break;
                            case 3:
                                value = (long)Analizing.future | (long)tagits[i][0].Ana | value;
                                break;
                            case 4:
                                value = (long)Analizing.paul | (long)tagits[i][0].Ana | value;
                                break;
                            case 5:
                                value = (long)Analizing.doo | (long)tagits[i][0].Ana | value;
                                break;
                            case 6:
                                value = (long)Analizing.makor | (long)tagits[i][0].Ana | value;
                                break;


                            default:
                                break;
                        }

                        tagits[i][0].Ana = (Analizing)(value | (long)tagits[i][0].Ana);//השמת הפענוח בתגית
                        hhhh = 0;
                    }
                }
            }
            return tagits;
        }
        /// <summary>
        /// האם המילה חיבת להיות בסוף התגית
        /// </summary>
        /// <param name="str">המילה</param>
        /// <returns></returns>
        public static bool getListEnd(string str)
        {

            //אם המילה נמצאה ברשימת המילים הסטטית היכולות להופיע בסוף התגית יוחזר אמת
            if (end_.Any(sublist => sublist.Contains(str)))
                return true;
            return false;
        }
        /// <summary>
        /// האם המילה חיבת להיות בתחילת התגית
        /// </summary>
        /// <param name="str">המילה</param>
        /// <returns></returns>
        public static bool getListStart(string str)
        {

            //אם המילה יכולה להופיע בתחילת התגית יוחזר אמת
            if (start_.Any(sublist => sublist.Contains(str)))
                return true;

            return false;
        }
        /// <summary>
        /// האם המילה חיבת להיות בתחילת התגית, כולל הרשימה
        /// </summary>
        /// <param name="str"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static bool mustStart(string str, Analizing a)
        {
            if (getListStart(str) || ((long)a & ((long)Analizing.doo | (long)Analizing.name | (long)Analizing.num)) > 0)//האם המילה נמצאת ברשימת המתחילות, האם היא מספר, האם היא שם
                return true;
            return false;
        }
        /// <summary>
        /// החזרת הרצף (פיזמון) הגדול ביותר בשיר
        /// </summary>
        /// <param name="str">השיר</param>
        /// <returns></returns>
        public static string LongestWords(string str)
        {
            var word = str;

            string result =
            (
                from i in Enumerable.Range(0, word.Length)
                from j in Enumerable.Range(1, word.Length - i)
                group i by word.Substring(i, j) into gis
                where gis.Skip(1).Any()
                orderby gis.Key.Length descending
                select gis.Key
            ).First();//החזרת הרצף הגדול ביותר מהשיר
            return result;
        }
        /// <summary>
        /// החזרת תת פיזמון לאינדקס הרצוי מהשיר
        /// </summary>
        /// <param name="str">השיר</param>
        /// <param index="times">(באינדקס (0=פזמון</param>
        /// <returns></returns>
        public static string LongestWordsLevelIndex(string str, int index)
        {
            var word = str;
            string result = "";
            //הרצת הפעולה למציאת הפיזמון לפי האינדקס הרצוי פעמים
            for (int m = 0; m < index; m++)
            {
                result =
            (
             from i in Enumerable.Range(0, word.Length)
             from j in Enumerable.Range(1, word.Length - i)
             group i by word.Substring(i, j) into gis
             where gis.Skip(1).Any()
             orderby gis.Key.Length descending
             select gis.Key
                ).First();

                word = word.Replace(result, "");
            }


            return result;
        }

        /// <summary>
        /// רשימה מקוננת עם תדירויות המילים לפי מס' הופעתן
        /// </summary>
        /// <param name="list">השיר</param>
        /// <returns></returns>
        public static void TimesWords(List<string> list)
        {

            System.Collections.Generic.List<System.Linq.IGrouping<string, string>> newList = list.GroupBy(s => s).Distinct().OrderByDescending(j => j.Count()).ToList();
            //מייני את רשימת תדירות המילים במשפט, כמובן סנני את מילות הפזמון

            int max = 0;

            foreach (var item in newList)
            {
                if (item.Count() > max)
                    max = item.Count();

            }
            var maxListApear = newList.Where(s => s.Count() == max).ToList();


            //איך מפרקים קיבוץ?
            return;
        }
        /// <summary>
        ///VAVCON בדיקה האם האות וו היא פותחת או מחברת, שאז ישתנה הנתוח ל
        /// </summary>
        /// <param name="finalTag">תגיות השיר</param>
        /// <param name="index">מספר התגית</param>
        /// <param name="j">מיקום המילה בתגית</param>
        /// <returns></returns>
        public static List<FinalTag> isVavCon(List<FinalTag> finalTag)
        {
            for (int i = 1; i < finalTag.Count - 1; i++)
            {
                long isVav = (long)Analizing.vav & (long)finalTag[i].InformTags[0].Ana;//אם יש במילה את וו החבור
                                                                                       //(long)finalTag[i-1].InformTags[0].Ana;//אם יש במילה את וו החבור
                if (isVav > 0)//האם יש במילה את וו החבור
                {
                    if ((((long)Analizing.first & (long)finalTag[i - 1].InformTags[0].Ana) > 0) && (((long)Analizing.first & (long)finalTag[i].InformTags[0].Ana) > 0) || (((long)Analizing.third & (long)finalTag[i - 1].InformTags[0].Ana) > 0) && (((long)Analizing.third & (long)finalTag[i].InformTags[0].Ana) > 0) || (((long)Analizing.second & (long)finalTag[i - 1].InformTags[0].Ana) > 0) && (((long)Analizing.second & (long)finalTag[i].InformTags[0].Ana) > 0))//אם יש זהות בין 2 המילים שביניהן וו החבור (גוף ראשון זהה שני או שלישי
                    {
                        if (((((long)Analizing.paul & (long)finalTag[i - 1].InformTags[0].Ana) > 0) && ((long)Analizing.paul & (long)finalTag[i].InformTags[0].Ana) > 0) || ((((long)Analizing.doo & (long)finalTag[i - 1].InformTags[0].Ana) > 0) && ((long)Analizing.doo & (long)finalTag[i].InformTags[0].Ana) > 0) || ((((long)Analizing.past & (long)finalTag[i - 1].InformTags[0].Ana) > 0) && ((long)Analizing.past & (long)finalTag[i].InformTags[0].Ana) > 0) || ((((long)Analizing.present & (long)finalTag[i - 1].InformTags[0].Ana) > 0) && ((long)Analizing.present & (long)finalTag[i].InformTags[0].Ana) > 0) || ((((long)Analizing.future & (long)finalTag[i - 1].InformTags[0].Ana) > 0) && ((long)Analizing.future & (long)finalTag[i].InformTags[0].Ana) > 0) || ((((long)Analizing.makor & (long)finalTag[i - 1].InformTags[0].Ana) > 0) && ((long)Analizing.makor & (long)finalTag[i].InformTags[0].Ana) > 0) || ((((long)Analizing.adverb & (long)finalTag[i - 1].InformTags[0].Ana) > 0) && ((long)Analizing.adverb & (long)finalTag[i].InformTags[0].Ana) > 0))//אם יש זהות בין המילים בפועל, תואר הפועל, או בזמן
                        {
                            long tag = ((long)Analizing.vav);
                            tag = ~tag;//יש לבטל את הנתוח של האות וו הרגילה
                            finalTag[i].InformTags[0].Ana = (Analizing)((long)finalTag[i].InformTags[0].Ana & tag);
                            finalTag[i].InformTags[0].Ana = (Analizing)((long)Analizing.vavCon | (long)finalTag[i].InformTags[0].Ana);//ולהפוך אותו לניתוח של וו החבור שאינה מפרידה בין המשפטים
                        }
                    }
                    else
                    {
                        if ((((long)Analizing.name & (long)finalTag[i - 1].InformTags[0].Ana) > 0) && (((long)Analizing.name & (long)finalTag[i].InformTags[0].Ana) > 0) || (((long)Analizing.noun & (long)finalTag[i - 1].InformTags[0].Ana) > 0) && (((long)Analizing.noun & (long)finalTag[i].InformTags[0].Ana) > 0) || (((long)Analizing.pnoun & (long)finalTag[i - 1].InformTags[0].Ana) > 0) && (((long)Analizing.pnoun & (long)finalTag[i].InformTags[0].Ana) > 0) || (((long)Analizing.adverb & (long)finalTag[i - 1].InformTags[0].Ana) > 0) && (((long)Analizing.adverb & (long)finalTag[i].InformTags[0].Ana) > 0))//אם יש זהות בתחום כמו: שם ,מקום, תואר הפועל
                        {
                            long tag = ((long)Analizing.vav);
                            tag = ~tag;//יש לבטל את הנתוח של האות וו הרגילה
                            finalTag[i].InformTags[0].Ana = (Analizing)((long)finalTag[i].InformTags[0].Ana & tag);
                            finalTag[i].InformTags[0].Ana = (Analizing)((long)Analizing.vavCon | (long)finalTag[i].InformTags[0].Ana);//ולהפוך אותו לניתוח של וו החבור שאינה מפרידה בין המשפטים
                        }

                    }
                }
            }

            return finalTag;
        }
        /// <summary>
        /// מחיקת רווחים כפולים והותרת רווח בין מילה למילה לאותיות עבריות בלבד
        /// </summary>
        /// <param name="song">השיר</param>
        /// <returns>השיר המסונן</returns>
        public static string cleanSong(string song)
        {
            string hebrewAlphabet = "אבגדהוזחטיכךלמנסעפצקרשתםןףץ";


            string output = "";

            foreach (char letter in song)
            {
                if (hebrewAlphabet.IndexOf(letter) > 0)
                {
                    output += letter;
                }
                if (letter == 'א' || letter == ' ')
                    output += letter;


            }

            string sentence = song;
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            song = regex.Replace(sentence, " ");

            song = Regex.Replace(song, @"\s+", " ");
            while (song[song.Length - 1] == ' ')
            {
                song = song.Substring(0, song.Length - 1);
            }
            while (song[0] == ' ')
            {
                song = song.Substring(1, song.Length - 1);
            }
            return song;
        }
        /// <summary>
        ///  פונקציה המקבלת רשימת תגיות (מילה+ניתוח) ומחזירה רשימת תגיות סופית-תגית מלאה+רשימת תגיות לכל אחת ממילות התגית
        /// </summary>
        /// <param name="tagits">רשימת תגיות</param>
        /// <returns></returns>
        public static List<FinalTag> getWords(List<List<Tagit>> tagits)
        {
            List<FinalTag> finalTags = new List<FinalTag>();
            int i = 0;
            foreach (var item in tagits)
            {

                finalTags.Add(new FinalTag(tagits[i][0].Word, tagits[i])); i++;
            }
            return finalTags;
        }

        /// <summary>
        /// התנאי if(word1=f1/f2)&(word2!=f1&f2) -- f is l
        /// </summary>
        /// <param name="f1"></param>
        /// <param name="f2"></param>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <param name="word1"></param>
        /// <param name="word2"></param>
        /// <returns></returns>
        public static bool isPizmonRight(string f1, string f2, string l1, string l2, string word1, string word2)
        {
            if ((word1 == f1 || word1 == f2) && word2 != f1 && word2 != f2)//בדיקת התנאי המבוקש, 2 אפשרויות:
                //או שמילה1 יכולה ליהות פותחת ומילה2 לא חיבת להיות פותחת
                //או שמילה1 יכולה להיות סוגרת ומילה2 לא חיבת להיות סוגרת
                return true;
            return false;

        }



        /// <summary>
        ///  מילה כפולה רצופה + בדיקה האם התגית אמורה להתלכד משני צדדיה. ללא התיחסות לפתיח או לסיום
        /// </summary>
        /// <param name="tagits">תגית</param>
        /// <param name="index">מיקום בתגית</param>
        /// <param name="minus">מספר המילים שהתלכדו</param>
        /// <returns></returns>
        public static List<FinalTag> firstJoiner(List<FinalTag> tagits, int index, out int minus, string firstWord1, string fistWord2, string lastWord1, string lastWord2)
        {
            //בדיקת רציפות ראשונה ובסיסית, לא כוללנית. שימי לב בעקר לליכוד מילים כפולות ורב מילות הקישור אך לא כולן כי הן כבר בתוככי התגית
            int minus_ = 0;//משתנה אאוט מאותחל רק פעם אחת, לכן מספר השנויים נשמר ורק לבסוף מאותחל האווט שיעודכן לפיו
                           //
            if (index < 0 || index + 1 > tagits.Count)//אם התקבלו ערכים לא תקינים, אינדקס לא קיים
            {
                minus = 0;
                return tagits;
            }
            List<Tagit> prev = null, current = null, next = null;
            if (index == tagits.Count - 1)
                index = index;
            if (index != 0 && index + 1 != tagits.Count)//אם התגית לא ראשונה/אחרונה 
            {
                prev = tagits[index - 1].InformTags;//תגית קודמת 
                next = tagits[index + 1].InformTags;//תגית הבאה
                current = tagits[index].InformTags;//תגית נוכחית




                if (next[0].Word == current[current.Count - 1].Word || getListStart(current[current.Count - 1].Word) == true)//אם המילה שפותחת את התגית הבאה שווה למילה האחרונה  או שהמילה שמסיימת את התגית היא פותחת  בתגית זו
                {
                    tagits = joinTagBackward(tagits, index + 1);//זמון פונקציה לצירוף התגית הבאה לתגית זו

                    minus_++;//בוצע שנוי אחד כרגע
                    if (prev[prev.Count - 1].Word == current[0].Word)//אם המילה שסוגרת את התגית הקודמת זהה למילה הראשונה בתגית זו
                    {
                        tagits = joinTagForward(tagits, index - 1);//זמון פונקציה לצירוף התגית הקודמת לתגית זו


                        minus_++;//בוצע שנוי נוסף
                    }
                }
                else//המילה שפותחת את התגית הבאה לא זהה למילה האחרונה בתגית זו
                {
                    if (prev[prev.Count - 1].Word == current[0].Word)//אם המילה שסוגרת את התגית הקודמת זהה למילה הראשונה בתגית זו
                    {
                        tagits = joinTagBackward(tagits, index);//זמון פונקציה לצירוף התגית הקודמת לתגית זו
                        minus_++;//בוצע שנוי אחד כרגע
                    }
                }


                long result2 = (long)Analizing.nismach;//ניסמך

                long result = (long)Analizing.vavCon | (long)Analizing.mem | (long)Analizing.shin | (long)Analizing.he | (long)Analizing.lamed | (long)Analizing.bet | (long)Analizing.kaf;//הביטים של אותיות משה לב  או וו החבור דלוקים
                result = result & (long)current[0].Ana;//חבור לוגי, האם המילה שפותחת את התגית מכילה אותיות משה לב
                if (getListMiddle(current[0].Word) == true || result > 0 || getListMiddle(prev[prev.Count - 1].Word) || ((long)prev[prev.Count - 1].Ana & result2) > 0)//אם המילה הראשונה שבתגית מכילה אותיות משה כלב או שהיא אחת מהמילים הקימות באמצע תגית, או שהמילה שלפניה היא נסמך/חייבת להיות באמצע תגית
                {

                    result = (long)Analizing.vavCon | (long)Analizing.mem | (long)Analizing.shin | (long)Analizing.he | (long)Analizing.lamed | (long)Analizing.bet | (long)Analizing.kaf;//הביטים של אותיות משה לב  או וו החבור דלוקים
                    result = result & (long)next[0].Ana;//חבור לוגי, האם המילה שפותחת את התגית מכילה אותיות משה לב
                    if (getListMiddle(current[current.Count - 1].Word) == true || getListMiddle(next[0].Word) || ((long)current[current.Count - 1].Ana & result2) > 0 || result > 0)//אם גם המילה האחרונה שבתגית מכילה מילה שחיבת להיות בסוף תגית או מילה נסמכת
                    {
                        tagits = joinTagAround(tagits, index);//צרף את התגית מסביבה 
                        minus_ += 2;//התחוללו שני שנויים
                    }
                    else
                    {
                        tagits = joinTagBackward(tagits, index);//צרף את התגית לתגית הקודמת 
                        minus_++;//התחולל שנוי אחד
                    }


                }


            }
            else//האינדקס ראשון או אחרון
            {
                if (index == 0)//אם התגית ראשונה
                {
                    next = tagits[index + 1].InformTags;//תגית הבאה
                    current = tagits[index].InformTags;//תגית נוכחית




                    if (next[0].Word == current[current.Count - 1].Word || getListStart(current[current.Count - 1].Word) == true)//אם המילה שפותחת את התגית הבאה שווה למילה האחרונה  או שהמילה שמסיימת את התגית היא פותחת  בתגית זו
                    {
                        tagits = joinTagBackward(tagits, index + 1);//זמון פונקציה לצירוף התגית הבאה לתגית זו

                        minus_++;//בוצע שנוי אחד כרגע

                    }



                    long result2 = (long)Analizing.nismach;//ניסמך

                    long result = (long)Analizing.vavCon | (long)Analizing.mem | (long)Analizing.shin | (long)Analizing.he | (long)Analizing.lamed | (long)Analizing.bet | (long)Analizing.kaf;//הביטים של אותיות משה לב  או וו החבור דלוקים
                    result = result & (long)current[0].Ana;//חבור לוגי, האם המילה שפותחת את התגית מכילה אותיות משה לב
                    if (getListMiddle(current[0].Word) == true || result > 0)//אם המילה הראשונה שבתגית מכילה אותיות משה כלב או שהיא אחת מהמילים הקימות באמצע תגית, או שהמילה שלפניה היא נסמך/חייבת להיות באמצע תגית
                    {

                        result = (long)Analizing.vavCon | (long)Analizing.mem | (long)Analizing.shin | (long)Analizing.he | (long)Analizing.lamed | (long)Analizing.bet | (long)Analizing.kaf;//הביטים של אותיות משה לב  או וו החבור דלוקים
                        result = result & (long)next[0].Ana;//חבור לוגי, האם המילה שפותחת את התגית מכילה אותיות משה לב
                        if (getListMiddle(current[current.Count - 1].Word) == true || getListMiddle(next[0].Word) || ((long)current[current.Count - 1].Ana & result2) > 0 || result > 0)//אם גם המילה האחרונה שבתגית מכילה מילה שחיבת להיות בסוף תגית או מילה נסמכת
                        {
                            tagits = joinTagForward(tagits, index);//צרף את התגית קדימה 
                            minus_ += 1;//התחוללו שני שנויים
                        }

                    }
                }

                if (index == tagits.Count - 1)//אם התגית אחרונה
                {
                    prev = tagits[index - 1].InformTags;//תגית קודמת
                    current = tagits[index].InformTags;//תגית נוכחית




                    if (getListStart(current[current.Count - 1].Word) == true)//אם המילה שפותחת את התגית הבאה שווה למילה האחרונה  או שהמילה שמסיימת את התגית היא פותחת  בתגית זו
                    {
                        tagits = joinTagBackward(tagits, index + 1);//זמון פונקציה לצירוף התגית הבאה לתגית זו

                        minus_++;//בוצע שנוי אחד כרגע
                        if (prev[prev.Count - 1].Word == current[0].Word)//אם המילה שסוגרת את התגית הקודמת זהה למילה הראשונה בתגית זו
                        {
                            tagits = joinTagForward(tagits, index - 1);//זמון פונקציה לצירוף התגית הקודמת לתגית זו


                            minus_++;//בוצע שנוי נוסף
                        }
                    }
                    else//המילה שפותחת את התגית הבאה לא זהה למילה האחרונה בתגית זו
                    {
                        if (prev[prev.Count - 1].Word == current[0].Word)//אם המילה שסוגרת את התגית הקודמת זהה למילה הראשונה בתגית זו
                        {
                            tagits = joinTagBackward(tagits, index);//זמון פונקציה לצירוף התגית הקודמת לתגית זו
                            minus_++;//בוצע שנוי אחד כרגע
                        }
                    }


                    long result2 = (long)Analizing.nismach;//ניסמך
                                                           //


                    long result = (long)Analizing.vavCon | (long)Analizing.mem | (long)Analizing.shin | (long)Analizing.he | (long)Analizing.lamed | (long)Analizing.bet | (long)Analizing.kaf;//הביטים של אותיות משה לב  או וו החבור דלוקים
                    result = result & (long)current[current.Count - 1].Ana;//חבור לוגי, האם המילה שפותחת את התגית האחרונה מכילה אותיות משה לב
                    if (getListMiddle(current[current.Count - 1].Word) == true || result > 0 || getListMiddle(prev[prev.Count - 1].Word) || ((long)prev[prev.Count - 1].Ana & result2) > 0)//אם המילה הראשונה שבתגית מכילה אותיות משה כלב או שהיא אחת מהמילים הקימות באמצע תגית, או שהמילה שלפניה היא נסמך/חייבת להיות באמצע תגית
                    {


                        {
                            tagits = joinTagBackward(tagits, index);//צרף את התגית לתגית הקודמת 
                            minus_++;//התחולל שנוי אחד
                        }

                    }
                }
            }
            minus = minus_;
            return tagits;
        }
        /// <summary>
        /// טיפול במילים בודדות
        /// </summary>
        /// <param name="t"></param>
        /// <param name="index"></param>
        /// <param name="minus"></param>
        /// <param name="firstWord1"></param>
        /// <param name="fistWord2"></param>
        /// <param name="lastWord1"></param>
        /// <param name="lastWord2"></param>
        /// <returns></returns>
        public static List<FinalTag> secondJoinerNotAlone(List<FinalTag> t, int index, out int minus, string firstWord1, string firstWord2, string lastWord1, string lastWord2)
        {
            //בדיקת רציפות ראשונה ובסיסית, לא כוללנית. שימי לב בעקר לליכוד מילים כפולות ורב מילות הקישור אך לא כולן כי הן כבר בתוככי התגית
            int minus_ = 0;//משתנה אאוט מאותחל רק פעם אחת, לכן מספר השנויים נשמר ורק לבסוף מאותחל האווט שיעודכן לפיו
                           //
            if (index < 0 || index + 1 > t.Count)//אם התקבלו ערכים לא תקינים, אינדקס לא קיים
            {
                minus = 0;
                return t;
            }
            if (index == 15)
                index = index;
            List<Tagit> prev = null, current = null, next = null;
            bool flagCanOpen = false;
            bool flagCanShut = false;
            if (index != 0 && index != t.Count - 1)//אם התגית לא ראשונה/אחרונה 
            {

                prev = t[index - 1].InformTags;//תגית קודמת 
                next = t[index + 1].InformTags;//תגית הבאה
                current = t[index].InformTags;//תגית נוכחית

                if (current.Count != 1)//המילה אינה בודדה
                {
                    minus = 0;
                    return t;
                }
                //המילה אכן בודדה
                {
                    if (next.Count > 1)//בודדה ואחריה רצף
                    {
                        flagCanOpen = isPizmonRight(firstWord1, firstWord2, lastWord1, lastWord2, current[0].Word, next[0].Word);//בדיקה נוספת לתנאי ששורה מתחת:
                        if (flagCanOpen && getListMayStart(current[0].Word) && (mustStart(next[0].Word, next[0].Ana) == false))//המילה הנוכחית יכולה לפתוח והבאה לא חיבת לפתוח
                        {
                            joinTagBackward(t, index);//התלכדות עם התגית הבאה
                            minus = 1;
                            return t;
                        }
                        else//המילה הנוכחית לא חיבת לפתח או שהבאה חיבת לפתח
                        {
                            flagCanShut = isPizmonRight(firstWord1, firstWord2, lastWord1, lastWord2, current[0].Word, prev[prev.Count - 1].Word);//בדיקה נוספת לתנאי ששורה מתחת:
                            if (getListMayEnd(current[0].Word) && getListEnd(prev[prev.Count - 1].Word) == false && flagCanShut)//אם יכולה לסגר והמילה שלפניה בתגית הקודמת לא חיבת לסגר
                            {
                                joinTagForward(t, index);
                                minus = 0;
                                return t;
                                //התלכדות עם התגית הקודמת
                            }

                            else//ברירת מחדל, אם המילה לא מקבוצת פותחות, סוגרות, בהתאם לבדיקות. - התלכדות לתגית הארוכה יותר
                            {
                                if (prev.Count >= next.Count)
                                {
                                    joinTagBackward(t, index);
                                    minus = 1;
                                    return t;
                                    //התלכדות עם התגית הקודמת
                                }
                                else
                                {
                                    joinTagForward(t, index);
                                    minus = 0;
                                    return t;
                                    //התלכדות עם התגית הבאה
                                }

                            }
                        }
                    }

                    else//יש אחריה בודדות
                    {
                        int i = 0;
                        int count = 1;
                        for (i = index + 1; i < t.Count - 2 && (t[i].InformTags.Count < 2); i++)//ספירת מילים בודדות
                        {
                            count++;
                            //להסתכל אם נכנס לתנאי הפור...
                            if (mustStart(t[i].InformTags[0].Word, t[i].InformTags[0].Ana) || t[i].InformTags[0].Word == firstWord1 || t[i].InformTags[0].Word == firstWord2)
                            //אם המילה חיבת לפתח (פותחת פזמונים או מהרשימה). הלכתי לישון, סמיכות ורציפות כבר התלכד

                            {
                                // אם מדובר ב1 התלכדות לאחרונה
                                if (count == 2)
                                {
                                    t = joinTagBackward(t, index);
                                    minus = 1;
                                    return t;
                                }

                                else//סגירת כל מי שהיו עד עכשיו לתגית,
                                {
                                    for (int j = index; j < count - 1; j++)
                                    {
                                        t = joinTagForward(t, index);
                                    }
                                    minus = 0;
                                    return t;
                                }


                            }

                            // אם מדובר ב1 התלכדות לתגית הקצרה יותר
                            if (count == 2)
                            {
                                if (next.Count >= prev.Count)
                                {
                                    t = joinTagBackward(t, index);
                                    minus = 1;
                                    return t;
                                }
                                else
                                {
                                    t = joinTagForward(t, index);
                                    minus = 0;
                                    return t;
                                }

                            }



                            if (getListEnd(t[i].InformTags[0].Word) || t[i].InformTags[0].Word == lastWord1 || t[i].InformTags[0].Word == lastWord2)//יש מילה שחיבת לסגר, התלכדות כולל היא
                            {

                                // התלכדות כולל היא לתגית


                                for (int j = index; j < count; j++)
                                {
                                    t = joinTagForward(t, index);
                                }
                                minus = 0;
                                return t;
                            }




                        }
                        t = joinTagBackward(t, index);//אם יש מילה אחת אחריו, התלכדות. הרי לא נכנס ללולאה בכלל
                        minus = 1;
                        return t;
                    }

                }
            }
            else
            {
                if (index == 0)//כאשר המילה הבודדה מופיעה בתגית הראשונה
                {
                    t = joinTagForward(t, index);//מילה ראשונה לא בודדה לעולם

                }
                if (index == t.Count - 1)//כשהמילה הבודדה הנה התגית המסימת את השיר
                {
                    t = joinTagBackward(t, index);//מילה אחרונה לא בודדה לעולם
                }
            }
            minus = 0;
            return t;
        }


        /// <summary>
        /// טיפול בתגיות ארוכות מדי
        /// </summary>
        /// <param name="t"></param>
        /// <param name="index"></param>
        /// <param name="minus"></param>
        /// <param name="firstWord1"></param>
        /// <param name="fistWord2"></param>
        /// <param name="lastWord1"></param>
        /// <param name="lastWord2"></param>
        /// <returns></returns>
        public static List<FinalTag> thirdSplit8Letters(List<FinalTag> t, int index, out int minus, string firstWord1, string firstWord2, string lastWord1, string lastWord2)
        {
            bool a=false, b=false, d=false, e=false, f=false, g=false, h=false, j=false, k=false, m=false, n=false, o=false, p=false;
            int c = 0;
            //בדיקת רציפות ראשונה ובסיסית, לא כוללנית. שימי לב בעקר לליכוד מילים כפולות ורב מילות הקישור אך לא כולן כי הן כבר בתוככי התגית
            int minus_ = 0;//משתנה אאוט מאותחל רק פעם אחת, לכן מספר השנויים נשמר ורק לבסוף מאותחל האווט שיעודכן לפיו
                    //
            if (index < 0 || index + 1 > t.Count)//אם התקבלו ערכים לא תקינים, אינדקס לא קיים
            {
                minus = 0;
                return t;
            }
            if (index == 7)
                index = index;
            List<Tagit> prev = null, current = null, next = null;
            bool flagCanOpen = false;
            bool flagCanShut = false;
            //   if (index != 0 && index != t.Count - 1)//אם התגית לא ראשונה/אחרונה 
            {

                // prev = t[index - 1].InformTags;//תגית קודמת 
                //  next = t[index + 1].InformTags;//תגית הבאה
                current = t[index].InformTags;//תגית נוכחית

                if (current.Count < 8)//התגית עד 8 מילים
                {
                    minus = 0;
                    return t;
                }
                else//יש מעל 8 מילים בתגית
                {
                    if (index != 0)//אם יש תגית קודמת
                        
                    {
                        prev = t[index - 1].InformTags;//תגית קודמת 
                    }
                    //Console.WriteLine("************************************************************************************");    
                    int numWords = current.Count;
                    int count = 0;

                    int i = 0;
                    int l = current.Count;
                    //אם הגעתי לשלב זה, ולא גמרתי לסרק: סימן שיש לי תגית בת למעלה מ8 תגים שלא הופרדה
                    if ((l == 9) || (l == 10) || (l == 11) || (l == 12) || (l == 13) || (l == 14) || (l == 15) || (l == 16))
                    {
                        if (l == 15)
                            l = l;
                        if (l % 2 == 0)
                        {
                           c = l / 2;
                            
                            
                        }
                        else
                        {
                             c = (l - 1) / 2;
                           
                            
                        }
                        
                        for ( i = c; i>0; i--)//נסיון לפצל את התגית מהחתך עד המילה השניה בתגית בסדר יורד
                        {
                            if (((i != current.Count - 1 && current[i].Word != current[i + 1].Word) || (i == current.Count - 1)) && ((long)current[i].Ana & (long)Analizing.nismach) == 0 && ((long)current[i].Ana & (long)Analizing.doo) == 0 && current[i].Word != firstWord1 && current[i].Word != firstWord2 && getListStart(current[i].Word) != true && getListMiddleMust(current[i].Word) != true && ((long)current[i].Ana & (long)Analizing.vav) == 0 &&
                                current[i + 1].Word != lastWord1 && current[i + 1].Word != lastWord2 && getListEnd(current[i + 1].Word) != true && getListMiddleMust(current[i + 1].Word) != true && ((long)current[i].Ana & (long)Analizing.mem) == 0 && ((long)current[i + 1].Ana & (long)Analizing.shin) == 0 && ((long)current[i + 1].Ana & (long)Analizing.he) == 0 && ((long)current[i + 1].Ana & (long)Analizing.kaf) == 0 && ((long)current[i + 1].Ana & (long)Analizing.lamed) == 0 && ((long)current[i + 1].Ana & (long)Analizing.vavCon) == 0)// אם המילה לא פותחת פזמון/חיבת אמצע/כפולה פעמיים/צווי/נסמך ושהמילה הבאה אינה חיבת להיות באמצע, משהוכלב, לא חיבת לסגר ולא בפזמונים סוגרים
                            {
                                t = splitTagStart(t, index, i);//פיצול התגית
                                minus = 0;
                               // Console.WriteLine("-----------------------------------------------------------------------------------");
                                return t;
                            }
                        }
                        //הבדיקה הבאה בודקת אם אפשר להצטרף לתגית הקודמת
                        if (index!=0&&(prev.Count<=8)&&((i != current.Count - 1 && current[i].Word != current[i + 1].Word) || (i == current.Count - 1)) && ((long)current[i].Ana & (long)Analizing.nismach) == 0 && ((long)current[i].Ana & (long)Analizing.doo) == 0 && current[i].Word != firstWord1 && current[i].Word != firstWord2 && getListStart(current[i].Word) != true && getListMiddleMust(current[i].Word) != true && ((long)current[i].Ana & (long)Analizing.vav) == 0 &&
                                current[i + 1].Word != lastWord1 && current[i + 1].Word != lastWord2 && getListEnd(current[i + 1].Word) != true && getListMiddleMust(current[i + 1].Word) != true && ((long)current[i].Ana & (long)Analizing.mem) == 0 && ((long)current[i + 1].Ana & (long)Analizing.shin) == 0 && ((long)current[i + 1].Ana & (long)Analizing.he) == 0 && ((long)current[i + 1].Ana & (long)Analizing.kaf) == 0 && ((long)current[i + 1].Ana & (long)Analizing.lamed) == 0 && ((long)current[i + 1].Ana & (long)Analizing.vavCon) == 0)// אם המילה לא פותחת פזמון/חיבת אמצע/כפולה פעמיים/צווי/נסמך ושהמילה הבאה אינה חיבת להיות באמצע, משהוכלב, לא חיבת לסגר ולא בפזמונים סוגרים
                        {
                            t = splitTagStart(t, index, i + 1);
                            t = joinTagBackward(t, index);
                            Console.WriteLine("vvvvvvvvvvvvvv");
                            minus = 1;
                            return t;
                        }


                            for ( i = c; i < c+2; i++)//לולא שמנסה לפצל את התגית ב2 המקומות שאחרי נקודת הפיצול, הנחה ל10 תווים
                        {
                            if (current[i].Word != current[i - 1].Word && ((long)current[i - 1].Ana & (long)Analizing.nismach) == 0)
                                a = true;
                            if (((long)current[i - 1].Ana & (long)Analizing.doo) == 0)
                                b = true;
                            if (current[i - 1].Word != firstWord1 && current[i - 1].Word != firstWord2)
                                p = true;
                            if (getListStart(current[i - 1].Word) != true)
                                d = true;
                            if (getListMiddleMust(current[i - 1].Word) != true)
                                e = true;
                            if (current[i].Word != lastWord1)
                                f = true;
                            if (current[i].Word != lastWord2 && getListEnd(current[i].Word) != true)
                                g = true;
                            if (getListMiddleMust(current[i].Word) != true)
                                h = true;
                            if (((long)current[i].Ana & (long)Analizing.mem) == 0 && ((long)current[i].Ana & (long)Analizing.shin) == 0 && ((long)current[i].Ana & (long)Analizing.he) == 0 && ((long)current[i].Ana & (long)Analizing.kaf) == 0)
                                o = true;
                            if (((long)current[i].Ana & (long)Analizing.lamed) == 0 && ((long)current[i].Ana & (long)Analizing.vavCon) == 0 && ((long)current[i - 1].Ana & (long)Analizing.vav) == 0)
                                j = true;

                            if (a && b && b && p && d && e && f && g && h && j && h && j && k && m && n && o)//אותה תקינות בדיוק כמו בלולאה הקודמת, הפעם עליו ועל הבא אחריו
                            {
                                t = splitTagStart(t, index, i-1);//הפיצול
                               minus = 0;// Console.WriteLine("-----------------------------------------------------------------------------------");
                                return t;
                            }
                        }
                        t = splitTagStart(t, index, c);//אין ברירה-חותכים בבשר החי, למרות שלכאורה אסור לחתך
                        minus = 0;
                        // Console.WriteLine("=====================================================");
                        return t;
              
                    }

                    if ((l == 24) || (l == 23) || (l == 22) || (l == 21) || (l == 20) || (l == 19) || (l == 18) || (l == 17))//מספר המילים בתגית
                    {
                        if (l % 2 == 0)//זוגי
                        {
                            c = l / 3;
                            
                        }
                        else
                        {
                            c = (l - 1) / 3;//אי זוגי
                            
                        }
                        for ( i = c; i > 0; i--)//נסיון לפצל את התגית מהחתך עד המילה השניה בתגית בסדר יורד
                        {
                            if (((i != current.Count - 1 && current[i].Word != current[i + 1].Word) || (i == current.Count - 1)) && ((long)current[i].Ana & (long)Analizing.nismach) == 0 && ((long)current[i].Ana & (long)Analizing.doo) == 0 && current[i].Word != firstWord1 && current[i].Word != firstWord2 && getListStart(current[i].Word) != true && getListMiddleMust(current[i].Word) != true && ((long)current[i].Ana & (long)Analizing.vav) == 0 &&
                                current[i + 1].Word != lastWord1 && current[i + 1].Word != lastWord2 && getListEnd(current[i + 1].Word) != true && getListMiddleMust(current[i + 1].Word) != true && ((long)current[i].Ana & (long)Analizing.mem) == 0 && ((long)current[i + 1].Ana & (long)Analizing.shin) == 0 && ((long)current[i + 1].Ana & (long)Analizing.he) == 0 && ((long)current[i + 1].Ana & (long)Analizing.kaf) == 0 && ((long)current[i + 1].Ana & (long)Analizing.lamed) == 0 && ((long)current[i + 1].Ana & (long)Analizing.vavCon) == 0)// אם המילה לא פותחת פזמון/חיבת אמצע/כפולה פעמיים/צווי/נסמך ושהמילה הבאה אינה חיבת להיות באמצע, משהוכלב, לא חיבת לסגר ולא בפזמונים סוגרים
                            {
                                t = splitTagStart(t, index, i);//פיצול התגית
                                minus = 0;
                                //Console.WriteLine("-----------------------------------------------------------------------------------");
                                return t;
                            }
                        }

                        if (index != 0 && (prev.Count <= 8) && ((i != current.Count - 1 && current[i].Word != current[i + 1].Word) || (i == current.Count - 1)) && ((long)current[i].Ana & (long)Analizing.nismach) == 0 && ((long)current[i].Ana & (long)Analizing.doo) == 0 && current[i].Word != firstWord1 && current[i].Word != firstWord2 && getListStart(current[i].Word) != true && getListMiddleMust(current[i].Word) != true && ((long)current[i].Ana & (long)Analizing.vav) == 0 &&
                                current[i + 1].Word != lastWord1 && current[i + 1].Word != lastWord2 && getListEnd(current[i + 1].Word) != true && getListMiddleMust(current[i + 1].Word) != true && ((long)current[i].Ana & (long)Analizing.mem) == 0 && ((long)current[i + 1].Ana & (long)Analizing.shin) == 0 && ((long)current[i + 1].Ana & (long)Analizing.he) == 0 && ((long)current[i + 1].Ana & (long)Analizing.kaf) == 0 && ((long)current[i + 1].Ana & (long)Analizing.lamed) == 0 && ((long)current[i + 1].Ana & (long)Analizing.vavCon) == 0)// אם המילה לא פותחת פזמון/חיבת אמצע/כפולה פעמיים/צווי/נסמך ושהמילה הבאה אינה חיבת להיות באמצע, משהוכלב, לא חיבת לסגר ולא בפזמונים סוגרים
                        {
                            t = splitTagStart(t, index, i + 1);
                            t = joinTagBackward(t, index);
                            Console.WriteLine("vvvvvvvvvvvvvv");
                            minus = 1;
                            return t;
                        }
                        for ( i = c; i < c + 2; i++)//לולא שמנסה לפצל את התגית ב2 המקומות שאחרי נקודת הפיצול, הנחה ל10 תווים
                        {
                            long cc =(long)current[i - 1].Ana;
                            long bb =(long)Analizing.nismach;
                            long dd = cc & bb;

                            if (current[i].Word != current[i - 1].Word && (dd ) == 0)
                                a = true;
                            if (((long)current[i - 1].Ana & (long)Analizing.doo) == 0)
                                b = true;
                            if (current[i - 1].Word != firstWord1 && current[i - 1].Word != firstWord2)
                                p = true;
                            if (getListStart(current[i - 1].Word) != true)
                                d = true;
                            if (getListMiddleMust(current[i - 1].Word) != true)
                                e = true;
                            if (current[i].Word != lastWord1)
                                f = true;
                            if (current[i].Word != lastWord2 && getListEnd(current[i].Word) != true)
                                g = true;
                            if (getListMiddleMust(current[i].Word) != true)
                                h = true;
                            if (((long)current[i].Ana & (long)Analizing.mem) == 0 && ((long)current[i].Ana & (long)Analizing.shin) == 0 && ((long)current[i].Ana & (long)Analizing.he) == 0 && ((long)current[i].Ana & (long)Analizing.kaf) == 0)
                                o = true;
                            if (((long)current[i].Ana & (long)Analizing.lamed) == 0 && ((long)current[i].Ana & (long)Analizing.vavCon) == 0 && ((long)current[i - 1].Ana & (long)Analizing.vav) == 0)
                                j = true;

                            if (a && b && b && p && d && e && f && g && h && j && h && j && k && m && n && o)//אותה תקינות בדיוק כמו בלולאה הקודמת, הפעם עליו ועל הבא אחריו
                            {
                                t = splitTagStart(t, index, i - 1);//הפיצול
                                minus = 0;
                                //Console.WriteLine("-----------------------------------------------------------------------------------");
                                return t;
                            }
                        }
                        t = splitTagStart(t, index, c);//אין ברירה-חותכים בבשר החי, למרות שלכאורה אסור לחתך
                        minus = 0;
                        //Console.WriteLine("=====================================================");
                        return t;
                    }

                    if (l>24)
                    {
                        if (l % 2 == 0)
                        {
                            c = l / 4;
                           
                        }
                        else
                        {
                            c = (l - 1) / 4;
                        }

                        for ( i = c; i > 0; i--)//נסיון לפצל את התגית מהחתך עד המילה השניה בתגית בסדר יורד
                        {
                            if (((i != current.Count - 1 && current[i].Word != current[i + 1].Word) || (i == current.Count - 1)) && ((long)current[i].Ana & (long)Analizing.nismach) == 0 && ((long)current[i].Ana & (long)Analizing.doo) == 0 && current[i].Word != firstWord1 && current[i].Word != firstWord2 && getListStart(current[i].Word) != true && getListMiddleMust(current[i].Word) != true && ((long)current[i].Ana & (long)Analizing.vav) == 0 &&
                                current[i + 1].Word != lastWord1 && current[i + 1].Word != lastWord2 && getListEnd(current[i + 1].Word) != true && getListMiddleMust(current[i + 1].Word )!= true && ((long)current[i].Ana & (long)Analizing.mem) == 0 && ((long)current[i + 1].Ana & (long)Analizing.shin) == 0 && ((long)current[i + 1].Ana & (long)Analizing.he) == 0 && ((long)current[i + 1].Ana & (long)Analizing.kaf) == 0 && ((long)current[i + 1].Ana & (long)Analizing.lamed) == 0 && ((long)current[i + 1].Ana & (long)Analizing.vavCon) == 0)// אם המילה לא פותחת פזמון/חיבת אמצע/כפולה פעמיים/צווי/נסמך ושהמילה הבאה אינה חיבת להיות באמצע, משהוכלב, לא חיבת לסגר ולא בפזמונים סוגרים
                            {
                                t = splitTagStart(t, index, i);//פיצול התגית
                                minus = 0;
                                //Console.WriteLine("-----------------------------------------------------------------------------------");
                                return t;
                            }
                        }
                        if (index != 0 && (prev.Count <= 8) && ((i != current.Count - 1 && current[i].Word != current[i + 1].Word) || (i == current.Count - 1)) && ((long)current[i].Ana & (long)Analizing.nismach) == 0 && ((long)current[i].Ana & (long)Analizing.doo) == 0 && current[i].Word != firstWord1 && current[i].Word != firstWord2 && getListStart(current[i].Word) != true && getListMiddleMust(current[i].Word) != true && ((long)current[i].Ana & (long)Analizing.vav) == 0 &&
                                current[i + 1].Word != lastWord1 && current[i + 1].Word != lastWord2 && getListEnd(current[i + 1].Word) != true && getListMiddleMust(current[i + 1].Word) != true && ((long)current[i].Ana & (long)Analizing.mem) == 0 && ((long)current[i + 1].Ana & (long)Analizing.shin) == 0 && ((long)current[i + 1].Ana & (long)Analizing.he) == 0 && ((long)current[i + 1].Ana & (long)Analizing.kaf) == 0 && ((long)current[i + 1].Ana & (long)Analizing.lamed) == 0 && ((long)current[i + 1].Ana & (long)Analizing.vavCon) == 0)// אם המילה לא פותחת פזמון/חיבת אמצע/כפולה פעמיים/צווי/נסמך ושהמילה הבאה אינה חיבת להיות באמצע, משהוכלב, לא חיבת לסגר ולא בפזמונים סוגרים
                        {
                            t = splitTagStart(t, index, i + 1);
                            t = joinTagBackward(t, index);
                            Console.WriteLine("vvvvvvvvvvvvvv");
                            minus = 1;
                            return t;
                        }

                        for ( i = c; i < c + 2; i++)//לולא שמנסה לפצל את התגית ב2 המקומות שאחרי נקודת הפיצול, הנחה ל10 תווים
                        {
                           
                            if (current[i].Word != current[i - 1].Word && ((long)current[i - 1].Ana & (long)Analizing.nismach) == 0)
                                a =true;
                            if (((long)current[i - 1].Ana & (long)Analizing.doo) == 0)
                                b = true;
                            if (current[i - 1].Word != firstWord1 && current[i - 1].Word != firstWord2)
                                p = true;
                            if (getListStart(current[i - 1].Word) != true)
                                d = true;
                            if (getListMiddleMust(current[i - 1].Word) != true)
                                e = true;
                            if ( current[i].Word != lastWord1)
                                f = true;
                            if ( current[i].Word != lastWord2 && getListEnd(current[i].Word) != true)
                                g = true;
                            if (getListMiddleMust(current[i].Word)!=true)
                                h = true;
                            if (((long)current[i].Ana & (long)Analizing.mem) == 0 && ((long)current[i].Ana & (long)Analizing.shin) == 0 && ((long)current[i].Ana & (long)Analizing.he) == 0 && ((long)current[i].Ana & (long)Analizing.kaf) == 0)
                                o = true;
                            if ( ((long)current[i].Ana & (long)Analizing.lamed) == 0 && ((long)current[i].Ana & (long)Analizing.vavCon) == 0 && ((long)current[i - 1].Ana & (long)Analizing.vav) == 0)
                                j = true;

                            if (  a&&b&& b && p && d && e && f && g && h && j && h&&j&&k&&m&&n&&o)//אותה תקינות בדיוק כמו בלולאה הקודמת, הפעם עליו ועל הבא אחריו
                            {
                                t = splitTagStart(t, index, i - 1);//הפיצול
                                minus = 0;
                                //Console.WriteLine("-----------------------------------------------------------------------------------");
                                return t;
                            }
                        }
                        t = splitTagStart(t, index, c);//אין ברירה-חותכים בבשר החי, למרות שלכאורה אסור לחתך
                        minus = 0;
                        //Console.WriteLine("=====================================================");
                        return t;
                    }

                    minus = 0;//FOR DELETE ERRORS
                    return t;
                }
            }
            //else
            //{
            //    if (index == 0)//תגית הראשונה
            //    {

            //        minus = 0;
            //        return t;
            //    }
            //    if (index == t.Count - 1)// הנה התגית המסימת את השיר
            //    {
            //        minus = 0;
            //        return t;
            //    }

            //    minus = 0;
            //    return t;


            //}
        }

        ////פונקציות לא שמישות
        ///// <summary>
        ///// אם המילה במיקום הזה חותמת, פיצול התגית+
        ///// </summary>
        ///// <param name="tagits">התגיות</param>
        ///// <param name="index">מיקום התגית</param>
        ///// <param name="j">מקום המילה בתגית</param>
        ///// <param name="minus">כמה התפצלויות היו</param>
        ///// <param name="song">השיר המקורי</param>
        ///// <returns></returns>
        //public static List<FinalTag> isJoinPrev(List<FinalTag> tagits, int index, int j, out int minus, string song)
        //{

        //    int minus_ = 0;

        //    Tagit prev = null, current = null, next = null;

        //    if (index < 0 || index >= tagits.Count || j >= tagits[index].InformTags.Count)//האם אינדקס וגי אינם תקינים
        //    {
        //        minus = 0;
        //        return tagits;
        //    }
        //    if (j == tagits[index].InformTags.Count() - 1)//אם המילה הזו בלאו הכי חותמת
        //    {
        //        minus = 0;
        //        return tagits;
        //    }
        //    if (j == 0)//האם זו המילה הראשונה בתגית
        //    {
        //        if (index != 0)//האם יש תגית קודמת
        //        {
        //            prev = tagits[index - 1].InformTags[((tagits[index - 1].InformTags.Count()) - 1)];
        //            current = tagits[index].InformTags[j];//[0]
        //            if (tagits[index].InformTags.Count() > 1)//האם יש אחרי מילה בתגית
        //                next = tagits[index].InformTags[j + 1];//[1]

        //            else
        //            {
        //                if (tagits.Count() > 1)//האם יש תגית נוספת
        //                    next = tagits[index + 1].InformTags[0];
        //            }



        //        }
        //        else//זו המילה הראשונה בתגית ואין לפניה אחרת, לכן אין אפשרות להניח מילה בודדה
        //        {
        //            minus = 0;
        //            return tagits;
        //        }


        //    }
        //    else
        //    {
        //        prev = tagits[index].InformTags[j - 1];
        //        current = tagits[index].InformTags[j];
        //        if (tagits[index].InformTags.Count() > 1)//האם יש אחרי מילה בתגית
        //            next = tagits[index].InformTags[j + 1];//

        //        else
        //        {
        //            if (tagits.Count() > 1)//האם יש תגית נוספת
        //                next = tagits[index + 1].InformTags[0];
        //        }

        //    }

        //    //המילה הראשונה והאחרונה בפזמון חייבת לפתוח או לסגר תווית
        //    string ditty = LongestWords(song);
        //    string firstWordDitty1 = song.Substring(0, song.IndexOf(' '));
        //    string lastWordDitty1 = song.Substring(song.LastIndexOf(' ') + 1, song.Length);
        //    //כנ"ל גם בתת הפזמון
        //    string ditty2 = LongestWordsLevelIndex(song, 1);
        //    string firstWordDitty2 = song.Substring(0, song.IndexOf(' '));
        //    string lastWordDitty2 = song.Substring(song.LastIndexOf(' ') + 1, song.Length);
        //    if (ditty2.Length < 22)
        //    {
        //        lastWordDitty2 = "";
        //    }
        //    long result = (long)Analizing.name | (long)Analizing.lamed | (long)Analizing.bet;
        //    result = (long)result & (long)current.Ana;
        //    long result2 = (long)Analizing.nismach | (long)Analizing.model;///////////אסור לו לסיים
        //    result2 = (long)result2 & (long)current.Ana;

        //    long verb = (long)Analizing.verb & (long)Analizing.adverb & (long)Analizing.doo;
        //    if ((verb & (long)current.Ana) > 0 && (verb & (long)next.Ana) > 0 && ((long)Analizing.lamed & (long)next.Ana) > 0)//הלכתי לישון, חשבתי לישון לך ללמוד . 2 פעלים עם ל' ביניהם
        //    {
        //        minus = 0;
        //        return tagits;
        //    }

        //    if ((getListEnd(current.Word) == true || current.Ana > 0) && current.Word != firstWordDitty1 && current.Word != firstWordDitty2 && result2 == 0)
        //    {
        //        tagits = splitTagEnd(tagits, index, j);

        //        minus_++;



        //    }
        //    minus = minus_;
        //    return tagits;
        //}
        ///// <summary>
        ///// אם המילה במיקום הזה פותחת, פיצול התגית+
        ///// </summary>
        ///// <param name="tagits">התגיות</param>
        ///// <param name="index">מיקום התגית</param>
        ///// <param name="j">מקום המילה בתגית</param>
        ///// <param name="minus">כמה התפצלויות היו</param>
        ///// <param name="song">השיר המקורי</param>
        ///// <returns></returns>
        //public static List<FinalTag> isJoinNext(List<FinalTag> tagits, int index, int j, out int minus, string song)
        //{

        //    int minus_ = 0;

        //    Tagit prev = null, current = null, next = null;

        //    if (index < 0 || index >= tagits.Count || j >= tagits[index].InformTags.Count)//האם אינדקס וגי אינם תקינים
        //    {
        //        minus = 0;
        //        return tagits;
        //    }
        //    if (j == 0)//אם המילה הזו בלאו הכי פותחת
        //    {
        //        minus = 0;
        //        return tagits;
        //    }
        //    if (j == tagits[index].InformTags.Count() - 1)//האם זו המילה האחרונה בתגית
        //    {
        //        if (index != tagits[index].InformTags.Count() - 1)//האם יש תגית הבאה
        //        {
        //            next = tagits[index + 1].InformTags[0];
        //            current = tagits[index].InformTags[j];
        //            if (tagits[index].InformTags.Count() > 1)//האם יש לפני מילה בתגית
        //                prev = tagits[index].InformTags[j - 1];

        //            else
        //            {
        //                if (tagits.Count() > 1)//האם יש תגית נוספת
        //                    prev = tagits[index - 1].InformTags[tagits[index - 1].InformTags.Count - 1];
        //            }



        //        }
        //        else//זו המילה האחרונה בתגית ואין אחריה אחרת, לכן אין אפשרות להניח מילה בודדה
        //        {
        //            minus = 0;
        //            return tagits;
        //        }


        //    }
        //    else//יש מילה נוספת בתגית
        //    {
        //        next = tagits[index].InformTags[j + 1];
        //        current = tagits[index].InformTags[j];


        //        if (tagits[index].InformTags.Count() > 1)//האם יש לפני מילה בתגית
        //            prev = tagits[index].InformTags[j - 1];
        //        else
        //        {
        //            if (index > 0)//האם יש תגית נוספת
        //                next = tagits[index - 1].InformTags[tagits[index - 1].InformTags.Count - 1];
        //        }

        //    }

        //    //המילה הראשונה והאחרונה בפזמון חייבת לפתוח או לסגר תווית
        //    string ditty = LongestWords(song);
        //    string firstWordDitty1 = song.Substring(0, song.IndexOf(' '));
        //    string lastWordDitty1 = song.Substring(song.LastIndexOf(' ') + 1, song.Length);
        //    //כנ"ל גם בתת הפזמון
        //    string ditty2 = LongestWordsLevelIndex(song, 1);
        //    string firstWordDitty2 = song.Substring(0, song.IndexOf(' '));
        //    string lastWordDitty2 = song.Substring(song.LastIndexOf(' ') + 1, song.Length);
        //    if (ditty2.Length < 22)
        //    {
        //        lastWordDitty2 = "";
        //    }
        //    long result = (long)Analizing.name | (long)Analizing.lamed | (long)Analizing.bet;
        //    result = (long)result & (long)current.Ana;
        //    long result2 = (long)Analizing.nismach | (long)Analizing.model;///////////אסור לו לסיים
        //    result2 = (long)result2 & (long)current.Ana;

        //    long verb = (long)Analizing.verb & (long)Analizing.adverb & (long)Analizing.doo;
        //    if ((verb & (long)current.Ana) > 0 && (verb & (long)prev.Ana) > 0 && ((long)Analizing.lamed & (long)current.Ana) > 0)//הלכתי לישון, חשבתי לישון לך ללמוד . 2 פעלים עם ל' ביניהם
        //    {
        //        minus = 0;
        //        return tagits;
        //    }

        //    if ((getListEnd(current.Word) == true || current.Ana > 0) && current.Word != lastWordDitty1 && current.Word != lastWordDitty2 && result2 == 0)
        //    {
        //        tagits = splitTagEnd(tagits, index, j);

        //        minus_++;



        //    }
        //    minus = minus_;
        //    return tagits;
        //}

        /// <summary>
        /// הפרדת 'והיא' מתגיות 'ו
        /// </summary>
        /// <param name="t"></param>
        /// <param name="index"></param>
        /// <param name="minus"></param>
        /// <param name="firstWord1"></param>
        /// <param name="fistWord2"></param>
        /// <param name="lastWord1"></param>
        /// <param name="lastWord2"></param>
        /// <returns></returns>
        public static List<FinalTag> thirdSplit(List<FinalTag> t, int index, out int minus, string firstWord1, string firstWord2, string lastWord1, string lastWord2)
        {
            //בדיקת רציפות ראשונה ובסיסית, לא כוללנית. שימי לב בעקר לליכוד מילים כפולות ורב מילות הקישור אך לא כולן כי הן כבר בתוככי התגית
            int minus_ = 0;//משתנה אאוט מאותחל רק פעם אחת, לכן מספר השנויים נשמר ורק לבסוף מאותחל האווט שיעודכן לפיו

            if (index < 0 || index + 1 > t.Count)//אם התקבלו ערכים לא תקינים, אינדקס לא קיים
            {
                minus = 0;
                return t;
            }

            List<Tagit> prev = null;

            List<Tagit> current = t[index].InformTags;//תגית נוכחית
            for (int i = 0; i < current.Count; i++)
            {
                if (getListSplit(current[i].Word) == true || ((long)Analizing.vav & (long)current[i].Ana) > 0)
                {
                    t = splitTagStart(t, index, i);

                    if (i == 1 && index != 0)//כאשר התגית התפצלה החל מאינדקס אחד ונותרה מילה בודדה
                    {
                        t = joinTagBackward(t, i);//יש לאחדה עם התגית הקודמת
                        minus = 1;//לא חובה
                    }
                    else
                        minus = 0;
                    return t;
                }
            }
            minus = 0;
            return t;
        }







        /// <summary>
        /// צירוף התגית לתגית הבאה ללא תקינויות
        /// </summary>
        /// <param name="t"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        ///
        public static List<FinalTag> joinTagForward(List<FinalTag> t, int index)
        {

            t[index].FullTag += " " + t[index + 1].FullTag;//שרשור התגית הבאה לתגית הזו
            foreach (Tagit i in t[index + 1].InformTags)//העתקת נתוחי כל מילות התגית הבאה לרשימת נתוחי התגית הזו
            {
                t[index].InformTags.Add(new Tagit(i.Ana, i.Word));
            }
            t.RemoveAt(index + 1);//הסרת התגית המיותרת

            return t;


        }
        /// <summary>
        /// איחוד התגית עם התגית שלפניה ושלאחריה ללא תקינויות
        /// </summary>
        /// <param name="t"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static List<FinalTag> joinTagAround(List<FinalTag> t, int index)
        {
            t[index].FullTag = t[index - 1].FullTag + " " + t[index].FullTag + " " + t[index + 1].FullTag;//השמת שרשור התגית הקודמת, הנוכחית והבאה לתגית הנוכחית
            List<Tagit> cloneList = t[index].InformTags.GetRange(0, t[index].InformTags.Count);//עותק של ניתוחי התגית הנוכחית
            t[index].InformTags = new List<Tagit>();//איפוס רשימת הניתוחים הנוכחית
            foreach (Tagit i in t[index - 1].InformTags)//הוספת נתוחי התגית הקודמת לרשימת הנתוחים הנוכחית הריקה
            {
                t[index].InformTags.Add(new Tagit(i.Ana, i.Word));
            }
            foreach (Tagit i in cloneList)//הוספת נתוחי התגית הנוכחית לרשימה 
            {
                t[index].InformTags.Add(new Tagit(i.Ana, i.Word));
            }
            foreach (Tagit i in t[index + 1].InformTags)//הוספת נתוחי התגית הבאה לרשימה
            {
                t[index].InformTags.Add(new Tagit(i.Ana, i.Word));
            }
            t.RemoveAt(index + 1);//הסרת התגית הבאה
            t.RemoveAt(index - 1);//הסרת התגית הקודמת
            return t;

        }
        /// <summary>
        /// איחוד התגית עם התגית שלפניה ללא תקינויות
        /// </summary>
        /// <param name="t"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static List<FinalTag> joinTagBackward(List<FinalTag> t, int index)

        {

            t[index].FullTag = t[index - 1].FullTag + " " + t[index].FullTag;//שרשור תגית נוכחית לתגית הקודמת
            List<Tagit> cloneList = t[index].InformTags.GetRange(0, t[index].InformTags.Count);//יצירת עותק מנתוחי התגית הנוכחית
            t[index].InformTags = new List<Tagit>();//איפוס רשימת הנתוחים של התגית הנוכחית
            foreach (Tagit i in t[index - 1].InformTags)//השמת רשימת נתוחי התגית הקודמת לרשימת התגיות הנוכחית הריקה
            {
                t[index].InformTags.Add(new Tagit(i.Ana, i.Word));
            }
            foreach (Tagit i in cloneList)//שרשור רשימת נתוחי תגית נוכחית לרשימת נתוחי התגית הקודמת
            {
                t[index].InformTags.Add(new Tagit(i.Ana, i.Word));
            }
            t.RemoveAt(index - 1);
            return t;
        }









        ///// <summary>
        ///// פיצול תגית במיקום מסוים עד חתימת ג'יי והחצי השני תגית חדשה
        ///// </summary>
        ///// <param name="t"></param>
        ///// <param name="index"></param>
        ///// <param name="j"></param>
        ///// <returns></returns>
        //public static List<FinalTag> splitTagEnd(List<FinalTag> t, int index, int j)
        //{
        //    int x = t[index].InformTags.Count;//מספר המילים בתגית
        //    if (j > 0 && j  < x-1)// אם זו לא מילה ראשונה/אחרונה בתגית  
        //    {

        //        int i;
        //        string newNext = t[index].FullTag;//התגית
        //        string newFirst = "", newSecond = "";
        //        String[] strgs = newNext.Split(" ");//מילות התגית הזו
        //        for (i = 0; i <= j; i++)//התגית החדשה כולל רווח לאחריה
        //        {
        //            newFirst += strgs[i] + " ";

        //        }
        //        newFirst = newFirst.Substring(0, newFirst.Length - 1);//הסרת הרווח האחרון המיותר
        //        for (; i < strgs.Length; i++)
        //        {
        //            newSecond += strgs[i] + " ";//התגית החדשה הבאה כולל רווח לאחריה
        //        }
        //        newSecond = newSecond.Substring(0, newSecond.Length - 1);//הסרת הרווח האחרון המיותר
        //        t[index].FullTag = newFirst;//עדכון התגית הקצוצה
        //        List<Tagit> tagits = t[index].InformTags.GetRange(j + 1, t[index].InformTags.Count - j - 1);//שרשור הניתוחים של התגיות שלה
        //        t.Insert(index + 1, new FinalTag(newSecond, tagits));//הוספת התגית הנוספת
        //        t[index].InformTags.RemoveRange(j + 1, t[index].InformTags.Count - j - 1);//הסרת הניתוחים של התגית הבאה מהתגית הנוכחית
        //        //כאן פיצלתי את התגית כך שהמילה במיקום הג'י תחתום את התגית הקודמת, והתגית החדשה היא החל מהמילה הבאה





        //    }
        //    else//מדובר במילה ראשונה/אחרונה בתגית
        //    {
        //        if (j == 0 && index != 0)//כאשר המילה ראשונה בתגית שאינה ראשונה
        //        {
        //            t = joinTagBackward(t, index);//אין מילה בודדה: זמן פונקציה לצרוף התגית לאחור 
        //        }

        //        else
        //        {

        //            if (index != t.Count - 1)//and the first next  not-must be at at beginning
        //            {

        //                int from = (t[index].FullTag.LastIndexOf(" "));
        //                from += 1;
        //                int long_ = t[index].FullTag.Count() - from;
        //                string word = t[index].FullTag.Substring(from, long_);
        //                t[index + 1].FullTag = word + t[index + 1].FullTag;
        //                from = t[index].FullTag.LastIndexOf(" ");
        //                from += 1;
        //                long_ = t[index].FullTag.Length - from;
        //                t[index].FullTag = t[index].FullTag.Substring(from, long_);


        //                List<Tagit> cloneList = t[index + 1].InformTags.GetRange(0, t[index + 1].InformTags.Count);
        //                t[index + 1].InformTags = new List<Tagit>();

        //                t[index + 1].InformTags.Add(new Tagit(t[index].InformTags[(t[index].InformTags.Count() - 1)].Ana, word));

        //                foreach (Tagit i in cloneList)
        //                {
        //                    t[index + 1].InformTags.Add(new Tagit(i.Ana, i.Word));
        //                }

        //                t[index + 1].InformTags.Add(new Tagit(t[index].InformTags[0].Ana, word));
        //                t[index].InformTags.RemoveAt(t[index].InformTags.Count() - 1);


        //            }
        //        }
        //    }
        //    return t;
        //}
        /// <summary>
        /// פיצול תגית במיקום אינדקס עד ג'יי ומג'יי תגית חדשה
        /// </summary>
        /// <param name="t"></param>
        /// <param name="index"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public static List<FinalTag> splitTagStart(List<FinalTag> t, int index, int j)
        {
            int x = t[index].InformTags.Count;//מספר המילים בתגית
            if (j > 0 && j < x - 1)//המילה אינה הראשונה/האחרונה בתגית
            {
                {
                    int i;
                    string newNext = t[index].FullTag;//טקסט כלל התגית
                    string newFirst = "", newSecond = "";
                    String[] strgs = newNext.Split(" ");//מילות התגית במערך
                    for (i = 0; i < j; i++)//שרשור התגית עד גיי
                    {
                        newFirst += strgs[i] + " ";

                    }
                    newFirst = newFirst.Substring(0, newFirst.Length - 1);//הסרת הרווח האחרון המיותר
                    for (; i < strgs.Length; i++)//שרשור התגית עד הסוף
                    {
                        newSecond += strgs[i] + " ";
                    }
                    newSecond = newSecond.Substring(0, newSecond.Length - 1);//הסרת הרווח המיותר האחרון
                    t[index].FullTag = newFirst;//השמת התגית העדכנית במיקום הנכון

                    List<Tagit> tagits = t[index].InformTags.GetRange(j, t[index].InformTags.Count - j);// שרשור הניתוח של שאר המילים-לתגית הבאה
                    t.Insert(index + 1, new FinalTag(newSecond, tagits));//השמת ערכי התגית הבאה וניתוחה
                    t[index].InformTags.RemoveRange(j, t[index].InformTags.Count - j);//השמת הניתוח ללא הניתוחים המיותרים שהועברו לתגית הבאה
                }

            }
           
            else//המילה ראשונה או אחרונה בתגית
            {
                if (j == x - 1 && index != t.Count - 1)//המילה היא אחרונה בתגית ויש תגית אחר כך
                {
                    t = joinTagForward(t, j);//הוספת המילה קדימה לתגית הבאה
                }

                if ((index == t.Count - 1) && (j == x))// אם אחרונה ללא תגית אחריה
                {
                    //אין ענין לפתח תגית על מילה אחת

                    //string word = t[index].FullTag.Substring(0, t[index].FullTag.IndexOf(" "));
                    //t[index - 1].FullTag += word;
                    //t[index].FullTag = t[index].FullTag.Substring(t[index].FullTag.IndexOf(" "), t[index].FullTag.Length);
                    //t[index - 1].InformTags.Add(new Tagit(t[index].InformTags[0].Ana, word));
                    //t[index].InformTags.RemoveAt(0);



                }
                //המילה ראשונה בתגית = תווצר תגית בודדה, אך אין לעשות זאת במקרה של תגית ראשונה
                if (j == 0&&index!=0)
                {
                    int i;
                    string newNext = t[index].FullTag;//טקסט כלל התגית
                    string newFirst = "", newSecond = "";
                    String[] strgs = newNext.Split(" ");//מילות התגית במערך
                   
                        newFirst += strgs[0];

                 
                    for ( i=1; i < strgs.Length; i++)//שרשור התגית עד הסוף
                    {
                        newSecond += strgs[i] + " ";
                    }
                    newSecond = newSecond.Substring(0, newSecond.Length - 1);//הסרת הרווח המיותר האחרון
                    t[index].FullTag = newFirst;//השמת התגית הבודדה במיקום הנכון

                    List<Tagit> tagits = t[index].InformTags.GetRange(j, t[index].InformTags.Count - j);// שרשור הניתוח של שאר המילים-לתגית הבאה
                    t.Insert(index + 1, new FinalTag(newSecond, tagits));//השמת ערכי התגית הבאה וניתוחה
                    t[index].InformTags.RemoveRange(j, t[index].InformTags.Count - j);//השמת הניתוח ללא הניתוחים המיותרים שהועברו לתגית הבאה
                }
            }
            return t;
        }


        public static void preMain()
        {

            //TODO fill the password
            HebrewNLP.HebrewNLP.Password = "OYnM3frqUbLTjFL";

            Console.OutputEncoding = new UTF8Encoding();
            Console.InputEncoding = new UTF8Encoding();

            //   Console.WriteLine(HebrewNLP.Morphology.AnalyzeRequest("שלום",))

            string a = "נר דולק קופסה נוצה אבא לחפש יצא מה ימצא חבילת מצה יין וביצה כד של חמיצה תפוח ואגוז מצאת דג לכבוד החג לא אבא לא ולא ואל נא תתאמץ כי לא תמצא אתנו פה בבית כל חמץ אינך מאמין חפש  האר היטב באור כל סדק בדוק כל חור עכשיו אמור האם היטבנו ובערנו כל שעור לא אבא לא ולא ואל נא תתאמץ כי לא תמצא אתנו פה בבית כל חמץ אינך מאמין חפש  האר היטב באור כל סדק בדוק כל חור עכשיו אמור האם היטבנו ובערנו כל שעור";

            string b = "אומרים ישנה ארץ ארץ שכורת שמש איה אותה ארץ איפה אותה שמש אומרים ישנה ארץ עמודיה שבעה שבעה כוכבי לכת צצים על כל גבעה איפה אותה ארץ כוכבי אותה גבעה מי ינחנו דרך יגיד לי הנתיבה כבר עברנו כמה מדברות וימים כבר הלכנו כמה כוחותינו תמים כיצד זה תעינו טרם הונח לנו אותה ארץ שמש אותה לא מצאנו ארץ בה יתקיים אשר כל איש קיווה נכנס כל הנכנס פגע בו עקיבא שלום לך עקיבא שלום לך רבי איפה הם הקדושים איפה המכבי עונה לו עקיבא אומר לו הרבי כל ישראל קדושים אתה המכבי אומרים ישנה ארץ ארץ שכורת שמש איה אותה ארץ איפה אותה שמש";
            string c = "איזה פלא איזה פלא מי הילדים האלה רחוצים וחפופים וכל כך כל כך יפים  הן היום בצהרים עוד ראינו את אפרים משחק בבוץ ומים איזה פלא איזה פלא מי הילדים האלה רחוצים וחפופים וכל כך כל כך יפים את ידיה דיני דינה לכלכה בפלסטלינה איזה פלא איזה פלא מי הילדים האלה רחוצים וחפופים וכל כך כל כך יפים חבורה של חמודים לא אותם הילדים אין כאן סוד ואין כאן פלא אם הם חמודים כאלה זה ברור כל בן ובת מתקשטים לכבוד שבת";
            string d = "אמא עוצמת עינים ומרימה הידיים  לה מטפחת לבנה אמא אמא את ישנה אמא עוצמת עינים ומרימה הידיים  לה מטפחת לבנה אמא אמא את ישנה אמא עוצמת עינים ומרימה הידיים  לה מטפחת לבנה אמא אמא את ישנה שקט ילדונת הסי בת אמא הדליקה נרות שבת";
            string e = "תפקחו את העינים  תסתכלו סביב פה ושם נגמר החרף ונכנס אביב  בשדה ליד הדרך יש כבר דגניות  אל תגידו לי שכל זה לא יכול להיות  אנשים טובים באמצע הדרך  אנשים טובים מאד  אנשים טובים יודעים את הדרך  ואיתם אפשר לצעד  איש אחד קנה לי ספר בן מאה שנה  איש אחר בנה כנור שיש בו מנגינה  ואשה טובה אחרת לי נתנה את שמה  ומאז אני בדרך שרה במקומה  אנשים טובים מאד  אנשים טובים יודעים את הדרך  ואיתם אפשר לצעד  איש אחד יבנה לי גשר כדי לחצות נהר  איש אחר יצמיח יער במורדות ההר  ואשה טובה אחרת  אם יהיה קשה  רק תצביע אל האפק ותבטיח שאנשים טובים מאד  אנשים טובים יודעים את הדרך  ואיתם אפשר לצעד  וממש כמו צמחי הבר הבודדים  הם עוצרים תמיד את החולות הנודדים הרקיע מתבהר וכבר אפשר לראות אנשים על אם הדרך מצפים לאות  אנשים טובים מאד אנשים טובים יודעים את הדרך ואיתם אפשר לצעד";
            string f = "בשנה הבאה נשב על המרפסת ונספור ציפורים נודדות  ילדים בחופשה ישחקו תופסת בין הבית לבין השדות  עוד תראה  עוד תראה כמה טוב יהיה בשנה  בשנה הבאה  ענבים אדומים יבשילו עד הערב ויוגשו צוננים לשולחן  ורוחות רדומים ישאו אל אם הדרך עיתונים ישנים וענן  עוד תראה  עוד תראה כמה טוב יהיה בשנה  בשנה הבאה בשנה הבאה נפרוש כפות ידיים מול האור הניגר הלבן אנפה לבנה תפרוש באור כנפיים והשמש תזרח בתוכן";
            string g = "גדול כבר אפרים  כמעט בן שנתים לו אבא ואמא  קנו אופנים טפס ועלה לו אפרים אך אוי קצרות הרגליים אמרו אבא אמא נתן לאפרים בבקר בערב ובצהרים פרות ירקות וחלב כוסותיים ואז חת ושתיים יגדל לו אפרים ירכב בכל יום או יומיים אל סבתא שבירושלים אמרו אבא אמא נתן לאפרים בבקר בערב ובצהרים פרות ירקות וחלב כוסותיים ואז חת ושתיים יגדל לו אפרים ירכב בכל יום או יומיים אל סבתא שבירושלים";
            string h = "גשם גשם קר רטוב חורף רוח ברחוב מתנועע עץ בנוף כף זקוף כף זקוף ובבית טוב על שמשת חלון אדים  מצירים הילדים פיל ודוב על שמשת חלון אדים  מצירים הילדים פיל ודוב";
            string i = "די נזלת חצופה לא טובה ולא יפה למה באת לאף של דן צאי מהר ולכי מכאן דן אינו אוהב נזלת מרגיזה ומבלבלת  מן הבוקר המטפחת מקנחת מקנחת דן אינו הולך לגן הפצי הפצי כל הזמן אוף נזלת עד מתי הסתלקי מכאן ודי דן אינו אוהב נזלת מרגיזה ומבלבלת  מן הבוקר המטפחת מקנחת מקנחת דן אינו הולך לגן הפצי הפצי כל הזמן אוף נזלת עד מתי הסתלקי מכאן ודי";
            string j = "דן טפס על הספסל הופ קפץ והופס נפל אוי כואב לו הוא צורח  מילל ומתיפח אמא באה מה קרה אוי ואבוי נורא נורא פצע דם בברך חור דן שכח כי הוא גבור אין דבר מכל תלבושת  דן אוהבת לחבש תחבשת אמא את פצעו חבשה בתחבשת חדשה אין כאב עוד ואין פגע הכאב עבר בן רגע פנו דרך פנו דרך לגבור פצוע ברך";
            string k = "לא לנגע בתנור זה מאד מאד אסור לתנור אל תתקרב הוא יעשה לך כואב כך אומרים לדני דן אבל דן אינו פחדן מתקרב הוא אל האש ובכלל אינו חושש להבה ורודה כחולה איזה יופי מה נפלא אך לפתע אוי אויה מה קרה לדן כויה דן קטן בוכה מרה התנור הוא ילד רע התנור עשה לי חם ברוגז ברוגז לעולם ומאז ועד היום אין עוד ביניהם שלום";
            string l = "שיר המעלות בשוב השם את שיבת ציון היינו כחולמים  אז ימלא שחוק פינו ולשוננו רינה  אז יאמרו  יאמרו בגוים הגדיל השם לעשות עם אלה  הגדיל השם לעשות עמנו היינו שמחים  שובה השם  את שביתנו כאפיקים בנגב הזורעים בדמעה ברנה יקצרו  הלך ילך ובכה נושא משך הזרע בא יבוא ברינה נושא אלומותיו";
            string m = "איזה יופי איזה סדר מי ארגן כך את החדר מי ערך שולחן שבת זו הבת זו הבת מה זאת רינה לא אאמינה אי אפשר לא יתכן איזה יופי איזה סדר מי ארגן כך את החדר מי ערך שולחן שבת זו הבת זו הבת  כן הבת זו הבת והכל לכבוד שבת";
            string n = "העץ הוא גבוה  העץ הוא ירוק הים הוא מלוח  הים הוא עמוק אם הים הוא עמוק  מה אכפת לו לעץ מה אכפת לו לים שהעץ הוא ירוק העץ הוא גבוה  העץ הוא ירוק  יפה הציפור  היא תעוף לה רחוק אם תעוף הציפור  מה אכפת לו לעץ מה אכפת לציפור שהעץ הוא ירוק הים הוא מלוח  הים הוא עמוק יפה הציפור  היא תעוף לה רחוק  אם תעוף הציפור  מה אכפת לו לים מה אכפת לציפור שהים הוא עמוק אדם שר שירים כי העץ הוא ירוק אדם שר שירים כי הים הוא עמוק  אם תעוף הציפור  לא ישיר עוד שירים מה אכפת לציפור אם ישיר או ישתוק";
            string o = "על הגבעה ליד חצב זהיר זיהר לאט לאט מוציא ראשו הצב מה המצב לפתע הב נבח כלבלב  הניע שיח את עליו נבהל הצב  כנס לתוך שריון ראשו רגליו על מקומו נצב חבל חשב כל כך יפה בחוץ עכשיו";
            string p = "בארץ אהבתי השקד פורח בארץ אהבתי מחכים לאורח שבע עלמות שבע אמהות בארץ אהבתי על הצריח דגל אל ארץ אהבתי יבוא עולה רגל בשעה טובה בשעה ברוכה המשכיחה כל צער  בשעה טובה המשכיחה כל צער  המשכיחה כל צער  אך מי עיני נשר לו ויראנו ומי לב חכם לו ויכירנו מי לא יטעה  מי לא ישגה מי יפתח לו הדלת בארץ אהבתי על הצריח דגל אל ארץ אהבתי יבוא עולה רגל בשעה טובה בשעה ברוכה המשכיחה כל צער  בשעה טובה המשכיחה כל צער  המשכיחה כל צער  אך מי עיני נשר לו";
            string q = "השקדיה פורחת ושמש פז זורחת  צפורים מראש כל גג מבשרות את בוא החג  טו בשבט הגיע חג לאילנות  טו בשבט הגיע חג לאילנות  טו בשבט הגיע חג לאילנות  טו בשבט הגיע חג לאילנות";
            string r = "יש לי צפור קטנה בלב והיא עושה בי מנגינות של סתו ושל אביב חולף ואלף מנגינות קטנות והיא עושה בי מזמורים והיא צובעת עלמות והיא פותרת לשירים כמעט את כל החלומות  יש לי בלב צפור קטנה עם שתי גומות ומנגינה יש לי צפור קטנה בלב והיא עושה בי ספורים של בית חם ובן אוהב ושל קטעי ילדות יפים והיא זוכרת בי כאב והיא אוספת בי שנים  והיא עושה אותי שלו אולי היא טעם החיים יש לי בלב צפור קטנה עם שתי גומות ומנגינה יש לי צפור קטנה בלב והיא אולי כל התקוות ולפעמים אני חושב שהיא תשובה לאכזבות והיא טובה לבנת כנף אתה אני מרגיש חפשי  והיא יושבת על ענף בין מיתרי לבי  יש לי בלב צפור קטנה עם שתי גומות ומנגינה";
            string s = "תורה אורה כזו יש לנו וגם הגדה ומגילה  ואלוקים אחד שלנו וקול חתן וקול כלה ארץ ישראל יפה ארץ ישראל פורחת  את יושבה בה וצופה את צופה בה וזורחת  לנו יש  אחי  הרים אלפים בם נאוו רגלי המבשר וגם מלאך מן השמים שאת נפשנו הוא שומר  וחסיד בעיר הזו יש לנו וגם מצוות וגם דרכים  והברכות כולן שלנו והבשורות והשבחים";
            string t = "ארץ ישראל יפה ארץ ישראל פורחת  את יושבה בה וצופה את צופה בה וזורחת  והעמק הוא כפתור ופרח וההר הוא פרח וכפתור  והצפון שלגים וקרח והדרום זהב טהור  כל הפרדסים נותנים כאן ריח והשקדיות כולן פורחות השמש כאן תמיד זורח על מי תוגה ומנוחות  ארץ ישראל יפה ארץ ישראל פורחת  את יושבה בה וצופה את צופה בה וזורחת  גם לחוטאים אשר בינינו יש מקום בארץ ישראל  מן הצרות שלא עלינו אתה ורק אתה גואל במזמור לתודה מניף הדגל";
            string u = "ולירושלים כל שירי  אנחנו שוב עולים לרגלהו  שירי עם ישראל חי  ארץ ישראל יפה ארץ ישראל פורחת  את יושבה בה וצופה את צופה בה וזורחת  כאן ביתי פה אני נולדתי במישור אשר על שפת הים כאן החברים איתם גדלתי ואין לי שום מקום אחר בעולם  כאן ביתי פה אני שיחקתי בשפלה אשר על גב ההר כאן מן הבאר שתיתי מים ושתלתי דשא במדבר  כאן נולדתי כאן נולדו לי ילדי כאן בניתי את ביתי בשתי ידי כאן גם אתה איתי וכאן כל אלף ידידי ואחרי שנים אלפיים סוף לנדודי  כאן את כל שירי אני ניגנתי והלכתי במסע לילי כאן בנעורי אני הגנתי על חלקת האדמה שלי  כאן נולדתי כאן נולדו לי ילדי כאן בניתי את ביתי בשתי ידי כאן גם אתה איתי וכאן כל אלף ידידי ואחרי שנים אלפיים סוף לנדודי כאן את שולחני אני ערכתי פת של לחם פרח רענן דלת לשכנים אני פתחתי מי שבא נאמר לו אהלן  כאן נולדתי כאן נולדו לי ילדי כאן בניתי את ביתי בשתי ידי כאן גם אתה איתי וכאן כל אלף ידידי ואחרי שנים אלפיים סוף לנדודי";
            string v = "שם הרי גולן  הושט היד וגע בם בדממה בוטחת מצווים עצור  בבדידות קורנת נם חרמון הסבא וצינה נושבת מפסגת הצחור  שם על חוף הים יש דקל שפל צמרת סתור שיער הדקל כתינוק שובב  שגלש למטה ובמי כינרת במי כינרת משכשך רגליו  מה ירבו פרחים בחורף על הכרך  דם הכלנית וכתם הכרכום  יש ימים פי שבע אז ירוק הירק  פי שבעים תכולה התכלת במרום  גם כי אוורש ואהלך שחוח  והיה הלב למשואות זרים  איך אוכל לבגוד בך  איך אוכל לשכוח איך אוכל לשכוח חסד נעורים  שם הרי גולן  הושט היד וגע בם בדממה בוטחת מצווים עצור  בבדידות קורנת נם חרמון הסבא וצינה נושבת מפסגת הצחור ";
            string w = "ארצנו הקטנטונת  ארצנו היפה מולדת בלי כותונת מולדת יחפה קבליני אל שירייך  כלה יפהפייה פתחי לי שערייך אבוא בם אודה קה בצל עצי החורש  הרחק מאור חמה יחדיו נכה פה שורש אל לב האדמה אל מעיינות הזוהר אל בארות התום מולדת ללא תואר וצועני יתום עוד לא תמו כל פלאייך ";
            string x = "עוד הזמר לו שט עוד לבי מכה עם ליל ולוחש לו בלאט את לי את האחת את לי את  אם ובת את לי את המעט המעט שנותר  נביאה בבגדינו את ריח הכפרים בפעמון ליבנו יכו העדרים  ישנה דממה רוגעת וקרן אור יפה  ולאורה נפסעה ברגל יחפה  עוד לא תמו כל פלאייך עוד הזמר לו שט עוד לבי מכה עם ליל ולוחש לו בלאט את לי את האחת את לי את  אם ובת את לי את המעט המעט שנותר";
            string y = "כל אחד ישא קולו  יחד לעולם כלו ונשירה פה אחד יחד יד ביד יד ביד ולב אל לב  כל אחד יתן מעט  כל אחד יושיט רק יד  יחד יחד לא לבד  אל האור נצעד  יד ביד ולב אל לב כל אחד יתן מעט  כל אחד יושיט רק יד  יחד יחד לא לבד אל האור נצעד   את ידי קח בידך  יחד כל המשפחה  איש אחד בלב אחד  יחד יד ביד יד ביד ולב אל לב  כל אחד יתן מעט  כל אחד יושיט רק יד  יחד יחד לא לבד  אל האור נצעד  פה אחד שירנו רם יחד יחד כאן כולם וישיר אז כל אחד  יחד יד ביד  כל אחד ישא קולו יחד לעולם כלו  ונשירה פה אחד  יחד יד ביד  יד ביד ולב אל לב  כל אחד יתן מעט  כל אחד יו שיט רק יד  יחד יחד לא לבד  אל האור נצעד יד ביד ולב אל לב  כל אחד יתן מעט  כל אחד יושיט רק יד  יחד יחד לא לבד  אל האור נצעד  את ידי קח בידך  יחד כל המשפחה  איש אחד בלב אחד  יחד יד ביד  יד ביד ולב אל לב  כל אחד יתן מעט  כל אחד יושיט רק יד  יחד יחד לא לבד  אל האור נצעד  פה אחד שירנו רם  יחד יחד כאן כולם  וישיר אז כל אחד  יחד יד ביד";
            string z = "ורד ורד תני לי יד לא אני לבד לבד בואי נסרק את הצמה ורד ורד בעצמה ורד כבר ילדה גדולה בעצמה תלבש שמלה ותנעל הנעלים ואפילו לא תאמינו תצחצח השנים היא רק היא תלבש הגרב ותפשט אותה בערב ותמצע לבד לבד כל דבר אשר אבד עסוקה היא כל היום לעזור לה לעזור לה מה פתאום ורד ורד בעצמה ורד כבר ילדה גדולה בעצמה תלבש שמלה ותנעל הנעלים ואפילו לא תאמינו תצחצח השנים";
            string aa = "התרופה קצת מרה אבל רותי גבורה לא נורא היא תפתח פה גדול ותבלע הכל הכל ויגידו לבריאות גבורה היא רותי רות היא תפתח פה גדול ותבלע הכל הכל ויגידו לבריאות גבורה היא רותי רות היא תפתח פה גדול ותבלע הכל הכל ויגידו לבריאות גבורה היא רותי רות";
            string bb = "לתת את הנשמה ואת הלב לתת לתת כשאתה אוהב ואיך מוצאים את ההבדל שבין לקחת ולקבל עוד תלמד לתת לתת לגלות סודות בסתר להתיר את סבך הקשר  כשהלב בך נצבט מכל חיוך מכל מבט אתה נזהר אתה יודע וחוץ ממך איש לא שומע  פוסע בין הדקויות וממלא שעות פנויות לתת את הנשמה ואת הלב לתת לתת כשאתה אוהב ואיך מוצאים את ההבדל שבין לקחת ולקבל עוד תלמד לתת לתת אתה לומד עם השנים לבנות ביחד בנינים  נאים  לרקום אתה ספור  חיים ולעבר ימים קשים במצוקות ורגושים תמיד לדעת לוותר ואת הטעם לשמר לתת את הנשמה ואת הלב לתת לתת כשאתה אוהב ואיך מוצאים את ההבדל שבין לקחת ולקבל עוד תלמד לתת לתת לראות בתוך הנפילה שיש מקום למחילה תמיד אפשר שוב להתחיל כמו יום חדש כמו כרגיל לתת";
            string cc = "שרב הכי כבד אפלו בשמש ידעתי שהגשם עוד ירד ראיתי בחלון שלי צפור בסופה וקר לא פעם זה קשה אבל לרב מלה טובה מיד עושה לי טוב  רק מלה טובה או שתים לא יותר מזה אפילו ברחוב ראשי סואן ראיתי איש יושב ומנגן  פגשתי אנשים מאושרים אפילו בין שבילי עפר צרים תמיד השארתי פתח לתקוה אפילו כשכבתה השלווה  חלמתי על ימים יותר יפים אפילו בלילות שינה טרופים לא פעם זה קשה אבל לרב מילה טובה מיד עושה לי טוב  רק מלה טובה או שתים לא יותר מזה";
            string dd = "אני נולדתי אל המנגינות ואל השירים של כל המדינות נולדתי ללשון וגם למקום למעט להמון שיושיט יד לשלום אני נולדתי לשלום שרק יגיע אני נולדתי לשלום שרק יבוא אני נולדתי לשלום שרק יופיע אני רוצה אני רוצה להיות כבר בו אני נולדתי אל החלום ובו אני רואה שיבוא השלום נולדתי לרצון ולאמונה שהנה הוא יבוא אחרי שלושים שנה אני נולדתי לשלום שרק יגיע אני נולדתי לשלום שרק יבוא אני נולדתי לשלום שרק יופיע אני רוצה אני רוצה להיות כבר בו נולדתי לאומה ולה שנים אלפיים שמורה לה אדמה וגם חלקת שמיים ולה רואה צופה הנה עולה היום והשעה יפה זוהי שעת שלום אני נולדתי לשלום שרק יגיע אני נולדתי לשלום שרק יבוא אני נולדתי לשלום שרק יופיע אני רוצה אני רוצה להיות כבר בו";
            string ee = "על שולחן שאותו ערכה אמא וברכה ברכה לידי על השולחן לסעודה הכל מוכן על שולחן אותו ערכה אמא וברכה ברכה לידי על השולחן לסעודה הכל מוכן בואו בואו בן ובת לסעודה של טו בשבט מה נאכל בחג אילן זה מובן פרות הגן לכל אחד כל מין וזן הנה תמר תאכל תמר ומה שמעון יאכל רמון צמוק חרוב ותאנה נתן לרות ודן מנה בואו בואו בן ובת לסעודה של טו בשבט מה נאכל בחג אילן זה מובן פרות הגן לכל אחד כל מין וזן קלמנטינה נתן לדינה את האגס תאכל הדס ואת גלית טלי לך אשכולית בואו בואו בן ובת לסעודה של טו בשבט מה נאכל בחג אילן זה מובן פרות הגן לכל אחד כל מין וזן נו  לאליהב תפוח מזהב ומה עתה ברוך אתה בורא פרי העץ תודה  בתאבון לסעודה";
            string ff = "על הדבש ועל העוקץ  על המר והמתוק  על בתנו התינוקת שמור קלי הטוב  על האש המבוערת  על המים הזכים  על האיש השב הביתה מן המרחקים  על כל אלה  על כל אלה  שמור נא לי קלי הטוב על הדבש ועל העוקץ  על המר והמתוק  אל נא תעקור נטוע  אל תשכח את התקוה השיבני ואשובה אל הארץ הטובה  שמור אלי על זה הבית  על הגן  על החומה  מיגון  מפחד פתע וממלחמה  שמור על המעט שיש לי  על האור ועל הטף על הפרי שלא הבשיל עוד ושנאסף  על כל אלה  על כל אלה  שמור נא לי קלי הטוב  על הדבש ועל העוקץ על המר והמתוק  אל נא תעקור נטוע  אל תשכח את התקוה השיבני ואשובה אל הארץ הטובה  מרשרש אילן ברוח  מרחוק נושר כוכב  משאלות ליבי בחושך נרשמות עכשיו  אנא  שמור לי על כל אלה ועל אהובי נפשי  על השקט  על הבכי ועל זה השיר  על כל אלה  על כל אלה  שמור נא לי קלי הטוב  על הדבש ועל העוקץ  על המר והמתוק אל נא תעקור נטוע  אל תשכח את התקוה השיבני ואשובה אל הארץ הטובה";
            string gg = "מעל פסגת הר הצופים אשתחוה לך אפיים מעל פסגת הר הצופים שלום לך ירושלים מאה דורות חלמתי עלייך לזכות  לראות באור פנייך ירושלים  ירושלים  האירי פנייך לבנך ירושלים  ירושלים  מחרבותייך אבנך מעל פסגת הר הצופים שלום לך ירושלים אלפי גולים מקצות כל תבל נושאים אלייך עיניים  באלפי ברכות היי ברוכה מקדש מלך  עיר מלוכה ירושלים  ירושלים  אני לא אזוז מפה ירושלים  ירושלים  יבוא המשיח יבוא";
            string hh = "עננים אמרו לשמש  אמא שמש אם חמה אל תאירי עוד לארץ מה לך ולאדמה את שלנו של שמים של כל רוח וענן אל תאירי לה לארץ רק תאירי כאן רק כאן את שלנו של שמים של כל רוח וענן אל תאירי לה לארץ רק תאירי כאן רק כאן צחקה להם השמש צחקה לטיפושנים ואמרה טוב טוב בסדר אך הקשיבו עננים  כי שדה וגן וירק וכל פרח לי חבר אם יהיו לי כאן כל אלה בשמים אשאר";
            string ii = "קום והתהלך בארץ בתרמיל ובמקל  וודאי תפגוש בדרך שוב את ארץ ישראל  יחבקו אותך דרכיה של הארץ הטובה  היא תקרא אותך אליה כמו אל ערש אהבה  זאת אכן אותה הארץ  זו אותה האדמה ואותה פיסת הסלע הנצרבת בחמה  ומתחת לאספלט למנהגים ולמצווה  מסתתרת המולדת ביישנית וענווה  קום והתהלך בארץ בתרמיל ובמקל  וודאי תפגוש בדרך שוב את ארץ ישראל  יחבקו אותך דרכיה של הארץ הטובה  היא תקרא אותך אליה כמו אל ערש אהבה  וכרמי עצי הזית ומסתור המעיין עוד שומרים על חלומה וחלומנו הישן  וגגות אודמים על הר וילדים על השבילים  במקום שבו הלכנו עם חגור ותרמילים  קום והתהלך בארץ בתרמיל ובמקל  וודאי תפגוש בדרך שוב את ארץ ישראל  יחבקו אותך דרכיה של הארץ הטובה  היא תקרא אותך אליה כמו אל ערש אהבה";
            string jj = "קח מקל  קח תרמיל  בוא אתי אל הגליל  בוא אתי ביום אביב נהלך סביב  סביב  קח מקל  קח תרמיל  בוא אתי אל הגליל  בוא אתי ביום אביב נהלך סביב  סביב  עם השמש הזורחת בחניתה ושוקעת באכזיב  כי השמש  כן השמש  כאן השמש  כאן השמש לא תכזיב  כי השמש  כן השמש  כאן השמש לא תכזיב  כאן השמש לא תכזיב  כאן השמש לא תכזיב  קח טמבור  קח חליל  בוא אתי אל הגליל  בוא נפתח נא השירון  בוא נשיר במלוא גרון  קח מקל  קח תרמיל  בוא אתי אל הגליל בוא אתי ביום אביב נהלך סביב  סביב";
            string kk = "עם הרוח הנוגנת בשלווה ושורקת בעברון  כי הרוח  כן הרוח  כאן הרוח  כאן הרוח רן תרן  כי הרוח  כן הרוח  כאן הרוח רן תרן  כאן הרוח רן תרן  כאן הרוח רן תרן קח צעיף  קח מעיל  בוא אתי אל הגליל  בוא בחרף מילל ונראה איך נשתולל  קח מקל  קח תרמיל  בוא אתי אל הגליל";
            string ll = "יחד  כל הדרך  יחד  לא אחרת  יד ביד נושיט לטוב שעוד יבוא  בא יבוא  יחד  כל הדרך  יחד  לא אחרת  יחד איש אל איש יפתח את לבבו  רק אם נאמין בעז הרוח  רק אם נאמין ולא  ננוח מן המצרים אל ים פתוח כמים רבים נסער  רק אם נאמין ודאי נצליח  רק אם נאמין ודאי נגיע  בימים של סער ברקיע כאש התמיד נבער  יחד  כל הדרך  יחד  לא אחרת  יד ביד נושיט לטוב שעוד יבוא  בא יבוא יחד  כל הדרך יחד לא אחרת  יחד איש אל איש יפתח את לבבו";
            string mm = "רש רש רשרש רעשן קשקוש קשקש קשקשן בקר ערב צהרים מקשקש קיש באזנים לא ננום ולא נישן  מי זה כאן כל כך מרעיש מי אוזנינו מחריש רש רש רשרש רעשן קשקוש קשקש קשקשן בקר ערב צהרים מקשקש קיש באזנים לא ננום ולא נישן";
            string nn = "הנה כוכב ועוד כוכב ועוד כוכב ניצת חבל כי כבר הלכת שבת חבל כי כבר יצאת  הנה כוכב ועוד כוכב ועוד כוכב ניצת חבל כי כבר הלכת שבת חבל כי כבר יצאת  ואבא שב מן התפילה דולק כבר נר של הבדלה אחד אחד המשפחה קופסת הבושם מריחה הנה כוכב ועוד כוכב ועוד כוכב ניצת חבל כי כבר הלכת שבת חבל כי כבר יצאת  הצעיר בחבורה מניף הנר עד התקרה מאיר הנר הדרך לך שבוע טוב ומבורך";
            string oo = "שבחי ירושלים את אלוקי  הללי אלוקייך ציון כי חיזק בריחי שערייך  ברך בנייך בקרבך ברך בנייך בקרבך ברך בנייך בקרבך הללי  הללי אלוקייך ציון";
            string pp = "אם בהר חצבת אבן להקים בנין חדש בהר חצבת אבן להקים בנין חדש כי מן האבנים האלה יבנה מקדש יבנה  יבנה  יבנה המקדש אם בהר נטעת ארז  ארז במקום דרדר בהר נטעת ארז  ארז במקום דרדר כי מן הארזים האלה ייבנה ההר יבנה  יבנה  יבנה ההר אם לא שרת לי שיר עדיין  שירה לי מזמור חדש שהוא עתיק מיין ומתוק מדבש שיר שהוא עתיק מיין ומתוק מדבש שיר שהוא כבן אלפיים ובכל יום חדש יבנה  יבנה  יבנה המקדש";
            string qq = "דע לך שכל רועה ורועה יש לו ניגון מיוחד משלו דע לך שכל עשב ועשב יש לו שירה מיוחדת משלו ומשירת העשבים נעשה ניגון של רועה כמה יפה כמה יפה ונאה כששומעים השירה שלהם טוב מאוד להתפלל ביניהם ובשמחה לעבוד את השם ומשירת העשבים מתמלא הלב ומשתוקק וכשהלב מן השירה מתעורר ומשתוקק אל ארץ ישראל אור גדול אזי נמשך והולך מקדושתה של הארץ ומשירת העשבים נעשה ניגון של הלב";
            string rr = "שלום שמש שלום חלמתי הלילה חלום קנו לי מחברת ילקוט עפרון הביטי הנה הם על החלון שלום שמש שלום  שלום אמא שלום אני גדול היום לבית הספר אלך אלך ללמוד יהיה לך ילד חכם מאד שלום אמא שלום שלום צפור שלום את יודעת מה היום היום אלמד לקרוא לכתב אהיה תלמיד חכם וטוב שלום צפור שלום שלום מורה שלום נתחיל ללמד היום שלום לאורי וברכה עלה ולמד בהצלחה שלום אורי שלום";
            string ss = "שלום לך אורחת שלום לך שבת חיכינו חיכינו סוף סוף הנה באת הבית שטפנו פרחים לך קטפנו פרשנו מפה לבנה על שלחן הנרות מאירים  כל הבית מוכן שלום לך אורחת שלום לך שבת חיכינו חיכינו סוף סוף הנה באת שלום לך אורחת שלום לך שבת חיכינו חיכינו סוף סוף הנה באת פרחים לך קטפנו פרשנו מפה לבנה על שלחן הנרות מאירים  כל הבית מוכן";
            string tt = "גשם גשם גשם רב מי זה בא כולו נרטב מגבים לרגליו  זה חלבן מביא חלב רותי דן ויהודה וכל ילד וילדה לבורא מילה מודה תודה בוראינו גדולה תודה";
            string uu = "אבא שלי מדבר איטלקית צרפתית ואנגלית ואפילו טורקית אבל אמא שלי אבל אמא שלי אבל אמא שלי מבינה תינוקית תינוקית היא דבור היא דבור של תינוק תינוקית היא דבור של אחי המתוק מה אומר הוא אחי הוא אומר ממנה ואומרת אמי הוא רוצה בננה ואחי מקשקש גללטטאל ואומרת אימי הוא רוצה לטיל דדד זהו ברוגז ואיה אוהב ואמי מבינה מבינה היא היטב מפטפט הוא אחי מפטפט הוא בקול ואמי מבינה מבינה את הקול כן יודע אבי איטלקית צרפתית ואנגלית ואפילו טורקית אבל אמא שלי אבל אמא שלי אבל אמא שלי מבינה תינוקית";
            string vv = "בת שנתיים היא תמר וסוחבת כל דבר רגע אמא לא תביט ותמר תסחב מפית ספל תה וכוס זכוכית כל צלחת חרסינה נתונה בסכנה מכיור את הסבון תלקק בתאבון מי פרחים שבעציץ היא תאהב יותר ממיץ במברשת השניים תצחצח נעליים הוי תמר  את שובבה  לא לא אל אני טובה הן לא בי הוא האשם אם הכל מונח כך סתם  יש לתקע הכלים למקומם במסמרים  הוי תמר  את שובבה  לא לא אל אני טובה הן לא בי הוא האשם אם הכל מונח כך סתם  יש לתקע הכלים למקומם במסמרים  הוי תמר  את שובבה  לא לא אל אני טובה הן לא בי הוא האשם אם הכל מונח כך סתם  יש לתקע הכלים למקומם במסמרים";
            string ww = "בפרדס הגדול בפרדס הרחב מזהיבים על כל עץ תפוחים של זהב ואומרים ענפים  אנו כבר עיפים  מהרו נא קוטפים תפוחינו יפים בוקר טוב מר ירקן תפוזים יש היום יש ויש למה לא בוקר טוב ושלום וקרוא הוא בקול תפוזים פה בזול במאזניים אשקול קחו נא קנו את הכל";
            string xx = "הארץ משוועת הגיעה עת לטעת  כל אחד יקח לו עץ באתים נצא חוצץ ארץ זבת חלב  חלב ודבש ארץ זבת חלב  חלב ודבש ארץ זבת חלב  חלב ודבש ארץ זבת חלב  חלב ודבש ארץ זבת חלב  זבת חלב ודבש ארץ זבת חלב  זבת חלב ודבש כך הולכים השותלים רון בלב ואת ביד מן העיר ומן הכפר מן העמק  מן ההר בטו בשבט בטו בשבט למה באתם  השותלים נכה בקרקע ובצור וגומות סביב נחפור בהרים ובמישור בטו בשבט בטו בשבט מה יהא פה  השותלים שתיל יבוא בכל גומה  יער עד יפרוש צילו על ארצנו ערומה  בטו בשבט  בטו בשבט";


            //string[] arr = { c,p, e, r, b, xx, i, nn, a,  d, f, g, h, j, k, l, m, n, o, q, s, t, u, v, w, x, y, z, aa, bb, cc, dd, ee, ff, gg, hh, ii, jj, kk, ll, mm, oo, pp, qq, rr, ss, tt, uu, vv, ww };
            //var WordsArray = arr[0].Split(' ');
            //string Items = WordsArray[0] + ' ' + WordsArray[1];
            //Console.WriteLine("השיר הוא  " + " : " + Items);
            //List<FinalTag> abc = start(arr[0]);
            //Console.WriteLine(" ");
            string[] arr = { a, b, a, c, d, e, f, g, h, i, j, k, l, m, n, o, p, q, r, s, t, u, v, w, x, y, z, aa, bb, cc, dd, ee, ff, gg, hh, ii, jj, kk, ll, mm, nn, oo, pp, qq, rr, ss, tt, uu, vv, ww, xx };
            List<string> cccc = startUp(arr[0]);
            foreach (var item in cccc)
            {
                Console.WriteLine(item);
            }
            for (int jk = 0; jk < arr.Length; jk++)
            {
                var WordsArray = arr[jk].Split(' ');
                string Items = WordsArray[0] + ' ' + WordsArray[1];
                Console.WriteLine("השיר הוא  " + " : " + Items);
                List<FinalTag> abc = start(arr[jk]);
                Console.WriteLine(" ");
               
            }
        
            //למורה גילה לוי!
            //ניתחתי את הספריה, וזה הקוד לפיצול התגיות
            //בהתאם לנתונים, ולמרות הכל-נכונות הפיצול היא 80 אחוז התאמה. אחרי הכל
            //הבינה המלאכותית זה ענין קשה. מקווה שזה בסדר, שהמורה תריץ ותציץ בקלט
            //List<HebrewNLP.Morphology.MorphInfo> a = HebrewMorphology.AnalyzeWords(s);
            //List<List<HebrewNLP.Morphology.MorphInfo>> a2 = HebrewMorphology.AnalyzeSentence("ילדת הסלון היתה יפה");
            //List<List<List<HebrewNLP.Morphology.MorphInfo>>> a3 = HebrewMorphology.AnalyzeText("ילדת הסלון היתה יפה ");
            //foreach (var option in a)
            //{
            //    //foreach(var ii in option)
            //    //{
            //    //    //Console.WriteLine(ii);
            //    //    //bool z=ii.Vav ;
            //    //    //Console.WriteLine(ii.Subordination);
            //    //    //Console.WriteLine(ii.PrepositionChars);
            //    //    //Console.WriteLine(ii.PartOfSpeech);
            //    //    //HebrewNLP.Morphology.PartOfSpeech.ADJECTIVE;
            //    //    //int value = ii.PartOfSpeech;
            //    //    //var enumDisplayStatus = (EnumDisplayStatus)value;
            //    //    //string stringValue = enumDisplayStatus.ToString();
            //    //    //Console.WriteLine(ii.Gender);
            //    //    //Console.WriteLine(ii.Subordination);
            //    //    //Console.WriteLine(ii.Subordination);
            //    //}
            //    Console.WriteLine("Option " + (++i) + ":");
            //    Console.WriteLine(ObjectDumper.Dump(option));
            //    //string a44 = ObjectDumper.Dump(option);

           Console.WriteLine((long)HebrewNLP.Morphology.Tense.FUTURE + " = " + HebrewNLP.Morphology.PartOfSpeech.ADVERB);
            Console.Write("ds");


        }

    }
}
