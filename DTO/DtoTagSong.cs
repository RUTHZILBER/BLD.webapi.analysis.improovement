using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using AutoMapper;

namespace DTO
{
    /// <summary>
    /// שיר עם הפרטים שלו עבור החפוש
    /// </summary>
    public class DtoTagSong
    {
        public int tagId { get; set; }
        public string tagName { get; set; }
        public int points { get; set; }
        public int? songId { get; set; }
        public string songName { get; set; }
        public string songContect { get; set; }
        public int userId { get; set; }
        public bool isPermit { get; set; }
        public DtoTagSong()
        {
        }

        public DtoTagSong(int tagId, string tagName, int points, int? songId, string songName, string songContect, int userId, bool isPermit)
        {
            this.tagId = tagId;
            this.tagName = tagName;
            this.points = points;
            this.songId = songId;
            this.songName = songName;
            this.songContect = songContect;
            this.userId = userId;
            this.isPermit = isPermit;
        }

       




    }
}
