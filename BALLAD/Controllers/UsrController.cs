using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using DTO;
using BLL;
namespace BALLAD.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsrController : ApiController
    {
    public DB db = new DB();
    // GET: api/Usr
    public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Usr/5
        public string Get(int id)
        {
            return "value";
        }


        // POST: api/Usr
        public void Post([FromBody]DtoSong dtoUsr)
        {
            //DtoUsr dtoUsr = new DtoUsr();
            DB dB = new DB();

            //return dB.InsertUsr(dtoUsr);RequestResult
        }

        // PUT: api/Usr/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Usr/5
        public void Delete(int id)
        {
        }

        [HttpPost]
        [Route("api/values/addUsr")]
        
        public RequestResult addUsr([FromBody]DtoUsr dtoUsr)
        {
            RequestResult x = db.insertUsr(dtoUsr);
            return x;
        }

        [HttpPost]
        [Route("api/values/updateUsr")]
        // POST: api/Usr
        public RequestResult updateUsr([FromBody]DtoUsr dtoUsr)
        {

            RequestResult x = db.updateUsr(dtoUsr);
            return x;
        }
        [HttpPost]
        [Route("api/values/checkUser")]
        // POST: api/Usr
        public RequestResult checkUser([FromBody]DtoUsr dtoUsr)
        {
            RequestResult requestResult=db.checkUsr(dtoUsr);
            return requestResult;
        }

    }
}
