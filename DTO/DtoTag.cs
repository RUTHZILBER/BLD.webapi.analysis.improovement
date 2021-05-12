using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using AutoMapper;

namespace DTO
{


    public class DtoTag
    {

        public DtoTag(int tagId, string tagName, int points, int? songId)
        {
            this.tagId = tagId;
            this.tagName = tagName;
            this.points = points;
            this.songId = songId;
        }

        public DtoTag()
        {
        }

        public int tagId { get; set; }
        public string tagName { get; set; }
        public int points { get; set; }
        public int? songId { get; set; }

        public  Tag ConvertToTag()
        {
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<DtoTag, Tag>()
                );
            var mapper = new Mapper(config);
            return mapper.Map<Tag>(this);

        }


        public static DtoTag ConvertToDtoTag(Tag Tag)
        {
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<Tag, DtoTag>());
            var mapper = new Mapper(config);
            return mapper.Map<DtoTag>(Tag);


        }
    }
}
