using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections;
using BLL.Models.Respone.Morphology;
using BLL.Services.Morphology;

namespace BLL
{
    /// <summary>
    /// enum for information about the tags, how to cut or join...
    /// </summary>
    //enum Demand
    //{
    //    zero = 0, first = 1, last = 2, between = 4, alone = 8, vav = 16, vavCon = 32, kaf = 64, lamed = 128, bet = 256
    //}
    //בכל שיר בממוצע 75 מילים, וכל מילה בת שלש וחצי תווים +רוח

    public class ProgramAndFunction
    {
        /// <summary>
        /// 3 enum for information about the tags, how to cut or join...
        /// </summary>
        /// 

        //בכל שיר בממוצע 75 מילים, וכל מילה בת שלש וחצי תווים +רוח


        //רשימות של מילים היכולות להופיע רק בסוף תגית, בתחילתה ובאמצעה
        //סוף 9 | 1 התחלה | + באמצע 
        public static List<string> achar = new List<string> { "אחרי", "אחריכם", "אחריכן", "אחריהם", "אחריהן", "אחריך", "אחריך", "אחריו", "אחריה", "אחרינו" };//9
        public static List<string> el = new List<string> { "אל", "אליכם", "אליכן", "אליהם", "אליהן", "אלי", "אליך", "אליך", "אליו", "אליה", "אלינו" };//9
        public static List<string> ezel = new List<string> { "אצל", "אצלכם", "אצלכן", "אצלם", "אצלן", "אצלי", "אצלך", "אצלך", "אצלו", "אצלה", "אצלנו" };//9
        public static List<string> et = new List<string> { "את", "אתכם", "אתכן", "אותם", "אותן", "אותי", "אותך", "אותך", "אותו", "אותה", "אותנו" };//9
        public static List<string> tachat = new List<string> { "תחתיכם", "תחתיכן", "תחתיהם", "תחת", "תחתי", "תחתיך", "תחתיך", "תחתיו", "תחתיה", "תחתינותחתיכם", "תחתיכן", "תחתיהם", "תחתיהן," };//9
        public static List<string> bishvil = new List<string> { "בשבילכם", "בשבילכן", "בשבילם", "בשבילן", "בשביל", "בשבילי", "בשבילך", "בשבילך", "בשבילו", "בשבילה", "בשבילנו" };//9
        public static List<string> meachor = new List<string> { "אחוריn", "מאחוריך", "מאחוריך", "מאחוריו", "מאחוריה", "מאחורינו", "מאחוריכם", "מאחוריכן", "מאחוריהם", "מאחוריהן" };//9
        public static List<string> shel = new List<string> { "שלי", "שלו", "שלה", "שלך", "שלכם", "שלנו", "שלהם", "שלהן" };//9
        public static List<string> lefanay = new List<string> { "לפניכם", "לפניכן", "לפניהם", "לפניהן", "לפני", "לפניך", "לפניך", "לפניו", "לפניה", "לפנינו" };//9
        public static List<string> be = new List<string> { "בכם", "בכן", "בהם", "בהן", "בי", "בך", "בך", "בו", "בה", "בנו" };//9+

        public static List<string> question = new List<string> { "היכן", "האם", "במי", "במה", "מי", "מה", "מתי", "למי", "למה", "לאן", "למה", "איך", "איפה", "איה", "איזה", "אילו", "איזו", "מדוע", "מאין", "מנין", "מתי", "ממי", "ממה", "כמה", "כיצד" };//1
        public static List<string> bein = new List<string> { "ביניכם", "ביניכן", "ביניהם", "ביני", "בינך", "בינך", "בינו", "בינה", "בינינו", "יהיה", "היה", "יהא" };//9+
        public static List<string> me = new List<string> { "מן", "מכם", "מכן", "מהם", "מהן", "ממנה", "ממנו", "ממני", "ממך", "ממך", "ממנו", "איתי", "איתך", "איתו", "איתה", "איתנו", "איתכם", "איתכן", "איתם", "איתן" };//9+
        public static List<string> lach = new List<string> { "לי", "לך", "לה", "לו", "לנו", "להם", "להן", "לכם", "להן" };//+9

        public static List<string> ani = new List<string> { "ואני", "והיא", "והוא", "וזה", "ואנחנו", "ואתם", "ואת", "וזו", "וזאת", "ואתה", "ואתן", "והם", "והן", "והנה", "אני", "היא", "הוא", "זה", "אנחנו", "אתם", "את", "זו", "זאת", "אתה", "אתן", "הם", "הן", "הנה" };//1
        public static List<string> bodyCon = new List<string> { "ואני", "והיא", "והוא", "וזה", "ואנחנו", "ואתם", "ואת", "וזו", "וזאת", "ואתה", "ואתן", "והם", "והן", "והנה", "אני", "היא", "הוא", "זה", "אנחנו", "אתם", "את", "זו", "זאת", "אתה", "אתן", "הם", "הן", "הנה" };//1
        public static List<string> legabey = new List<string> { "השם", "לגביכם", "לגביכן", "לגביהם", "לגביהן", "לגבי", "לגביך", "לגביך", "לגביו", "לגביה", "לגבינו" };//1


        public static List<string> alay = new List<string> { "עליכם", "עליכן", "עליהם", "עליהם", "עלי", "עליך", "עליך", "עליו", "עליה", "עלינו" };//+
        public static List<string> eini = new List<string> { "איני", "אינך", "אינו", "אינה", "אינם", "אינן", "איננו", "אינכם", "אינכן", "כי" };//+


        public static List<string> leyad = new List<string> { "לידכם", "לידכן", "לידם", "לידן", "ליד", "לידי", "לידך", "לידך", "לידו", "לידה", "לידינו" };//91
        public static List<string> biglal = new List<string> { "בגללכם", "בגללכן", "בגללכם", "בגללן", "בגלל", "בגללי", "בגללך", "בגללך", "בגללו", "בגללה", "בגללנו" };
        public static List<string> bilad = new List<string> { "בלעדיכם", "בלעדיכן", "בלעדיהם", "בלעדיהן", "בלעדי", "בלעדי", "בלעדיך", "בלעדיך", "בלעדיו", "בלעדיה", "בלעדינו" };//91
        public static List<string> betoch = new List<string> { "בתוככם", "בתוככן", "בתוכם", "בתוכן", "בתוך", "בתוכי", "בתוכך", "בתוכך", "בתוכו", "בתוכה", "בתוכנו" };//9+1 

        public static List<string> middle = new List<string> { "אל", "אשר", "ליד", "תחת", "בתוך", "את", "על", "אצל", "ללא", "הכל", "מכל", "בכל", "לכל", "אל", "כבר", "את", "אשר", "רק", "על", "כן", "לא", "כל", "של", "בתוך", "ליד" };
        public static List<string> middleEnd = new List<string> { "הזה", "הזו", "ההוא", "הלז", "ההיא", "ההם", "ההן", "האלה", "האלו", "הזו", "הזאת", "מעל" };
        public static List<string> middleStart = new List<string> { "בין", "בשביל", "אחר", "מן", "כמו", "המה" };
        public static List<string> startss = new List<string> { "אנוכי", "אלו", "אלה", "גם", "אז", "אך" };
        public static List<List<string>> canStart_1_1p_19 = new List<List<string>> { betoch, leyad, ani, legabey, question, startss, middleStart };//מילים שיכולות לפתח ולא חיבות
        public static List<List<string>> canEnd_9_9p_19 = new List<List<string>> { achar, el, ezel, et, tachat, bishvil, meachor, shel, lefanay, be, bein, me, lach, leyad, bilad, betoch, middleEnd };//מילים שיכולות לסגר ולא חייבות
        public static List<List<string>> end_ = new List<List<string>>
            { achar, el, ezel, et,tachat,bishvil,shel,lefanay,meachor};//מילים החיבות להיות בסוף
        public static List<List<string>> middle_ = new List<List<string>>
            {be, bein,me,lach,alay,eini,middleEnd,middle,middleStart};//מילים היכולות להיות באמצע
        public static List<List<string>> middle_must = new List<List<string>>
            {alay,middle};//מילים החיבות להיות באמצע

        public static List<List<string>> start_ = new List<List<string>> { startss, ani, legabey, question };//מילים חיבות להיות בהתחלה

        /// <summary>
        /// פונקציה המקבלת את רשימת התגיות הסופית ומחזירה אותן כמחרוזת הפוכה כתצוגה נוחה בסביבת פיתוח ויזואל סטודיו
        /// </summary>
        /// <param name="tags">רשימת התגיות הסופית</param>
        /// <returns>מחרוזת הפוכה</returns>
        public static string lightStreamReverse(List<string> tags)
        {
            string endi = "";
            foreach (var item in tags)
            {

                char[] charArray = item.ToCharArray();
                Array.Reverse(charArray);
                string reverseItem = new string(charArray);
                endi += reverseItem;
                endi += " 📍 ";



            }
            return endi;
        }
        /// <summary>
        /// פונקציה המקבלת את רשימת התגיות הסופית ומחזירה אותן כמחרוזת
        /// </summary>
        /// <param name="tags">רשימת התגיות הסופית</param>
        /// <returns>מחרוזת נוחה לקריאה</returns>
        public static string lightStream(List<string> tags)
        {
            string endi = "";
            foreach (var item in tags)
            {
                endi += item;
                endi += " 📍 ";
            }
            return endi;
        }
        /// <summary>
        /// החזרת מילים פותחות וסוגרות לשני הפזמונים הראשונים
        /// </summary>
        /// <param name="song">השיר</param>
        /// <param name="firstD1">השמחת מילה פותחת פזמון ראשי</param>
        /// <param name="lastD1">השמת מילה חותמת פזמון ראשי</param>
        /// <param name="firstD2">השמת מילה פותחת פזמון משני</param>
        /// <param name="lastD2">השמת מילה חותמת פזמון משני</param>
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


