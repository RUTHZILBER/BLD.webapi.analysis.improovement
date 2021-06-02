using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
using System.Data;
using System.Text.RegularExpressions;
using Microsoft;



namespace BLL
{
    /// <summary>
    /// שיפור התגיות לפי הקלדות המשתמשים
    /// </summary>
    public class Improveement
    {

    /// <summary>
    /// פונקציה למציאת אינדקס של תת מערך , פונצית עזר לבאה GSIS
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static List<int> StartingIndexString(string[] x, string[] y, Ballad120Entities ent)
        {
            List<int> index = Enumerable.Range(0, x.Length - y.Length + 1).ToList();
            for (int i = 0; i < y.Length; i++)
            {
                index = index.Where(n => x[n + i] == y[i]).ToList();
            }
            return index;
        }

        /// <summary>
        /// xב y פונקציה המקבלת 2 מערכי תו ומחזירה רשימת אינדקסים של תחילת הופעת    
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static List<int> getStartingIndexesString(string[] x, string[] y, Ballad120Entities ent)
        {
            List<int> startsIndexes = new List<int>();

            foreach (int i in StartingIndexString(x, y,ent))
            {
                startsIndexes.Add(i);
            }
            return startsIndexes;
        }
       
        /// <summary>
        ///איתור רשימת השירים המתאימה לטקסט 
        /// </summary>
        /// <param name="text">הטקסט</param>
        /// <param name="ent">מבנה הנתונים כמודל EF</param>
        /// <param name="usrId">מזהה לקוח</param>
        /// <param name="state">מצב כל השירים או אישי</param>
        /// <returns></returns>
        public static RequestResult getMatchSongs(string text, Ballad120Entities ent,int usrId=0,string state="")
        {
            try
            {
                if (usrId == 0)
                {
                    //הסרת רווחים מיותרים
                    text = Regex.Replace(text, @"\s+", " ");//הסרת רווחים כפולים
                    if (text[0] == ' ')//הסרת רווח מתחילת המחרוזת
                        text = text.Substring(1, text.Length - 1);
                    if (text[text.Length - 1] == ' ')//הסרת רווח מסוף המחרוזת
                        text = text.Substring(0, text.Length - 1);

                    string[] textSplit = text.Split(' ');//תיבת הטקסט מפוצלת

                    List<DtoSong> correctSongs = new List<DtoSong>();

                    foreach (Song song in ent.Songs.ToList())
                    {
                        string[] wordsSong = song.songContect.Split(' ');
                        List<WordInform> wordInforms = new List<WordInform>();

                        List<List<WordInform>> songTagsInform = new List<List<WordInform>>();

                        string songContect = song.songContect;//תוכן השיר
                        songContect = songContect.Substring(0, songContect.Length);//הורדת תו של ירידת שורה המופיע בסוף כל שיר? ? ? בסוף לא, אם כן בסוף אז שנה למינוס 2
                        string[] songContectSplit = songContect.Split(' ');//פיצול תוכן השיר לפי מילים 
                                                                           //קבלת אינדקס מציאת תוכן תיבת הטקסט בתוכן השיר, אינדקס של מילה!
                        List<int> indexesFound = getStartingIndexesString(songContectSplit, textSplit, ent);//מציאת תוכן המערך טקסטספליט בתוך תוכן מערך סונגקונטקטספליט = מציאת תוכן כל תיבת הטקסט בתוך השיר



                        if (indexesFound.Count > 0)//הוספת השירים שחלק מתוכן תיבת האיתור נמצא בהם
                        {

                            correctSongs.Add(DtoSong.ConvertToDtoSong(song));
                        }

                    }

                    if (correctSongs.Count == 0)
                    {
                        return new RequestResult
                        {
                            Data = null,
                            Status = false,
                            Message = " error: not found the mathc songs"
                        };
                    }

                    return new RequestResult
                    {
                        Data = correctSongs,
                        Status = true,
                        Message = " good, the songs had entered succesfully "
                    };
                }
                else
                {
                    //הסרת רווחים מיותרים
                    text = Regex.Replace(text, @"\s+", " ");//הסרת רווחים כפולים
                    if (text[0] == ' ')//הסרת רווח מתחילת המחרוזת
                        text = text.Substring(1, text.Length - 1);
                    if (text[text.Length - 1] == ' ')//הסרת רווח מסוף המחרוזת
                        text = text.Substring(0, text.Length - 1);

                    string[] textSplit = text.Split(' ');//תיבת הטקסט מפוצלת

                    List<DtoSong> correctSongs = new List<DtoSong>();
                    List<Song> songs = ent.Songs.Where(i => i.userId == usrId).ToList();
                    if(state=="all")
                        songs= ent.Songs.Where(i => i.userId == usrId||i.isPermit==true).ToList();
                    foreach (Song song in songs)
                    {
                        int ddd;
                        if (song.songId ==1425)
                             ddd = 4;
                        string[] wordsSong = song.songContect.Split(' ');
                        List<WordInform> wordInforms = new List<WordInform>();

                        List<List<WordInform>> songTagsInform = new List<List<WordInform>>();

                        string songContect = song.songContect;//תוכן השיר
                        songContect = songContect.Substring(0, songContect.Length);//הורדת תו של ירידת שורה המופיע בסוף כל שיר? ? ? בסוף לא, אם כן בסוף אז שנה למינוס 2
                        string[] songContectSplit = songContect.Split(' ');//פיצול תוכן השיר לפי מילים 
                                                                           //קבלת אינדקס מציאת תוכן תיבת הטקסט בתוכן השיר, אינדקס של מילה!
                        List<int> indexesFound = getStartingIndexesString(songContectSplit, textSplit, ent);//מציאת תוכן המערך טקסטספליט בתוך תוכן מערך סונגקונטקטספליט = מציאת תוכן כל תיבת הטקסט בתוך השיר



                        if (indexesFound.Count > 0)//הוספת השירים שחלק מתוכן תיבת האיתור נמצא בהם
                        {

                            correctSongs.Add(DtoSong.ConvertToDtoSong(song));
                        }

                    }

                    if (correctSongs.Count == 0)
                    {
                        return new RequestResult
                        {
                            Data = null,
                            Status = false,
                            Message = " לא נמצא שיר מתאים לטקסט הנכתב "
                        };
                    }

                    return new RequestResult
                    {
                        Data = correctSongs,
                        Status = true,
                        Message = " נמצאה רשימת השירים המתאימה "
                    };
                }
            }
            catch (Exception e)
            {
                return new RequestResult
                {

                    Data = null,
                    Status = false,
                    
                    Message= " שגיאה במהלך התהליך "
                };
            }

        }

