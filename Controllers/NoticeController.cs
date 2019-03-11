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

        [Route("api/noticedetail")]
        [EnableCors("AllowOrigin")]
        [HttpPost]
        public ArrayList GetNoticeDetail([FromForm] string nno)
        {
            Database db = new Database();
            Hashtable param = new Hashtable();
            param.Add("@nNo", nno);
            ArrayList result = db.GetList("sp_SelectNoticeDetail", param);
            db.Close();
            return result;
        }

        [Route("api/noticeinsert")]
        [EnableCors("AllowOrigin")]
        [HttpPost]
        public int GetInsertNotice([FromForm] string fileName, [FromForm] string fileData, [FromForm] string nTitle, [FromForm] string nContents, [FromForm] string mNo)
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            byte[] data = Convert.FromBase64String(fileData);

            try
            {
                string ext = fileName.Substring(fileName.LastIndexOf("."));
                Guid saveName = Guid.NewGuid();
                string fullName = saveName + ext;   // 저장되는 파일명 생성
                string fullPath = string.Format("{0}/{1}", path, fullName);  // 전체경로 + 저장파일명 (주소) 
                FileInfo fileInfo = new FileInfo(fullPath);
                FileStream fileStream = fileInfo.Create();
                fileStream.Write(data, 0, data.Length);
                fileStream.Close();

                string url = fullName;

                Database db = new Database();
                Hashtable param = new Hashtable();
                param.Add("@nTitle", nTitle);
                param.Add("@nContents", nContents);
                param.Add("@nUrl", url);
                param.Add("@mNo",mNo);
                int result = db.NonQuery("sp_InsertNotice", param);

                db.Close();

                return result;
            }
            catch
            {
                return 0;
            }

        }

        [Route("api/noticedelete")]
        [EnableCors("AllowOrigin")]
        [HttpPost]
        public int GetDeleteNotice([FromForm] string nno, [FromForm] string nurl)
        {
            Database db = new Database();
            Hashtable param = new Hashtable();
            param.Add("@nNo", nno);

            ArrayList result = db.GetList("sp_DeleteNotice", param);
            string path = System.IO.Directory.GetCurrentDirectory();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            db.Close();
            return 1;
        }
    }
}
