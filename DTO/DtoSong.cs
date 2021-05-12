

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using AutoMapper;

namespace DTO
{

    public class DtoSong
    {

        public DtoSong(int songId, string songName, string songContect, int userId, bool isPermit)
        {
            this.songId = songId;
            this.songName = songName;
            this.songContect = songContect;
           
            this.userId = userId;
            this.isPermit = isPermit;
        }

        public DtoSong()
        {
        }

        public int songId { get; set; }
        public string songName { get; set; }
        public string songContect { get; set; }
       
        public int userId { get; set; }
        public bool isPermit { get; set; }


        public Song ConvertToSong()
        {
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<DtoSong, Song>()
                );
            var mapper = new Mapper(config);
            return mapper.Map<Song>(this);
        }

        public static DtoSong ConvertToDtoSong(Song song)
        {
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<Song, DtoSong>());
            var mapper = new Mapper(config);
            return mapper.Map<DtoSong>(song);


        }

       

    }
}




