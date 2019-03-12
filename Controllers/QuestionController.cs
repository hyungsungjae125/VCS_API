using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

        [Route("api/questiondetail")]
        [EnableCors("AllowOrigin")]
        [HttpPost]
        public ArrayList GetQuestionDetail([FromForm] string qNo)
        {
            Database db = new Database();
            Hashtable param = new Hashtable();
            param.Add("@qNo", qNo);
            ArrayList result = db.GetList("sp_SelectQuestionDetail", param);
            db.Close();
            return result;
        }

        [Route("api/answerinsert")]
        [EnableCors("AllowOrigin")]
        [HttpPost]
        public int GetInsertAnswer([FromForm] string fileName, [FromForm] string fileData, [FromForm] string aTitle, [FromForm] string aContents, [FromForm] string dNo, [FromForm] string qNo)
        {
            string path = System.IO.Directory.GetCurrentDirectory() + "/wwwroot/answer";//"/root/VCS_API/wwwroot/answer";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            byte[] data =null;
            if(fileData!=null){
                data= Convert.FromBase64String(fileData);
            }

            try
            {
                string fullName="";
                string url="";
                if(fileName != null)
                {
                    string ext = fileName.Substring(fileName.LastIndexOf("."));
                    Guid saveName = Guid.NewGuid();
                    fullName = saveName + ext;   // 저장되는 파일명 생성
                    string fullPath = string.Format("{0}/{1}", path, fullName);  // 전체경로 + 저장파일명 (주소) 
                    FileInfo fileInfo = new FileInfo(fullPath);
                    FileStream fileStream = fileInfo.Create();
                    fileStream.Write(data, 0, data.Length);
                    fileStream.Close();
                    url = "answer/"+fullName;
                }
                
                Database db = new Database();
                Hashtable param = new Hashtable();
                param.Add("@aTitle", aTitle);
                param.Add("@aContents", aContents);
                param.Add("@aUrl", url);
                param.Add("@qNo", qNo);
                param.Add("@dNo", dNo);
                int result;
                if(dNo=="3"){
                    result= db.NonQuery("sp_InsertAnswer", param);
                }
                else{
                    result= -1;
                }

                db.Close();

                return result;
            }
            catch
            {
                return 0;
            }

        }
    }
}
