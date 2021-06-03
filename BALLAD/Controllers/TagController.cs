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
    public DB db = new DB();
    // GET: api/Tag
    public RequestResult Get()
        {
           
            RequestResult requestResult = db.getTagsWithSongDetails();
            return requestResult;
        }

        // GET: api/Tag/5
        public RequestResult Get(int id)
        {
           
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
          
            RequestResult requestResult = db.upgrowPointsTag(tagDetails.SongId, tagDetails.Text);
            return requestResult;
        }

        [HttpPut]
        [Route("api/upgrowTagPoint")]   
        public RequestResult upgrowTagPoint([FromBody]TextCode tagDetails)
        {
           
            RequestResult requestResult = db.upgrowPointsTag(tagDetails.SongId,tagDetails.Text);
            return requestResult;
        }
        [HttpPost]
        [Route("api/values/getTableInform")]

        public RequestResult getTableInform([FromBody] Text usrId)
        {
            RequestResult requestResult1 = db.getSongsTagsName3(Convert.ToInt32( usrId.Texts));


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
