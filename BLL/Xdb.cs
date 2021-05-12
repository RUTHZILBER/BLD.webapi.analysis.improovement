using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class Xdb
    {
       
        public static RequestResult deleteSong( Ballad120Entities ent,int songId)
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
        
    }
}
