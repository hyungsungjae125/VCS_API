using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace VCSdotnet.Controllers
{
    
    [ApiController]
    public class MemberController : ControllerBase
    {
        [Route("api/get")]
        [EnableCors("AllowOrigin")]
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            Database db = new Database();
            return new string[] { "value1", "value2" };
        }

        [Route("api/login")]
        [EnableCors("AllowOrigin")]
        [HttpPost]
        public ArrayList Login([FromForm] string id,[FromForm] string pw)
        {
            Hashtable param = new Hashtable();
            param.Add("@id",id);
            param.Add("@pw",pw);
            ArrayList result = new ArrayList();
            try{
                Database db = new Database();
                string sql = "sp_SearchMember";
                result = db.GetList(sql,param);
                db.Close();
                return result;
            }catch{
                return new ArrayList();
            }
            
        }
    }
}
