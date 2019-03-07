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
    public class QuestionController : ControllerBase
    {
        [Route("api/questionlist")]
        [EnableCors("AllowOrigin")]
        [HttpGet]
        public ArrayList GetQuestionList()
        {
            Database db = new Database();
            ArrayList result = db.GetList("sp_SelectQuestionList");
            db.Close();
            return result;
        }
    }
}