        /// <summary>
        /// שיפור התגיות בשיר לפי הטקסט מתיבת האיתור
        /// </summary>
        /// <param name="text">הטקסט שהתקבל</param>
        /// <param name="songId">קוד השיר לשיפור</param>
        /// <returns>עצם עם הערכים: הצלחת הפעולה, הודעה והתגיות המעודכנות</returns>
        public static RequestResult improveementTags(string text, int songId, Ballad120Entities ent)
        {
            try
            {
                //הסרת רווחים מיותרים
                text = Regex.Replace(text, @"\s+", " ");//הסרת רווחים כפולים
                if (text[0] == ' ')//הסרת רווח מתחילת המחרוזת
                    text = text.Substring(1, text.Length - 1);
                if (text[text.Length - 1] == ' ')//הסרת רווח מסוף המחרוזת
                    text = text.Substring(0, text.Length - 1);

                string[] textSplit = text.Split(' ');//תיבת הטקסט מפוצלת
                Song song = ent.Songs.Where(p => p.songId == songId).FirstOrDefault();

                string[] wordsSong = song.songContect.Split(' ');
                List<WordInform> wordInforms = new List<WordInform>();
                int k = 0;

                List<List<WordInform>> songTagsInform = new List<List<WordInform>>();
                int isti = 0;//אינדקס למערך המוצהר בשורה שמעל
                int i = 0;
                List<DAL.Tag> tags = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList();//התגיות עבור שיר זה ממוינות לפי סדר כרונולוגי

                //לולאה הרצה על כל תגיות השיר וממלאה את הרשימה 'מידע על תגיות שיר' בערכים מתאימים, בכל אינדקס של תגית במידע על התגית המתאים
                foreach (DAL.Tag tag in tags)
                {

                    string[] wordsTag = tag.tagName.Split(' ');//מידע על אורך התגית הנוכחית
                    songTagsInform.Add(new List<WordInform>());//הוספת איבר שהוא אינפורמציה על תגית אחת
                    for (i = 0; i < wordsTag.Length; i++)//סריקת כל מילות התגית
                    {
                        //השמת הערכים לכל מילה: מיקומה בשיר, בתגית, מספר התגית ומספר המילים בתגית אליה שייכת
                        songTagsInform[isti].Add(new WordInform(k++, wordsTag[i], isti, wordsTag.Length));

                    }
                    isti++;//קידום האינדקס לתגית הבאה
                }
                string songContect = song.songContect;//תוכן השיר
                songContect = songContect.Substring(0, songContect.Length);//הורדת תו של ירידת שורה המופיע בסוף כל שיר? ? ? בסוף לא, אם כן בסוף אז שנה למינוס 2
                string[] songContectSplit = songContect.Split(' ');//פיצול תוכן השיר לפי מילים 
                //קבלת אינדקס מציאת תוכן תיבת הטקסט בתוכן השיר, אינדקס של מילה!
                List<int> indexesFound = getStartingIndexesString(songContectSplit, textSplit,ent);//מציאת תוכן המערך טקסטספליט בתוך תוכן מערך סונגקונטקטספליט = מציאת תוכן כל תיבת הטקסט בתוך השיר
                int ii = 0, jj = 0;
                Song ss = ent.Songs.Where(dd => dd.songId == 404).ToList().FirstOrDefault();
                int found = 0;
                if (indexesFound.Count > 0)
                {
                    found = indexesFound[0];
                }
                else//התוכן לא נמצא, משום מה
                {
                    return new RequestResult
                    {
                        Data = null,
                        Status = false,
                        Message = " the textbox contect not found! "
                    };
                }


                int startLine = 0;//אינדקס התגית בה החלה תיבת הטקסט להופיע/להמצא
                int startColumn = 0;//האינדקסים במערך מילה בתגית בהם מתחילה תיבת הטקסט להמצא...
                foreach (List<WordInform> tagit in songTagsInform)//סריקת כל התגיות
                {
                    foreach (WordInform wordInTagit in tagit)//סריקת כל מילות התגית
                    {
                        if (wordInTagit.SongIndex == found)//במילה זו אכן תוכן תיבת הטקסט החל להמצא
                        {
                            startLine = ii;
                            startColumn = jj;
                            break;
                        }
                        jj++;
                    }
                    ii++;
                }


                int length = textSplit.Length;//אורך תוכן תיבת הטקסט

                int column = 9;
                List<WordInform> wisList = new List<WordInform>();//רשימה להכנסה זמנית של קבוצת מילים מתגית
                int count = 0;//מונה עד אורך תוכן תיבת הטקסט
                i = startLine;//מספר התגית הראשונה בה נמצא תחילת תוכן תיבת הטקסט 
                for (int o = 0; o < songTagsInform[i].Count(); o++)
                {
                    if (songTagsInform[i][o].SongIndex == startColumn)
                    {
                        column = o;
                        break;
                    }
                }
                List<List<WordInform>> matchTags = new List<List<WordInform>>();//לכאן יכנסו התגיות הקשורות לתיבת הטקסט מהשיר המקורי

                int imT = 0;//האינדקס עבור המערך שהוצהר בשורה הקודמת
                matchTags.Add(new List<WordInform>());
                while (count < length)//כל עוד לא נמצאו כל המילים
                {

                    wisList = songTagsInform[i];//השמת תגית מסוימת
                    for (int h = column; h < wisList.Count() && count < length; h++)//מעבר על מילות התגית, בתגית הראשונה מהמיקום בו נמצאה ההתאמה ובתגית האחרונה עד שלא נמצאו כל המילים שבתיבת הטקסט, לפי סדר כרונולוגי
                    {

                        matchTags[imT].Add(wisList[h]);//הוספת התגית
                        count++;

                    }
                    imT++;
                    column = 0;//בפעם הראשונה בלבד אין להתחיל מאפס, אך בהמשך חובה
                    i++;//התגית הבאה
                    if (count < length)//הוספת תא למערך מלבד בפעם האחרונה שאז אין צורך בתא ריק
                        matchTags.Add(new List<WordInform>());

                }

                int countNewTag = 0;//מספר המילים שהוקשו בתיבת הטקסט, הפוריצ' הבא מיותר כי אפשר להגיע דרך אורך תיבת הטקסט, מידע זה שקול ל: string [] c=text.split(' '); int countNewTag=c.Count();
                foreach (List<WordInform> wordInform in matchTags)
                {
                    countNewTag += wordInform.Count();
                }
                string newTag = "";//התגית החדשה
                foreach (List<WordInform> wordInform in matchTags)
                {
                    foreach (WordInform wi in wordInform)
                    {
                        newTag += wi.Word + " ";//שרשור מילות התגית אל התגית החדשה
                    }
                }
                newTag = newTag.Substring(0, newTag.Length - 1);//הסרת הרווח האחרון


                List<DtoTag> dtoTags = new List<DtoTag>();//החזרת רשימת התגיות המעודכנת
                //ניתן לוותר על הפוריצ' האחרון ופשוט להשתמש בתיבת הטקסט שהתקבלה כפרמטר לפונקציה

                if (countNewTag < 11 && matchTags.Count > 1)//מספר המילים בתגית הראשונה קטן מ11, ולכן תוכן תיבת הטקסט יהיה תגית חדשה
                {
                    //עץ בינארי בעל 4 רמות שהבן הימני חיובי והשמאלי שלילי. שורש-> התחלה-סוף-ראשון-אחרון
                    //התחלה=התגית החדשה מתחילה בתחילת תגית מקור
                    //סוף=התגית החדשה מסתיימת בסוף תגית מקור
                    //ראשונה=תחילת תיבת הטקסט נלקח מלפחות חלק מתגית ראשונה בשיר
                    //אחרונה=סוף תיבת הטקסט נלקח מלפחות חלק מתגית אחרונה בשיר
                    //מספור העלים מימין לשמאל: 1 עד 16 כולל
                    //
                    //התחלה = המילה הראשונה בטקסט הנדרש באתור, פותחת תגית בשיר המקור
                    //לא התחלה = המילה הראשונה בטקסט הנדרש באתור, אינה פותחת תגית בשיר המקור
                    //סיום = המילה האחרונה בטקסט הנדרש באיתור, חותמת תגית בשיר המקור
                    //לא סיום = המילה האחרונה בטקסט הנדרש באיתור, לא חותמת תגית בשיר המקור
                    //ראשון = הטקסט הנדרש באיתור, שייך (לפחות לסוף ) תגית ראשונה בשיר המקור
                    //לא ראשון = הטקסט הנדרש באיתור, לא שייך(לפחות לסוף) תגית ראשונה בשיר המקור
                    //אחרון = הטקסט הנדרש באיתור, שייך (לפחות לתחילת התגית ) לתגית האחרונה בשיר המקור
                    //לא אחרון = הטקסט הנדרש באיתור, לא  שייך (לפחות לתחילת התגית) לתגית האחרונה בשיר המקור

                    if (matchTags[0].Count == songTagsInform[matchTags[0][0].TagIndex].Count)//התחלה = המילה הראשונה בטקסט הנדרש באתור, פותחת תגית בשיר המקור
                    {
                        if (matchTags[matchTags.Count - 1].Count == matchTags[matchTags.Count - 1][0].TagCountWords)// סיום = המילה האחרונה בטקסט הנדרש באיתור, חותמת תגית בשיר המקור

                        {

                            if (matchTags[0][0].SongIndex < songTagsInform[0].Count) //ראשון = הטקסט הנדרש באיתור, שייך (לפחות לסוף ) תגית ראשונה בשיר המקור.
                            {
                                if (matchTags[matchTags.Count - 1][0].SongIndex == songTagsInform[songTagsInform.Count - 1][0].SongIndex)//אחרון = הטקסט הנדרש באיתור, שייך (לפחות לתחילת התגית ) לתגית האחרונה בשיר המקור.
                                {
                                    //1
                                    //הסבר ע"פ העץ הבינארי: סטטוס התווית => התחלה => סיום => ראשון => אחרון.


                                    int areaNewTag = matchTags[0][0].TagIndex;//המיקום אליו תכנס התגית החדשה
                                    ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine].tagName = text;//הכנסת התגית החדשה


                                    for (i = 1; i < matchTags.Count; i++)//מחיקת התגיות המיותרות ששורשרו בעבר
                                    {

                                        DAL.Tag theTag = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine + i];//התגית שצריכה להמחק
                                        ent.Tags.Remove(theTag);//המחיקה בפועל
                                    }

                                    ent.SaveChanges();//שמירת השנויים בבסיס הנתונים
                                    //| לכידת כל מילות השיר לתגית אחת. (=זה כל השיר)

                                }
                                else   //לא אחרון = הטקסט הנדרש באיתור, לא  שייך(לפחות לתחילת התגית) לתגית האחרונה בשיר המקור.
                                {
                                    //2
                                    //הסבר ע"פ העץ הבינארי: סטטוס התווית => התחלה => סיום => ראשון => לא אחרון.
                                    int areaNewTag = matchTags[0][0].TagIndex;//המיקום אליו תכנס התגית החדשה
                                    ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine].tagName = text;//הכנסת התגית החדשה


                                    for (i = 1; i < matchTags.Count; i++)//מחיקת התגיות המיותרות ששורשרו בעבר
                                    {

                                        DAL.Tag theTag = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine + i];//התגית שצריכה להמחק
                                        ent.Tags.Remove(theTag);//המחיקה בפועל
                                    }

                                    ent.SaveChanges();//שמירת השנויים בבסיס הנתונים
                                }
                            }

                            else  //לא ראשון = הטקסט הנדרש באיתור, לא שייך(לפחות לסוף) תגית ראשונה בשיר המקור
                            {
                                if (matchTags[matchTags.Count - 1][0].SongIndex == songTagsInform[songTagsInform.Count - 1][0].SongIndex) //אחרון = הטקסט הנדרש באיתור, שייך (לפחות לתחילת התגית ) לתגית האחרונה בשיר המקור.
                                {

                                    //3
                                    //התחלה - סוף - לא ראשונה - אחרונה
                                    int areaNewTag = matchTags[0][0].TagIndex;//המיקום אליו תכנס התגית החדשה
                                    ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine].tagName = text;//הכנסת התגית החדשה


                                    for (i = 1; i < matchTags.Count; i++)//מחיקת התגיות המיותרות ששורשרו בעבר
                                    {

                                        DAL.Tag theTag = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine + i];//התגית שצריכה להמחק
                                        ent.Tags.Remove(theTag);//המחיקה בפועל
                                    }

                                    ent.SaveChanges();//שמירת השנויים בבסיס הנתונים
                                }
                                else   //לא אחרון = הטקסט הנדרש באיתור, לא  שייך (לפחות לתחילת התגית) לתגית האחרונה בשיר המקור.
                                {
                                    //4
                                    //התחלה - סוף - לא ראשון - לא אחרון
                                    int areaNewTag = matchTags[0][0].TagIndex;//המיקום אליו תכנס התגית החדשה
                                    ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine].tagName = text;//הכנסת התגית החדשה


                                    for (i = 1; i < matchTags.Count; i++)//מחיקת התגיות המיותרות ששורשרו בעבר
                                    {

                                        DAL.Tag theTag = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine + i];//התגית שצריכה להמחק
                                        ent.Tags.Remove(theTag);//המחיקה בפועל
                                    }

