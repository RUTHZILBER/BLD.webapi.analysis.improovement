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
       
        // GET: api/Song/5
        public RequestResult Get(int id)
        {
            DB db = new DB();
            RequestResult requestResult = db.deleteSong(id);
            return requestResult;
        }

        

        
        [HttpPost]
        [Route("api/values/improveTags")]

        public RequestResult improveTags([FromBody] TextCode textCode)
        {
            DB dB = new DB();

            RequestResult requestResult1 = dB.improveementTags(textCode.Text, textCode.SongId);


            int x = 34;
            return requestResult1;
        }



        [HttpPost]
        [Route("api/values/getMatchSongsList")]

        public RequestResult getMatchSongsList([FromBody] TextUserId textUserId)
        {
            DB dB = new DB();
           
            RequestResult requestResult1 = dB.getMatchSongs(textUserId.Text, textUserId.UsrId, textUserId.State);

            int x = 34;
            return requestResult1;
        }

        // POST: api/Song
        public RequestResult Post([FromBody]DtoSong dtoSong)
        {
            DB dB = new DB();
            return dB.insertSong(dtoSong);
        }

        // PUT: api/Song/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Song/5
        public RequestResult Delete(int id)//int a,int b
        {
            
            DB db = new DB();
            RequestResult requestResult = db.deleteSong(id);
            return requestResult;
        }

        [HttpPost]
        [Route("api/values/addSong")]

        public RequestResult addSong([FromBody]DtoSong dtoSong)
        {

            DB dB = new DB();
            RequestResult requestResult = dB.insertSong(dtoSong,dtoSong.userId);
            return requestResult;

        }

        [HttpPost]
        [Route("api/values/addSong2")]
        // POST: api/Usr
        public void addSong2([FromBody]string dtoUsr)
        {

            DB dB = new DB();
            //RequestResult x = dB.InsertUsr(dtoUsr);

        }


    }
}
