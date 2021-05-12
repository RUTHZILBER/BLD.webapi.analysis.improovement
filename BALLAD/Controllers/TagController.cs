using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BLL;
using DTO;

namespace BALLAD.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TagController : ApiController
    {
        
        // GET: api/Tag
        public RequestResult Get()
        {
            DB db = new DB();
            RequestResult requestResult = db.getTagsWithSongDetails();
            return requestResult;
        }

        // GET: api/Tag/5
        public RequestResult Get(int id)
        {
            DB db = new DB();
            RequestResult requestResult = db.getTagsWithSongDetails(id);
            return requestResult;
        }

        // POST: api/Tag
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Tag/5
        public RequestResult Put([FromBody]TextCode tagDetails)
        {
           //two options!
            DB dB = new DB();
            RequestResult requestResult = dB.upgrowPointsTag(tagDetails.SongId, tagDetails.Text);
            return requestResult;
        }

        [HttpPut]
        [Route("api/upgrowTagPoint")]   
        public RequestResult upgrowTagPoint([FromBody]TextCode tagDetails)
        {
            DB dB = new DB();
            RequestResult requestResult = dB.upgrowPointsTag(tagDetails.SongId,tagDetails.Text);
            return requestResult;
        }
        [HttpPost]
        [Route("api/values/getTableInform")]

        public RequestResult getTableInform([FromBody] Text usrId)
        {
            DB dB = new DB();

            RequestResult requestResult1 = dB.getSongsTagsName3(Convert.ToInt32( usrId.Texts));


            int x = 34;
            return requestResult1;
        }
        //[HttpPut]
        //[Route("api/getTagNameSong")]
        //public RequestResult getTagNameSong()
        //{  
        //    DB dB = new DB();
        //    RequestResult requestResult = dB.getSongsTagsName3();
        //    return requestResult;
        //}

        // DELETE: api/Tag/5
        public void Delete(int id)
        {
        }
    }
}
