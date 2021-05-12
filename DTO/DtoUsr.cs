using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using AutoMapper;

namespace DTO
{
   public class DtoUsr
    {
        //d
        public DtoUsr(int userId, string userName, string firstName, string lastName, string email, string password, DateTime? notingTime, string comment)
        {
            this.userId = userId;
            this.userName = userName;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.password = password;
            this.notingTime = notingTime;
            this.comment = comment;
        }

        public DtoUsr()
        {
        }

        public int userId { get; set; }
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime? notingTime { get; set; }
        public string comment { get; set; }


        public Usr ConvertToUsr()
        {
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<DtoUsr, Usr>()
                );
            var mapper = new Mapper(config);
            return mapper.Map<Usr>(this);

        }


        public static DtoUsr ConvertToDtoUsr(Usr usr)
        {
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<Usr, DtoUsr>());
            var mapper = new Mapper(config);
            return mapper.Map<DtoUsr>(usr);
        }
    }
}