                                    ent.SaveChanges();//שמירת השנויים בבסיס הנתונים
                                }
                            }

                            //1234 same, ha?

                        }

                        else//לא סיום = המילה האחרונה בטקסט הנדרש באיתור, לא חותמת תגית בשיר המקור.
                        {
                            if (matchTags[0][0].SongIndex < songTagsInform[0].Count) //ראשון = הטקסט הנדרש באיתור, שייך (לפחות לסוף ) תגית ראשונה בשיר המקור.
                            {
                                if (matchTags[matchTags.Count - 1][0].SongIndex == songTagsInform[songTagsInform.Count - 1][0].SongIndex)//אחרון = הטקסט הנדרש באיתור, שייך (לפחות לתחילת התגית ) לתגית האחרונה בשיר המקור.
                                {
                                    //5

                                    if (matchTags[matchTags.Count - 1].Count + 1 < matchTags[matchTags.Count - 1][0].TagCountWords)//נותרה יותר מממילה אחת עד סיום השיר (ניתן לומר גם: התגית הנוכחית...) ייי

                                    {
                                        //עד מילה זו

                                        int areaNewTag = startLine;//המיקום אליו תכנס התגית החדשה

                                        ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[areaNewTag].tagName = text;//הכנסת התגית החדשה
                                        string newNextTag = "";
                                        for (int l = (matchTags[matchTags.Count - 1].Count - 1 + 1); l < songTagsInform[songTagsInform.Count - 1].Count; l++)//התגית האחרונה המורכבת מהמילה הבאה ואילך
                                        {
                                            newNextTag = newNextTag + songTagsInform[songTagsInform.Count - 1][l].Word + " ";
                                        }
                                        newNextTag = newNextTag.Substring(0, newNextTag.Length - 1);//הסרת הרווח האחרון


                                        ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine + matchTags.Count - 1].tagName = newNextTag;
                                        for (i = 1; i < matchTags.Count - 1; i++)//מחיקת התגיות המיותרות ששורשרו בעבר
                                        {

                                            DAL.Tag theTag = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine + i];//התגית שצריכה להמחק
                                            ent.Tags.Remove(theTag);//המחיקה בפועל
                                        }

                                        ent.SaveChanges();//שמירת השנויים בבסיס הנתונים


                                    }
                                    else//נותרה מילה אחת כולל המילה הזו
                                    {
                                        //עד סוף השיר


                                        int areaNewTag = matchTags[0][0].TagIndex;//המיקום אליו תכנס התגית החדשה
                                        text = songContect;
                                        ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine].tagName = text;//הכנסת התגית החדשה


                                        for (i = 1; i < matchTags.Count; i++)//מחיקת התגיות המיותרות ששורשרו בעבר
                                        {

                                            DAL.Tag theTag = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine + i];//התגית שצריכה להמחק
                                            ent.Tags.Remove(theTag);//המחיקה בפועל
                                        }

                                        ent.SaveChanges();//שמירת השנויים בבסיס הנתונים
                                    }


                                }
                                else   //לא אחרון = הטקסט הנדרש באיתור, לא  שייך (לפחות לתחילת התגית) לתגית האחרונה בשיר המקור.
                                {
                                    //6

                                    if (matchTags[matchTags.Count - 1].Count + 1 < matchTags[matchTags.Count - 1][0].TagCountWords)//נותרה יותר מממילה אחת עד סיום  התגית הנוכחית

                                    {
                                        //עד מילה זו

                                        int areaNewTag = startLine;//המיקום אליו תכנס התגית החדשה



                                        string newNextTag = "";
                                        int ll = (matchTags[matchTags.Count - 1][0].TagIndex);
                                        for (int l = (matchTags[matchTags.Count - 1].Count - 1 + 1); l < songTagsInform[ll].Count; l++)//התגית האחרונה המורכבת מהמילה הבאה ואילך
                                        {
                                            newNextTag = newNextTag + songTagsInform[ll][l].Word + " ";
                                        }
                                        newNextTag = newNextTag.Substring(0, newNextTag.Length - 1);//הסרת הרווח האחרון

                                        ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[areaNewTag].tagName = text;//הכנסת התגית החדשה
                                        ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine + matchTags.Count - 1].tagName = newNextTag;
                                        for (i = 1; i < matchTags.Count - 1; i++)//מחיקת התגיות המיותרות ששורשרו בעבר
                                        {

                                            DAL.Tag theTag = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine + i];//התגית שצריכה להמחק
                                            ent.Tags.Remove(theTag);//המחיקה בפועל
                                        }

                                        ent.SaveChanges();//שמירת השנויים בבסיס הנתונים


                                    }
                                    else//נותרה מילה אחת כולל המילה הזו
                                    {
                                        //עד סוף השיר


                                        int areaNewTag = startLine;//המיקום אליו תכנס התגית החדשה
                                        int areaNextNewTag = areaNewTag + 1;
                                        string newNextNewTag = "";
                                        int ll = matchTags[matchTags.Count - 1][0].TagIndex;
                                        //songTagsInform[ll].Count
                                        newNextNewTag = newNextNewTag + " " + songTagsInform[ll][songTagsInform[ll].Count - 1].Word;


                                        ll++;
                                        for (int l = 0; l < songTagsInform[ll].Count; l++)
                                        {
                                            newNextNewTag = newNextNewTag + " " + songTagsInform[ll][l].Word;
                                        }
                                        //אין רווח אחרון וממילא אין צורך בהסרתו
                                        ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[areaNewTag].tagName = text;//הכנסת התגית החדשה
                                        ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[areaNextNewTag].tagName = newNextNewTag;//הכנסת התגית 
                                        //  החדשה




                                        for (i = 2; i < matchTags.Count + 1; i++)//מחיקת התגיות המיותרות ששורשרו בעבר
                                        {

                                            DAL.Tag theTag = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine + i];//התגית שצריכה להמחק
                                            ent.Tags.Remove(theTag);//המחיקה בפועל
                                        }




                                        ent.SaveChanges();//שמירת השנויים בבסיס הנתונים
                                    }
                                }
                            }

                            else  //לא ראשון = הטקסט הנדרש באיתור, לא שייך(לפחות לסוף) תגית ראשונה בשיר המקור.
                            {
                                if (matchTags[matchTags.Count - 1][0].SongIndex == songTagsInform[songTagsInform.Count - 1][0].SongIndex) //אחרון = הטקסט הנדרש באיתור, שייך (לפחות לתחילת התגית ) לתגית האחרונה בשיר המקור.
                                {

                                    //7

                                    if (matchTags[matchTags.Count - 1].Count + 1 < matchTags[matchTags.Count - 1][0].TagCountWords)//נותרה יותר מממילה אחת עד סיום השיר (ניתן לומר גם: התגית הנוכחית...) ייי

                                    {
                                        // עד מילה זו

                                        int areaNewTag = startLine;//המיקום אליו תכנס התגית החדשה

                                        ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[areaNewTag].tagName = text;//הכנסת התגית החדשה
                                        string newNextTag = "";
                                        for (int l = (matchTags[matchTags.Count - 1].Count - 1 + 1); l < songTagsInform[songTagsInform.Count - 1].Count; l++)//התגית האחרונה המורכבת מהמילה הבאה ואילך
                                        {
                                            newNextTag = newNextTag + songTagsInform[songTagsInform.Count - 1][l].Word + " ";
                                        }
                                        newNextTag = newNextTag.Substring(0, newNextTag.Length - 1);//הסרת הרווח האחרון


                                        ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine + matchTags.Count - 1].tagName = newNextTag;
                                        for (i = 1; i < matchTags.Count - 1; i++)//מחיקת התגיות המיותרות ששורשרו בעבר
                                        {

                                            DAL.Tag theTag = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine + i];//התגית שצריכה להמחק
                                            ent.Tags.Remove(theTag);//המחיקה בפועל
                                        }

                                        ent.SaveChanges();//שמירת השנויים בבסיס הנתונים


                                    }
                                    else//נותרה מילה אחת כולל המילה הזו
                                    {
                                        //עד סוף השיר


                                        int areaNewTag = startLine;//המיקום אליו תכנס התגית החדשה
                                        text = text + " " + songContectSplit[songContectSplit.Length - 1];
                                        ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[areaNewTag].tagName = text;//הכנסת התגית החדשה


                                        for (i = 1; i < matchTags.Count; i++)//מחיקת התגיות המיותרות ששורשרו בעבר
                                        {

                                            DAL.Tag theTag = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine + i];//התגית שצריכה להמחק
                                            ent.Tags.Remove(theTag);//המחיקה בפועל
                                        }

                                        ent.SaveChanges();//שמירת השנויים בבסיס הנתונים
                                    }
                                }
                                else   //לא אחרון = הטקסט הנדרש באיתור, לא  שייך (לפחות לתחילת התגית) לתגית האחרונה בשיר המקור.
                                {
                                    //8


                                    if (matchTags[matchTags.Count - 1].Count + 1 < matchTags[matchTags.Count - 1][0].TagCountWords)//נותרה יותר מממילה אחת עד סיום  התגית הנוכחית

                                    {
                                        //עד מילה זו

                                        int areaNewTag = startLine;//המיקום אליו תכנס התגית החדשה



                                        string newNextTag = "";
                                        int ll = (matchTags[matchTags.Count - 1][0].TagIndex);
                                        for (int l = (matchTags[matchTags.Count - 1].Count - 1 + 1); l < songTagsInform[ll].Count; l++)//התגית האחרונה המורכבת מהמילה הבאה ואילך
                                        {
                                            newNextTag = newNextTag + songTagsInform[ll][l].Word + " ";
                                        }
                                        newNextTag = newNextTag.Substring(0, newNextTag.Length - 1);//הסרת הרווח האחרון

                                        ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[areaNewTag].tagName = text;//הכנסת התגית החדשה
                                        ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine + matchTags.Count - 1].tagName = newNextTag;
                                        for (i = 1; i < matchTags.Count - 1; i++)//מחיקת התגיות המיותרות ששורשרו בעבר
                                        {

                                            DAL.Tag theTag = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine + i];//התגית שצריכה להמחק
                                            ent.Tags.Remove(theTag);//המחיקה בפועל
                                        }

                                        ent.SaveChanges();//שמירת השנויים בבסיס הנתונים


                                    }
                                    else//נותרה מילה אחת כולל המילה הזו
                                    {
                                        //עד סוף השיר


                                        int areaNewTag = startLine;//המיקום אליו תכנס התגית החדשה
                                        int areaNextNewTag = areaNewTag + 1;
                                        string newNextNewTag = "";
                                        int ll = matchTags[matchTags.Count - 1][0].TagIndex;

                                        newNextNewTag = newNextNewTag + " " + songTagsInform[ll][songTagsInform[ll].Count - 1].Word;


                                        ll++;
                                        for (int l = 0; l < songTagsInform[ll].Count; l++)
                                        {
                                            newNextNewTag = newNextNewTag + " " + songTagsInform[ll][l].Word;
                                        }
                                        //אין רווח אחרון וממילא אין צורך בהסרתו
                                        ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[areaNewTag].tagName = text;//הכנסת התגית החדשה
                                        ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[areaNextNewTag].tagName = newNextNewTag;//הכנסת התגית 
                                        //  החדשה




                                        for (i = 2; i < matchTags.Count + 1; i++)//מחיקת התגיות המיותרות ששורשרו בעבר
                                        {

                                            DAL.Tag theTag = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine + i];//התגית שצריכה להמחק
                                            ent.Tags.Remove(theTag);//המחיקה בפועל
                                        }




                                        ent.SaveChanges();//שמירת השנויים בבסיס הנתונים
                                    }
                                }
                            }
                        }
                    }
                    else    //לא התחלה = המילה הראשונה בטקסט הנדרש באתור, אינה פותחת תגית בשיר המקור.
                    {
                        if (matchTags[matchTags.Count - 1].Count == matchTags[matchTags.Count - 1][0].TagCountWords)// סיום = המילה האחרונה בטקסט הנדרש באיתור, חותמת תגית בשיר המקור

                        {

                            if (matchTags[0][0].SongIndex < songTagsInform[0].Count) //ראשון = הטקסט הנדרש באיתור, שייך (לפחות לסוף ) תגית ראשונה בשיר המקור.
                            {
                                if (matchTags[matchTags.Count - 1][0].SongIndex == songTagsInform[songTagsInform.Count - 1][0].SongIndex)//אחרון = הטקסט הנדרש באיתור, שייך (לפחות לתחילת התגית ) לתגית האחרונה בשיר המקור.
                                {
                                    //9
                                    int difference = matchTags[0][0].SongIndex - songTagsInform[matchTags[0][0].TagIndex][0].SongIndex;//ההפרש בין המיקום של המילה הראשונה שהוקשה בתגית לבין אורך התגית הזו
                                    if (difference != 1)//מספר המילים מתחילת השיר עד תחילת תוכן האיתור, גדול מ1
                                    {
                                        string newTagFirst = "";
                                        for (int l = 0; l < matchTags[0][0].SongIndex; l++)//שרשור התגית של המילה הראשונה
                                        {
                                            newTagFirst = newTagFirst + songContectSplit[l] + " ";
                                        }
                                        newTagFirst = newTagFirst.Substring(0, newTagFirst.Length - 1);
                                        ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine].tagName = newTagFirst;//הכנסת התגית הראשונה
                                        ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine + 1].tagName = text;//הכנסת התגית החדשה, האחרונה

                                        int length1 = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList().Count;
                                        for (i = 2; i < length1; i++)//מחיקת התגיות המיותרות ששורשרו בעבר
                                        {

                                            DAL.Tag theTag = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[i];//התגית שצריכה להמחק
                                            ent.Tags.Remove(theTag);//המחיקה בפועל
                                        }

                                        ent.SaveChanges();//שמירת השנויים בבסיס הנתונים
                                                          //| לכידת כל מילות השיר לתגית אחת. (=זה כל השיר)

                                    }
                                    else//מספר המילים מתחילת השיר עד תחילת תוכן האיתור, שווה ל1
                                    {
                                        int areaNewTag = matchTags[0][0].TagIndex;//המיקום אליו תכנס התגית החדשה
                                        ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine].tagName = text;//הכנסת התגית החדשה


                                        for (i = 1; i < matchTags.Count; i++)//מחיקת התגיות המיותרות ששורשרו בעבר
                                        {

                                            DAL.Tag theTag = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine + i];//התגית שצריכה להמחק
                                            ent.Tags.Remove(theTag);//המחיקה בפועל
                                        }

                                        ent.SaveChanges();//שמירת השנויים בבסיס הנתונים
                                                          //| לכידת כל מילות השיר לתגית אחת. (=זה כל השיר)
                                    }

                                }
                                else   //לא אחרון = הטקסט הנדרש באיתור, לא  שייך(לפחות לתחילת התגית) לתגית האחרונה בשיר המקור.
                                {
                                    //10

                                    int difference = matchTags[0][0].SongIndex - songTagsInform[matchTags[0][0].TagIndex][0].SongIndex;//ההפרש בין המיקום של המילה הראשונה שהוקשה בתגית לבין אורך התגית הזו
                                    if (difference != 1)//מספר המילים מתחילת השיר עד תחילת תוכן האיתור, גדול מ1
                                    {


                                        string newTagFirst = "";
                                        for (int l = 0; l < matchTags[0][0].SongIndex; l++)//שרשור התגית של המילה הראשונה
                                        {
                                            newTagFirst = newTagFirst + songContectSplit[l] + " ";
                                        }
                                        newTagFirst = newTagFirst.Substring(0, newTagFirst.Length - 1);
                                        ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[0].tagName = newTagFirst;//הכנסת התגית הקודמת
                                        ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[1].tagName = text;//הכנסת התגית החדשה
                                        int length1 = matchTags.Count;
                                        for (int l = 2; l < length1; l++)
                                        {
                                            DAL.Tag theTag = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[l];//התגית שצריכה להמחק
                                            ent.Tags.Remove(theTag);//המחיקה בפועל
                                        }
                                        ent.SaveChanges();
                                    }
                                    else//מילה אחת
                                    {
                                        string newTagit = songContectSplit[0] + " " + text;
                                        ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[0].tagName = newTagit;//הכנסת התגית החדשה
                                        int length1 = matchTags.Count;
                                        for (int l = 1; l < length1; l++)
                                        {
                                            DAL.Tag theTag = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[l];//התגית שצריכה להמחק
                                            ent.Tags.Remove(theTag);//המחיקה בפועל
                                        }
                                        ent.SaveChanges();
                                    }

                                    // לכידה כך: מ: @מספר המילים מתחילת השיר עד תחילת תוכן האיתור, גדול מ1: מהמילה הראשונה שהוקשה באיתור(עד אליה תגית נפרדת).
                                    //@ אחרת, (= מילה אחת):  מתחילת השיר. ("לא... אומר")
                                    //עד סוף תגית.

                                }
                            }

                            else  //לא ראשון = הטקסט הנדרש באיתור, לא שייך(לפחות לסוף) תגית ראשונה בשיר המקור
                            {
                                if (matchTags[matchTags.Count - 1][0].SongIndex == songTagsInform[songTagsInform.Count - 1][0].SongIndex) //אחרון = הטקסט הנדרש באיתור, שייך (לפחות לתחילת התגית ) לתגית האחרונה בשיר המקור.
                                {

                                    //11

                                    int difference = matchTags[0][0].SongIndex - songTagsInform[matchTags[0][0].TagIndex][0].SongIndex;//ההפרש בין המיקום של המילה הראשונה שהוקשה בתגית לבין אורך התגית הזו
                                    if (difference != 1)//מספר המילים מתחילת השיר עד תחילת תוכן האיתור, גדול מ1
                                    {
                                        string newTagFirst = "";
                                        for (int l = 0; l < matchTags[0][0].TagCountWords - matchTags[0].Count; l++)//שרשור התגית של המילה הראשונה
                                        {
                                            newTagFirst = newTagFirst + songTagsInform[matchTags[0][0].TagIndex][l].Word + " ";
                                        }
                                        newTagFirst = newTagFirst.Substring(0, newTagFirst.Length - 1);
                                        ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine].tagName = newTagFirst;//הכנסת התגית הקודמת
                                        ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine + 1].tagName = text;//הכנסת התגית החדשה
                                        int length1 = matchTags.Count;
                                        for (int l = 2; l < length1; l++)
                                        {
                                            DAL.Tag theTag = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[l];//התגית שצריכה להמחק
                                            ent.Tags.Remove(theTag);//המחיקה בפועל
                                        }
                                        ent.SaveChanges();

                                    }
                                    else
                                    {
                                        string newBackTag = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine - 1].tagName;
                                        newBackTag = newBackTag + " " + songTagsInform[matchTags[0][0].TagIndex][0].Word;
                                        ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine - 1].tagName = newBackTag;//הכנסת התגית הקודמת

                                        ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine].tagName = text;//הכנסת התגית הקודמת

                                        int length1 = matchTags.Count;
                                        for (int l = 1; l < length1; l++)
                                        {
                                            DAL.Tag theTag = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine + l];//התגית שצריכה להמחק
                                            ent.Tags.Remove(theTag);//המחיקה בפועל
                                        }
                                        ent.SaveChanges();

                                    }

                                }
                                else   //לא אחרון = הטקסט הנדרש באיתור, לא  שייך (לפחות לתחילת התגית) לתגית האחרונה בשיר המקור.
                                {
                                    //12
                                    int difference = matchTags[0][0].SongIndex - songTagsInform[matchTags[0][0].TagIndex][0].SongIndex;//ההפרש בין המיקום של המילה הראשונה שהוקשה בתגית לבין אורך התגית הזו
                                    if (difference != 1)//מספר המילים מתחילת השיר עד תחילת תוכן האיתור, גדול מ1
                                    {
                                        string newTagFirst = "";
                                        for (int l = 0; l < matchTags[0][0].TagCountWords - matchTags[0].Count; l++)//שרשור התגית של המילה הראשונה
                                        {
                                            newTagFirst = newTagFirst + songTagsInform[matchTags[0][0].TagIndex][l].Word + " ";
                                        }
                                        newTagFirst = newTagFirst.Substring(0, newTagFirst.Length - 1);
                                        ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine].tagName = newTagFirst;//הכנסת התגית הקודמת
                                        ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine + 1].tagName = text;//הכנסת התגית החדשה
                                        int length1 = matchTags.Count;
                                        for (int l = 2; l < length1; l++)
                                        {
                                            DAL.Tag theTag = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[l];//התגית שצריכה להמחק
                                            ent.Tags.Remove(theTag);//המחיקה בפועל
                                        }
                                        ent.SaveChanges();

                                    }
                                    else
                                    {
                                        string newBackTag = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine - 1].tagName;
                                        newBackTag = newBackTag + " " + songTagsInform[matchTags[0][0].TagIndex][0].Word;
                                        ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine - 1].tagName = newBackTag;//הכנסת התגית הקודמת

                                        ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine].tagName = text;//הכנסת התגית הקודמת

                                        int length1 = matchTags.Count;
                                        for (int l = 1; l < length1; l++)
                                        {
                                            DAL.Tag theTag = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine + l];//התגית שצריכה להמחק
                                            ent.Tags.Remove(theTag);//המחיקה בפועל
                                        }
                                        ent.SaveChanges();

                                    }
                                }
                            }



                        }


                        else//לא סיום = המילה האחרונה בטקסט הנדרש באיתור, לא חותמת תגית בשיר המקור.
                        {
                            if (matchTags[0][0].SongIndex < songTagsInform[0].Count) //ראשון = הטקסט הנדרש באיתור, שייך (לפחות לסוף ) תגית ראשונה בשיר המקור.
                            {
                                if (matchTags[matchTags.Count - 1][0].SongIndex == songTagsInform[songTagsInform.Count - 1][0].SongIndex)//אחרון = הטקסט הנדרש באיתור, שייך (לפחות לתחילת התגית ) לתגית האחרונה בשיר המקור.
                                {
                                    //13

                                    int difference = matchTags[0][0].SongIndex - songTagsInform[matchTags[0][0].TagIndex][0].SongIndex;//ההפרש בין המיקום של המילה הראשונה שהוקשה בתגית לבין אורך התגית הזו
                                    if (difference != 1)//מספר המילים מתחילת השיר עד תחילת תוכן האיתור, גדול מ1
                                    {
                                        if (matchTags[matchTags.Count - 1][(matchTags[matchTags.Count - 1].Count - 1)].SongIndex + 1 < songTagsInform[(matchTags[matchTags.Count - 1][(matchTags[matchTags.Count - 1].Count - 1)].TagIndex)][(songTagsInform[(matchTags[matchTags.Count - 1][(matchTags[matchTags.Count - 1].Count - 1)].TagIndex)].Count - 1)].SongIndex)// נותרה יותר ממילה אחת עד סוף התגית
                                        {
                                            //13-1
                                            //יתכן ומספר התגיות יגדלו מבעבר, לכן יש למחק מתחילת התגית הנבחרת עד הסוף ולשכפל. להוסיף נקודה עבור התגית הנוכחית, הנבחרת
                                            string newPrevTag = "";//תגית קודמת
                                            string newCurrentTag = "";//תגית נוכחית
                                            string newNextTag = "";//תגית הבאה
                                            newCurrentTag = text;//תוכן תיבת הטקסט בשלמות
                                            for (int l = 0; l < songTagsInform[matchTags[0][0].TagIndex].Count - matchTags[0].Count; l++)//כל מילות התגית הקודמת החדשה, שהיא תחילת התגית הנוכחית עד תיבת הטקסט
                                            {
                                                newPrevTag += songTagsInform[matchTags[0][0].TagIndex][l].Word + " ";
                                            }
                                            newPrevTag = newPrevTag.Substring(0, newPrevTag.Length - 1);//הסרת הרווח האחרון

                                            int baseCount = songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex].Count;//מספר המילים בתגית האחרונה ממנה לקוח תוכן תיבת הטקסט
                                            int thisCount = matchTags[matchTags.Count - 1].Count;//מספר המילים בתגית האחרונה שהוקלדו בתיבת האתור
                                            int times = baseCount - thisCount;//נניח שלוש פחות אחד, מספר המילים בסוף התגית שיש לסרוק
                                            for (int l = 0; l < times; l++)//תחילת התגית הבאה החדשה
                                            {
                                                newNextTag += songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex][thisCount + l].Word + " ";
                                            }
                                            newNextTag = newNextTag.Substring(0, newNextTag.Length - 1);//הסרת רווח אחרון

                                            //יש להעתיק את כל התגיות הנותרות לתגיות חדשות
                                            //מאחר ויש תגית חדשה נוספת

                                            int from = 0;//מכאן למחק
                                            int toEnd = 0;//מחיקה עד הסוף

                                            from = startLine;//הטקסט מתיבת האיתור שיך בתחילתו לתגית זו
                                            toEnd = songTagsInform[songTagsInform.Count - 1][0].TagIndex + 1;//עד התגית האחרונה בשיר המחיקה
                                            List<string> lastTags = new List<string>();//שימור התגיות להוספה
                                            lastTags.Add(newPrevTag);//התגית הקודמת
                                            lastTags.Add(newCurrentTag);//התגית הנוכחית, תוכן תיבת הטקסט
                                            lastTags.Add(newNextTag);//התגית הבאה

                                            List<int> points = new List<int>();//רשימת ניקוד לתגיות המועתקות
                                            points.Add(0);//תגית חדשה ללא ניקוד
                                            points.Add(1);//התגית מקבלת כעת ניקוד אחד
                                            points.Add(0);//תגית חדשה ללא ניקוד
                                            for (int l = from; l < toEnd; l++)//הסרת התגיות עד הסוף
                                            {
                                                DAL.Tag tag = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[l];//קבלת התגית
                                                string currentDeltetedTag = tag.tagName;//תוכן התגית
                                                int pointsCurrentTag = tag.points;//ניקוד התגית
                                                ent.Tags.Remove(tag);//הסרת התגית

                                                if (l > matchTags[matchTags.Count - 1][0].TagIndex)//הוספת התגית אם אינה שיכת לתגיות המטופלות
                                                {
                                                    lastTags.Add(currentDeltetedTag);//שמירת ערך התגית
                                                    points.Add(pointsCurrentTag);//שמירת מספר התגיות
                                                }

                                            }
                                            int p1 = 0;//מונה לרשימת הניקוד החדשה לתגית
                                            foreach (string tagitContent in lastTags)//הכנסת התגיות לרשימה חזרה
                                            {

                                                ent.Tags.Add(new DAL.Tag { songId = song.songId, tagName = tagitContent, points = points[p1] });//הכנסת התגיות החדשות והשאר עד סוף השיר לרשימה
                                                p1++;//במקביל עבודה עם רשימת הניקוד המעודכנת
                                            }

                                            ent.SaveChanges();//שמירת השנויים
                                                              //
                                        }
                                        else// נותרה מילה אחת עד סיום התגית
                                        {
                                            //13-2
                                            string newPrevTag = "";//תגית קודמת
                                            string newCurrentTag = "";//תגית נוכחית
                                                                      // string newNextTag = "";//תגית הבאה
                                            newCurrentTag = text;//תוכן תגית נוכחית: תיבת הטקסט
                                            for (int l = 0; l < songTagsInform[matchTags[0][0].TagIndex].Count - matchTags[0].Count; l++)//התגית הקודמת
                                            {
                                                newPrevTag += songTagsInform[matchTags[0][0].TagIndex][l].Word + " ";//שרשור
                                            }
                                            newPrevTag = newPrevTag.Substring(0, newPrevTag.Length - 1);//הסרת הרווח



                                            string endCurrentTag = songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex][(songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex].Count - 1)].Word;//תוכן התגית שאחרי התגית הקשורה לתוכן תיבת הטקסט

                                            newCurrentTag = newCurrentTag + " " + endCurrentTag;

                                            ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine].tagName = newPrevTag;//השמת התגית הקודמת
                                            ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine + 1].tagName = newCurrentTag;//השמת התגית הנוכחית

                                            for (int l = startLine + 2; l < matchTags[matchTags.Count - 1][0].TagIndex + 1; l++)//הסרת התגיות המיותרות
                                            {
                                                DAL.Tag tag = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[l];//איתור תגית מסוימת
                                                ent.Tags.Remove(tag);//מחיקת התגית
                                            }



                                            ent.SaveChanges();//שמירת השנויים בבסיס הנתונים
                                        }




                                    }
                                    else//
                                    {
                                        if (matchTags[matchTags.Count - 1][(matchTags[matchTags.Count - 1].Count - 1)].SongIndex + 1 < songTagsInform[(matchTags[matchTags.Count - 1][(matchTags[matchTags.Count - 1].Count - 1)].TagIndex)][(songTagsInform[(matchTags[matchTags.Count - 1][(matchTags[matchTags.Count - 1].Count - 1)].TagIndex)].Count - 1)].SongIndex)// נותרה יותר ממילה אחת עד סוף התגית
                                        {

                                            //13-3


                                            //יתכן ומספר התגיות יגדלו מבעבר, לכן יש למחק מתחילת התגית הנבחרת עד הסוף ולשכפל. להוסיף נקודה עבור התגית הנוכחית, הנבחרת
                                            string newCurrentTag = "";//תגית נוכחית
                                            string newNextTag = "";//תגית הבאה
                                            newCurrentTag = text;//תוכן תיבת הטקסט בשלמות
                                            newCurrentTag = songTagsInform[matchTags[0][0].TagIndex][0].Word + " " + text;
                                            int baseCount = songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex].Count;//מספר המילים בתגית האחרונה ממנה לקוח תוכן תיבת הטקסט
                                            int thisCount = matchTags[matchTags.Count - 1].Count;//מספר המילים בתגית האחרונה שהוקלדו בתיבת האתור
                                            int times = baseCount - thisCount;//נניח שלוש פחות אחד, מספר המילים בסוף התגית שיש לסרוק
                                            for (int l = 0; l < times; l++)//תחילת התגית הבאה החדשה
                                            {
                                                newNextTag += songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex][thisCount + l].Word + " ";
                                            }
                                            newNextTag = newNextTag.Substring(0, newNextTag.Length - 1);//הסרת רווח אחרון


                                            ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[matchTags[matchTags.Count - 1][0].TagIndex].tagName = newNextTag;//השמת התגית הבאה
                                            //ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine - 1].tagName = newPrevTag;//השמת התגית הקודמת
                                            ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine].tagName = newCurrentTag;//השמת התגית הנוכחית

                                            for (int l = startLine + 1; l < matchTags[matchTags.Count - 1][0].TagIndex; l++)//הסרת התגיות המיותרות
                                            {
                                                DAL.Tag t = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[l];//איתור תגית מסוימת
                                                ent.Tags.Remove(t);//מחיקת התגית
                                            }



                                            ent.SaveChanges();//שמירת השנויים

                                        }
                                        else// נותרה מילה אחת עד סיום התגית
                                        {
                                            //13-4


                                            ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[0].tagName = song.songContect;//הכנסת התגית החדשה


                                            for (i = 1; i < ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList().Count; i++)//מחיקת התגיות המיותרות ששורשרו בעבר
                                            {

                                                DAL.Tag theTag = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine + i];//התגית שצריכה להמחק
                                                ent.Tags.Remove(theTag);//המחיקה בפועל
                                            }

                                            ent.SaveChanges();//שמירת השנויים בבסיס הנתונים
                                                              //| לכידת כל מילות השיר לתגית אחת. (=זה כל השיר)
                                        }



                                    }


                                }
                                else   //לא אחרון = הטקסט הנדרש באיתור, לא  שייך (לפחות לתחילת התגית) לתגית האחרונה בשיר המקור.
                                {
                                    //14

                                    int difference = matchTags[0][0].SongIndex - songTagsInform[matchTags[0][0].TagIndex][0].SongIndex;//ההפרש בין המיקום של המילה הראשונה שהוקשה בתגית לבין אורך התגית הזו
                                    if (difference != 1)//מספר המילים מתחילת השיר עד תחילת תוכן האיתור, גדול מ1
                                    {
                                        if (matchTags[matchTags.Count - 1][(matchTags[matchTags.Count - 1].Count - 1)].SongIndex + 1 < songTagsInform[(matchTags[matchTags.Count - 1][(matchTags[matchTags.Count - 1].Count - 1)].TagIndex)][(songTagsInform[(matchTags[matchTags.Count - 1][(matchTags[matchTags.Count - 1].Count - 1)].TagIndex)].Count - 1)].SongIndex)// נותרה יותר ממילה אחת עד סוף התגית
                                        {
                                            //14-1
                                            //יתכן ומספר התגיות יגדלו מבעבר, לכן יש למחק מתחילת התגית הנבחרת עד הסוף ולשכפל. להוסיף נקודה עבור התגית הנוכחית, הנבחרת
                                            string newPrevTag = "";//תגית קודמת
                                            string newCurrentTag = "";//תגית נוכחית
                                            string newNextTag = "";//תגית הבאה
                                            newCurrentTag = text;//תוכן תיבת הטקסט בשלמות
                                            for (int l = 0; l < songTagsInform[matchTags[0][0].TagIndex].Count - matchTags[0].Count; l++)//כל מילות התגית הקודמת החדשה, שהיא תחילת התגית הנוכחית עד תיבת הטקסט
                                            {
                                                newPrevTag += songTagsInform[matchTags[0][0].TagIndex][l].Word + " ";
                                            }
                                            newPrevTag = newPrevTag.Substring(0, newPrevTag.Length - 1);//הסרת הרווח האחרון

                                            int baseCount = songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex].Count;//מספר המילים בתגית האחרונה ממנה לקוח תוכן תיבת הטקסט
                                            int thisCount = matchTags[matchTags.Count - 1].Count;//מספר המילים בתגית האחרונה שהוקלדו בתיבת האתור
                                            int times = baseCount - thisCount;//נניח שלוש פחות אחד, מספר המילים בסוף התגית שיש לסרוק
                                            for (int l = 0; l < times; l++)//תחילת התגית הבאה החדשה
                                            {
                                                newNextTag += songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex][thisCount + l].Word + " ";
                                            }
                                            newNextTag = newNextTag.Substring(0, newNextTag.Length - 1);//הסרת רווח אחרון

                                            //יש להעתיק את כל התגיות הנותרות לתגיות חדשות
                                            //מאחר ויש תגית חדשה נוספת

                                            int from = 0;//מכאן למחק
                                            int toEnd = 0;//מחיקה עד הסוף

                                            from = startLine;//הטקסט מתיבת האיתור שיך בתחילתו לתגית זו
                                            toEnd = songTagsInform[songTagsInform.Count - 1][0].TagIndex + 1;//עד התגית האחרונה בשיר המחיקה
                                            List<string> lastTags = new List<string>();//שימור התגיות להוספה
                                            lastTags.Add(newPrevTag);//התגית הקודמת
                                            lastTags.Add(newCurrentTag);//התגית הנוכחית, תוכן תיבת הטקסט
                                            lastTags.Add(newNextTag);//התגית הבאה

                                            List<int> points = new List<int>();//רשימת ניקוד לתגיות המועתקות
                                            points.Add(0);//תגית חדשה ללא ניקוד
                                            points.Add(1);//התגית מקבלת כעת ניקוד אחד
                                            points.Add(0);//תגית חדשה ללא ניקוד
                                            for (int l = from; l < toEnd; l++)//הסרת התגיות עד הסוף
                                            {
                                                DAL.Tag tag = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[l];//קבלת התגית
                                                string currentDeltetedTag = tag.tagName;//תוכן התגית
                                                int pointsCurrentTag = tag.points;//ניקוד התגית
                                                ent.Tags.Remove(tag);//הסרת התגית

                                                if (l > matchTags[matchTags.Count - 1][0].TagIndex)//הוספת התגית אם אינה שיכת לתגיות המטופלות
                                                {
                                                    lastTags.Add(currentDeltetedTag);//שמירת ערך התגית
                                                    points.Add(pointsCurrentTag);//שמירת מספר התגיות
                                                }

                                            }
                                            int p1 = 0;//מונה לרשימת הניקוד החדשה לתגית
                                            foreach (string tagitContent in lastTags)//הכנסת התגיות לרשימה חזרה
                                            {

                                                ent.Tags.Add(new DAL.Tag { songId = song.songId, tagName = tagitContent, points = points[p1] });//הכנסת התגיות החדשות והשאר עד סוף השיר לרשימה
                                                p1++;//במקביל עבודה עם רשימת הניקוד המעודכנת
                                            }

                                            ent.SaveChanges();//שמירת השנויים

                                        }
                                        else// נותרה מילה אחת עד סיום התגית
                                        {
                                            //14-2
                                            string newPrevTag = "";//תגית קודמת
                                            string newCurrentTag = "";//תגית נוכחית
                                            string newNextTag = "";//תגית הבאה
                                            newCurrentTag = text;//תוכן תגית נוכחית: תיבת הטקסט
                                            for (int l = 0; l < songTagsInform[matchTags[0][0].TagIndex].Count - matchTags[0].Count; l++)//התגית הקודמת
                                            {
                                                newPrevTag += songTagsInform[matchTags[0][0].TagIndex][l].Word + " ";//שרשור
                                            }
                                            newPrevTag = newPrevTag.Substring(0, newPrevTag.Length - 1);//הסרת הרווח


                                            string nextNextTag1 = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[matchTags[matchTags.Count - 1][0].TagIndex + 1].tagName;//המילה האחרונה שאחרי תוכן תיבת הטקסט
                                            string d = songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex][(songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex].Count - 1)].Word;//תוכן התגית שאחרי התגית הקשורה לתוכן תיבת הטקסט
                                            newNextTag = d + " " + nextNextTag1;//לכידה כאמור לתגית הבאה

                                            ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[matchTags[matchTags.Count - 1][0].TagIndex + 1].tagName = newNextTag;//השמת התגית הבאה
                                            ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine].tagName = newPrevTag;//השמת התגית הקודמת
                                            ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine + 1].tagName = newCurrentTag;//השמת התגית הנוכחית

                                            for (int l = startLine + 2; l < matchTags[matchTags.Count - 1][0].TagIndex + 1; l++)//הסרת התגיות המיותרות
                                            {
                                                DAL.Tag t = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[l];//איתור תגית מסוימת
                                                ent.Tags.Remove(t);//מחיקת התגית
                                            }



                                            ent.SaveChanges();//שמירת השנויים בבסיס הנתונים
                                        }



                                    }
                                    else
                                    {
                                        if (matchTags[matchTags.Count - 1][(matchTags[matchTags.Count - 1].Count - 1)].SongIndex + 1 < songTagsInform[(matchTags[matchTags.Count - 1][(matchTags[matchTags.Count - 1].Count - 1)].TagIndex)][(songTagsInform[(matchTags[matchTags.Count - 1][(matchTags[matchTags.Count - 1].Count - 1)].TagIndex)].Count - 1)].SongIndex)// נותרה יותר ממילה אחת עד סוף התגית
                                        {

                                            //14-3


                                            //יתכן ומספר התגיות יגדלו מבעבר, לכן יש למחק מתחילת התגית הנבחרת עד הסוף ולשכפל. להוסיף נקודה עבור התגית הנוכחית, הנבחרת
                                            string newCurrentTag = "";//תגית נוכחית
                                            string newNextTag = "";//תגית הבאה
                                            newCurrentTag = text;//תוכן תיבת הטקסט בשלמות
                                            newCurrentTag = songTagsInform[matchTags[0][0].TagIndex][0].Word + " " + text;
                                            int baseCount = songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex].Count;//מספר המילים בתגית האחרונה ממנה לקוח תוכן תיבת הטקסט
                                            int thisCount = matchTags[matchTags.Count - 1].Count;//מספר המילים בתגית האחרונה שהוקלדו בתיבת האתור
                                            int times = baseCount - thisCount;//נניח שלוש פחות אחד, מספר המילים בסוף התגית שיש לסרוק
                                            for (int l = 0; l < times; l++)//תחילת התגית הבאה החדשה
                                            {
                                                newNextTag += songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex][thisCount + l].Word + " ";
                                            }
                                            newNextTag = newNextTag.Substring(0, newNextTag.Length - 1);//הסרת רווח אחרון


                                            ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[matchTags[matchTags.Count - 1][0].TagIndex].tagName = newNextTag;//השמת התגית הבאה
                                            //ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine - 1].tagName = newPrevTag;//השמת התגית הקודמת
                                            ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine].tagName = newCurrentTag;//השמת התגית הנוכחית

                                            for (int l = startLine + 1; l < matchTags[matchTags.Count - 1][0].TagIndex; l++)//הסרת התגיות המיותרות
                                            {
                                                DAL.Tag t = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[l];//איתור תגית מסוימת
                                                ent.Tags.Remove(t);//מחיקת התגית
                                            }



                                            ent.SaveChanges();//שמירת השנויים

                                        }
                                        else// נותרה מילה אחת עד סיום התגית
                                        {
                                            //14-4

                                            //יתכן ומספר התגיות יגדלו מבעבר, לכן יש למחק מתחילת התגית הנבחרת עד הסוף ולשכפל. להוסיף נקודה עבור התגית הנוכחית, הנבחרת
                                            string newCurrentTag = "";//תגית נוכחית
                                            string newNextTag = "";//תגית הבאה
                                            newCurrentTag = text;//תוכן תיבת הטקסט בשלמות
                                            newCurrentTag = songTagsInform[matchTags[0][0].TagIndex][0].Word + " " + text;
                                            int baseCount = songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex].Count;//מספר המילים בתגית האחרונה ממנה לקוח תוכן תיבת הטקסט
                                            int thisCount = matchTags[matchTags.Count - 1].Count;//מספר המילים בתגית האחרונה שהוקלדו בתיבת האתור
                                            int times = songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex + 1].Count;//נניח שלוש פחות אחד, מספר המילים בסוף התגית שיש לסרוק

                                            for (int l = 0; l < times; l++)//תחילת התגית הבאה החדשה
                                            {
                                                newNextTag += songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex + 1][l].Word + " ";
                                            }
                                            newNextTag = songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex][(songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex].Count) - 1].Word + " " + newNextTag;

                                            ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[matchTags[matchTags.Count - 1][0].TagIndex + 1].tagName = newNextTag;//השמת התגית הבאה
                                            ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine].tagName = newCurrentTag;//השמת התגית הנוכחית

                                            for (int l = startLine + 1; l < matchTags[matchTags.Count - 1][0].TagIndex + 1; l++)//הסרת התגיות המיותרות
                                            {
                                                DAL.Tag t = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[l];//איתור תגית מסוימת
                                                ent.Tags.Remove(t);//מחיקת התגית
                                            }



                                            ent.SaveChanges();//שמירת השנויים
                                        }


                                    }

                                }
                            }

                            else  //לא ראשון = הטקסט הנדרש באיתור, לא שייך(לפחות לסוף) תגית ראשונה בשיר המקור.
                            {
                                if (matchTags[matchTags.Count - 1][0].SongIndex == songTagsInform[songTagsInform.Count - 1][0].SongIndex) //אחרון = הטקסט הנדרש באיתור, שייך (לפחות לתחילת התגית ) לתגית האחרונה בשיר המקור.
                                {

                                    //15

                                    int difference = matchTags[0][0].SongIndex - songTagsInform[matchTags[0][0].TagIndex][0].SongIndex;//ההפרש בין המיקום של המילה הראשונה שהוקשה בתגית לבין אורך התגית הזו
                                    if (difference != 1)
                                    {
                                        if (matchTags[matchTags.Count - 1][(matchTags[matchTags.Count - 1].Count - 1)].SongIndex + 1 < songTagsInform[(matchTags[matchTags.Count - 1][(matchTags[matchTags.Count - 1].Count - 1)].TagIndex)][(songTagsInform[(matchTags[matchTags.Count - 1][(matchTags[matchTags.Count - 1].Count - 1)].TagIndex)].Count - 1)].SongIndex)// נותרה יותר ממילה אחת עד סוף התגית
                                        {
                                            //15-1
                                            //יתכן ומספר התגיות יגדלו מבעבר, לכן יש למחק מתחילת התגית הנבחרת עד הסוף ולשכפל. להוסיף נקודה עבור התגית הנוכחית, הנבחרת
                                            string newPrevTag = "";//תגית קודמת
                                            string newCurrentTag = "";//תגית נוכחית
                                            string newNextTag = "";//תגית הבאה
                                            newCurrentTag = text;//תוכן תיבת הטקסט בשלמות
                                            for (int l = 0; l < songTagsInform[matchTags[0][0].TagIndex].Count - matchTags[0].Count; l++)//כל מילות התגית הקודמת החדשה, שהיא תחילת התגית הנוכחית עד תיבת הטקסט
                                            {
                                                newPrevTag += songTagsInform[matchTags[0][0].TagIndex][l].Word + " ";
                                            }
                                            newPrevTag = newPrevTag.Substring(0, newPrevTag.Length - 1);//הסרת הרווח האחרון

                                            int baseCount = songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex].Count;//מספר המילים בתגית האחרונה ממנה לקוח תוכן תיבת הטקסט
                                            int thisCount = matchTags[matchTags.Count - 1].Count;//מספר המילים בתגית האחרונה שהוקלדו בתיבת האתור
                                            int times = baseCount - thisCount;//נניח שלוש פחות אחד, מספר המילים בסוף התגית שיש לסרוק
                                            for (int l = 0; l < times; l++)//תחילת התגית הבאה החדשה
                                            {
                                                newNextTag += songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex][thisCount + l].Word + " ";
                                            }
                                            newNextTag = newNextTag.Substring(0, newNextTag.Length - 1);//הסרת רווח אחרון

                                            //יש להעתיק את כל התגיות הנותרות לתגיות חדשות
                                            //מאחר ויש תגית חדשה נוספת

                                            int from = 0;//מכאן למחק
                                            int toEnd = 0;//מחיקה עד הסוף

                                            from = startLine;//הטקסט מתיבת האיתור שיך בתחילתו לתגית זו
                                            toEnd = songTagsInform[songTagsInform.Count - 1][0].TagIndex + 1;//עד התגית האחרונה בשיר המחיקה
                                            List<string> lastTags = new List<string>();//שימור התגיות להוספה
                                            lastTags.Add(newPrevTag);//התגית הקודמת
                                            lastTags.Add(newCurrentTag);//התגית הנוכחית, תוכן תיבת הטקסט
                                            lastTags.Add(newNextTag);//התגית הבאה

                                            List<int> points = new List<int>();//רשימת ניקוד לתגיות המועתקות
                                            points.Add(0);//תגית חדשה ללא ניקוד
                                            points.Add(1);//התגית מקבלת כעת ניקוד אחד
                                            points.Add(0);//תגית חדשה ללא ניקוד
                                            for (int l = from; l < toEnd; l++)//הסרת התגיות עד הסוף
                                            {
                                                DAL.Tag tag = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[l];//קבלת התגית
                                                string currentDeltetedTag = tag.tagName;//תוכן התגית
                                                int pointsCurrentTag = tag.points;//ניקוד התגית
                                                ent.Tags.Remove(tag);//הסרת התגית

                                                if (l > matchTags[matchTags.Count - 1][0].TagIndex)//הוספת התגית אם אינה שיכת לתגיות המטופלות
                                                {
                                                    lastTags.Add(currentDeltetedTag);//שמירת ערך התגית
                                                    points.Add(pointsCurrentTag);//שמירת מספר התגיות
                                                }

                                            }
                                            int p1 = 0;//מונה לרשימת הניקוד החדשה לתגית
                                            foreach (string tagitContent in lastTags)//הכנסת התגיות לרשימה חזרה
                                            {

                                                ent.Tags.Add(new DAL.Tag { songId = song.songId, tagName = tagitContent, points = points[p1] });//הכנסת התגיות החדשות והשאר עד סוף השיר לרשימה
                                                p1++;//במקביל עבודה עם רשימת הניקוד המעודכנת
                                            }

                                            ent.SaveChanges();//שמירת השנויים
                                                              //
                                        }
                                        else// נותרה מילה אחת עד סיום התגית
                                        {
                                            //15-2
                                            string newPrevTag = "";//תגית קודמת
                                            string newCurrentTag = "";//תגית נוכחית
                                                                      // string newNextTag = "";//תגית הבאה
                                            newCurrentTag = text;//תוכן תגית נוכחית: תיבת הטקסט
                                            for (int l = 0; l < songTagsInform[matchTags[0][0].TagIndex].Count - matchTags[0].Count; l++)//התגית הקודמת
                                            {
                                                newPrevTag += songTagsInform[matchTags[0][0].TagIndex][l].Word + " ";//שרשור
                                            }
                                            newPrevTag = newPrevTag.Substring(0, newPrevTag.Length - 1);//הסרת הרווח



                                            string d = songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex][(songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex].Count - 1)].Word;//תוכן התגית שאחרי התגית הקשורה לתוכן תיבת הטקסט

                                            newCurrentTag = newCurrentTag + " " + d;

                                            ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine].tagName = newPrevTag;//השמת התגית הקודמת
                                            ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine + 1].tagName = newCurrentTag;//השמת התגית הנוכחית

                                            for (int l = startLine + 2; l < matchTags[matchTags.Count - 1][0].TagIndex + 1; l++)//הסרת התגיות המיותרות
                                            {
                                                DAL.Tag t = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[l];//איתור תגית מסוימת
                                                ent.Tags.Remove(t);//מחיקת התגית
                                            }



                                            ent.SaveChanges();//שמירת השנויים בבסיס הנתונים
                                        }
                                    }
                                    else
                                    {


                                        if (matchTags[matchTags.Count - 1][(matchTags[matchTags.Count - 1].Count - 1)].SongIndex + 1 < songTagsInform[(matchTags[matchTags.Count - 1][(matchTags[matchTags.Count - 1].Count - 1)].TagIndex)][(songTagsInform[(matchTags[matchTags.Count - 1][(matchTags[matchTags.Count - 1].Count - 1)].TagIndex)].Count - 1)].SongIndex)// נותרה יותר ממילה אחת עד סוף התגית
                                        {

                                            //15-3


                                            //יתכן ומספר התגיות יגדלו מבעבר, לכן יש למחק מתחילת התגית הנבחרת עד הסוף ולשכפל. להוסיף נקודה עבור התגית הנוכחית, הנבחרת

                                            string newPrevTag = "";//תגית קודמת
                                            string newCurrentTag = "";//תגית נוכחית
                                            string newNextTag = "";//תגית הבאה
                                            newCurrentTag = text;//תוכן תיבת הטקסט בשלמות
                                            string beginPrevTag = "";
                                            for (int l = 0; l < songTagsInform[matchTags[0][0].TagIndex - 1].Count; l++)//כל מילות התגית הקודמת החדשה, שהיא תחילת התגית הנוכחית עד תיבת הטקסט
                                            {
                                                beginPrevTag += songTagsInform[matchTags[0][0].TagIndex - 1][l].Word + " ";
                                            }
                                            beginPrevTag = beginPrevTag.Substring(0, beginPrevTag.Length - 1);
                                            newPrevTag = beginPrevTag + " " + songTagsInform[matchTags[0][0].TagIndex][0].Word;

                                            int baseCount = songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex].Count;//מספר המילים בתגית האחרונה ממנה לקוח תוכן תיבת הטקסט
                                            int thisCount = matchTags[matchTags.Count - 1].Count;//מספר המילים בתגית האחרונה שהוקלדו בתיבת האתור
                                            int times = baseCount - thisCount;//נניח שלוש פחות אחד, מספר המילים בסוף התגית שיש לסרוק
                                            for (int l = 0; l < times; l++)//תחילת התגית הבאה החדשה
                                            {
                                                newNextTag += songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex][thisCount + l].Word + " ";
                                            }
                                            newNextTag = newNextTag.Substring(0, newNextTag.Length - 1);//הסרת רווח אחרון


                                            ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[matchTags[matchTags.Count - 1][0].TagIndex].tagName = newNextTag;//השמת התגית הבאה
                                            ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine - 1].tagName = newPrevTag;//השמת התגית הקודמת
                                            ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine].tagName = newCurrentTag;//השמת התגית הנוכחית

                                            for (int l = startLine + 1; l < matchTags[matchTags.Count - 1][0].TagIndex; l++)//הסרת התגיות המיותרות
                                            {
                                                DAL.Tag t = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[l];//איתור תגית מסוימת
                                                ent.Tags.Remove(t);//מחיקת התגית
                                            }



                                            ent.SaveChanges();//שמירת השנויים

                                        }
                                        else// נותרה מילה אחת עד סיום התגית
                                        {
                                            //15-4

                                            string newPrevTag = "";//תגית קודמת
                                            string newCurrentTag = "";//תגית נוכחית
                                            newCurrentTag = text;//תוכן תיבת הטקסט בשלמות
                                            string v = "";
                                            for (int l = 0; l < songTagsInform[matchTags[0][0].TagIndex - 1].Count; l++)//כל מילות התגית הקודמת החדשה, שהיא תחילת התגית הנוכחית עד תיבת הטקסט
                                            {
                                                v += songTagsInform[matchTags[0][0].TagIndex - 1][l].Word + " ";
                                            }
                                            v = v.Substring(0, v.Length - 1);
                                            newPrevTag = v + " " + songTagsInform[matchTags[0][0].TagIndex][0].Word;
                                            string lastWord = "";
                                            lastWord = songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex][(songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex].Count - 1)].Word;
                                            newCurrentTag = newCurrentTag + " " + lastWord;

                                            ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine - 1].tagName = newPrevTag;//השמת התגית הקודמת
                                            ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine].tagName = newCurrentTag;//השמת התגית הנוכחית

                                            for (int l = startLine + 1; l < matchTags[matchTags.Count - 1][0].TagIndex + 1; l++)//הסרת התגיות המיותרות
                                            {
                                                DAL.Tag t = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[l];//איתור תגית מסוימת
                                                ent.Tags.Remove(t);//מחיקת התגית
                                            }
                                            ent.SaveChanges();




                                            ent.SaveChanges();//שמירת השנויים
                                        }


                                    }

                                }
                                else   //לא אחרון = הטקסט הנדרש באיתור, לא  שייך (לפחות לתחילת התגית) לתגית האחרונה בשיר המקור.
                                {
                                    //16
                                    int difference = matchTags[0][0].SongIndex - songTagsInform[matchTags[0][0].TagIndex][0].SongIndex;//ההפרש בין המיקום של המילה הראשונה שהוקשה בתגית לבין אורך התגית הזו
                                    if (difference != 1)//מספר המילים מתחילת השיר עד תחילת תוכן האיתור, גדול מ1
                                    {
                                        if (matchTags[matchTags.Count - 1][(matchTags[matchTags.Count - 1].Count - 1)].SongIndex + 1 < songTagsInform[(matchTags[matchTags.Count - 1][(matchTags[matchTags.Count - 1].Count - 1)].TagIndex)][(songTagsInform[(matchTags[matchTags.Count - 1][(matchTags[matchTags.Count - 1].Count - 1)].TagIndex)].Count - 1)].SongIndex)// נותרה יותר ממילה אחת עד סוף התגית
                                        {
                                            //16-1
                                            //יתכן ומספר התגיות יגדלו מבעבר, לכן יש למחק מתחילת התגית הנבחרת עד הסוף ולשכפל. להוסיף נקודה עבור התגית הנוכחית, הנבחרת
                                            string newPrevTag = "";//תגית קודמת
                                            string newCurrentTag = "";//תגית נוכחית
                                            string newNextTag = "";//תגית הבאה
                                            newCurrentTag = text;//תוכן תיבת הטקסט בשלמות
                                            for (int l = 0; l < songTagsInform[matchTags[0][0].TagIndex].Count - matchTags[0].Count; l++)//כל מילות התגית הקודמת החדשה, שהיא תחילת התגית הנוכחית עד תיבת הטקסט
                                            {
                                                newPrevTag += songTagsInform[matchTags[0][0].TagIndex][l].Word + " ";
                                            }
                                            newPrevTag = newPrevTag.Substring(0, newPrevTag.Length - 1);//הסרת הרווח האחרון

                                            int baseCount = songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex].Count;//מספר המילים בתגית האחרונה ממנה לקוח תוכן תיבת הטקסט
                                            int thisCount = matchTags[matchTags.Count - 1].Count;//מספר המילים בתגית האחרונה שהוקלדו בתיבת האתור
                                            int times = baseCount - thisCount;//נניח שלוש פחות אחד, מספר המילים בסוף התגית שיש לסרוק
                                            for (int l = 0; l < times; l++)//תחילת התגית הבאה החדשה
                                            {
                                                newNextTag += songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex][thisCount + l].Word + " ";
                                            }
                                            newNextTag = newNextTag.Substring(0, newNextTag.Length - 1);//הסרת רווח אחרון

                                            //יש להעתיק את כל התגיות הנותרות לתגיות חדשות
                                            //מאחר ויש תגית חדשה נוספת

                                            int from = 0;//מכאן למחק
                                            int toEnd = 0;//מחיקה עד הסוף

                                            from = startLine;//הטקסט מתיבת האיתור שיך בתחילתו לתגית זו
                                            toEnd = songTagsInform[songTagsInform.Count - 1][0].TagIndex + 1;//עד התגית האחרונה בשיר המחיקה
                                            List<string> lastTags = new List<string>();//שימור התגיות להוספה
                                            lastTags.Add(newPrevTag);//התגית הקודמת
                                            lastTags.Add(newCurrentTag);//התגית הנוכחית, תוכן תיבת הטקסט
                                            lastTags.Add(newNextTag);//התגית הבאה

                                            List<int> points = new List<int>();//רשימת ניקוד לתגיות המועתקות
                                            points.Add(0);//תגית חדשה ללא ניקוד
                                            points.Add(1);//התגית מקבלת כעת ניקוד אחד
                                            points.Add(0);//תגית חדשה ללא ניקוד
                                            for (int l = from; l < toEnd; l++)//הסרת התגיות עד הסוף
                                            {
                                                DAL.Tag tag = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[l];//קבלת התגית
                                                string currentDeltetedTag = tag.tagName;//תוכן התגית
                                                int pointsCurrentTag = tag.points;//ניקוד התגית
                                                ent.Tags.Remove(tag);//הסרת התגית

                                                if (l > matchTags[matchTags.Count - 1][0].TagIndex)//הוספת התגית אם אינה שיכת לתגיות המטופלות
                                                {
                                                    lastTags.Add(currentDeltetedTag);//שמירת ערך התגית
                                                    points.Add(pointsCurrentTag);//שמירת מספר התגיות
                                                }

                                            }
                                            int p1 = 0;//מונה לרשימת הניקוד החדשה לתגית
                                            foreach (string tagitContent in lastTags)//הכנסת התגיות לרשימה חזרה
                                            {

                                                ent.Tags.Add(new DAL.Tag { songId = song.songId, tagName = tagitContent, points = points[p1] });//הכנסת התגיות החדשות והשאר עד סוף השיר לרשימה
                                                p1++;//במקביל עבודה עם רשימת הניקוד המעודכנת
                                            }

                                            ent.SaveChanges();//שמירת השנויים

                                        }
                                        else// נותרה מילה אחת עד סיום התגית
                                        {
                                            //16-2
                                            string newPrevTag = "";//תגית קודמת
                                            string newCurrentTag = "";//תגית נוכחית
                                            string newNextTag = "";//תגית הבאה
                                            newCurrentTag = text;//תוכן תגית נוכחית: תיבת הטקסט
                                            for (int l = 0; l < songTagsInform[matchTags[0][0].TagIndex].Count - matchTags[0].Count; l++)//התגית הקודמת
                                            {
                                                newPrevTag += songTagsInform[matchTags[0][0].TagIndex][l].Word + " ";//שרשור
                                            }
                                            newPrevTag = newPrevTag.Substring(0, newPrevTag.Length - 1);//הסרת הרווח


                                            string nextNextTag1 = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[matchTags[matchTags.Count - 1][0].TagIndex + 1].tagName;//המילה האחרונה שאחרי תוכן תיבת הטקסט
                                            string beginNextTag = songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex][(songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex].Count - 1)].Word;//תוכן התגית שאחרי התגית הקשורה לתוכן תיבת הטקסט
                                            newNextTag = beginNextTag + " " + nextNextTag1;//לכידה כאמור לתגית הבאה

                                            ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[matchTags[matchTags.Count - 1][0].TagIndex + 1].tagName = newNextTag;//השמת התגית הבאה
                                            ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine].tagName = newPrevTag;//השמת התגית הקודמת
                                            ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine + 1].tagName = newCurrentTag;//השמת התגית הנוכחית

                                            for (int l = startLine + 2; l < matchTags[matchTags.Count - 1][0].TagIndex + 1; l++)//הסרת התגיות המיותרות
                                            {
                                                DAL.Tag t = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[l];//איתור תגית מסוימת
                                                ent.Tags.Remove(t);//מחיקת התגית
                                            }



                                            ent.SaveChanges();//שמירת השנויים בבסיס הנתונים
                                        }


                                    }
                                    else
                                    {


                                        if (matchTags[matchTags.Count - 1][(matchTags[matchTags.Count - 1].Count - 1)].SongIndex + 1 < songTagsInform[(matchTags[matchTags.Count - 1][(matchTags[matchTags.Count - 1].Count - 1)].TagIndex)][(songTagsInform[(matchTags[matchTags.Count - 1][(matchTags[matchTags.Count - 1].Count - 1)].TagIndex)].Count - 1)].SongIndex)// נותרה יותר ממילה אחת עד סוף התגית
                                        {

                                            //16-3


                                            //יתכן ומספר התגיות יגדלו מבעבר, לכן יש למחק מתחילת התגית הנבחרת עד הסוף ולשכפל. להוסיף נקודה עבור התגית הנוכחית, הנבחרת

                                            string newPrevTag = "";//תגית קודמת
                                            string newCurrentTag = "";//תגית נוכחית
                                            string newNextTag = "";//תגית הבאה
                                            newCurrentTag = text;//תוכן תיבת הטקסט בשלמות
                                            string v = "";
                                            for (int l = 0; l < songTagsInform[matchTags[0][0].TagIndex - 1].Count; l++)//כל מילות התגית הקודמת החדשה, שהיא תחילת התגית הנוכחית עד תיבת הטקסט
                                            {
                                                v += songTagsInform[matchTags[0][0].TagIndex - 1][l].Word + " ";
                                            }
                                            v = v.Substring(0, v.Length - 1);
                                            newPrevTag = v + " " + songTagsInform[matchTags[0][0].TagIndex][0].Word;

                                            int baseCount = songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex].Count;//מספר המילים בתגית האחרונה ממנה לקוח תוכן תיבת הטקסט
                                            int thisCount = matchTags[matchTags.Count - 1].Count;//מספר המילים בתגית האחרונה שהוקלדו בתיבת האתור
                                            int times = baseCount - thisCount;//נניח שלוש פחות אחד, מספר המילים בסוף התגית שיש לסרוק
                                            for (int l = 0; l < times; l++)//תחילת התגית הבאה החדשה
                                            {
                                                newNextTag += songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex][thisCount + l].Word + " ";
                                            }
                                            newNextTag = newNextTag.Substring(0, newNextTag.Length - 1);//הסרת רווח אחרון


                                            ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[matchTags[matchTags.Count - 1][0].TagIndex].tagName = newNextTag;//השמת התגית הבאה
                                            ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine - 1].tagName = newPrevTag;//השמת התגית הקודמת
                                            ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine].tagName = newCurrentTag;//השמת התגית הנוכחית

                                            for (int l = startLine + 1; l < matchTags[matchTags.Count - 1][0].TagIndex; l++)//הסרת התגיות המיותרות
                                            {
                                                DAL.Tag t = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[l];//איתור תגית מסוימת
                                                ent.Tags.Remove(t);//מחיקת התגית
                                            }



                                            ent.SaveChanges();//שמירת השנויים

                                        }
                                        else// נותרה מילה אחת עד סיום התגית
                                        {
                                            //16-4

                                            string newPrevTag = "";//תגית קודמת
                                            string newCurrentTag = "";//תגית נוכחית
                                            newCurrentTag = text;//תוכן תיבת הטקסט בשלמות
                                            string v = "";
                                            for (int l = 0; l < songTagsInform[matchTags[0][0].TagIndex - 1].Count; l++)//כל מילות התגית הקודמת החדשה, שהיא תחילת התגית הנוכחית עד תיבת הטקסט
                                            {
                                                v += songTagsInform[matchTags[0][0].TagIndex - 1][l].Word + " ";
                                            }
                                            v = v.Substring(0, v.Length - 1);
                                            newPrevTag = v + " " + songTagsInform[matchTags[0][0].TagIndex][0].Word;
                                            string lastWord = "";
                                            lastWord = songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex][(songTagsInform[matchTags[matchTags.Count - 1][0].TagIndex].Count - 1)].Word;
                                            newCurrentTag = newCurrentTag + " " + lastWord;

                                            ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine - 1].tagName = newPrevTag;//השמת התגית הקודמת
                                            ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[startLine].tagName = newCurrentTag;//השמת התגית הנוכחית

                                            for (int l = startLine + 1; l < matchTags[matchTags.Count - 1][0].TagIndex + 1; l++)//הסרת התגיות המיותרות
                                            {
                                                DAL.Tag t = ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList()[l];//איתור תגית מסוימת
                                                ent.Tags.Remove(t);//מחיקת התגית
                                            }
                                            ent.SaveChanges();
                                        }

                                    }
                                }
                            }
                        }
                    }


                    foreach (DAL.Tag tag in ent.Tags.Where(p => p.songId == songId).OrderBy(q => q.tagId).ToList())//התגיות החדשות
                    {
                        dtoTags.Add(DtoTag.ConvertToDtoTag(tag));
                    }

                }
                else//לכאורה אין לאפשר למשתמש להקיש יותר מ10 מילים
                {
                    return new RequestResult
                    {

                        Data = null,
                        Status = false,
                        Message = " error: not able more than 10 words, or when spliting tags! "
                    };

                }

                return new RequestResult
                {

                    Data = dtoTags,
                    Status = true,
                    Message = " the tags in this song had entered succesfully! "
                };
            }
            catch (Exception ex)
            {
                return new RequestResult
                {

                    Data = null,
                    Status = false,
                    Message = " error: maybe index not in the range "
                };

            }

        }

    public void Method()
    {
      throw new System.NotImplementedException();
    }
  }
}
