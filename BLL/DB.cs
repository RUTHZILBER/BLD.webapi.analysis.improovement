
using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using DTO;
using System.Data;



namespace BLL
{
  public class DB
  {
   public static Ballad120Entities ent = new Ballad120Entities();
    /// <summary>
    /// תקינות לקוח
    /// </summary>
    /// <param name="usr"></param>
    /// <returns></returns>
    public bool isValidateUsr(DtoUsr dtoUsr, out string message)
    {
      message = "";
      Usr usr = dtoUsr.ConvertToUsr();
      bool flag = false;
      flag = ValidateUtilities.isValidateEnglishHebrewGeresh(usr.userName) && usr.userName.Length <= 40 && ValidateUtilities.isValidateEnglishHebrewGeresh(usr.firstName) && usr.firstName.Length <= 15 && ValidateUtilities.isValidateEnglishHebrewGeresh(usr.lastName) && usr.lastName.Length <= 15 && ValidateUtilities.isValidateEmail(usr.email) && ValidateUtilities.isValidatePassword(usr.password);
      if (flag == true)
        return true;
      else
      {//בדיקת השגיאה ושידורה למשתמש , ע"י סימון: תו ראשון ותו שני. הטפול בריאקט עצמו
       //F=FIRSTNAME L=LASTNAME ETC. V=VALIDATE ERROR. L=TOO LENGTH.
        if (!ValidateUtilities.isValidateEnglishHebrewGeresh(usr.userName))
          message += "UV ";
        if (usr.userName.Length > 40)
          message += "UL ";
        if (!ValidateUtilities.isValidateEnglishHebrewGeresh(usr.firstName))
          message += "FV ";
        if (usr.firstName.Length > 15)
          message += "FL ";
        if (!ValidateUtilities.isValidateEnglishHebrewGeresh(usr.lastName))
          message += "LV ";
        if (usr.lastName.Length > 15)
          message += "LL ";
        if (!ValidateUtilities.isValidateEmail(usr.email))
          message += "EV ";
        if (!ValidateUtilities.isValidatePassword(usr.password))
          message += "PV ";

        return flag;
      }

    }
    public static RequestResult deleteSong(Ballad120Entities ent, int songId)
    {
      try
      {
        Song song = ent.Songs.Where(i => i.songId == songId).FirstOrDefault();
        if (song != null)
        {
          ent.Songs.Remove(song);

          foreach (DAL.Tag tag in ent.Tags.Where(i => i.songId == songId).ToList())
          {
            ent.Tags.Remove(tag);
          }
          ent.SaveChanges();

          return new RequestResult
          {
            Status = true,
            Data = DtoSong.ConvertToDtoSong(song),
            Message = " השיר נמחק בהצלחה "

          };
        }
        else
        {
          return new RequestResult
          {
            Status = false,
            Data = DtoSong.ConvertToDtoSong(song),
            Message = " שגיאה במהלך המחיקה "

          };
        }

      }

      catch (Exception ex)
      {
        return new RequestResult
        {
          Status = false,
          Data = null,
          Message = " תקלת מערכת. נסה שנית "


        };
      }
    }
    /// <summary>
    /// לקוח קים:  תקינות לקוח
    /// </summary>
    /// <param name="usr"></param>
    /// <returns></returns>
    public bool isValidateExistUsr(DtoUsr dtoUsr, out string message)
    {
      message = "";
      Usr usr = dtoUsr.ConvertToUsr();
      bool flag = false;
      flag = ValidateUtilities.isValidateEnglishHebrewGeresh(usr.userName) && usr.userName.Length <= 40 && ValidateUtilities.isValidateEnglishHebrewGeresh(usr.firstName) && usr.firstName.Length <= 15 && ValidateUtilities.isValidateEnglishHebrewGeresh(usr.lastName) && usr.lastName.Length <= 15 && ValidateUtilities.isValidateEmail(usr.email) && ValidateUtilities.isValidatePassword(usr.password);
      if (flag == true)
        return true;
      else
      {//בדיקת השגיאה ושידורה למשתמש , ע"י סימון: תו ראשון ותו שני. הטפול בריאקט עצמו
        if (!ValidateUtilities.isValidateEnglishHebrewGeresh(usr.userName))
          message += "UValidate ";
        if (usr.userName.Length > 40)
          message += "ULength ";
        if (!ValidateUtilities.isValidateEnglishHebrewGeresh(usr.firstName))
          message += "FValidate ";
        if (usr.firstName.Length > 15)
          message += "FLength ";
        if (!ValidateUtilities.isValidateEnglishHebrewGeresh(usr.lastName))
          message += "LValidate ";
        if (usr.lastName.Length > 15)
          message += "LLength ";
        if (!ValidateUtilities.isValidateEmail(usr.email))
          message += "EValidate ";
        if (!ValidateUtilities.isValidatePassword(usr.password))
          message += "PValidate ";

        return flag;
      }

    }
    /// <summary>
    /// עדכון לקוח
    /// </summary>
    /// <param name="dtoUsr"></param>
    /// <returns></returns>
    public RequestResult updateUsr(DtoUsr dtoUsr)
    {

      try
      {


        string message;
        if (!(isValidateExistUsr(dtoUsr, out message)))
        {
          return new RequestResult
          {
            Message = message,
            Status = false
          };
        }
        List<Usr> usrs = ent.Usrs.Where(i => i.userId != dtoUsr.userId).ToList();

        if (usrs.Where(i => i.email == dtoUsr.email).ToList().Count > 0)
        {
          if (usrs.Where(i => i.userName == dtoUsr.userName).ToList().Count > 0)
          {
            return new RequestResult
            {
              Message = " כתובת מייל זו ושם המשתמש שהוקשו מופיע במערכת. בחר אחרים ",
              Status = false,
              Data = null

            };
          }
          else
          {
            return new RequestResult
            {
              Message = " כתובת מייל זו מופיעה במערכת. בחר אחרת ",
              Status = false,
              Data = null

            };
          }

        }

        if (usrs.Where(i => i.userName == dtoUsr.userName).ToList().Count > 0)
        {
          return new RequestResult
          {
            Message = " שם משתמש זה מופיע במערכת. בחר אחר ",
            Status = false,
            Data = null

          };
        }





        Usr cloneUsr = ent.Usrs.Where(i => i.userId == dtoUsr.userId).FirstOrDefault();
        cloneUsr.email = dtoUsr.email;
        cloneUsr.password = dtoUsr.password;
        cloneUsr.firstName = dtoUsr.firstName;
        cloneUsr.lastName = dtoUsr.lastName;
        cloneUsr.userName = dtoUsr.userName;
        dtoUsr = DtoUsr.ConvertToDtoUsr(cloneUsr);



        ent.SaveChanges();


        return new RequestResult
        {
          Data = dtoUsr,
          Message = " the details had saved successfully ",
          Status = true
        };
      }
      catch (Exception)
      {

        return new RequestResult
        {
          Message = " exception : try again ",
          Status = false
        };
      }






    }
    /// הוספת לקוח
    /// </summary>
    /// <param name="dtoUsr"></param>
    /// <returns></returns>
    public RequestResult insertUsr(DtoUsr dtoUsr)
    {

      try
      {

        string message;
        if (!isValidateUsr(dtoUsr, out message))
        {
          return new RequestResult
          {
            Message = message,
            Status = false,
            Data = null

          };
        }

        if (ent.Usrs.Where(i => i.email == dtoUsr.email).ToList().Count > 0)
        {
          if (ent.Usrs.Where(i => i.userName == dtoUsr.userName).ToList().Count > 0)
          {
            return new RequestResult
            {
              Message = " כתובת מייל זו ושם המשתמש שהוקשו מופיע במערכת. בחר אחרים ",
              Status = false,
              Data = null

            };
          }
          else
          {
            return new RequestResult
            {
              Message = " כתובת מייל זו מופיעה במערכת. בחר אחרת ",
              Status = false,
              Data = null

            };
          }

        }

        if (ent.Usrs.Where(i => i.userName == dtoUsr.userName).ToList().Count > 0)
        {
          return new RequestResult
          {
            Message = " שם משתמש זה מופיע במערכת. בחר אחר ",
            Status = false,
            Data = null

          };
        }

        dtoUsr.notingTime = DateTime.Today;
        Usr usr = dtoUsr.ConvertToUsr();


        using (Ballad120Entities ent = new Ballad120Entities())
        {
          usr.notingTime = DateTime.Today;
          ent.Usrs.Add(usr);
          ent.SaveChanges();
        }
        dtoUsr = DtoUsr.ConvertToDtoUsr(usr);

        return new RequestResult
        {
          Data = dtoUsr,
          Message = " הפרטים נשמרו בהצלחה ",
          Status = true
        };
      }
      catch (Exception)
      {

        return new RequestResult
        {
          Data = null,
          Message = " שגיאת מערכת, נסה שנית ",
          Status = false
        };
      }






    }
    /// <summary>
    /// קריאה לפונקציה המחלקת את השיר לתגיות : קבלת השם הזמני והתגיות
    /// </summary>
    /// <param name="song"></param>
    /// <returns></returns>
    public static List<string> getTags(string song, out string name)
    {

      List<string> wordsSong = ProgramAndFunction.startUp(song);
      name = wordsSong[0];
      return wordsSong;
    }
    /// <summary>
    /// בדיקה האם לקוח קים
    /// </summary>
    /// <param name="dtoUsr"></param>
    /// <returns></returns>
    public RequestResult checkUsr(DtoUsr dtoUsr)
    {

      try
      {
        Usr usr = dtoUsr.ConvertToUsr();

        using (Ballad120Entities ent = new Ballad120Entities())
        {
          var thisUsr = ent.Usrs.Where(i => i.email == usr.email && i.password == usr.password).FirstOrDefault();

          if (thisUsr != null)
          {
            var thisUsrDto = DtoUsr.ConvertToDtoUsr(thisUsr);
            return
                new RequestResult
                {
                  Data = thisUsrDto,
                  Status = true,
                  Message = " הלקוח אותר בהצלחה ",


                };

          }
          else
          {
            if (ent.Usrs.Where(i => i.email != usr.email).ToList().FirstOrDefault() != null)//הסיסמא שגויה
            {
              return
                  new RequestResult
                  {
                    Data = null,
                    Status = false,
                    Message = " הסיסמא שגויה. נסה שנית",


                  };
            }
            else//אין לקוח כזה
            {
              return
                  new RequestResult
                  {
                    Data = null,
                    Status = false,
                    Message = " כתובת הדוא''ל שגויה. נסה שנית ",


                  };
            }
          }

        }

      }
      catch (Exception ex)
      {
        return new RequestResult
        {
          Data = null,
          Status = false,
          Message = " שגיאת מערכת, נסה שנית ",


        };
      }


    }
    /// <summary>
    /// קבלת אובייקט המכיל את פרטי התגיות עם השירים
    /// </summary>
    /// <returns></returns>
    public RequestResult getTagsWithSongDetails(int userId = 0)
    {
      try
      {


        using (Ballad120Entities ent = new Ballad120Entities())
        {
          if (userId == 0)//שליפת נתונים ללא קשר ללקוח הדורש
          {
            List<DtoTagSong> dtoTagSongs = new List<DtoTagSong>();
            foreach (var song in ent.Songs)
            {

              var e = ent.Tags.Where(i => i.songId == song.songId);
              var tagForSong = (e.Join(ent.Songs, o => o.songId, z => z.songId, (t, s) => new { t.tagId, t.tagName, t.points, s.songId, s.songName, s.songContect, s.userId, s.isPermit }).ToList());

              foreach (var tag in tagForSong)
              {
                int currentOfSongId = tag.songId;



                var isThrereASameTag = dtoTagSongs.Where(i => ((i.tagName == tag.tagName) && (i.songId == currentOfSongId))).ToList().Count;

                if (isThrereASameTag == 0)
                {
                  {
                    dtoTagSongs.Add(new DtoTagSong(tag.tagId, tag.tagName, tag.points, tag.songId, tag.songName, tag.songContect, tag.userId, tag.isPermit));
                  }

                }


              }
            }

            return new RequestResult { Status = true, Message = " song and their tags had sent successfully ", Data = dtoTagSongs };
          }
          else//הלקוח דורש את השירים אך האם את כולם או את שלו בלבד, לפי הדגל
          {
            /* if (isAll == true)*///הלקוח רוצה את כל השירים שבמאגר, כמובן שיש אליהם גישה דרכו
            {//שליפת כל השירים של הלקוח הזה + כל השירים המורשים, ובצד לקוח אסנן לפי דרישת הלקוח
              List<DtoTagSong> dtoTagSongs = new List<DtoTagSong>();
              foreach (var song in ent.Songs.Where(i => i.userId == userId || i.isPermit == true).ToList())//שליפת השירים שלו והשירים שמורשים להתפרסם במאגר
              {

                var e = ent.Tags.Where(i => i.songId == song.songId);
                var tagForSong = (e.Join(ent.Songs, o => o.songId, z => z.songId, (t, s) => new { t.tagId, t.tagName, t.points, s.songId, s.songName, s.songContect, s.userId, s.isPermit }).ToList());

                foreach (var tag in tagForSong)
                {
                  int currentOfSongId = tag.songId;



                  var isThrereASameTag = dtoTagSongs.Where(i => ((i.tagName == tag.tagName) && (i.songId == currentOfSongId))).ToList().Count;

                  if (isThrereASameTag == 0)
                  {
                    {
                      dtoTagSongs.Add(new DtoTagSong(tag.tagId, tag.tagName, tag.points, tag.songId, tag.songName, tag.songContect, tag.userId, tag.isPermit));
                    }

                  }


                }
              }

              return new RequestResult { Status = true, Message = " song and their tags had sent successfully ", Data = dtoTagSongs };
            }

          }


        }
      }
      catch (Exception e)
      {
        return new RequestResult { Status = false, Message = " error with the tags and songs sending ", Data = null };
      }
    }
    /// <summary>
    /// הכנסת שיר למסד הנתונים כולל התגיות שלו
    /// </summary>
    /// <param name="dtoSong"></param>
    /// <returns></returns>
    public RequestResult insertSong(DtoSong dtoSong, int userId = 0)
    {
      string songName = "";
      try
      {
        List<DtoTag> list = new List<DtoTag>();
        Song song = dtoSong.ConvertToSong();
        List<string> tagList = getTags(song.songContect, out songName);//קבלת רשימת התגיות לפי הנתוח הנ"ל
                                                                       //tagList = tagList.Distinct().ToList();//הסרת תגיות חוזרות על עצמן

        song.songName = songName;

        //בדיקה האם שיר זה כבר קים במאגר השירים המשותפים לכולם או שלך 
        if (userId == 0)
        {
          var isThereSameSong = ent.Songs.Where(i => (i.songName == songName && i.isPermit == true) || (i.songName == songName && i.userId == userId)).ToList().FirstOrDefault();
          if (isThereSameSong != null)
          {
            var isThereSameSong_ = ent.Tags.Where(i => i.songId == isThereSameSong.songId).ToList();
            if (isThereSameSong_ != null)
            {
              if (isThereSameSong.songContect == song.songContect)// האם קים שיר בתוכן זהה לשיר זה עם שם כמו השיר הנוכחי שמנסים להעלות
              {
                return new RequestResult//אם אכן קים, אל תאפשר להעלות שיר זה
                {
                  Data = null,
                  Message = " שיר זה קים במאגר ",
                  Status = false
                };
              }

            }
          }
        }
        else
        {
          var filterSongs = ent.Songs.Where(i => i.isPermit == true || i.userId == song.userId).ToList();

          if (filterSongs != null)
          {
            var isThereSameSong = filterSongs.Where(i => i.songContect == song.songContect).ToList().FirstOrDefault();
            if (isThereSameSong != null)// האם קים שיר בתוכן זהה לשיר זה עם שם כמו השיר הנוכחי שמנסים להעלות
            {
              return new RequestResult//אם אכן קים, אל תאפשר להעלות שיר זה
              {
                Data = "",
                Message = " שיר זה קים במאגר ",
                Status = false
              };
            }
          }
        }


        //ent.Songs.Add(new Song { isPermit=song.isPermit,songContect=song.songContect,songId=song.songId,songName=song.songName,userId=song.songId});//הוספת השיר למסד
        ent.Songs.Add(song);
        ent.SaveChanges();
        foreach (var item in tagList)//הכנת התגיות למסד
        {
          ent.Tags.Add(new DAL.Tag { tagName = item, songId = song.songId });
        }
        ent.SaveChanges();
        //DtoSong dtoSong1 = DtoSong.ConvertToDtoSong(song);
        List<string> tags = new List<string>();
        List<int> points = new List<int>();
        foreach (var item in song.Tags)
        {
          DtoTag tag = DtoTag.ConvertToDtoTag(item);

          tags.Add(tag.tagName);
          points.Add(tag.points);
        }

        DtoTagsNameSong songWithTags = new DtoTagsNameSong { isPermit = song.isPermit, songId = song.songId, userId = song.userId, songName = song.songName, userName = ent.Usrs.Where(i => i.userId == userId).FirstOrDefault().userName, neatTags = tags, pointsTags = points, tag1 = "", tag2 = "", tag3 = "" };
        return new RequestResult
        {
          Data = songWithTags,
          Status = true,
          Message = " השיר הועלה לאתר ופיצול התגיות עבר בהצלחה "
        };


      }
      catch (Exception e)
      {
        return new RequestResult
        {
          Data = "",
          Message = " שגיאה בהכנסת השיר ",
          Status = false
        };
      }
    }
    /// <summary>
    /// קבלת כל התגיות של כל השירים
    /// </summary>
    /// <returns></returns>
    public RequestResult getAllTags(int userId = 0, bool isAll = true)
    {
      List<DtoTag> list = new List<DtoTag>();
      try
      {
        if (userId == 0)//כל השירים
        {
          using (Ballad120Entities ent = new Ballad120Entities())
          {
            foreach (var item in ent.Tags)
            {
              DtoTag dtoTag = DtoTag.ConvertToDtoTag(item);
              list.Add(dtoTag);
            }
          }
          return new RequestResult
          {
            Data = list,
            Status = true
          };
        }
        else
        {
          if (isAll == true)//הלקוח מעונין בכל השירים שיש לו גישה אליהם
          {
            using (Ballad120Entities ent = new Ballad120Entities())
            {
              List<Song> songs = ent.Songs.Where(i => i.userId == userId || i.isPermit == true).ToList();//סינון השירים המורשים לפירסום או הפרטיים ללקוח


              List<DAL.Tag> enablesTags = ent.Tags.Where(i => (songs.Any(d => d.songId == i.songId))).ToList();

              foreach (DAL.Tag item in enablesTags)
              {
                DtoTag dtoTag = DtoTag.ConvertToDtoTag(item);
                list.Add(dtoTag);
              }
            }
            return new RequestResult
            {
              Data = list,
              Status = true
            };
          }
          else//הלקוח מעונין בשירים שלו בלבד
          {
            List<Song> songs = ent.Songs.Where(i => i.userId == userId).ToList();//סינון שירי הלקוח בלבד


            List<DAL.Tag> enablesTags = ent.Tags.Where(i => (songs.Any(d => d.songId == i.songId))).ToList();

            foreach (DAL.Tag item in enablesTags)
            {
              DtoTag dtoTag = DtoTag.ConvertToDtoTag(item);
              list.Add(dtoTag);
            }
            return new RequestResult
            {
              Data = list,
              Status = true
            };
          }
        }

      }
      catch
      {
        return new RequestResult
        {
          Data = "",
          Message = " error : there no cameback any tags ",
          Status = false
        };
      }

    }
    /// <summary>
    /// הוספת נקודה לתגית מסוימת
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public RequestResult upgrowPointsTag(int songId, string tagName)
    {
      try
      {


        int countUpgrousedSongs = 0;



        foreach (DAL.Tag tag in ent.Tags.Where(i => i.songId == songId && i.tagName == tagName))
        {

          countUpgrousedSongs++;
          tag.points = tag.points + 1;
        }
        //שיום השיר לתגית שקבלה למעלה מ100 נקודות והיא הגדולה ביותר
        List<int> songPoints = ent.Tags.Where(i => i.songId == songId && i.tagName == tagName).Select(i => i.points).ToList();
        int maxPoint = songPoints.Max();

        if (maxPoint > 100)
        {
          var maxTagPoints = ent.Tags.Where(i => i.songId == songId && i.points == maxPoint).ToList().FirstOrDefault();
          if (maxTagPoints != null)
          {
            var song = ent.Songs.Where(i => i.songId == songId).ToList().FirstOrDefault();
            if (song != null)
              song.songName = maxTagPoints.tagName;
          }
        }




        ent.SaveChanges();
        return new RequestResult
        {
          Status = true,
          Message = " the points had increased successfully ",
          Data = countUpgrousedSongs
        };



      }
      catch (Exception e)
      {
        return new RequestResult
        {
          Status = false,
          Message = " error in increasing the points. look at expection: " + e.GetBaseException(),
          Data = null
        };
      }
    }
    /// <summary>
    /// תוכנית המקבלת מזהה שיר ומחזירה רשימה של התגיות שבו
    /// </summary>
    /// <param name="songId"></param>
    /// <returns></returns>
    public List<DtoTag> getSpecifiedSongAllTags(int songId)
    {
      List<DtoTag> dtoTags = new List<DtoTag>();

      try
      {
        Song song = ent.Songs.Where(i => i.songId == songId).FirstOrDefault();
        List<DAL.Tag> tags = ent.Tags.Where(i => i.songId == songId).ToList();

        foreach (var item in tags)
        {
          DtoTag tag = DtoTag.ConvertToDtoTag(item);
          dtoTags.Add(tag);
        }

        return dtoTags;
      }
      catch
      {
        return dtoTags;
      }
    }
    /// <summary>
    /// פונקציה המקבלת מזהה שיר ומחזירה רשימה עם שמו ושלשת התגיות המובילות ?
    /// </summary>
    /// <param name="songId"></param>
    /// <returns></returns>
    public List<DtoTag> getSpecifiedSongTop3Tags(int songId)
    {
      List<DtoTag> dtoTags = new List<DtoTag>();

      try

      {

        Song song = ent.Songs.Where(i => i.songId == songId).FirstOrDefault();
        List<DAL.Tag> tags = ent.Tags.Where(i => i.songId == songId).OrderBy(j => j.points).Take(3).ToList();

        //Tag tag1 = ent.Tags.Where(i => i.tagName == song.songName).FirstOrDefault(); שם השיר, שהוא התגית
        DAL.Tag tag1 = ent.Tags.FirstOrDefault();
        dtoTags.Add(new DtoTag { songId = song.songId, tagName = song.songName, tagId = tag1.tagId });
        foreach (var item in tags)
        {
          DtoTag tag = DtoTag.ConvertToDtoTag(item);
          dtoTags.Add(tag);

        }

        return dtoTags;
      }
      catch
      {
        return null;
      }
    }
    /// <summary>
    /// קבלת שירים, ולכל אחד את שמו, התגיות, שלשת השמות ושם המשתמש
    /// </summary>
    /// <returns></returns>
    public RequestResult getSongsTagsName3(int usrId = 0)
    {
      try
      {



        if (usrId == 0)
        {
          ///////////////////////
          List<string> tagWords;
          List<int> tagPoints;
          List<DtoTag> allTags = new List<DtoTag>();//שליפת כל התגיות לפי סדר הנקודות
          List<DtoTag> dtoTagsTop3 = new List<DtoTag>();// והכנסת שלשת הראשונות לתוך רשימה
          List<DtoTagsNameSong> dtoTagsNameSongs = new List<DtoTagsNameSong>();
          foreach (var item in ent.Songs)
          {
            tagPoints = new List<int>();
            tagWords = new List<string>();
            allTags = getSpecifiedSongAllTags(item.songId);
            foreach (var m in allTags)
            {
              tagWords.Add(m.tagName);
              tagPoints.Add(m.points);
            }
            dtoTagsTop3 = getSpecifiedSongTop3Tags(item.songId);

            //
            int firstTag = 0;
            int secondTag = 1;
            int thirdTag = 2;
            if (dtoTagsTop3.Count == 1 || dtoTagsTop3.Count == 2 || dtoTagsTop3.Count == 3)//כאשר יש 1-2-3  תגיות בלבד
            {
              secondTag = 0;
              thirdTag = 0;
            }


            if (dtoTagsTop3.Count != 0)//כאשר ישנן תגיות
            {
              // שליפת 3 התגיות הנפוצות
              dtoTagsNameSongs.Add(new DtoTagsNameSong { pointsTags = tagPoints, songId = item.songId, songName = item.songName, neatTags = tagWords, tag1 = dtoTagsTop3[firstTag].tagName, tag2 = dtoTagsTop3[secondTag].tagName, tag3 = dtoTagsTop3[thirdTag].tagName, userName = (ent.Usrs.Where(i => i.userId == item.userId).FirstOrDefault().userName), isPermit = item.isPermit, userId = item.userId });
            }


          }


          return new RequestResult
          {
            Data = dtoTagsNameSongs,
            Message = " you get the song, tags and 3 top ! ",
            Status = true

          };
        }
        else
        {
          ///////////////////////
          List<string> tagWords;
          List<int> tagPoints;
          List<DtoTag> dtoTags;//שליפת כל תגיות השיר לפי סדר הופעתן בשיר (מספור אוטומאטי)
          List<DtoTag> allTags = new List<DtoTag>();//שליפת כל התגיות לפי סדר הנקודות
          List<DtoTag> dtoTagsTop3 = new List<DtoTag>();// והכנסת שלשת הראשונות לתוך רשימה
          List<DtoTagsNameSong> dtoTagsNameSongs = new List<DtoTagsNameSong>();
          List<Song> songs = ent.Songs.Where(i => i.userId == usrId || i.isPermit == true).ToList();
          foreach (var item in songs)
          {
            tagPoints = new List<int>();
            tagWords = new List<string>();
            allTags = getSpecifiedSongAllTags(item.songId);
            foreach (var m in allTags)
            {
              tagWords.Add(m.tagName);
              tagPoints.Add(m.points);
            }
            dtoTagsTop3 = getSpecifiedSongTop3Tags(item.songId);

            //
            int firstTag = 0;
            int secondTag = 1;
            int thirdTag = 2;
            if (dtoTagsTop3.Count == 1 || dtoTagsTop3.Count == 2 || dtoTagsTop3.Count == 3)//כאשר יש 1-2-3  תגיות בלבד
            {
              secondTag = 0;
              thirdTag = 0;
            }
            if (dtoTagsTop3.Count != 0)//כאשר ישנן תגיות
            {
              // שליפת 3 התגיות הנפוצות
              dtoTagsNameSongs.Add(new DtoTagsNameSong { pointsTags = tagPoints, songId = item.songId, songName = item.songName, neatTags = tagWords, tag1 = dtoTagsTop3[firstTag].tagName, tag2 = dtoTagsTop3[secondTag].tagName, tag3 = dtoTagsTop3[thirdTag].tagName, userName = (ent.Usrs.Where(i => i.userId == item.userId).FirstOrDefault().userName), isPermit = item.isPermit, userId = item.userId });
            }


          }

          return new RequestResult
          {
            Data = dtoTagsNameSongs,
            Message = " you get the song, tags and 3 top ! ",
            Status = true

          };
        }



      }
      catch
      {
        return new RequestResult
        {
          Data = null,
          Message = " sorry, we didn't succeed to bring songs with tags and three head tags! ",
          Status = false

        };
      }
    }

    public RequestResult getMatchSongs(string text, int usrId = 0, string state = "")
    {
      return Improveement.getMatchSongs(text, ent, usrId, state);
    }

    /// <summary>
    /// שיפור התגיות בשיר לפי הטקסט מתיבת האיתור
    /// </summary>
    /// <param name="text"></param>
    /// <param name="songId"></param>
    /// <returns></returns>
    public RequestResult improveementTags(string text, int songId)
    {
      return Improveement.improveementTags(text, songId, ent);

    }


    public RequestResult deleteSong(int songId)
    {
      return deleteSong(ent, songId);
    }

  }




}



