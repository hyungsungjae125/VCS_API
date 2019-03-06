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
    public class VolunteerController : ControllerBase
    {
        [Route("api/volunteerlistinsert")]
        [EnableCors("AllowOrigin")]
        [HttpPost]
        public bool VoluteerListInsert([FromForm] int mno,[FromForm] string name,[FromForm] string contents,[FromForm] string city,[FromForm] string gu,[FromForm] string field,[FromForm] string place,[FromForm] string startcollect,[FromForm] string endcollect,[FromForm] string startvol,[FromForm] string endvol,[FromForm] int collectnum,[FromForm] int time,[FromForm] string week,[FromForm] string vobject ,[FromForm] int count)
        {
            Hashtable param = new Hashtable();
            param.Add("@mNo",mno);
            param.Add("@name",name);
            param.Add("@contents",contents);
            param.Add("@city",city);
            param.Add("@gu",gu);
            param.Add("@field",field);
            param.Add("@place",place);
            param.Add("@startcollect",startcollect);
            param.Add("@endcollect",endcollect);
            param.Add("@startvol",startvol);
            param.Add("@endvol",endvol);
            param.Add("@collectnum",collectnum);
            param.Add("@time",time);
            param.Add("@week",week);
            param.Add("@object",vobject);
            param.Add("@count",count);
            
            try{
                Database db = new Database();
                string sql = "sp_InsertVolunteerList";
                if(db.NonQuery(sql,param)){
                    Console.WriteLine("봉사목록 모집등록 성공");
                    return true;
                }
                else{
                    Console.WriteLine("봉사목록 모집등록 실패...");
                    return false;
                }
            }catch{
                return false;
            }
            
        }

        [Route("api/applylist")]
        [EnableCors("AllowOrigin")]
        [HttpGet]
        public ArrayList GetApplyList()
        {
            Database db = new Database();
            ArrayList result = db.GetList("sp_SelectVolunteerList");
            db.Close();
            return result;
        }
    }
}
