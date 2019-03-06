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
    public class NoticeController : ControllerBase
    {
        [Route("api/noticelist")]
        [EnableCors("AllowOrigin")]
        [HttpGet]
        public ArrayList GetNoticeList()
        {
            Database db = new Database();
            ArrayList result = db.GetList("sp_SelectNoticeList");
            db.Close();
            return result;
        }
    }
}
