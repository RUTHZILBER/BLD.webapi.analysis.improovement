using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    /// <summary>
    /// 
    /// ומזהה שיר, שם משתמש, האם מורשה לפרסום, 3 תגיות ראשונות ורשימת תגיות
    /// </summary>
    public class DtoTagsNameSong
    {
        public DtoTagsNameSong()
        {
        }

        public DtoTagsNameSong(int? songId, string songName, List<string> neatTags, List<int> pointsTags, string tag1, string tag2, string tag3, string userName, int userId, bool isPermit)
        {
            this.songId = songId;
            this.songName = songName;
            this.neatTags = neatTags;
            this.pointsTags = pointsTags;
            this.tag1 = tag1;
            this.tag2 = tag2;
            this.tag3 = tag3;
            this.userName = userName;
            this.userId = userId;
            this.isPermit = isPermit;
        }

        public int? songId { get; set; }
        public string songName { get; set; }
        public List<string> neatTags { get; set; }
        public List<int> pointsTags { get; set; }
        public string tag1 { get; set; }
        public string tag2 { get; set; }
        public string tag3 { get; set; }
        public string userName { get; set; }
        public int userId { get; set; }
        public bool isPermit { get; set; }


        
    }

   
}
