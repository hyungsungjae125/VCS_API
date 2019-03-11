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
                if(db.NonQuery(sql,param)==1){
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

        [Route("api/volunteerlistupdate")]
        [EnableCors("AllowOrigin")]
        [HttpPost]
        public bool VoluteerListUpdate([FromForm] int vno,[FromForm] int mno,[FromForm] string name,[FromForm] string contents,[FromForm] string city,[FromForm] string gu,[FromForm] string field,[FromForm] string place,[FromForm] string startcollect,[FromForm] string endcollect,[FromForm] string startvol,[FromForm] string endvol,[FromForm] int collectnum,[FromForm] int time,[FromForm] string week,[FromForm] string vobject ,[FromForm] int count)
        {
            Hashtable param = new Hashtable();
            param.Add("@vNo",vno);
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
                string sql = "sp_UpdateVolunteerList";
                if(db.NonQuery(sql,param)==1){
                    Console.WriteLine("봉사목록 수정 성공");
                    return true;
                }
                else{
                    Console.WriteLine("봉사목록 수정 실패...");
                    return false;
                }
            }catch{
                return false;
            }
            
        }

        [Route("api/volunteerlistdelete")]
        [EnableCors("AllowOrigin")]
        [HttpPost]
        public int VoluteerListDelete([FromForm] int mno,[FromForm] string vno)
        {
            Hashtable param = new Hashtable();
            param.Add("@mNo",mno);
            param.Add("@vNo",vno);
            
            try{
                Database db = new Database();
                string sql = "sp_DeleteVolunteer";
                if(db.NonQuery(sql,param)==1){
                    Console.WriteLine("봉사목록 삭제 성공");
                    return 1;
                }
                else{
                    Console.WriteLine("봉사목록 삭제 실패...");
                    return 0;
                }
            }catch{
                return 0;
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

        [Route("api/applylistdetail")]
        [EnableCors("AllowOrigin")]
        [HttpPost]
        public ArrayList GetApplyListDetail([FromForm] string vno)
        {
            Database db = new Database();
            Hashtable param = new Hashtable();
            param.Add("@vNo",vno);
            ArrayList result = db.GetList("sp_SelectVolunteerListDetail",param);
            db.Close();
            return result;
        }
    }
}
