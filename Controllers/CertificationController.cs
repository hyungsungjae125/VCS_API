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
    public class CertificationController : ControllerBase
    {
        [Route("api/certificationlist")]
        [EnableCors("AllowOrigin")]
        [HttpGet]
        public ArrayList GetCertificationList()
        {
            Database db = new Database();
            ArrayList result = db.GetList("sp_SelectCertificationList");
            db.Close();
            return result;
        }

        [Route("api/certificationdetail")]
        [EnableCors("AllowOrigin")]
        [HttpPost]
        public ArrayList GetCertificationDetail([FromForm] string ono)
        {
            Database db = new Database();
            Hashtable param = new Hashtable();
            param.Add("@oNo",ono);
            ArrayList result = db.GetList("sp_SelectCertificationDetail",param);
            db.Close();
            return result;
        }

        [Route("api/certificationok")]
        [EnableCors("AllowOrigin")]
        [HttpPost]
        public int GetCertificationOk([FromForm] string ono,[FromForm] string mno,[FromForm] string time)
        {
            Database db = new Database();
            Hashtable param = new Hashtable();
            param.Add("@oNo",ono);
            param.Add("@mNo",mno);
            param.Add("@time",time);
            int result = db.NonQuery("sp_SelectCertificationOk",param);
            db.Close();
            return result;
        }
    }
}
