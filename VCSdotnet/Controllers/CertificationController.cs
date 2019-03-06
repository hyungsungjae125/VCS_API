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
    }
}
