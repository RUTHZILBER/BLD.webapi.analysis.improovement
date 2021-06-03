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
    
    public class SongController : ApiController
    {

        public DB db = new DB();
        // GET: api/Song/5
        public RequestResult Get(int id)
        {
            RequestResult requestResult = db.deleteSong(id);
            return requestResult;
        }

        

        
        [HttpPost]
        [Route("api/values/improveTags")]

        public RequestResult improveTags([FromBody] TextCode textCode)
        {
            RequestResult requestResult = db.improveementTags(textCode.Text, textCode.SongId);
            return requestResult;
        }



        [HttpPost]
        [Route("api/values/getMatchSongsList")]

        public RequestResult getMatchSongsList([FromBody] TextUserId textUserId)
        {
            RequestResult requestResult1 = db.getMatchSongs(textUserId.Text, textUserId.UsrId, textUserId.State);
            return requestResult1;
        }

        // POST: api/Song
        public RequestResult Post([FromBody]DtoSong dtoSong)
        {
            return db.insertSong(dtoSong);
        }

        // PUT: api/Song/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Song/5
        public RequestResult Delete(int id)//int a,int b
        {
            RequestResult requestResult = db.deleteSong(id);
            return requestResult;
        }

        [HttpPost]
        [Route("api/values/addSong")]

        public RequestResult addSong([FromBody]DtoSong dtoSong)
        {
            RequestResult requestResult = db.insertSong(dtoSong,dtoSong.userId);
            return requestResult;

        }

        //[HttpPost]
        //[Route("api/values/addSong2")]
        //// POST: api/Usr
        //public void addSong2([FromBody]string dtoUsr)
        //{

           
          

        //}


    }
}