                firstWordDitty2 = ditty2.Substring(0, ditty2.IndexOf(' '));
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
        /// <returns>האם יכולה להיות באמצע</returns>
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
        /// <returns>האם המילה חותכת</returns>
        public static bool getListSplit(string str)
        {


            if (bodyCon.Any(sublist => sublist.Contains(str)))
                return true;
            return false;
        }
        /// <summary>
        /// האם המילה יכולה ולא חיבת להיות בהתחלה
        /// </summary>
        /// <param name="str">המילה</param>
        /// <returns>האם יכולה להיות בהתחלה</returns>
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
        /// <returns>האם יכולה להיות באמצע</returns>
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
        /// <returns>התגיות וניתוחן לפי אינם בינארי</returns>
        public static List<List<DWord>> getMorphList(string song)
        {
            long value;
            long allBitsOn = (long)Analizing.all;// 1099511627775;//בזמן הנתוח, התיחסות רק בפעם הראשונה
                                                 //allBitsOn: 1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111

            List<List<List<MorphInfo>>> morphInfos3 = new List<List<List<MorphInfo>>>() { HebrewMorphology.AnalyzeSentence(song) };
            String[] strgs = song.Split(' ');//פיצול השיר למילים
            ArrayList arrayList = new ArrayList();
            List<List<DWord>> tagits = new List<List<DWord>>();
            for (int i = 0; i < strgs.Length; i++)
            {
                tagits.Add(new List<DWord>());
                tagits[i].Add(new DWord(Analizing.zero, strgs[i]));
            }
            long subordination, prepositionchars, partOfSpeech, gender, person, constructstate, tense;//עבור תוצאות הנתוח ופינוחם לפי האינם שהוגדר

            for (int i = 0; i < strgs.Length; i++)
            {

                for (int j = 0; j < morphInfos3.Count; j++)
                {

                    List<MorphInfo> lm = morphInfos3[j][i];
                    foreach (MorphInfo item in lm)//הרצת הפוריצ' כמספר אפשרויות הניתוח של השיר אך בעזרת אלביצאון התיחסות רק בחלק מהמקרים
                    {

                        value = 0;
                        subordination = (long)item.Subordination;
                        prepositionchars = (long)item.PrepositionChars;
                        partOfSpeech = (long)item.PartOfSpeech;
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

                        switch (partOfSpeech)
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
                                        value = (long)Analizing.nismach | (long)tagits[i][0].Ana | value & allBitsOn;//מהפעם השניה אין משמעות לשורה זו
                                        break;
                                    }

                                }

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
                        allBitsOn = 0;//איפוס כדי שיפסיק להתיחס לניתוח זה החל מהפעם השניה, מאפשרות הניתוח הבא
                    }
                }
            }
            return tagits;
        }

        /// <summary>
        /// האם המילה חיבת להיות בסוף התגית
        /// </summary>
        /// <param name="str">המילה</param>
        /// <returns>האם המילה חיבת להיות בסוף תגית</returns>
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
        /// <returns> האם המילה חיבת להיות בתחילת התגית</returns>
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
        /// <param name="str">המילה</param>
        /// <param name="ana">הניתוח הנוכחי</param>
        /// <returns>האם המילה חיבת להיות בתחילת התגית, כולל הרשימה</returns>
        public static bool mustStart(string str, Analizing ana)
        {
            if (getListStart(str) || ((long)ana & ((long)Analizing.doo | (long)Analizing.name | (long)Analizing.num)) > 0)//האם המילה נמצאת ברשימת המתחילות, האם היא מספר, האם היא שם
                return true;
            return false;
        }
        /// <summary>
        /// החזרת הרצף (פיזמון) הגדול ביותר בשיר
        /// </summary>
        /// <param name="str">השיר</param>
        /// <returns>הפזמון הראשי בשיר</returns>
        public static string longestWords(string str)
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
        /// <returns> החזרת תת פיזמון לאינדקס הרצוי מהשיר</returns>
        public static string longestWordsLevelIndex(string str, int index)
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
        ///VAVCON בדיקה האם האות וו היא פותחת או מחברת, שאז ישתנה הנתוח ל
        /// </summary>
        /// <param name="tags">תגיות השיר</param>
        /// <param name="index">מספר התגית</param>
        /// <param name="j">מיקום המילה בתגית</param>
        /// <returns>רשימת התגיות המעודכנת לכל המילים עם וו מחברת</returns>
        public static List<Tag> isVavCon(List<Tag> tags)
        {
            for (int i = 1; i < tags.Count - 1; i++)
            {
                long isVav = (long)Analizing.vav & (long)tags[i].InformTags[0].Ana;//אם יש במילה את וו החבור
                                                                                   //(long)tags[i-1].InformTags[0].Ana;//אם יש במילה את וו החבור
                if (isVav > 0)//האם יש במילה את וו החבור
                {
                    if ((((long)Analizing.first & (long)tags[i - 1].InformTags[0].Ana) > 0) && (((long)Analizing.first & (long)tags[i].InformTags[0].Ana) > 0) || (((long)Analizing.third & (long)tags[i - 1].InformTags[0].Ana) > 0) && (((long)Analizing.third & (long)tags[i].InformTags[0].Ana) > 0) || (((long)Analizing.second & (long)tags[i - 1].InformTags[0].Ana) > 0) && (((long)Analizing.second & (long)tags[i].InformTags[0].Ana) > 0))//אם יש זהות בין 2 המילים שביניהן וו החבור (גוף ראשון זהה שני או שלישי
                    {
                        if (((((long)Analizing.paul & (long)tags[i - 1].InformTags[0].Ana) > 0) && ((long)Analizing.paul & (long)tags[i].InformTags[0].Ana) > 0) || ((((long)Analizing.doo & (long)tags[i - 1].InformTags[0].Ana) > 0) && ((long)Analizing.doo & (long)tags[i].InformTags[0].Ana) > 0) || ((((long)Analizing.past & (long)tags[i - 1].InformTags[0].Ana) > 0) && ((long)Analizing.past & (long)tags[i].InformTags[0].Ana) > 0) || ((((long)Analizing.present & (long)tags[i - 1].InformTags[0].Ana) > 0) && ((long)Analizing.present & (long)tags[i].InformTags[0].Ana) > 0) || ((((long)Analizing.future & (long)tags[i - 1].InformTags[0].Ana) > 0) && ((long)Analizing.future & (long)tags[i].InformTags[0].Ana) > 0) || ((((long)Analizing.makor & (long)tags[i - 1].InformTags[0].Ana) > 0) && ((long)Analizing.makor & (long)tags[i].InformTags[0].Ana) > 0) || ((((long)Analizing.adverb & (long)tags[i - 1].InformTags[0].Ana) > 0) && ((long)Analizing.adverb & (long)tags[i].InformTags[0].Ana) > 0))//אם יש זהות בין המילים בפועל, תואר הפועל, או בזמן
                        {
                            long tag = ((long)Analizing.vav);
                            tag = ~tag;//יש לבטל את הנתוח של האות וו הרגילה
                            tags[i].InformTags[0].Ana = (Analizing)((long)tags[i].InformTags[0].Ana & tag);
                            tags[i].InformTags[0].Ana = (Analizing)((long)Analizing.vavCon | (long)tags[i].InformTags[0].Ana);//ולהפוך אותו לניתוח של וו החבור שאינה מפרידה בין המשפטים
                        }
                    }
                    else
                    {
                        if ((((long)Analizing.name & (long)tags[i - 1].InformTags[0].Ana) > 0) && (((long)Analizing.name & (long)tags[i].InformTags[0].Ana) > 0) || (((long)Analizing.noun & (long)tags[i - 1].InformTags[0].Ana) > 0) && (((long)Analizing.noun & (long)tags[i].InformTags[0].Ana) > 0) || (((long)Analizing.pnoun & (long)tags[i - 1].InformTags[0].Ana) > 0) && (((long)Analizing.pnoun & (long)tags[i].InformTags[0].Ana) > 0) || (((long)Analizing.adverb & (long)tags[i - 1].InformTags[0].Ana) > 0) && (((long)Analizing.adverb & (long)tags[i].InformTags[0].Ana) > 0))//אם יש זהות בתחום כמו: שם ,מקום, תואר הפועל
                        {
                            long tag = ((long)Analizing.vav);
                            tag = ~tag;//יש לבטל את הנתוח של האות וו הרגילה
                            tags[i].InformTags[0].Ana = (Analizing)((long)tags[i].InformTags[0].Ana & tag);
                            tags[i].InformTags[0].Ana = (Analizing)((long)Analizing.vavCon | (long)tags[i].InformTags[0].Ana);//ולהפוך אותו לניתוח של וו החבור שאינה מפרידה בין המשפטים
                        }

                    }
                }
            }

            return tags;
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
            song = output;
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
        /// <returns>רשימת תגיות סופיות מאותחלת לפי תגית תגית</returns>
        public static List<Tag> getWords(List<List<DWord>> tagits)
        {
            List<Tag> LTags = new List<Tag>();
            int i = 0;
            foreach (var item in tagits)
            {

                LTags.Add(new Tag(tagits[i][0].Word, tagits[i])); i++;
            }
            return LTags;
        }

        /// <summary>
        /// התנאי if(word1=f1/f2)&(word2!=f1&f2) אמת -- 
        /// </summary>
        /// 
        /// <param name="f1"></param>
        /// <param name="f2"></param>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <param name="word1"></param>
        /// <param name="word2"></param>
        /// <returns>האם מילות הפזמון תואמות למבנה השיר ולמקום המילים </returns>
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
        /// <returns>החזרת הרשימה המעודכנת לפי לכידת מילים רציפות</returns>
        public static List<Tag> firstJoiner(List<Tag> tagits, int index, out int minus, string firstWord1, string fistWord2, string lastWord1, string lastWord2, string timeIteration)
        {
            //בדיקת רציפות ראשונה ובסיסית, לא כוללנית. שימי לב בעקר לליכוד מילים כפולות ורב מילות הקישור אך לא כולן כי הן כבר בתוככי התגית
            int minus_ = 0;//משתנה אאוט מאותחל רק פעם אחת, לכן מספר השנויים נשמר ורק לבסוף מאותחל האווט שיעודכן לפיו
                           //
            if (index < 0 || index + 1 > tagits.Count)//אם התקבלו ערכים לא תקינים, אינדקס לא קיים
            {
                minus = 0;
                return tagits;
            }
            List<DWord> prev = null, current = null, next = null;

            if (index != 0 && index + 1 != tagits.Count)//אם התגית לא ראשונה/אחרונה 
            {
                prev = tagits[index - 1].InformTags;//תגית קודמת 
                next = tagits[index + 1].InformTags;//תגית הבאה
                current = tagits[index].InformTags;//תגית נוכחית




                if (next[0].Word == current[current.Count - 1].Word || getListStart(current[current.Count - 1].Word) == true)//אם המילה שפותחת את התגית הבאה שווה למילה האחרונה  או שהמילה שמסיימת את התגית היא פותחת  בתגית זו
                {
                    tagits = joinTagBackward(tagits, index + 1, timeIteration);//זמון פונקציה לצירוף התגית הבאה לתגית זו

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
                        tagits = joinTagBackward(tagits, index, timeIteration);//זמון פונקציה לצירוף התגית הקודמת לתגית זו
                        minus_++;//בוצע שנוי אחד כרגע
                    }
                }


                long result2 = (long)Analizing.nismach;//ניסמך

                long result = (long)Analizing.vavCon | (long)Analizing.mem | (long)Analizing.shin | (long)Analizing.he | (long)Analizing.lamed | (long)Analizing.bet | (long)Analizing.kaf;//הביטים של אותיות משה לב  או וו החבור דלוקים
                result = result & (long)current[0].Ana;//חבור לוגי, האם המילה שפותחת את התגית מכילה אותיות משה לב

                if (getListMiddle(current[0].Word) == true || result > 0 || getListMiddle(prev[prev.Count - 1].Word) || ((long)prev[prev.Count - 1].Ana & result2) > 0)//אם המילה הראשונה שבתגית מכילה אותיות משה לב או שהיא אחת מהמילים הקימות באמצע תגית, או שהמילה שלפניה היא נסמך/חייבת להיות באמצע תגית
                {

                    result = (long)Analizing.vavCon | (long)Analizing.mem | (long)Analizing.shin | (long)Analizing.he | (long)Analizing.lamed | (long)Analizing.bet | (long)Analizing.kaf;//הביטים של אותיות משה לב  או וו החבור דלוקים
                    result = result & (long)next[0].Ana;//חבור לוגי, האם המילה שפותחת את התגית מכילה אותיות משה לב
                    if (((index + 1) < tagits.Count()) && (getListMiddle(current[current.Count - 1].Word) == true || getListMiddle(next[0].Word) || ((long)current[current.Count - 1].Ana & result2) > 0 || result > 0))//אם גם המילה האחרונה שבתגית מכילה מילה שחיבת להיות בסוף תגית או מילה נסמכת
                                                                                                                                                                                                                        //וגם זו אינה תגית אחרונה
                    {

                        tagits = joinTagAround(tagits, index);//צרף את התגית מסביבה 
                        minus_ += 2;//התחוללו שני שנויים

                    }
                    else
                    {
                        tagits = joinTagBackward(tagits, index, timeIteration);//צרף את התגית לתגית הקודמת 
                        minus_++;//התחולל שנוי אחד
                    }


                }


            }
            else//האינדקס ראשון או אחרון
            {
                if (index == 0 && tagits.Count > 1)//אם התגית ראשונה
                {
                    next = tagits[index + 1].InformTags;//תגית הבאה
                    current = tagits[index].InformTags;//תגית נוכחית




                    if (next[0].Word == current[current.Count - 1].Word || getListStart(current[current.Count - 1].Word) == true)//אם המילה שפותחת את התגית הבאה שווה למילה האחרונה  או שהמילה שמסיימת את התגית היא פותחת  בתגית זו
                    {
                        tagits = joinTagBackward(tagits, index + 1, timeIteration);//זמון פונקציה לצירוף התגית הבאה לתגית זו

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

                if (index == tagits.Count - 1 && tagits.Count > 1)//אם התגית אחרונה
                {
                    prev = tagits[index - 1].InformTags;//תגית קודמת
                    current = tagits[index].InformTags;//תגית נוכחית




                    if (getListStart(current[current.Count - 1].Word) == true)//אם המילה שפותחת את התגית הבאה שווה למילה האחרונה  או שהמילה שמסיימת את התגית היא פותחת  בתגית זו
                    {
                        tagits = joinTagBackward(tagits, index + 1, timeIteration);//זמון פונקציה לצירוף התגית הבאה לתגית זו

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
                            tagits = joinTagBackward(tagits, index, timeIteration);//זמון פונקציה לצירוף התגית הקודמת לתגית זו
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
                            tagits = joinTagBackward(tagits, index, timeIteration);//צרף את התגית לתגית הקודמת 
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
        /// <param name="LTags">רשימת התגיות הסופית</param>
        /// <param name="index">אינדקס</param>
        /// <param name="minus">מספר השנויים</param>
        /// <param name="firstWord1">מילה פותחת פזמון ראשי</param>
        /// <param name="firstWord2">מילה פותחת פזמון משני</param>
        /// <param name="lastWord1">מילה חותמת פזמון ראשי</param>
        /// <param name="lastWord2">מילה חותמת פזמון משני</param>
        /// <returns>החזרת הרשימה המעודכנת לפי לכידת מילים בודדות, לכידה ראשונית.</returns>
        public static List<Tag> secondJoinerNotAlone(List<Tag> LTags, int index, out int minus, string firstWord1, string firstWord2, string lastWord1, string lastWord2)
        {
            //בדיקת רציפות ראשונה ובסיסית, לא כוללנית. שימי לב בעקר לליכוד מילים כפולות ורב מילות הקישור אך לא כולן כי הן כבר בתוככי התגית

            if (index < 0 || index + 1 > LTags.Count)//אם התקבלו ערכים לא תקינים, אינדקס לא קיים
            {
                minus = 0;
                return LTags;
            }

            List<DWord> prev = null, current = null, next = null;
            bool flagCanOpen = false;
            bool flagCanShut = false;
            if (index != 0 && index != LTags.Count - 1)//אם התגית לא ראשונה/אחרונה 
            {

                prev = LTags[index - 1].InformTags;//תגית קודמת 
                next = LTags[index + 1].InformTags;//תגית הבאה
                current = LTags[index].InformTags;//תגית נוכחית

                if (current.Count != 1)//המילה אינה בודדה
                {
                    minus = 0;
                    return LTags;
                }
                //המילה אכן בודדה
                {
                    if (next.Count > 1)//בודדה ואחריה רצף
                    {
                        flagCanOpen = isPizmonRight(firstWord1, firstWord2, lastWord1, lastWord2, current[0].Word, next[0].Word);//בדיקה נוספת לתנאי ששורה מתחת:
                        if (flagCanOpen && getListMayStart(current[0].Word) && (mustStart(next[0].Word, next[0].Ana) == false))//המילה הנוכחית יכולה לפתוח והבאה לא חיבת לפתוח
                        {
                            joinTagBackward(LTags, index, "");//התלכדות עם התגית הבאה
                            minus = 1;
                            return LTags;
                        }
                        else//המילה הנוכחית לא חיבת לפתח או שהבאה חיבת לפתח
                        {
                            flagCanShut = isPizmonRight(firstWord1, firstWord2, lastWord1, lastWord2, current[0].Word, prev[prev.Count - 1].Word);//בדיקה נוספת לתנאי ששורה מתחת:
                            if (getListMayEnd(current[0].Word) && getListEnd(prev[prev.Count - 1].Word) == false && flagCanShut)//אם יכולה לסגר והמילה שלפניה בתגית הקודמת לא חיבת לסגר
                            {
                                joinTagForward(LTags, index);
                                minus = 0;
                                return LTags;
                                //התלכדות עם התגית הקודמת
                            }

                            else//ברירת מחדל, אם המילה לא מקבוצת פותחות, סוגרות, בהתאם לבדיקות. - התלכדות לתגית הארוכה יותר
                            {
                                if (prev.Count >= next.Count)
                                {
                                    joinTagBackward(LTags, index, "");
                                    minus = 1;
                                    return LTags;
                                    //התלכדות עם התגית הקודמת
                                }
                                else
                                {
                                    joinTagForward(LTags, index);
                                    minus = 0;
                                    return LTags;
                                    //התלכדות עם התגית הבאה
                                }

                            }
                        }
                    }

                    else//יש אחריה בודדות
                    {
                        int i = 0;
                        int count = 1;
                        for (i = index + 1; i < LTags.Count - 2 && (LTags[i].InformTags.Count < 2); i++)//ספירת מילים בודדות
                        {
                            count++;
                            //להסתכל אם נכנס לתנאי הפור...
                            if (mustStart(LTags[i].InformTags[0].Word, LTags[i].InformTags[0].Ana) || LTags[i].InformTags[0].Word == firstWord1 || LTags[i].InformTags[0].Word == firstWord2)
                            //אם המילה חיבת לפתח (פותחת פזמונים או מהרשימה). הלכתי לישון, סמיכות ורציפות כבר התלכד

                            {
                                // אם מדובר ב1 התלכדות לאחרונה
                                if (count == 2)
                                {
                                    LTags = joinTagBackward(LTags, index, "");
                                    minus = 1;
                                    return LTags;
                                }

                                else//סגירת כל מי שהיו עד עכשיו לתגית,
                                {
                                    for (int j = index; j < count - 1; j++)
                                    {
                                        LTags = joinTagForward(LTags, index);
                                    }
                                    minus = 0;
                                    return LTags;
                                }


                            }

                            // אם מדובר ב1 התלכדות לתגית הקצרה יותר
                            if (count == 2)
                            {
                                if (next.Count >= prev.Count)
                                {
                                    LTags = joinTagBackward(LTags, index, "");
                                    minus = 1;
                                    return LTags;
                                }
                                else
                                {
                                    LTags = joinTagForward(LTags, index);
                                    minus = 0;
                                    return LTags;
                                }

                            }



                            if (getListEnd(LTags[i].InformTags[0].Word) || LTags[i].InformTags[0].Word == lastWord1 || LTags[i].InformTags[0].Word == lastWord2)//יש מילה שחיבת לסגר, התלכדות כולל היא
                            {

                                // התלכדות כולל היא לתגית
                                for (int j = index; j < count; j++)
                                {
                                    LTags = joinTagForward(LTags, index);
                                }
                                minus = 0;
                                return LTags;
                            }




                        }
                        LTags = joinTagBackward(LTags, index, "" + index);//אם יש מילה אחת אחריו, התלכדות. הרי לא נכנס ללולאה בכלל
                        minus = 1;
                        return LTags;
                    }

                }
            }
            else
            {
                if (index == 0 && LTags.Count > 1)//כאשר המילה הבודדה מופיעה בתגית הראשונה
                {
                    LTags = joinTagForward(LTags, index);//מילה ראשונה לא בודדה לעולם

                }
                if (index == LTags.Count - 1 && LTags.Count > 1)//כשהמילה הבודדה הנה התגית המסימת את השיר
                {
                    LTags = joinTagBackward(LTags, index, "" + index);//מילה אחרונה לא בודדה לעולם
                }
            }
            minus = 0;
            return LTags;
        }

        /// <summary>
        /// הפרדת 'והיא' מתגיות 'ו
        /// </summary>
        /// <param name="LTags">רשימת תגיות סופית</param>
        /// <param name="index">האינדקס</param>
        /// <param name="minus">מספר השנויים</param>
        /// <param name="firstWord1">מילה פותחת פזמון ראשי</param>
        /// <param name="firstWord2">מילה פותחת פזמון משני</param>
        /// <param name="lastWord1">מילה חותמת פזמון ראשי</param>
        /// <param name="lastWord2">מילה חותמת פזמון משני</param>
        /// <returns>החזרת רשימת התגיות המעודכנת </returns>
        public static List<Tag> thirdSplit(List<Tag> LTags, int index, out int minus, string firstWord1, string firstWord2, string lastWord1, string lastWord2)
        {
            //בדיקת רציפות ראשונה ובסיסית, לא כוללנית. שימי לב בעקר לליכוד מילים כפולות ורב מילות הקישור אך לא כולן כי הן כבר בתוככי התגית

            if (index < 0 || index + 1 > LTags.Count)//אם התקבלו ערכים לא תקינים, אינדקס לא קיים
            {
                minus = 0;
                return LTags;
            }



            List<DWord> current = LTags[index].InformTags;//תגית נוכחית
            for (int i = 0; i < current.Count; i++)
            {
                if (getListSplit(current[i].Word) == true || ((long)Analizing.vav & (long)current[i].Ana) > 0)
                {
                    LTags = splitTagInJPoint(LTags, index, i);

                    if (i == 1 && index != 0)//כאשר התגית התפצלה החל מאינדקס אחד ונותרה מילה בודדה
                    {
                        LTags = joinTagBackward(LTags, i, "");//יש לאחדה עם התגית הקודמת
                        minus = 1;//לא חובה
                    }
                    else
                        minus = 0;
                    return LTags;
                }
            }
            minus = 0;
            return LTags;
        }

        /// <summary>
        /// איחוד התגית לתגית הבאה ללא תקינויות
        /// </summary>
        /// <param name="LTags">רשימת תגיות סופית</param>
        /// <param name="index">האינדקס</param>
        /// <returns>רשימת התגיות המעודכנת לפי איחוד לתגית הבאה</returns>
        ///
        public static List<Tag> joinTagForward(List<Tag> LTags, int index)
        {

            LTags[index].FullTag += " " + LTags[index + 1].FullTag;//שרשור התגית הבאה לתגית הזו
            foreach (DWord i in LTags[index + 1].InformTags)//העתקת נתוחי כל מילות התגית הבאה לרשימת נתוחי התגית הזו
            {
                LTags[index].InformTags.Add(new DWord(i.Ana, i.Word));
            }
            LTags.RemoveAt(index + 1);//הסרת התגית המיותרת

            return LTags;


        }

        /// <summary>
        /// איחוד התגית עם התגית שלפניה ושלאחריה ללא תקינויות
        /// </summary>
        /// <param name="LTags">רשימת תגיות סופית</param>
        /// <param name="index">אינדקס</param>
        /// <returns>רשימת התגיות הסופיות מעודכנת עם איחוד התגית סביבה</returns>
        public static List<Tag> joinTagAround(List<Tag> LTags, int index)
        {
            LTags[index].FullTag = LTags[index - 1].FullTag + " " + LTags[index].FullTag + " " + LTags[index + 1].FullTag;//השמת שרשור התגית הקודמת, הנוכחית והבאה לתגית הנוכחית
            List<DWord> cloneList = LTags[index].InformTags.GetRange(0, LTags[index].InformTags.Count);//עותק של ניתוחי התגית הנוכחית
            LTags[index].InformTags = new List<DWord>();//איפוס רשימת הניתוחים הנוכחית
            foreach (DWord i in LTags[index - 1].InformTags)//הוספת נתוחי התגית הקודמת לרשימת הנתוחים הנוכחית הריקה
            {
                LTags[index].InformTags.Add(new DWord(i.Ana, i.Word));
            }
            foreach (DWord i in cloneList)//הוספת נתוחי התגית הנוכחית לרשימה 
            {
                LTags[index].InformTags.Add(new DWord(i.Ana, i.Word));
            }
            foreach (DWord i in LTags[index + 1].InformTags)//הוספת נתוחי התגית הבאה לרשימה
            {
                LTags[index].InformTags.Add(new DWord(i.Ana, i.Word));
            }
            LTags.RemoveAt(index + 1);//הסרת התגית הבאה
            LTags.RemoveAt(index - 1);//הסרת התגית הקודמת
            return LTags;

        }
        /// <summary>
        /// איחוד התגית עם התגית שלפניה ללא תקינויות
        /// </summary>
        /// <param name="LTags"></param>
        /// <param name="index"></param>
        /// <returns>רשימת התגיות הסופיות לאחר איחוד עם קודמתה </returns>
        public static List<Tag> joinTagBackward(List<Tag> LTags, int index, string timeIteration)

        {

            LTags[index].FullTag = LTags[index - 1].FullTag + " " + LTags[index].FullTag;//שרשור תגית נוכחית לתגית הקודמת
            List<DWord> cloneList = LTags[index].InformTags.GetRange(0, LTags[index].InformTags.Count);//יצירת עותק מנתוחי התגית הנוכחית
            LTags[index].InformTags = new List<DWord>();//איפוס רשימת הנתוחים של התגית הנוכחית
            foreach (DWord i in LTags[index - 1].InformTags)//השמת רשימת נתוחי התגית הקודמת לרשימת התגיות הנוכחית הריקה
            {
                LTags[index].InformTags.Add(new DWord(i.Ana, i.Word));
            }
            foreach (DWord i in cloneList)//שרשור רשימת נתוחי תגית נוכחית לרשימת נתוחי התגית הקודמת
            {
                LTags[index].InformTags.Add(new DWord(i.Ana, i.Word));
            }
            LTags.RemoveAt(index - 1);
            return LTags;
        }

        /// <summary>
        /// פיצול תגית במיקום אינדקס עד ג'יי ומג'יי תגית חדשה
        /// </summary>
        /// <param name="LTags">רשימת התגיות הסופית</param>
        /// <param name="index">האינדקס</param>
        /// <param name="j">נקודת הפיצול</param>
        /// <returns>רשימת התגיות המעודכנת והמפוצלת בנקודה ג'יי</returns>
        public static List<Tag> splitTagInJPoint(List<Tag> LTags, int index, int j)
        {
            int wordsTagCount = LTags[index].InformTags.Count;//מספר המילים בתגית
            if (j > 0 && j < wordsTagCount - 1)//המילה אינה הראשונה/האחרונה בתגית
            {
                {
                    int i;
                    string newNext = LTags[index].FullTag;//טקסט כלל התגית
                    string newFirst = "", newSecond = "";
                    String[] strgs = newNext.Split(' ');//מילות התגית במערך
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
                    LTags[index].FullTag = newFirst;//השמת התגית העדכנית במיקום הנכון

                    List<DWord> tagits = LTags[index].InformTags.GetRange(j, LTags[index].InformTags.Count - j);// שרשור הניתוח של שאר המילים-לתגית הבאה
                    LTags.Insert(index + 1, new Tag(newSecond, tagits));//השמת ערכי התגית הבאה וניתוחה
                    LTags[index].InformTags.RemoveRange(j, LTags[index].InformTags.Count - j);//השמת הניתוח ללא הניתוחים המיותרים שהועברו לתגית הבאה
                }

            }

            else//המילה ראשונה או אחרונה בתגית
            {
                if (j == wordsTagCount - 1 && index != LTags.Count - 1)//המילה היא אחרונה בתגית ויש תגית אחר כך
                {
                    LTags = joinTagForward(LTags, j);//הוספת המילה קדימה לתגית הבאה
                }

                if ((index == LTags.Count - 1) && (j == wordsTagCount))// אם אחרונה ללא תגית אחריה
                {
                    //אין ענין לפתח תגית על מילה אחת





                }
                //המילה ראשונה בתגית = תווצר תגית בודדה, אך אין לעשות זאת במקרה של תגית ראשונה
                if (j == 0 && index != 0)
                {
                    int i;
                    string newNext = LTags[index].FullTag;//טקסט כלל התגית
                    string newFirst = "", newSecond = "";
                    String[] strgs = newNext.Split(' ');//מילות התגית במערך

                    newFirst += strgs[0];


                    for (i = 1; i < strgs.Length; i++)//שרשור התגית עד הסוף
                    {
                        newSecond += strgs[i] + " ";
                    }
                    newSecond = newSecond.Substring(0, newSecond.Length - 1);//הסרת הרווח המיותר האחרון
                    LTags[index].FullTag = newFirst;//השמת התגית הבודדה במיקום הנכון

                    List<DWord> tagits = LTags[index].InformTags.GetRange(j, LTags[index].InformTags.Count - j);// שרשור הניתוח של שאר המילים-לתגית הבאה
                    LTags.Insert(index + 1, new Tag(newSecond, tagits));//השמת ערכי התגית הבאה וניתוחה
                    LTags[index].InformTags.RemoveRange(j, LTags[index].InformTags.Count - j);//השמת הניתוח ללא הניתוחים המיותרים שהועברו לתגית הבאה
                }
            }
            return LTags;
        }

        /// <summary>
        /// הרצת פיצול התגיות לשיר: ניתוח התגיות ופונקציות איחוד ופיצול
        /// </summary>
        /// <param name="song">השיר</param>
        /// <returns>רשימת התגיות מפוצלות</returns>
        public static List<Tag> start(string song1)
        {
            int i = 3999;
            string song = cleanSong(song1);//טהרת השיר מתווים אסורים ומרווחים מיותרים 
            string firstWordDitty1, firstWordDitty2, lastWordDitty1, lastWordDitty2;

            dittyWords(song, out firstWordDitty1, out firstWordDitty2, out lastWordDitty1, out lastWordDitty2);//קבלת מילים פותחות וסוגרות עבור שני הפזמונים הראשיים
            List<List<DWord>> a = getMorphList(song);//הפעלת ניתוח של הספריה לפי האינם שלי
            List<Tag> b = getWords(a);//המרת הערכים לרשימת תגיות
            b = isVavCon(b);//זמון פונקציה למציאת וו החיבור שלא מפרידה

            int minus;
            for (i = 0; i < b.Count; i++)//איגוד תגיות של מילים מאחדות
            {

                b = firstJoiner(b, i, out minus, firstWordDitty1, firstWordDitty2, lastWordDitty1, lastWordDitty2, "firstjoiner" + i);

                if (i > 0)
                {
                    i -= minus;
                }
            }

            int minus0 = 0;

            for (i = 0; i < b.Count; i++)//איגוד של מילים בודדות
            {
                b = secondJoinerNotAlone(b, i, out minus0, firstWordDitty1, firstWordDitty2, lastWordDitty1, lastWordDitty2);
                if (i > 0)
                {
                    i -= minus0;
                }

            }

            int min = 0;
            int count = 0;
            for (i = 0; i < b.Count; i++)//פיצול תגיות במקום בו אפשר, ואם אי אפשר והתגית מעל 8 תווים, גמישות עד 10 ואז חתך שרירותי
            {
                count++;

                b = thirdSplit8Letters(b, i, out min, firstWordDitty1, firstWordDitty2, lastWordDitty1, lastWordDitty2);
                if (min == 1 && i > 0)
                {
                    i--;
                }
            }
            return b;



        }

        /// <summary>
        /// טיפול בתגיות ארוכות מדי
        /// </summary>
        /// <param name="LTags">רשימת התגיות</param>
        /// <param name="index">אינדקס</param>
        /// <param name="minus">מספר השנויים</param>
        /// <param name="firstWord1">מילה פותחת פזמון ראשי</param>
        /// <param name="fistWord2">מילה פותחת פזמון משני</param>
        /// <param name="lastWord1">מילה חותמת פזמון ראשי</param>
        /// <param name="lastWord2">מילה חותמת פזמון משני</param>
        /// <returns>רשימת התגיות המעודכנת לאחר פיצול ארוכות</returns>
        public static List<Tag> thirdSplit8Letters(List<Tag> LTags, int index, out int minus, string firstWord1, string firstWord2, string lastWord1, string lastWord2)
        {

            int c = 0;
            //בדיקת רציפות ראשונה ובסיסית, לא כוללנית. שימי לב בעקר לליכוד מילים כפולות ורב מילות הקישור אך לא כולן כי הן כבר בתוככי התגית

            if (index < 0 || index + 1 > LTags.Count)//אם התקבלו ערכים לא תקינים, אינדקס לא קיים
            {
                minus = 0;
                return LTags;
            }

            List<DWord> prev = null, current = null;


            {
                current = LTags[index].InformTags;//תגית נוכחית

                if (current.Count < 8)//התגית עד 8 מילים
                {
                    minus = 0;
                    return LTags;
                }
                else//יש מעל 8 מילים בתגית
                {
                    if (index != 0)//אם יש תגית קודמת
                    {
                        prev = LTags[index - 1].InformTags;//תגית קודמת 
                    }

                    int numWords = current.Count;
                    int i = 0;
                    int l = current.Count;
                    //אם הגעתי לשלב זה, ולא גמרתי לסרק: סימן שיש לי תגית בת למעלה מ8 תגים שלא הופרדה
                    if ((l >= 9) && (l <= 16))
                    {
                        if (l % 2 == 0)
                            c = l / 2;
                        else
                            c = (l - 1) / 2;

                        for (i = c; i > 1; i--)//נסיון לפצל את התגית מהחתך עד המילה השניה בתגית בסדר יורד
                        {
                            if (((i != current.Count - 1
                                && current[i].Word != current[i + 1].Word) || (i == current.Count - 1))
                                && ((long)current[i].Ana & (long)Analizing.nismach) == 0
                                && ((long)current[i].Ana & (long)Analizing.doo) == 0
                                && current[i].Word != firstWord1 && current[i].Word != firstWord2
                                && getListStart(current[i].Word) != true
                                && getListMiddleMust(current[i].Word) != true
                                && current[i + 1].Word != lastWord1
                                && current[i + 1].Word != lastWord2
                                && getListEnd(current[i + 1].Word) != true
                                && getListMiddleMust(current[i + 1].Word) != true)
                                // אם המילה לא פותחת פזמון/חיבת אמצע/כפולה פעמיים/צווי/נסמך ושהמילה הבאה אינה חיבת להיות באמצע, משהוכלב, לא חיבת לסגר ולא בפזמונים סוגרים

                            {
                                LTags = splitTagInJPoint(LTags, index, i);//פיצול התגית
                                minus = 0;
                                return LTags;
                            }
                        }
                        //הבדיקה הבאה בודקת אם אפשר להצטרף לתגית הקודמת
                        // פותחת פזמון/ חיבת אמצע / כפולה פעמיים / צווי / נסמך ושהמילה הבאה אינה חיבת להיות באמצע, משהוכלב, לא חיבת לסגר ולא בפזמונים סוגרים
                        if (
                            index != 0
                            && (prev.Count <= 8)
                            && ((i != current.Count - 1
                            && current[i].Word != current[i + 1].Word) || (i == current.Count - 1))
                            && ((long)current[i].Ana & (long)Analizing.nismach) == 0
                            && ((long)current[i].Ana & (long)Analizing.doo) == 0
                            && current[i].Word != firstWord1 && current[i].Word != firstWord2
                            && getListStart(current[i].Word) != true
                            && getListMiddleMust(current[i].Word) != true
                            && current[i + 1].Word != lastWord1
                            && current[i + 1].Word != lastWord2
                            && getListEnd(current[i + 1].Word) != true
                            && getListMiddleMust(current[i + 1].Word) != true
                            )// אם המילה לא פותחת פזמון/חיבת אמצע/כפולה פעמיים/צווי/נסמך ושהמילה הבאה אינה חיבת להיות באמצע, משהוכלב, לא חיבת לסגר ולא בפזמונים 

                        {
                            LTags = splitTagInJPoint(LTags, index, i + 1);
                            LTags = joinTagBackward(LTags, index, "");
                            minus = 1;
                            return LTags;
                        }


                        for (i = c; i < c + 2; i++)//לולא שמנסה לפצל את התגית ב2 המקומות שאחרי נקודת הפיצול, הנחה ל10 תווים
                        {
                            long cc = (long)current[i - 1].Ana;
                            long bb = (long)Analizing.nismach;
                            long dd = cc & bb;
                            if (
                               current[i].Word != current[i - 1].Word && (dd) == 0
                               && ((long)current[i - 1].Ana & (long)Analizing.doo) == 0
                               && current[i - 1].Word != firstWord1 && current[i - 1].Word != firstWord2
                               && getListStart(current[i - 1].Word) != true
                               && getListMiddleMust(current[i - 1].Word) != true
                               && current[i].Word != lastWord1
                               && current[i].Word != lastWord2 && getListEnd(current[i].Word) != true
                               && getListMiddleMust(current[i].Word) != true
                              )
                            //אותה תקינות בדיוק כמו בלולאה הקודמת, הפעם עליו ועל הבא אחריו
                            {
                                LTags = splitTagInJPoint(LTags, index, i - 1);//הפיצול
                                minus = 0;
                                return LTags;
                            }
                        }
                        LTags = splitTagInJPoint(LTags, index, c);//אין ברירה-חותכים בבשר החי, למרות שלכאורה אסור לחתך
                        minus = 0;
                        return LTags;

                    }

                    if ((l<=24)&&(l>=17))//מספר המילים בתגית
                    {
                        if (l % 2 == 0)//זוגי
                        {
                            c = l / 3;

                        }
                        else
                        {
                            c = (l - 1) / 3;//אי זוגי

                        }
                        for (i = c; i > 1; i--)//נסיון לפצל את התגית מהחתך עד המילה השניה בתגית בסדר יורד
                        {
                            if (
                                ((i != current.Count - 1
                                && current[i].Word != current[i + 1].Word) || (i == current.Count - 1))
                                && ((long)current[i].Ana & (long)Analizing.nismach) == 0
                                && ((long)current[i].Ana & (long)Analizing.doo) == 0
                                && current[i].Word != firstWord1 && current[i].Word != firstWord2
                                && getListStart(current[i].Word) != true
                                && getListMiddleMust(current[i].Word) != true
                                && current[i + 1].Word != lastWord1 && current[i + 1].Word != lastWord2
                                && getListEnd(current[i + 1].Word) != true
                                && getListMiddleMust(current[i + 1].Word) != true
                               
                                )// אם המילה לא פותחת פזמון/חיבת אמצע/כפולה פעמיים/צווי/נסמך ושהמילה הבאה אינה חיבת להיות באמצע, משהוכלב, לא חיבת לסגר ולא בפזמונים סוגרים
                            {
                                LTags = splitTagInJPoint(LTags, index, i);//פיצול התגית
                                minus = 0;
                                return LTags;
                            }
                        }

                                              if (
                        index != 0
                        && (prev.Count <= 8)
                        && ((i != current.Count - 1
                        && current[i].Word != current[i + 1].Word) || (i == current.Count - 1))
                        && ((long)current[i].Ana & (long)Analizing.nismach) == 0
                        && ((long)current[i].Ana & (long)Analizing.doo) == 0
                        && current[i].Word != firstWord1 && current[i].Word != firstWord2
                        && getListStart(current[i].Word) != true
                        && getListMiddleMust(current[i].Word) != true
                        && current[i + 1].Word != lastWord1 && current[i + 1].Word != lastWord2
                        && getListEnd(current[i + 1].Word) != true
                        && getListMiddleMust(current[i + 1].Word) != true
                         
                           )// אם המילה לא פותחת פזמון/חיבת אמצע/כפולה פעמיים/צווי/נסמך ושהמילה הבאה אינה חיבת להיות באמצע, משהוכלב, לא חיבת לסגר ולא בפזמונים סוגרים
                        {
                            LTags = splitTagInJPoint(LTags, index, i + 1);
                            LTags = joinTagBackward(LTags, index, "");
                            minus = 1;
                            return LTags;
                        }
                        for (i = c; i < c + 2; i++)//לולא שמנסה לפצל את התגית ב2 המקומות שאחרי נקודת הפיצול, הנחה ל10 תווים
                        {
                            long cc = (long)current[i - 1].Ana;
                            long bb = (long)Analizing.nismach;
                            long dd = cc & bb;

                            if (
                                current[i].Word != current[i - 1].Word && (dd) == 0
                                && ((long)current[i - 1].Ana & (long)Analizing.doo) == 0
                                && current[i - 1].Word != firstWord1 && current[i - 1].Word != firstWord2
                                && getListStart(current[i - 1].Word) != true
                                && getListMiddleMust(current[i - 1].Word) != true
                                && current[i].Word != lastWord1
                                && current[i].Word != lastWord2 && getListEnd(current[i].Word) != true
                                && getListMiddleMust(current[i].Word) != true
                               //&&((long)current[i].Ana & (long)Analizing.mem) == 0 
                               //&& ((long)current[i].Ana & (long)Analizing.shin) == 0 
                               //&& ((long)current[i].Ana & (long)Analizing.he) == 0 
                               //&& ((long)current[i].Ana & (long)Analizing.kaf) == 0
                               //&&((long)current[i].Ana & (long)Analizing.lamed) == 0 
                               // && ((long)current[i].Ana & (long)Analizing.vavCon) == 0 
                               // && ((long)current[i - 1].Ana & (long)Analizing.vav) == 0
                               )

                            {
                                LTags = splitTagInJPoint(LTags, index, i - 1);//הפיצול
                                minus = 0;
                                //Console.WriteLine("-----------------------------------------------------------------------------------");
                                return LTags;
                            }
                        }
                        LTags = splitTagInJPoint(LTags, index, c);//אין ברירה-חותכים בבשר החי, למרות שלכאורה אסור לחתך
                        minus = 0;
                        return LTags;
                    }

                    if (l > 24)
                    {
                        if (l % 2 == 0)
                        {
                            c = l / 4;

                        }
                        else
                        {
                            c = (l - 1) / 4;
                        }

                        for (i = c; i > 1; i--)//נסיון לפצל את התגית מהחתך עד המילה השניה בתגית בסדר יורד
                        {
                            if (
                                                            ((i != current.Count - 1
                                                            && current[i].Word != current[i + 1].Word) || (i == current.Count - 1))
                                                            && ((long)current[i].Ana & (long)Analizing.nismach) == 0
                                                            && ((long)current[i].Ana & (long)Analizing.doo) == 0
                                                            && current[i].Word != firstWord1
                                                            && current[i].Word != firstWord2
                                                            && getListStart(current[i].Word) != true
                                                            && getListMiddleMust(current[i].Word) != true
                                                            && current[i + 1].Word != lastWord1
                                                            && current[i + 1].Word != lastWord2
                                                            && getListEnd(current[i + 1].Word) != true
                                                            && getListMiddleMust(current[i + 1].Word) != true
                                                            )// אם המילה לא פותחת פזמון/חיבת אמצע/כפולה פעמיים/צווי/נסמך ושהמילה הבאה אינה חיבת להיות באמצע, משהוכלב, לא חיבת לסגר ולא בפזמונים סוגרים



                            {
                                LTags = splitTagInJPoint(LTags, index, i);//פיצול התגית
                                minus = 0;
                                return LTags;
                            }
                        }


                        if (
                            index != 0 && (prev.Count <= 8)
                            && ((i != current.Count - 1
                            && current[i].Word != current[i + 1].Word) || (i == current.Count - 1))
                            && ((long)current[i].Ana & (long)Analizing.nismach) == 0
                            && ((long)current[i].Ana & (long)Analizing.doo) == 0
                            && current[i].Word != firstWord1 && current[i].Word != firstWord2
                            && getListStart(current[i].Word) != true
                            && getListMiddleMust(current[i].Word) != true
                            && current[i + 1].Word != lastWord1
                            && current[i + 1].Word != lastWord2
                            && getListEnd(current[i + 1].Word) != true
                            && getListMiddleMust(current[i + 1].Word) != true
                            )// אם המילה לא פותחת פזמון/חיבת אמצע/כפולה פעמיים/צווי/נסמך ושהמילה הבאה אינה חיבת להיות באמצע, משהוכלב, לא חיבת לסגר ולא בפזמונים סוגרים
                        {
                            LTags = splitTagInJPoint(LTags, index, i + 1);
                            LTags = joinTagBackward(LTags, index, "");
                            minus = 1;
                            return LTags;
                        }

                        for (i = c; i < c + 2; i++)//לולא שמנסה לפצל את התגית ב2 המקומות שאחרי נקודת הפיצול, הנחה ל10 תווים
                        {
                            if (
                               current[i].Word != current[i - 1].Word
                               && ((long)current[i - 1].Ana & (long)Analizing.nismach) == 0
                               && ((long)current[i - 1].Ana & (long)Analizing.doo) == 0
                               && current[i - 1].Word != firstWord1 && current[i - 1].Word != firstWord2
                               && getListStart(current[i - 1].Word) != true
                               && getListMiddleMust(current[i - 1].Word) != true
                               && current[i].Word != lastWord1
                               && current[i].Word != lastWord2
                               && getListEnd(current[i].Word) != true
                               && getListMiddleMust(current[i].Word) != true
                                )//אותה תקינות בדיוק כמו בלולאה הקודמת, הפעם עליו ועל הבא אחריו

                            {
                                LTags = splitTagInJPoint(LTags, index, i - 1);//הפיצול
                                minus = 0;

                                return LTags;
                            }
                        }
                        LTags = splitTagInJPoint(LTags, index, c);//אין ברירה-חותכים בבשר החי, למרות שלכאורה אסור לחתך
                        minus = 0;
                        return LTags;
                    }

                    minus = 0;//FOR DELETE ERRORS
                    return LTags;
                }
            }
        }


        /// <summary>
        /// הרצת פונקצית הפיצול והחזרת התגיות כרשימת מחרוזות
        /// </summary>
        /// <param name="song">השיר</param>
        /// <returns>רשימת התגיות</returns>
        public static List<string> startUp(string song)
        {
            List<string> tags = new List<string>();
            List<Tag> LTags = start(song);
            for (int i = 0; i < LTags.Count; i++)
            {
                tags.Add(LTags[i].FullTag);
            }
            return tags;
        }


        public static void functionMain()
        {

            //TODO fill the password
            HebrewNLP.Password = "OYnM3frqUbLTjFL";

            Console.OutputEncoding = new UTF8Encoding();
            Console.InputEncoding = new UTF8Encoding();

            //   Console.WriteLine(HebrewNLP.Morphology.AnalyzeRequest("שלום",))
            // רשימת השירים האדירה

            string song0 = "נר דולק קופסה נוצה אבא לחפש יצא מה ימצא חבילת מצה יין וביצה כד של חמיצה תפוח ואגוז מצאת דג לכבוד החג לא אבא לא ולא ואל נא תתאמץ כי לא תמצא אתנו פה בבית כל חמץ אינך מאמין חפש  האר היטב באור כל סדק בדוק כל חור עכשיו אמור האם היטבנו ובערנו כל שעור לא אבא לא ולא ואל נא תתאמץ כי לא תמצא אתנו פה בבית כל חמץ אינך מאמין חפש  האר היטב באור כל סדק בדוק כל חור עכשיו אמור האם היטבנו ובערנו כל שעור ";
            string song1 = "אומרים ישנה ארץ ארץ שכורת שמש איה אותה ארץ איפה אותה שמש אומרים ישנה ארץ עמודיה שבעה שבעה כוכבי לכת צצים על כל גבעה איפה אותה ארץ כוכבי אותה גבעה מי ינחנו דרך יגיד לי הנתיבה כבר עברנו כמה מדברות וימים כבר הלכנו כמה כוחותינו תמים כיצד זה תעינו טרם הונח לנו אותה ארץ שמש אותה לא מצאנו ארץ בה יתקיים אשר כל איש קיווה נכנס כל הנכנס פגע בו עקיבא שלום לך עקיבא שלום לך רבי איפה הם הקדושים איפה המכבי עונה לו עקיבא אומר לו הרבי כל ישראל קדושים אתה המכבי אומרים ישנה ארץ ארץ שכורת שמש איה אותה ארץ איפה אותה שמש ";
            string song2 = "איזה פלא איזה פלא מי הילדים האלה רחוצים וחפופים וכל כך כל כך יפים  הן היום בצהרים עוד ראינו את אפרים משחק בבוץ ומים איזה פלא איזה פלא מי הילדים האלה רחוצים וחפופים וכל כך כל כך יפים את ידיה דיני דינה לכלכה בפלסטלינה איזה פלא איזה פלא מי הילדים האלה רחוצים וחפופים וכל כך כל כך יפים חבורה של חמודים לא אותם הילדים אין כאן סוד ואין כאן פלא אם הם חמודים כאלה זה ברור כל בן ובת מתקשטים לכבוד שבת";
            string song3 = "אמא עוצמת עינים ומרימה הידיים  לה מטפחת לבנה אמא אמא את ישנה אמא עוצמת עינים ומרימה הידיים  לה מטפחת לבנה אמא אמא את ישנה אמא עוצמת עינים ומרימה הידיים  לה מטפחת לבנה אמא אמא את ישנה שקט ילדונת הסי בת אמא הדליקה נרות שבת";
            string song4 = "תפקחו את העינים  תסתכלו סביב פה ושם נגמר החרף ונכנס אביב  בשדה ליד הדרך יש כבר דגניות  אל תגידו לי שכל זה לא יכול להיות  אנשים טובים באמצע הדרך  אנשים טובים מאד  אנשים טובים יודעים את הדרך  ואיתם אפשר לצעד  איש אחד קנה לי ספר בן מאה שנה  איש אחר בנה כנור שיש בו מנגינה  ואשה טובה אחרת לי נתנה את שמה  ומאז אני בדרך שרה במקומה  אנשים טובים מאד  אנשים טובים יודעים את הדרך  ואיתם אפשר לצעד  איש אחד יבנה לי גשר כדי לחצות נהר  איש אחר יצמיח יער במורדות ההר  ואשה טובה אחרת  אם יהיה קשה  רק תצביע אל האפק ותבטיח שאנשים טובים מאד  אנשים טובים יודעים את הדרך  ואיתם אפשר לצעד  וממש כמו צמחי הבר הבודדים  הם עוצרים תמיד את החולות הנודדים הרקיע מתבהר וכבר אפשר לראות אנשים על אם הדרך מצפים לראות  אנשים טובים מאד אנשים טובים יודעים את הדרך ואיתם אפשר לצעד ";
            string song5 = "ארץ  ארץ  ארץ  ארץ תכול אין עב  והשמש לה כדבש וחלב  ארץ בה נולדנו ארץ בה נחיה ונשב בה  יהיה מה שיהיה ארץ שנאהב היא לנו אם ואב ארץ של העם ארץ לעולם ארץ בה נולדנו ארץ בה נחיה יהיה מה שיהיה  ארץ  ארץ  ארץ  ים אל מול החוף ופרחים וילדים בלי סוף  בצפון כינרת בדרום חולות ומזרח למערב נושק גבולות  ארץ שנאהב היא לנו אם ואב ארץ של העם ארץ לעולם ארץ בה נולדנו ארץ בה נחיה יהיה מה שיהיה  ארץ  ארץ  ארץ  ארץ התורה את מקור האור ושפת האמונה  ארץ  ארץ  ארץ  ארץ יקרה  הן הבטחת שאין זו אגדה  ארץ שנאהב היא לנו אם ואב ארץ של העם ארץ לעולם ארץ בה נולדנו ארץ בה נחיה יהיה מה שיהיה ";
            string song6 = "גשם בראשית ברוח ובסער וצולל בכרמיאל כי הגשם  כן הגשם  כאן הגשם  כאן הגשם  גשם אל  כי הגשם  כן הגשם  כאן הגשם  גשם אל כאן הגשם  גשם אל  כאן הגשם  גשם אל בוא בהר  בוא בשביל בוא אתי אל הגליל בוא בליל ירח חם ונישן באור הנם בוא בהר  בוא בשביל בוא אתי אל הגליל בוא בליל ירח חם ונישן באור הנם על הדשא הצומח לו ברגבה ומוריק ביחיעם  כי הדשא  כן הדשא  כאן הדשא  כאן הדשא עד עולם כי הדשא  כאן הדשא  כאן הדשא עד עולם כאן הדשא עד עולם  כאן הדשא עד עולם כי הרוח  כן הרוח  כאן הרוח  כאן הרוח רן תרן כי הרוח  כן הרוח  כאן הרוח רן תרן  כאן הרוח רן תרן  כאן הרוח רן תרן כי השמש  כן השמש  כאן השמש  כאן השמש לא תכזיב  כי השמש  כן השמש קח מקל  קח תרמיל  בוא אתי אל הגליל  בוא אתי אל הגליל";
            string song7 = "ואני ראיתי ברוש שנצב בתוך שדה מול פני השמש  בחמסין  בקרה אל מול פני הסערה  על צדו נטה הברוש  לא נשבר את צמרתו הרכין עד עשב  והנה  מול הים  קם הברוש ירק ורם  הנה ברוש  לבדו  מול אש ומים  הנה ברוש  לבדו עד השמים ברוש  לבדו איתן  לו רק נתן ואלמד את דרכו של עץ אחד ואני כמו תינוק שנשבר ולא יכל מול פני השמש בחמסין  בקרה  אל מול פני הסערה  הנה ברוש  לבדו מול אש ומים  הנה ברוש  לבדו  עד השמים ברוש  לבדו איתן  לו רק נתן ואלמד את דרכו של עץ אחד ";
            string song8 = "בשנה הבאה נשב על המרפסת ונספור ציפורים נודדות  ילדים בחופשה ישחקו תופסת בין הבית לבין השדות  עוד תראה  עוד תראה כמה טוב יהיה בשנה  בשנה הבאה  ענבים אדומים יבשילו עד הערב ויוגשו צוננים לשולחן  ורוחות רדומים ישאו אל אם הדרך עיתונים ישנים וענן  עוד תראה  עוד תראה כמה טוב יהיה בשנה  בשנה הבאה בשנה הבאה נפרוש כפות ידיים מול האור הניגר הלבן אנפה לבנה תפרוש באור כנפיים והשמש תזרח בתוכן ";
            string song9 = "גדול כבר אפרים  כמעט בן שנתים לו אבא ואמא  קנו אופנים טפס ועלה לו אפרים אך אוי קצרות הרגליים אמרו אבא אמא נתן לאפרים בבקר בערב ובצהרים פרות ירקות וחלב כוסותיים ואז חת ושתיים יגדל לו אפרים ירכב בכל יום או יומיים אל סבתא שבירושלים אמרו אבא אמא נתן לאפרים בבקר בערב ובצהרים פרות ירקות וחלב כוסותיים ואז חת ושתיים יגדל לו אפרים ירכב בכל יום או יומיים אל סבתא שבירושלים";
            string song10 = "גשם גשם קר רטוב חורף רוח ברחוב מתנועע עץ בנוף כף זקוף כף זקוף ובבית טוב על שמשת חלון אדים  מצירים הילדים פיל ודוב על שמשת חלון אדים  מצירים הילדים פיל ודוב";
            string song11 = " די נזלת חצופה לא טובה ולא יפה למה באת לאף של דן צאי מהר ולכי מכאן דן אינו אוהב נזלת מרגיזה ומבלבלת  מן הבוקר המטפחת מקנחת מקנחת דן אינו הולך לגן הפצי הפצי כל הזמן אוף נזלת עד מתי הסתלקי מכאן ודי דן אינו אוהב נזלת מרגיזה ומבלבלת  מן הבוקר המטפחת מקנחת מקנחת דן אינו הולך לגן הפצי הפצי כל הזמן אוף נזלת עד מתי הסתלקי מכאן ודי ";
            string song12 = "דן טפס על הספסל הופ קפץ והופס נפל אוי כואב לו הוא צורח  מילל ומתיפח אמא באה מה קרה אוי ואבוי נורא נורא פצע דם בברך חור דן שכח כי הוא גבור אין דבר מכל תלבושת  דן אוהבת לחבש תחבשת אמא את פצעו חבשה בתחבשת חדשה אין כאב עוד ואין פגע הכאב עבר בן רגע פנו דרך פנו דרך לגבור פצוע ברך";

            string song13 = "לא לנגע בתנור זה מאד מאד אסור לתנור אל תתקרב הוא יעשה לך כואב כך אומרים לדני דן אבל דן אינו פחדן מתקרב הוא אל האש ובכלל אינו חושש להבה ורודה כחולה איזה יופי מה נפלא אך לפתע אוי אויה מה קרה לדן כויה דן קטן בוכה מרה התנור הוא ילד רע התנור עשה לי חם ברוגז ברוגז לעולם ומאז ועד היום אין עוד ביניהם שלום";
            string song14 = "שיר המעלות בשוב השם את שיבת ציון היינו כחולמים  אז ימלא שחוק פינו ולשוננו רינה  אז יאמרו  יאמרו בגוים הגדיל השם לעשות עם אלה  הגדיל השם לעשות עמנו היינו שמחים  שובה השם  את שביתנו כאפיקים בנגב הזורעים בדמעה ברנה יקצרו  הלך ילך ובכה נושא משך הזרע בא יבוא ברינה נושא אלומותיו ";
            string song15 = "איזה יופי איזה סדר מי ארגן כך את החדר מי ערך שולחן שבת זו הבת זו הבת מה זאת רינה לא אאמינה אי אפשר לא יתכן איזה יופי איזה סדר מי ארגן כך את החדר מי ערך שולחן שבת זו הבת זו הבת  כן הבת זו הבת והכל לכבוד שבת";
            string song16 = "העץ הוא גבוה  העץ הוא ירוק הים הוא מלוח  הים הוא עמוק אם הים הוא עמוק  מה אכפת לו לעץ מה אכפת לו לים שהעץ הוא ירוק העץ הוא גבוה  העץ הוא ירוק  יפה הציפור  היא תעוף לה רחוק אם תעוף הציפור  מה אכפת לו לעץ מה אכפת לציפור שהעץ הוא ירוק הים הוא מלוח  הים הוא עמוק יפה הציפור  היא תעוף לה רחוק  אם תעוף הציפור  מה אכפת לו לים מה אכפת לציפור שהים הוא עמוק אדם שר שירים כי העץ הוא ירוק אדם שר שירים כי הים הוא עמוק  אם תעוף הציפור  לא ישיר עוד שירים מה אכפת לציפור אם ישיר או ישתוק  ";
            string song17 = "על הגבעה ליד חצב זהיר זיהר לאט לאט מוציא ראשו הצב מה המצב לפתע הב נבח כלבלב  הניע שיח את עליו נבהל הצב  כנס לתוך שריון ראשו רגליו על מקומו נצב חבל חשב כל כך יפה בחוץ עכשיו";
            string song18 = "בארץ אהבתי השקד פורח בארץ אהבתי מחכים לאורח שבע עלמות שבע אמהות בארץ אהבתי על הצריח דגל אל ארץ אהבתי יבוא עולה רגל בשעה טובה בשעה ברוכה המשכיחה כל צער  בשעה טובה המשכיחה כל צער  המשכיחה כל צער  אך מי עיני נשר לו ויראנו ומי לב חכם לו ויכירנו מי לא יטעה  מי לא ישגה מי יפתח לו הדלת בארץ אהבתי על הצריח דגל אל ארץ אהבתי יבוא עולה רגל בשעה טובה בשעה ברוכה המשכיחה כל צער  בשעה טובה המשכיחה כל צער  המשכיחה כל צער  אך מי עיני נשר לו";
            string song19 = "השקדיה פורחת ושמש פז זורחת  צפורים מראש כל גג מבשרות את בוא החג  טו בשבט הגיע חג לאילנות  טו בשבט הגיע חג לאילנות  טו בשבט הגיע חג לאילנות  טו בשבט הגיע חג לאילנות ";
            string song20 = "הארץ משוועת הגיעה עת לטעת  כל אחד יקח לו עץ באתים נצא חוצץ ארץ זבת חלב  חלב ודבש ארץ זבת חלב  חלב ודבש ארץ זבת חלב  חלב ודבש ארץ זבת חלב  חלב ודבש ארץ זבת חלב  זבת חלב ודבש ארץ זבת חלב  זבת חלב ודבש כך הולכים השותלים רון בלב ואת ביד מן העיר ומן הכפר מן העמק  מן ההר בטו בשבט בטו בשבט למה באתם  השותלים נכה בקרקע ובצור וגומות סביב נחפור בהרים ובמישור בטו בשבט בטו בשבט מה יהא פה  השותלים שתיל יבוא בכל גומה  יער עד יפרוש צילו על ארצנו ערומה  בטו בשבט  בטו בשבט";
            string song21 = "יש לי צפור קטנה בלב והיא עושה בי מנגינות של סתו ושל אביב חולף ואלף מנגינות קטנות והיא עושה בי מזמורים והיא צובעת עלמות והיא פותרת לשירים כמעט את כל החלומות  יש לי בלב צפור קטנה עם שתי גומות ומנגינה יש לי צפור קטנה בלב והיא עושה בי ספורים של בית חם ובן אוהב ושל קטעי ילדות יפים והיא זוכרת בי כאב והיא אוספת בי שנים  והיא עושה אותי שלו אולי היא טעם החיים יש לי בלב צפור קטנה עם שתי גומות ומנגינה יש לי צפור קטנה בלב והיא אולי כל התקוות ולפעמים אני חושב שהיא תשובה לאכזבות והיא טובה לבנת כנף אתה אני מרגיש חפשי  והיא יושבת על ענף בין מיתרי לבי  יש לי בלב צפור קטנה עם שתי גומות ומנגינה";
            string song22 = "תורה אורה כזו יש לנו וגם הגדה ומגילה  ואלוקים אחד שלנו וקול חתן וקול כלה ארץ ישראל יפה ארץ ישראל פורחת  את יושבה בה וצופה את צופה בה וזורחת  לנו יש  אחי  הרים אלפים בם נאוו רגלי המבשר וגם מלאך מן השמים שאת נפשנו הוא שומר  וחסיד בעיר הזו יש לנו וגם מצוות וגם דרכים  והברכות כולן שלנו והבשורות והשבחים  ";
            string song23 = "ארץ ישראל יפה ארץ ישראל פורחת  את יושבה בה וצופה את צופה בה וזורחת  והעמק הוא כפתור ופרח וההר הוא פרח וכפתור  והצפון שלגים וקרח והדרום זהב טהור  כל הפרדסים נותנים כאן ריח והשקדיות כולן פורחות השמש כאן תמיד זורח על מי תוגה ומנוחות  ארץ ישראל יפה ארץ ישראל פורחת  את יושבה בה וצופה את צופה בה וזורחת  גם לחוטאים אשר בינינו יש מקום בארץ ישראל  מן הצרות שלא עלינו אתה ורק אתה גואל במזמור לתודה מניף הדגל";
            string song24 = "ולירושלים כל שירי  אנחנו שוב עולים לרגלהו  שירי עם ישראל חי  ארץ ישראל יפה ארץ ישראל פורחת  את יושבה בה וצופה את צופה בה וזורחת  כאן ביתי פה אני נולדתי במישור אשר על שפת הים כאן החברים איתם גדלתי ואין לי שום מקום אחר בעולם  כאן ביתי פה אני שיחקתי בשפלה אשר על גב ההר כאן מן הבאר שתיתי מים ושתלתי דשא במדבר  כאן נולדתי כאן נולדו לי ילדי כאן בניתי את ביתי בשתי ידי כאן גם אתה איתי וכאן כל אלף ידידי ואחרי שנים אלפיים סוף לנדודי  כאן את כל שירי אני ניגנתי והלכתי במסע לילי כאן בנעורי אני הגנתי על חלקת האדמה שלי  כאן נולדתי כאן נולדו לי ילדי כאן בניתי את ביתי בשתי ידי כאן גם אתה איתי וכאן כל אלף ידידי ואחרי שנים אלפיים סוף לנדודי כאן את שולחני אני ערכתי פת של לחם פרח רענן דלת לשכנים אני פתחתי מי שבא נאמר לו אהלן  כאן נולדתי כאן נולדו לי ילדי כאן בניתי את ביתי בשתי ידי כאן גם אתה איתי וכאן כל אלף ידידי ואחרי שנים אלפיים סוף לנדודי";
            string song25 = "שם הרי גולן  הושט היד וגע בם בדממה בוטחת מצווים עצור  בבדידות קורנת נם חרמון הסבא וצינה נושבת מפסגת הצחור  שם על חוף הים יש דקל שפל צמרת סתור שיער הדקל כתינוק שובב  שגלש למטה ובמי כינרת במי כינרת משכשך רגליו  מה ירבו פרחים בחורף על הכרך  דם הכלנית וכתם הכרכום  יש ימים פי שבע אז ירוק הירק  פי שבעים תכולה התכלת במרום  גם כי אוורש ואהלך שחוח  והיה הלב למשואות זרים  איך אוכל לבגוד בך  איך אוכל לשכוח איך אוכל לשכוח חסד נעורים  שם הרי גולן  הושט היד וגע בם בדממה בוטחת מצווים עצור  בבדידות קורנת נם חרמון הסבא וצינה נושבת מפסגת הצחור ";
            string song26 = "ארצנו הקטנטונת  ארצנו היפה מולדת בלי כותונת  מולדת יחפה קבליני אל שירייך  כלה יפהפייה פתחי לי שערייך אבוא בם אודה קה  בצל עצי החורש  הרחק מאור חמה יחדיו נכה פה שורש אל לב האדמה אל מעיינות הזוהר  אל בארות התום מולדת ללא תואר וצועני יתום  עוד לא תמו כל פלאייך";
            string song27 = "עוד הזמר לו שט עוד לבי מכה עם ליל ולוחש לו בלאט את לי את האחת את לי את  אם ובת את לי את המעט המעט שנותר  נביאה בבגדינו את ריח הכפרים בפעמון ליבנו יכו העדרים  ישנה דממה רוגעת וקרן אור יפה  ולאורה נפסעה ברגל יחפה  עוד לא תמו כל פלאייך עוד הזמר לו שט עוד לבי מכה עם ליל ולוחש לו בלאט את לי את האחת את לי את  אם ובת את לי את המעט המעט שנותר ";
            string song28 = "כל אחד ישא קולו  יחד לעולם כלו ונשירה פה אחד יחד יד ביד יד ביד ולב אל לב  כל אחד יתן מעט  כל אחד יושיט רק יד  יחד יחד לא לבד  אל האור נצעד  יד ביד ולב אל לב כל אחד יתן מעט  כל אחד יושיט רק יד  יחד יחד לא לבד אל האור נצעד   את ידי קח בידך  יחד כל המשפחה  איש אחד בלב אחד  יחד יד ביד יד ביד ולב אל לב  כל אחד יתן מעט  כל אחד יושיט רק יד  יחד יחד לא לבד  אל האור נצעד  פה אחד שירנו רם יחד יחד כאן כולם וישיר אז כל אחד  יחד יד ביד  כל אחד ישא קולו יחד לעולם כלו  ונשירה פה אחד  יחד יד ביד  יד ביד ולב אל לב  כל אחד יתן מעט  כל אחד יו שיט רק יד  יחד יחד לא לבד  אל האור נצעד יד ביד ולב אל לב  כל אחד יתן מעט  כל אחד יושיט רק יד  יחד יחד לא לבד  אל האור נצעד  את ידי קח בידך  יחד כל המשפחה  איש אחד בלב אחד  יחד יד ביד  יד ביד ולב אל לב  כל אחד יתן מעט  כל אחד יושיט רק יד  יחד יחד לא לבד  אל האור נצעד  פה אחד שירנו רם  יחד יחד כאן כולם  וישיר אז כל אחד  יחד יד ביד ";
            string song29 = "ורד ורד תני לי יד לא אני לבד לבד בואי נסרק את הצמה ורד ורד בעצמה ורד כבר ילדה גדולה בעצמה תלבש שמלה ותנעל הנעלים ואפילו לא תאמינו תצחצח השנים היא רק היא תלבש הגרב ותפשט אותה בערב ותמצע לבד לבד כל דבר אשר אבד עסוקה היא כל היום לעזור לה לעזור לה מה פתאום ורד ורד בעצמה ורד כבר ילדה גדולה בעצמה תלבש שמלה ותנעל הנעלים ואפילו לא תאמינו תצחצח השנים  ";
            string song30 = "התרופה קצת מרה אבל רותי גבורה לא נורא היא תפתח פה גדול ותבלע הכל הכל ויגידו לבריאות גבורה היא רותי רות היא תפתח פה גדול ותבלע הכל הכל ויגידו לבריאות גבורה היא רותי רות היא תפתח פה גדול ותבלע הכל הכל ויגידו לבריאות גבורה היא רותי רות";
            string song31 = "לתת את הנשמה ואת הלב לתת לתת כשאתה אוהב ואיך מוצאים את ההבדל שבין לקחת ולקבל עוד תלמד לתת לתת לגלות סודות בסתר להתיר את סבך הקשר  כשהלב בך נצבט מכל חיוך מכל מבט אתה נזהר אתה יודע וחוץ ממך איש לא שומע  פוסע בין הדקויות וממלא שעות פנויות לתת את הנשמה ואת הלב לתת לתת כשאתה אוהב ואיך מוצאים את ההבדל שבין לקחת ולקבל עוד תלמד לתת לתת אתה לומד עם השנים לבנות ביחד בנינים  נאים  לרקום אתה ספור  חיים ולעבר ימים קשים במצוקות ורגושים תמיד לדעת לוותר ואת הטעם לשמר לתת את הנשמה ואת הלב לתת לתת כשאתה אוהב ואיך מוצאים את ההבדל שבין לקחת ולקבל עוד תלמד לתת לתת לראות בתוך הנפילה שיש מקום למחילה תמיד אפשר שוב להתחיל כמו יום חדש כמו כרגיל לתת";
            string song32 = "שרב הכי כבד אפלו בשמש ידעתי שהגשם עוד ירד ראיתי בחלון שלי צפור בסופה וקר לא פעם זה קשה אבל לרב מלה טובה מיד עושה לי טוב  רק מלה טובה או שתים לא יותר מזה אפילו ברחוב ראשי סואן ראיתי איש יושב ומנגן  פגשתי אנשים מאושרים אפילו בין שבילי עפר צרים תמיד השארתי פתח לתקוה אפילו כשכבתה השלווה  חלמתי על ימים יותר יפים אפילו בלילות שינה טרופים לא פעם זה קשה אבל לרב מילה טובה מיד עושה לי טוב  רק מלה טובה או שתים לא יותר מזה  ";
            string song33 = "אני נולדתי אל המנגינות ואל השירים של כל המדינות נולדתי ללשון וגם למקום למעט להמון שיושיט יד לשלום אני נולדתי לשלום שרק יגיע אני נולדתי לשלום שרק יבוא אני נולדתי לשלום שרק יופיע אני רוצה אני רוצה להיות כבר בו אני נולדתי אל החלום ובו אני רואה שיבוא השלום נולדתי לרצון ולאמונה שהנה הוא יבוא אחרי שלושים שנה אני נולדתי לשלום שרק יגיע אני נולדתי לשלום שרק יבוא אני נולדתי לשלום שרק יופיע אני רוצה אני רוצה להיות כבר בו נולדתי לאומה ולה שנים אלפיים שמורה לה אדמה וגם חלקת שמיים ולה רואה צופה הנה עולה היום והשעה יפה זוהי שעת שלום אני נולדתי לשלום שרק יגיע אני נולדתי לשלום שרק יבוא אני נולדתי לשלום שרק יופיע אני רוצה אני רוצה להיות כבר בו";
            string song34 = "על שולחן שאותו ערכה אמא וברכה ברכה לידי על השולחן לסעודה הכל מוכן על שולחן אותו ערכה אמא וברכה ברכה לידי על השולחן לסעודה הכל מוכן בואו בואו בן ובת לסעודה של טו בשבט מה נאכל בחג אילן זה מובן פרות הגן לכל אחד כל מין וזן הנה תמר תאכל תמר ומה שמעון יאכל רמון צמוק חרוב ותאנה נתן לרות ודן מנה בואו בואו בן ובת לסעודה של טו בשבט מה נאכל בחג אילן זה מובן פרות הגן לכל אחד כל מין וזן קלמנטינה נתן לדינה את האגס תאכל הדס ואת גלית טלי לך אשכולית בואו בואו בן ובת לסעודה של טו בשבט מה נאכל בחג אילן זה מובן פרות הגן לכל אחד כל מין וזן נו  לאליהב תפוח מזהב ומה עתה ברוך אתה בורא פרי העץ תודה  בתאבון לסעודה";
            string song35 = "על הדבש ועל העוקץ  על המר והמתוק  על בתנו התינוקת שמור קלי הטוב  על האש המבוערת  על המים הזכים  על האיש השב הביתה מן המרחקים  על כל אלה  על כל אלה  שמור נא לי קלי הטוב על הדבש ועל העוקץ  על המר והמתוק  אל נא תעקור נטוע  אל תשכח את התקוה השיבני ואשובה אל הארץ הטובה  שמור אלי על זה הבית  על הגן  על החומה  מיגון  מפחד פתע וממלחמה  שמור על המעט שיש לי  על האור ועל הטף על הפרי שלא הבשיל עוד ושנאסף  על כל אלה  על כל אלה  שמור נא לי קלי הטוב  על הדבש ועל העוקץ על המר והמתוק  אל נא תעקור נטוע  אל תשכח את התקוה השיבני ואשובה אל הארץ הטובה  מרשרש אילן ברוח  מרחוק נושר כוכב  משאלות ליבי בחושך נרשמות עכשיו  אנא  שמור לי על כל אלה ועל אהובי נפשי  על השקט  על הבכי ועל זה השיר  על כל אלה  על כל אלה  שמור נא לי קלי הטוב  על הדבש ועל העוקץ  על המר והמתוק אל נא תעקור נטוע  אל תשכח את התקוה השיבני ואשובה אל הארץ הטובה";
            string song36 = "מעל פסגת הר הצופים אשתחוה לך אפיים מעל פסגת הר הצופים שלום לך ירושלים מאה דורות חלמתי עלייך לזכות  לראות באור פנייך ירושלים  ירושלים  האירי פנייך לבנך ירושלים  ירושלים  מחרבותייך אבנך מעל פסגת הר הצופים שלום לך ירושלים אלפי גולים מקצות כל תבל נושאים אלייך עיניים  באלפי ברכות היי ברוכה מקדש מלך  עיר מלוכה ירושלים  ירושלים  אני לא אזוז מפה ירושלים  ירושלים  יבוא המשיח יבוא  ";
            string song37 = "עננים אמרו לשמש  אמא שמש אם חמה אל תאירי עוד לארץ מה לך ולאדמה את שלנו של שמים של כל רוח וענן אל תאירי לה לארץ רק תאירי כאן רק כאן את שלנו של שמים של כל רוח וענן אל תאירי לה לארץ רק תאירי כאן רק כאן צחקה להם השמש צחקה לטיפושנים ואמרה טוב טוב בסדר אך הקשיבו עננים  כי שדה וגן וירק וכל פרח לי חבר אם יהיו לי כאן כל אלה בשמים אשאר";
            string song38 = "קום והתהלך בארץ בתרמיל ובמקל  וודאי תפגוש בדרך שוב את ארץ ישראל  יחבקו אותך דרכיה של הארץ הטובה  היא תקרא אותך אליה כמו אל ערש אהבה  זאת אכן אותה הארץ  זו אותה האדמה ואותה פיסת הסלע הנצרבת בחמה  ומתחת לאספלט למנהגים ולמצווה  מסתתרת המולדת ביישנית וענווה  קום והתהלך בארץ בתרמיל ובמקל  וודאי תפגוש בדרך שוב את ארץ ישראל  יחבקו אותך דרכיה של הארץ הטובה  היא תקרא אותך אליה כמו אל ערש אהבה  וכרמי עצי הזית ומסתור המעיין עוד שומרים על חלומה וחלומנו הישן  וגגות אודמים על הר וילדים על השבילים  במקום שבו הלכנו עם חגור ותרמילים  קום והתהלך בארץ בתרמיל ובמקל  וודאי תפגוש בדרך שוב את ארץ ישראל  יחבקו אותך דרכיה של הארץ הטובה  היא תקרא אותך אליה כמו אל ערש אהבה ";
            string song39 = "קח מקל  קח תרמיל  בוא אתי אל הגליל  בוא אתי ביום אביב נהלך סביב  סביב  קח מקל  קח תרמיל  בוא אתי אל הגליל  בוא אתי ביום אביב נהלך סביב  סביב  עם השמש הזורחת בחניתה ושוקעת באכזיב  כי השמש  כן השמש  כאן השמש  כאן השמש לא תכזיב  כי השמש  כן השמש  כאן השמש לא תכזיב  כאן השמש לא תכזיב  כאן השמש לא תכזיב  ";
            string song40 = "קח טמבור  קח חליל  בוא אתי אל הגליל  בוא נפתח נא השירון  בוא נשיר במלוא גרון  ";
            string song41 = "קח מקל  קח תרמיל  בוא אתי אל הגליל בוא אתי ביום אביב נהלך סביב  סביב  ";
            string song42 = "עם הרוח הנוגנת בשלווה ושורקת בעברון  כי הרוח  כן הרוח  כאן הרוח  כאן הרוח רן תרן  כי הרוח  כן הרוח  כאן הרוח רן תרן  כאן הרוח רן תרן  כאן הרוח רן תרן קח צעיף  קח מעיל  בוא אתי אל הגליל  בוא בחרף מילל ונראה איך נשתולל  קח מקל  קח תרמיל  בוא אתי אל הגליל ";
            string song43 = "בוא אתי ביום אביב נהלך סביב  סביב  גשם גשם גשם מים גשם יום ועוד יומים אפורים שחורים שמים מים החיות ביערות התחבאו במערות פחד פחד הוי מבול בביתו נחבא שבלול הארנבת בחורה הוי מבול נורא נורא הוי מבול הוא בא הוא בא העולם כולו יטבע מים גשם גשם גשם מים גשם יום ועוד יומים אפורים שחורים שמים מים ילד למרום הביט קשת קשת אות הברית  לאדם ולכל חי  לא יהיה מבול עוד די די";
            string song44 = "עוד נדע ימים טובים מאלה  עוד נדע ימים טובים פי אלף שרשינו יעמיקו סלע כמו ארזים בהר  עוד נדע ימים טובים מאלה  עוד נמתיק מימיו של ים המלח כשדה ירק אחר השלף כאשד בלב מדבר ";
            string song45 = "יחד  כל הדרך  יחד  לא אחרת  יד ביד נושיט לטוב שעוד יבוא  בא יבוא  יחד  כל הדרך  יחד  לא אחרת  יחד איש אל איש יפתח את לבבו  רק אם נאמין בעז הרוח  רק אם נאמין ולא  ננוח מן המצרים אל ים פתוח כמים רבים נסער  רק אם נאמין ודאי נצליח  רק אם נאמין ודאי נגיע  בימים של סער ברקיע כאש התמיד נבער  יחד  כל הדרך  יחד  לא אחרת  יד ביד נושיט לטוב שעוד יבוא  בא יבוא יחד  כל הדרך יחד לא אחרת  יחד איש אל איש יפתח את לבבו ";
            string song46 = "רש רש רשרש רעשן קשקוש קשקש קשקשן בקר ערב צהרים מקשקש קיש באזנים לא ננום ולא נישן  מי זה כאן כל כך מרעיש מי אוזנינו מחריש רש רש רשרש רעשן קשקוש קשקש קשקשן בקר ערב צהרים מקשקש קיש באזנים לא ננום ולא נישן";
            string song47 = "הנה כוכב ועוד כוכב ועוד כוכב ניצת חבל כי כבר הלכת שבת חבל כי כבר יצאת  הנה כוכב ועוד כוכב ועוד כוכב ניצת חבל כי כבר הלכת שבת חבל כי כבר יצאת  ואבא שב מן התפילה דולק כבר נר של הבדלה אחד אחד המשפחה קופסת הבושם מריחה הנה כוכב ועוד כוכב ועוד כוכב ניצת חבל כי כבר הלכת שבת חבל כי כבר יצאת  הצעיר בחבורה מניף הנר עד התקרה מאיר הנר הדרך לך שבוע טוב ומבורך";
            string song48 = "שבחי ירושלים את אלוקי  הללי אלוקייך ציון כי חיזק בריחי שערייך  ברך בנייך בקרבך ברך בנייך בקרבך ברך בנייך בקרבך הללי  הללי אלוקייך ציון";
            string song49 = "אם בהר חצבת אבן להקים בנין חדש בהר חצבת אבן להקים בנין חדש כי מן האבנים האלה יבנה מקדש יבנה  יבנה  יבנה המקדש אם בהר נטעת ארז  ארז במקום דרדר בהר נטעת ארז  ארז במקום דרדר כי מן הארזים האלה ייבנה ההר יבנה  יבנה  יבנה ההר אם לא שרת לי שיר עדיין  שירה לי מזמור חדש שהוא עתיק מיין ומתוק מדבש שיר שהוא עתיק מיין ומתוק מדבש שיר שהוא כבן אלפיים ובכל יום חדש יבנה  יבנה  יבנה המקדש ";
            string song50 = "דע לך שכל רועה ורועה יש לו ניגון מיוחד משלו דע לך שכל עשב ועשב יש לו שירה מיוחדת משלו ומשירת העשבים נעשה ניגון של רועה כמה יפה כמה יפה ונאה כששומעים השירה שלהם טוב מאוד להתפלל ביניהם ובשמחה לעבוד את השם ומשירת העשבים מתמלא הלב ומשתוקק וכשהלב מן השירה מתעורר ומשתוקק אל ארץ ישראל אור גדול אזי נמשך והולך מקדושתה של הארץ ומשירת העשבים נעשה ניגון של הלב  ";
            string song51 = "שלום שמש שלום חלמתי הלילה חלום קנו לי מחברת ילקוט עפרון הביטי הנה הם על החלון שלום שמש שלום  שלום אמא שלום אני גדול היום לבית הספר אלך אלך ללמוד יהיה לך ילד חכם מאד שלום אמא שלום שלום צפור שלום את יודעת מה היום היום אלמד לקרוא לכתב אהיה תלמיד חכם וטוב שלום צפור שלום שלום מורה שלום נתחיל ללמד היום שלום לאורי וברכה עלה ולמד בהצלחה שלום אורי שלום";
            string song52 = "שלום לך אורחת שלום לך שבת חיכינו חיכינו סוף סוף הנה באת הבית שטפנו פרחים לך קטפנו פרשנו מפה לבנה על שלחן הנרות מאירים  כל הבית מוכן שלום לך אורחת שלום לך שבת חיכינו חיכינו סוף סוף הנה באת שלום לך אורחת שלום לך שבת חיכינו חיכינו סוף סוף הנה באת פרחים לך קטפנו פרשנו מפה לבנה על שלחן הנרות מאירים  כל הבית מוכן";
            string song53 = "גשם גשם גשם רב מי זה בא כולו נרטב מגבים לרגליו  זה חלבן מביא חלב רותי דן ויהודה וכל ילד וילדה לבורא מילה מודה תודה בוראינו גדולה תודה ";
            string song54 = "אבא שלי מדבר איטלקית צרפתית ואנגלית ואפילו טורקית אבל אמא שלי אבל אמא שלי אבל אמא שלי מבינה תינוקית תינוקית היא דבור היא דבור של תינוק תינוקית היא דבור של אחי המתוק מה אומר הוא אחי הוא אומר ממנה ואומרת אמי הוא רוצה בננה ואחי מקשקש גללטטאל ואומרת אימי הוא רוצה לטיל דדד זהו ברוגז ואיה אוהב ואמי מבינה מבינה היא היטב מפטפט הוא אחי מפטפט הוא בקול ואמי מבינה מבינה את הקול כן יודע אבי איטלקית צרפתית ואנגלית ואפילו טורקית אבל אמא שלי אבל אמא שלי אבל אמא שלי מבינה תינוקית";
            string song55 = "בת שנתיים היא תמר וסוחבת כל דבר רגע אמא לא תביט ותמר תסחב מפית ספל תה וכוס זכוכית כל צלחת חרסינה נתונה בסכנה מכיור את הסבון תלקק בתאבון מי פרחים שבעציץ היא תאהב יותר ממיץ במברשת השניים תצחצח נעליים הוי תמר  את שובבה  לא לא אל אני טובה הן לא בי הוא האשם אם הכל מונח כך סתם  יש לתקע הכלים למקומם במסמרים  הוי תמר  את שובבה  לא לא אל אני טובה הן לא בי הוא האשם אם הכל מונח כך סתם  יש לתקע הכלים למקומם במסמרים  הוי תמר  את שובבה  לא לא אל אני טובה הן לא בי הוא האשם אם הכל מונח כך סתם  יש לתקע הכלים למקומם במסמרים";
            string song56 = "בפרדס הגדול בפרדס הרחב מזהיבים על כל עץ תפוחים של זהב ואומרים ענפים  אנו כבר עיפים  מהרו נא קוטפים תפוחינו יפים בוקר טוב מר ירקן תפוזים יש היום יש ויש למה לא בוקר טוב ושלום וקרוא הוא בקול תפוזים פה בזול במאזניים אשקול קחו נא קנו את הכל";
            string song57 = "אב הרחמים הוא ירחם עם עמוסים ויזכור ברית איתנים ויציל נפשותינו מן השעות הרעות";
            string song58 = "אבינו אב הרחמן המרחם רחם עלינו ותן בלבנו בינה להבין ולהשכיל לשמע ללמד וללמד לשמר ולעשות ולקים את כל דברי תלמוד תורתך באהבה והאר עינינו בתורתך ודבק לבנו במצותיך ויחד לבבנו לאהבה וליראה את שמך";
            string song59 = "אבינו מלכנו אין לנו מלך אלא אתה אבינו מלכנו אין לנו מלך אלא אתה מאן דיתיב בי מלכא ונטל ליה בלחודוי כל מה דבעי שאיל ויהיב ליה";
            string song60 = "אבינו מלכנו פתח שערי שמים לתפלתנו";
            string song61 = "אדון עולם אשר מלך בטרם כל יציר נברא לעת נעשה בחפצו כל אזי מלך שמו נקרא ואחרי ככלות הכל לבדו ימלך נורא והוא היה והוא הוה והוא יהיה בתפארה והוא אחד ואין שני להמשיל לו להחבירה בלי ראשית בלי תכלית ולו העז והמשרה והוא אלי וחי גואלי וצור חבלי בעת צרה והוא נסי ומנוס לי מנת כוסי ביום אקרא בידו אפקיד רוחי בעת אישן ואעירה ועם רוחי גויתי ה לי ולא אירא";
            string song62 = "אדיר אדירנו ה אדנינו מה אדיר שמך בכל הארץ והיה ה למלך על כל הארץ ביום ההוא יהיה ה אחד ושמו אחד";
            string song63 = "אדיר הוא יבנה ביתו בקרוב במהרה במהרה בימינו בקרוב אל בנה אל בנה בנה ביתך בקרוב ";
            string song64 = "אדרבה תן בלבנו שנראה כל אחד מעלת חברינו ולא חסרונם ושנדבר כל אחד את חברו בדרך הישר והרצוי לפניך ואל יעלה שום שנאה מאחד על חברו חלילה ותחזק התקשרותנו באהבה אליך כאשר גלוי וידוע לפניך שיהא הכל נחת רוח אליך";
            string song65 = "אור זרוע לצדיק ולישרי לב שמחה";
            string song66 = "אחינו כל בית ישראל הנתונים בצרה ובשביה העומדים בין בים ובין ביבשה המקום ירחם עליהם ויוציאם מצרה לרוחה ומאפלה לאורה ומשעבוד לגאולה השתא בעגלא ובזמן קריב";
            string song67 = "אחת שאלתי מאת ה אותה אבקש שבתי בבית ה כל ימי חיי לחזות בנעם ה ולבקר בהיכלו";
            string song68 = "אילו היה לי כוח הייתי יוצא לשוק הייתי מכריז ואומר שבת היום לה";
            string song69 = "אין ערוך לך השם בעולם הזה ואין זולתך מלכנו לחיי העולם הבא אפס בלתך גואלנו לימות המשיח ואין דומה לך מושיענו לתחית המתים";
            string song70 = "אין קצבה לשנותיך ואין קץ לאורך ימיך ואין לשער מרכבות כבודך ואין לפרש עילום שמך שמך נאה לך ואתה נאה לשמך ושמנו קראת בשמך עשה למען שמך וקדש את שמך על מקדישי שמך בעבור כבוד שמך הנערץ והנקדש כסוד שיח שרפי קדש המקדישים שמך בקדש דרי מעלה עם דרי מטה קוראים ומשלשים בשלוש קדשה בקדש";
            string song71 = "אל ההודאות אדון השלום מקדש השבת ומברך שביעי ומניח בקדשה לעם מדשני ענג זכר למעשה בראשית";
            string song72 = "אלה חמדה לבי חוסה נא ואל תתעלם";
            string song73 = "אלי אתה ואודך אלהי ארוממך";
            string song74 = "אליך השם אקרא ואל השם אתחנן  שמע השם וחנני השם ה‍יה עזר לי";
            string song75 = "אם אשכחך ירושלם תשכח ימיני";
            string song76 = "אם היו בני אדם מרגישין במתיקות ועריבות טוב התורה היו משתגעים ומתלהטים אחריה ולא יחשב בעיניהם מלוא עולם כסף וזהב למאומה כי התורה כוללת כל הטובות שבעולם";
            string song77 = "אמר לצפון תני ולתימן אל תכלאי הביאי בני מרחוק ובנותי מקצה הארץ";
            string song78 = "אמר רבי עקיבא אשריכם ישראל לפני מי אתם מטהרין ומי מטהר אתכם אביכם שבשמים ואומר מקוה ישראל המה מקוה מטהר את הטמאים אף הקדוש ברוך הוא מטהר את ישראל";
            string song79 = "אמת אתה הוא ראשון ואתה הוא אחרון ומבלעדיך אין לנו מלך גואל ומושיע";
            string song80 = "אם היו בני אדם מרגישים במתיקות ועריבות טוב התורה היו משתגעים ומתלהטים אחריה ולא יחשב בעיניהם מלוא עולם כסף וזהב למאומה כי התורה כוללת כל הטובות שבעולם";
            string song81 = "אנא בכח גדולת ימינך תתיר צרורה קבל רנת עמך שגבנו טהרנו נורא";
            string song82 = "אני מאמין באמונה שלמה שהבורא יתברך שמו הוא בורא ומנהיג לכל הברואים והוא לבדו עשה ועושה ויעשה לכל המעשיםאני מאמין באמונה שלמה בביאת המשיח ואף על פי שיתמהמה עם כל זה אחכה לו בכל יום שיבוא";
            string song83 = "אשא עיני אל ההרים מאין יבא עזרי עזרי מעם ה עשה שמים וארץ אל יתן למוט רגלך אל ינום שמרך הנה לא ינום ולא יישן שומר ישראל";
            string song84 = "אתה בחרתנו מכל העמים אהבת אותנו ורצית בנו ורוממתנו מכל הלשונות וקדשתנו במצותיך וקרבתנו מלכנו לעבודתך ושמך הגדול והקדוש עלינו קראת";
            string song85 = "באי בשלום עטרת בעלה גם בשמחה ובצהלה תוך אמוני עם סגלה בואי כלה בואי כלה שבת מלכתא לכה דודי לקראת כלה פני שבת נקבלה ";
            string song86 = "ביום ההוא יושר השיר הזה בארץ יהודהעיר עז לנו ישועה ישית חומות וחל פתחו שערים ויבא גוי צדיק שמר אמנים";
            string song87 = "בלבבי משכן אבנה להדר כבודו ובמשכן מזבח אקים לקרני הודו ולנר תמיד אקח לי את אש העקדה ולקרבן אקריב לו את נפשי היחידה";
            string song88 = "בן אדם עלה למעלה עלה כי כח עז לך יש לך כנפי רוח כנפי נשרים אבירים אל תכחש בם פן יכחשו לך דרוש אותם וימצאו לך מיד";
            string song89 = "ברוך הוא אלהינו שבראנו לכבודו והבדילנו מן התועים ונתן לנו תורת אמת וחיי עולם נטע בתוכנו ";
            string song90 = "בשם ה אלהי ישראל מימיני מיכאל ומשמאלי גבריאל ומלפני אוריאל ומאחורי רפאל ועל ראשי שכינת אל";
            string song91 = "בשעה שמלך המשיח בא עומד על גג בית המקדש ומשמיע לישראל ואומר ענוים ענוים הגיע זמן גאלתכם ואם אין אתם מאמינים ראו באורי שזורח";
            string song92 = "דוד המלך עליו השלום היה מתפלל על כל הענינים שצריכים ישראל עד ביאת משיחנו על החולים שיתרפאו על הבריאים שלא יחלו ועל פרנסתם שיתברכו ולבטל מהם כל גזרות קשות";
            string song93 = "הבן יקיר לי אפרים אם ילד שעשעים כי מדי דברי בו זכר אזכרנו עוד על כן המו מעי לו רחם א‍רחמנו נאם ה";
            string song94 = "הוא יבנה בית לשמי בית לי הוא יבנה וחסדי לא יסור ממנו ואני אהיה לו לאב והוא יהיה לי לבן וחסדי לא יסור ממנו אני לאב והוא לבן";
            string song95 = "הנה אנכי שלח לכם את אליה הנביא לפני בוא יום ה הגדול והנורא והשיב לב אבות על בנים ולב בנים על אבותם";
            string song96 = "הנה ימים באים נאם הוהשלחתי רעב בארץ לא רעב ללחם ולא צמא למים כי אם לשמע את דברי ה";
            string song97 = "התנערי מעפר קומי לבשי בגדי תפארתך עמי על יד בן ישי בית הלחמי קרבה אל נפשי גאלה באי בשלום עטרת בעלה גם בשמחה ובצהלה תוך אמוני עם סגלה בואי כלה בואי כלה";
            string song98 = "ואמר ביום ההוא הנה אלהינו זה קוינו לו ויושיענו זה יהוה קוינו לו נגילה ונשמחה בישועתו";
            string song99 = "ואפילו בהסתרה שבתוך ההסתרה בודאי גם שם נמצא השם יתברך גם מאחורי הדברים הקשים העוברים עליך אני עומד";
            string song100 = "וארשתיך לי לעולם וארשתיך לי בצדק ובמשפט בחסד וברחמים וארשתיך לי באמונה וידעת את ה ";
            string song101 = "והאר עינינו בתורתך ודבק לבנו במצותיך ויחד לבבנו לאהבה וליראה את שמך ולא נבוש ולא נכלם ולא נכשל לעולם ועד";
            string song102 = "והביאותים אל הר קדשי ושמחתים בבית תפלתי עולתיהם וזבחיהם לרצון על מזבחי כי ביתי בית תפלה יקרא לכל העמים";
            string song103 = "והכהנים והעם העומדים בעזרה כשהיו שומעים את השם הנכבד והנורא היו כורעים ומשתחוים ומודים ונופלים על פניהם";
            string song104 = "והראנו ה אלקינו בנחמת ציון עירך ובבנין ירושלים עיר קדשך כי אתה הוא בעל הישועות ובעל הנחמות ";
            string song105 = "וזכני לגדל בנים ובני בנים חכמים ונבונים אוהבי ה יראי אלהים אנשי אמת זרע קדש בה דבקים ומאירים את העולם בתורה ובמעשים טובים ובכל מלאכת עבודת הבורא";
            string song106 = "וידע כל פעול כי אתה פעלתו ויבין כל יצור כי אתה יצרתו ויאמר כל אשר נשמה באפו ה אלהי ישראל מלך ומלכותו בכל משלה";
            string song107 = "ולירושלים עירך ברחמים תשוב ותשכן בתוכה כאשר דברת ובנה אותה בקרוב בימינו בנין עולם וכסא דוד עבדך מהרה לתוכה תכין";
            string song108 = "ומחה השם אלקים דמעה מעל כל פנים וחרפת עמו יסיר מעל כל הארץ ואמר ביום ההוא הנה אלקינו זה זה השם קוינו לו נגילה ונשמחה בישועתו";
            string song109 = "ונשגב השם לבדו ביום ההוא ויותר יעקב לבדו ויאבק איש עמו עד עלות השחר";
            string song110 = "ועתה כתבו לכם את השירה הזאת ולמדה את בני ישראל תורה הקדושה התחנני בבקשה פני צור נערץ בקדושה";
            string song111 = "וקרב פזורינו מבין הגוים ונפוצותינו כנס מירכתי ארץ והביאנו לציון עירך ברנה ולירושלים בית מקדשך בשמחת עולם ושם נעשה לפניך את קרבנות חובותינו תמידים כסדרם ומוספים כהלכתם ";
            string song112 = "זכר דבר לעבדך על אשר יחלתני זאת נחמתי בעניי כי אמרתך חיתני זדים הליצני עד מאד מתורתך לא נטיתי";
            string song113 = "זכרני נא וחזקני נא אך הפעם הזה האלהים ואנקמה נקם אחת משתי עיני מפלשתים";
            string song114 = "חמול על מעשיך ותשמח במעשיך ויאמרו לך חוסיך בצדקך עמוסיך תוקדש אדון על כל מעשיך כי מקדישיך בקדושתך קדשת נאה לקדוש פאר מקדושים";
            string song115 = "חשוף זרוע קדשך וקרב קץ הישועה נקם נקמת עבדיך מאמה הרשעה כי ארכה השעה ואין קץ לימי הרעה דחה אדמון בצל צלמון הקם לנו רועים שבעה";
            string song116 = "טובים מאורות שברא אלקינו יצרם בדעת בבינה ובהשכל כח וגבורה נתן בהם להיות מושלים בקרב תבל מלאים זיו ומפיקים נוגה נאה זיום בכל העולם שמחים בצאתם וששים בבואם עשים באימה רצון קונם פאר וכבוד נותנים לשמו צהלה ורנה לזכר מלכותו קרא לשמש ויזרח אור ראה והתקין צורת הלבנה יהא רעוא קדמך דתפתח לבאי באוריתא ותשלים משאלין דלבאי ולבא דכל עמך ישראל לטב ולחיין ולשלם";
            string song117 = "יהי החדש הזה כנבואת אבי חוזהוישמע בבית זה קול ששון וקול שמחה חזק ימלא משאלותינו אמיץ יעשה בקשתנו והוא ישלח במעשה ידינו ברכה והצלחה";
            string song118 = "יהי שלום בחילך שלוה בארמנותיך למען אחי ורעי אדברה נא שלום בך למען בית ה אלהינו אבקשה טוב לך";
            string song119 = "יונים נקבצו עלי אזי בימי חשמנים ופרצו חומות מגדלי וטמאו כל השמנים ומנותר קנקנים נעשה נס לשושנים בני בינה ימי שמונה קבעו שיר ורננים";
            string song120 = "יעלה ויבא יגיע יראה וירצה ישמע יפקד ויזכר זכרוננו ופקדוננו וזכרון אבותינו וזכרון משיח בן דוד עבדך וזכרון ירושלים עיר קדשך וזכרון כל עמך בית ישראל לפניך לפלטה לטובה";
            string song121 = "היי שלום אמא היי שלום אבא היום בנכם יוצא אל ארץ נוד רבה ובצאתי מידכם מאום לא אדרשה זולתי תפלתכם ברכתכם הקדושה כי ישמור אל בנכם נערכם הקטן ממכשול בדרך מפגע ושטן כי סוסי לא ימעד ולא תכשל עגלתו עד יבוא בשלום אל ארץ חמדתו";
            string song122 = "ישמח משה במתנת חלקו כי עבד נאמן קראת לו כליל תפארת בראשו נתת בעמדו לפניך על הר סיני";
            string song123 = "ישמחו במלכותך שומרי שבת וקוראי ענג עם מקדשי שביעי כלם ישבעו ויתענגו מטובך ובשביעי רצית בו וקדשתו חמדת ימים אותו קראת זכר למעשה בראשית";
            string song124 = "ישראל בטח בה עזרם ומגנם הוא אנחנו מאמינים בני מאמינים ואין לנו על מי להשען אלא על אבינו שבשמים";
            string song125 = "כאיל תערג על אפיקי מים כן נפשי תערג אליך אלהים צמאה נפשי לאלהים לאל חי מתי אבוא ואראה פני אלהים";
            string song126 = "כה אמר ה מצא חן במדבר עם שרידי חרב הלוך להרגיעו ישראל כה אמר ה זכרתי לך חסד נעוריך אהבת כלולתיך לכתך אחרי במדבר בארץ לא זרועה";
            string song127 = "כי את כל הארץ אשר אתה ראה לך אתננה ולזרעך עד עולם לך אתן את הארץ הזאת ותן ברכה על פני האדמה";
            string song128 = "כי אתה הוא מלך מלכי המלכים מלכותו נצח נוראותיו שיחו ספרו עזו פארוהו צבאיו קדשוהו רוממוהו רון שיר ושבח תוקף תהלות תפארתו";
            string song129 = "כי בשמחה תצאו ובשלום תובלון ההרים והגבעות יפצחו לפניכם רנה וכל עצי השדה ימחאו כף ";
            string song130 = "כי הרבית טובות אלי כי הגדלת חסדך עלי ומה אשיב לך והכל שלך לך שמים אף ארץ לך ואנחנו עמך וצאנך וחפצים לעשות רצונך";
            string song131 = "כי נחם השם ציון נחם כל חרבתיה וישם מדברה כעדן וערבתה כגן ה ששון ושמחה ימצא בה תודה וקול זמרה";
            string song132 = "לדוד ה אורי וישעי ממי אירא ה מעוז חיי ממי אפחד אם תחנה עלי מחנה לא יירא לבי אם תקום עלי מלחמה בזאת אני בוטח";
            string song133 = "מה אשיב להשם כל תגמולוהי עלי כוס ישועות אשא ובשם השם אקרא נדרי להשם אשלם נגדה נא לכל עמו יקר בעיני השם המותה לחסידיו אנה השם כי אני עבדך א‍ני עבדך בן אמתך פתחת למוסרי לך אזבח זבח תודה ובשם השם אקרא נדרי לה אשלם נגדה נא לכל עמו בחצרות בית ה בתוככי ירושל‍ם הללו יה";
            string song134 = "מהרה השם אלקינו ישמע בערי יהודה ובחצות ירושלים קול ששון וקול שמחה קול חתן וקול כלה קול מצהלות חתנים מחפתם ונערים ממשתה נגינתם";
            string song135 = "מי האיש החפץ חיים אהב ימים לראות טוב נצר לשונך מרע ושפתיך מדבר מרמה סור מרע ועשה טוב בקש שלום ורדפהו";
            string song136 = "מידת הרחמים עלינו התגלגלי ולפני קונך תחינתנו הפילי ובעד עמך רחמים שאלי כי כל לבב דווי וכל ראש לחולי יהי רצון מלפניך שומע קול בכיות שתשים דמעותינו בנאדך להיות ותצילנו מכל גזירות אכזריות כי לך לבדך עינינו תלויות";
            string song137 = "מכניסי רחמים הכניסו רחמינו לפני בעל הרחמים משמיעי תפלה השמיעו תפלתנו לפני שומע תפלה משמיעי צעקה השמיעו צעקתנו לפני שומע צעקה מכניסי דמעה הכניסו דמעותינו לפני מלך מתרצה בדמעות השתדלו והרבו תחנה ובקשה לפני מלך אל רם ונשא ";
            string song138 = "ממקומך מלכנו תופיע ותמלוך עלינו כי מחכים אנחנו לך מתי תמלך בציון בקרוב בימינו לעולם ועד תשכון תתגדל ותתקדש בתוך ירושלים עירך לדור ודור ולנצח נצחים ועינינו תראינה מלכותך כדבר האמור בשירי עזך על ידי דוד משיח צדקך";
            string song139 = "נחמו נחמו עמי יאמר אלקיכם דברו על לב ירושל‍ם וקראו אליה כי מלאה צבאה כי נרצה עו‍נה כי לקחה מיד ה כפלים בכל חטאתיה";
            string song140 = "פיה פתחה בחכמה ותורת חסד על לשונה צופיה הליכות ביתה ולחם עצלות לא תאכל קמו בניה ויאשרוה בעלה ויהללה רבות בנות עשו חיל ואת עלית על כלנה";
            string song141 = "צדיק כתמר יפרח כארז בלבנון ישגה שתולים בבית השם בחצרות אלקינו יפריחו עוד ינובון בשיבה דשנים ורעננים יהיו להגיד כי ישר ה צורי ולא עולתה בו";
            string song142 = "קדשנו במצותיך ותן חלקנו בתורתך שבענו מטובך ושמחנו בישועתך וטהר לבנו לעבדך באמת והנחילנו ה אלקינו באהבה וברצון שבת קדשך";
            string song143 = "קול ברמה נשמע נהי בכי תמרורים רחל מבכה על בניה מאנה להנחם על בניה כי איננו כה אמר המנעי קולך מבכי ועיניך מדמעה כי יש שכר לפעלתך נאם ה ושבו מארץ אויב ויש תקוה לאחריתך נאם ה ושבו בנים לגבולם ";
            string song144 = "קרב יום אשר הוא לא יום ולא לילה רם הודע כי לך היום אף לך הלילה שומרים הפקד לעירך כל היום וכל הלילה תאיר כאור יום חשכת לילה";
            string song145 = "רחם בחסדך על עמך צורנו על ציון משכן כבודך זבול בית תפארתנו בן דוד עבדך יבא ויגאלנו רוח אפינו משיח ה יבנה המקדש עיר ציון תמלא ושם נשיר שיר חדש וברננה נעלה הרחמן הנקדש יתברך ויתעלה על כוס יין מלא כברכת ה";
            string song146 = "רבון העולמים ידעתי כי הנני בידך לבד כחמר ביד היוצר ואם גם אתאמץ בעצות ותחבולות וכל יושבי תבל יעמדו לימיני להושיעני ולתמך נפשי מבלעדי עזך ועזרתך אין עזרה וישועה";
            string song147 = "רצה ה אלקינו בעמך ישראל ובתפלתם והשב את העבודה לדביר ביתך ואשי ישראל ותפלתם באהבה תקבל ברצון ותהי לרצון תמיד עבודת ישראל עמך ותחזינה עינינו בשובך לציון ברחמים ";
            string song148 = "שושנת יעקב צהלה ושמחה בראותם יחד תכלת מרדכי תשועתם היית לנצח ותקותם בכל דור ודורברוך מרדכי היהודי";
            string song149 = "שיר למעלות אשא עיני אל ההרים מאין יבא עזרי עזרי מעם ה עשה שמים וארץ אל יתן למוט רגלך אל ינום שמרך הנה לא ינום ולא יישן שומר ישראל ה שמרך ה צלך על יד ימינך יומם השמש לא יככה וירח בלילה ה ישמרך מכל רע ישמר את נפשך השם ישמר צאתך ובואך מעתה ועד עולם ";
            string song150 = "שמחו את ירושל‍ם וגילו בה כל אהביה שישו אתה משוש כל המתאבלים עליה על חומתיך ירושל‍ם הפקדתי שמרים כל היום וכל הלילה ";
            string song151 = "שמע ה קולי אקרא וחנני וענני לך אמר לבי בקשו פני את פניך ה אבקש אל תסתר פניך ממני אל תט באף עבדך עזרתי היית אל תטשני ואל תעזבני אלקי ישעי ";
            string song152 = "תפלה לעני כי יעטף ולפני ה ישפך שיחו ה שמעה תפלתי ושועתי אליך תבוא אל תסתר פניך ממני ביום צר לי ";
            string song153 = "נד נד נד נד רד עלה עלה ורד מהלמעלה מהלמטה רק אני אני ואתה שנינו שקולים במאזנים בין הארץ לשמים";
            string song154 = "חוג יחוג מרכופי ורחובות ובתים וראשי סחרחר אנכי הסובב אני ולא אחר ורחובות ובתים אי ראשם אי סופם כגלגל בגלגל כאופן באופן אהה וחצוצרתי נשמטה נשמטה סמכוני פן אצנח גם אני למטה";
            string song155 = "לשבע ולא לרזון אמן  רועה צאן היה בארץ רועה נאמן וישמר על צאן מרעיתו ויאהבנה ויהי היום ותאחזהו תנומה ויישן וייקץ והנה צאן מרעיתו איננה איננה אהה אנה אני בא מחר איככה אשובה למעוני ביתי השמם בלעדי הצאן ואצא לדרך וארא והנה קוצים בעגלה ואמרה אין אלה קרני צאני האמללה איננה אהה אנה אני בא מחר איככה אשובה למעוני ביתי השמם בלעדי הצאן ואצא לדרך וארא והנה תפוחים בעגלה ואמרה אין אלה גלגלות צאני האמללה איננה אהה אנה אני בא מחר איככה אשובה למעוני ביתי השמם בלעדי הצאן ואצא לדרך וארא והנה אבנים בעגלה ואמרה אין אלה עצמות צאני האמללה איננה אהה אנה אני בא  מחר איככה אשובה למעוני ביתי השמם בלעדי הצאן";
            string song156 = "החמה מראש האילנות נסתלקה באו ונצא לקראת שבת המלכה הנה היא יורדת הקדושה הברוכה ועמה מלאכים צבא שלום ומנוחה באי באי המלכה באי באי המלכה שלום עליכם מלאכי השלום ";
            string song157 = "קבלנו פני שבת ברננה ותפלה הביתה נשובה בלב מלא גילה שם ערוך השלחן הנרות יאירו כל פנות הבית יזרחו יזהירו שבת שלום ומברך  שבת שלום ומברך באכם לשלום מלאכי השלום שבי זכה עמנו ובזיוך נא אורי לילה ויום אחר תעברי ואנחנו נכבדך בבגדי חמודות בזמירות ותפלות ובשלש סעדות ובמנוחה שלמה ובמנוחה נעמה ברכונו לשלום מלאכי השלום החמה מראש האילנות נסתלקה באו ונלוה את שבת המלכה צאתך לשלום הקדושה הזכה דעי ששת ימים אל שובך נחכה כן לשבת הבאה  כן לשבת הבאה צאתכם לשלום מלאכי השלום ";
            string song158 = "קן לצפור בין העצים ובקן לה שלש ביצים ובכל ביצה הס פן תעיר ישן לו אפרוח זעיר";
            string song159 = "קול מנסרהחרגל מבין חציר יתן קול איש בדשא אל יסתתר מי כרעים לווינתר צאו חרגלים צאו כרכרו זמרו לאלהים זמרו יענה כל הנמצא פה ברוך הוא וברוך שמו איש מכם באשר יכל במצלתים ובמחול ברוך אל שנתן לנו את הקיץ ושמחנו וערך לנו משתה שמן לשבע ולא לרזון אמן ";
            string song160 = "הו רב חובל קברניט שלי סופה כבר שככה אל הנמל שבעת קרבות חותרת ספינתך זרי פרחים פעמונים המון אדם צוהל כאשר ספינת הקרב שלך קרבה אל הנמל אבוי ליבי ליבי ליבי הו כתם דם שותת באשר רב החובל שלי צונח קר ומת";
            string song161 = "בוער אחים בוער אוי עיירתנו הדלה בוערת רוחות רעים בירגזון משברים מבעירים ומפיחיםביתר עוז בשלהבות פרא הכל מסביב כבר בוער ואתם עומדים ומביטים לכם כך בחיבוק ידיים ואתם עומדים ומביטים לכם כך כיצד עיירתנו בוערת";
            string song162 = "טום בלליי טום בלליי טום בללייקה טום בלליי טום בלליי טום בללייקה טום בללייקה נגני בללייקה טום בללייקה עלינו לשמוח עלמתי עלמה אבקש לשאול אותך מה יכול לצמוח לצמוח בלי גשם מה יכול להישרף ולא להיגמר מה יכול לערוג לבכות בלי דמעות ילדון טיפש מדוע אתה מוכרח לשאול אבן יכולה לצמוח לצמוח בלי גשם התקווה יכולה להישרף ולא להיגמר לב יכול לערוג לבכות בלי דמעות";
            string song163 = "שקט שקט בני נחרישה כאן צומחים קברים השונאים אותם נטעו פה מעברים אל פונאר דרכים יובילו דרך אין לחזר לבלי שוב הלך לו אבא ועמו האור שקט בני לי מטמוני לי אל נבכה בכאב כי בין כה וכה בכינו לא יבין אויב גם הים גבולות וחוף לו גם הכלא סיג וסוף לו ענותנו זאת היא בלי גבולות היא בלי גבולות";
            string song164 = "אל נא תאמרהנה דרכי האחרונה את אור היום הסתירו שמי העננה זה יום נכספנו לו עוד יעל ויבוא ומצעדנו עוד ירעיםאנחנו פה ";
            string song165 = "מארץ התמר עד ירכתי כפורים אנחנו פה במכאובות ויסורים ובאשר טפת דמנו שם נגרה הלא ינוב עוד עז רוחנו בגבורה עמוד השחר על יומנו אור יהל עם הצורר יחלף תמולנו כמו צל אך אם חלילה יאחר לבוא האור כמו סיסמה יהא השיר מדור לדור בכתב הדם והעופרת הוא נכתב הוא לא שירת צפור הדרור והמרחב כי בין קירות נופלים שרוהו כל העם יחדיו שרוהו וכלי נגן בידם";
            string song166 = "על כן אל נא תאמרדרכי האחרונה את אור היום הסתירו שמי העננה זה יום נכספנו לו עוד יעל ויבוא ומצעדנו עוד ירעיםאנחנו פה ";
            string song167 = "בין חרבותיה של פולין ראש בלונדיני יזהיב הראש החרבן שניהם יחד אמת שאין להשיב פולין הגבירה המרשעת הרויחה כחוק מכתה מן הסתם גבתה יסוריה בהקיצה משנתה דאליע מיינע דאליע גורלי מיינע דאליע בין חרבותיה של פולין ראש זהב תלתלים כמוהו שם מיליונים בין החרבות מטלים הה גורל גורלי מה המר הרע לי ארץ פולין הצוררת מלא המחיר גבתה שחורה וגם קודרת תנום היא את שנתה הה גורל גורלי מה המר הרע לי מה גבוה יותר מאשר בית מה מהיר יותר מעכבר מה עמוק יותר ממעיין מה מר מריר יותר מן המרה טום בלליי טום בלליי ארובה גבוהה יותר מבית חתול מהיר יותר מעכבר התורה עמוקה יותר ממעיין המוות מר מריר יותר מן המרה טום בלליי טום בלליי";
            string song168 = "שטמפפר בא וגוטמן בא וזרח ברנט ויואל משה סלומון עם חרב באבנט אם ציפורים אינן נראות המוות פה מולך כדאי לצאת מפה מהר הנה אני הולך";


            string[] arr = {
                song0, song1, song2, song3, song4, song5, song6, song7, song8,song9, song10, song11, song12, song13, song14, song15, song16, song17, song18, song19, song20, song21, song22, song23, song24, song25, song26, song27, song28, song29, song30, song31, song32, song33, song34, song35, song36, song37, song38, song39, song40, song41, song42, song43, song44, song45, song46, song47, song48, song49, song50, song51, song52, song53, song54, song55,song154, song155, song156, song157, song158, song159, song160, song161, song162, song163, song164, song165, song166, song167, song168
            };

            //song56, song57, song58, song59, song60, song61, song62, song63, song64, song65, song66, song67, song68, song69, song70, song71, song72, song73, song74, song75, song76, song77, song78, song79, song80, song81, song82, song83, song84, song85, song86, song87, song88, song89, song90, song91, song92, song93, song94, song95, song96, song97, song98, song99, song100, song101, song102, song103, song104, song105, song106, song107, song108, song109, song110, song111, song112, song113, song114, song115, song116, song117, song118, song119, song120, song121, song122, song123, song124, song125, song126, song127, song128, song129, song130, song131, song132, song133, song134, song135, song136, song137, song138, song139, song140, song141, song142, song143, song144, song145, song146, song147, song148, song149, song150, song151, song152, song153,

            string[,] matTags = new string[arr.Length, 3];
            for (int index = 0; index < arr.Length; index++)
            {
                var WordsArray = arr[index].Split(' ');
                string songName = WordsArray[0] + ' ' + WordsArray[1];
                Console.Write(" השיר:  " + songName + " ");
                List<string> abc1 = startUp(arr[index]);
                Console.WriteLine(" מספר התגיות הוא  :  " + abc1.Count);
                string crowded1 = lightStream(abc1);
                // string crowdedReverse = lightStreamReverse(abc1);
                matTags[index, 1] = crowded1;
                Console.WriteLine(crowded1);

            }

            //TODO fill the password
            HebrewNLP.Password = "OYnM3frqUbLTjFL";

            Console.OutputEncoding = new UTF8Encoding();
            Console.InputEncoding = new UTF8Encoding();


            Console.WriteLine((long)Tense.FUTURE + " = " + PartOfSpeech.ADVERB);
            Console.Write("ds");


        }

    }
}

