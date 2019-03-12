using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            string path = System.IO.Directory.GetCurrentDirectory() + "/wwwroot";//"/root/VCS_API/wwwroot";
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
                param.Add("@mNo", mNo);
                int result = db.NonQuery("sp_InsertNotice", param);

                db.Close();

                return result;
            }
            catch
            {
                return 0;
            }

        }

        [Route("api/noticeupdate")]
        [EnableCors("AllowOrigin")]
        [HttpPost]
        public int GetUpdateNotice([FromForm] string nNo, [FromForm] string fileName, [FromForm] string fileData, [FromForm] string nTitle, [FromForm] string nContents, [FromForm] string mNo)
        {
            string path = System.IO.Directory.GetCurrentDirectory() + "/wwwroot";//"/root/VCS_API/wwwroot";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            byte[] data=null;
            if(fileData!=null){
                data = Convert.FromBase64String(fileData);
            }
            else{
                data=null;
            } 
            try
            {
                string fullName="";
                if (fileName != null)
                {
                    string ext = fileName.Substring(fileName.LastIndexOf("."));
                    Guid saveName = Guid.NewGuid();
                    fullName = saveName + ext;   // 저장되는 파일명 생성
                    string fullPath = string.Format("{0}/{1}", path, fullName);  // 전체경로 + 저장파일명 (주소) 
                    FileInfo fileInfo = new FileInfo(fullPath);
                    FileStream fileStream = fileInfo.Create();
                    fileStream.Write(data, 0, data.Length);
                    fileStream.Close();
                }


                string url = fullName;
                Database db = new Database();
                Hashtable param = new Hashtable();
                param.Add("@nNo", nNo);
                param.Add("@mNo", mNo);
                param.Add("@nTitle", nTitle);
                param.Add("@nContents", nContents);
                param.Add("@nUrl", url);
                SqlDataReader sdr = db.GetReader("sp_UpdateNotice", param);
                string fileUrl = "";
                while(sdr.Read()){
                    fileUrl = sdr.GetValue(0).ToString();
                    //Console.Write(fileUrl+">>-----------<<");
                }

                DirectoryInfo d = new DirectoryInfo(path);       //Assuming Test is your Folder
                FileInfo[] Files = d.GetFiles("*.png");                  //Getting Text files

                foreach (FileInfo file in Files)
                {
                    Console.Write("삭제하려고 찾는 파일 : "+fileUrl);
                    Console.WriteLine(" 검색된 파일 : "+file.Name);
                    if (fileUrl == file.Name)
                    {
                        Console.WriteLine("---------찾앗음-----------삭제");
                        file.Delete();
                    }
                }

                db.Close();
                return 1;
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

            SqlDataReader sdr = db.GetReader("sp_DeleteNotice", param);
            string path = System.IO.Directory.GetCurrentDirectory() + "/wwwroot";
            string fileUrl = "";
            while (sdr.Read())
            {
                fileUrl = sdr.GetValue(0).ToString();
                //Console.Write(fileUrl+">>-----------<<");
            }

            DirectoryInfo d = new DirectoryInfo(path);       //Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.png");                  //Getting Text files

            foreach (FileInfo file in Files)
            {
                Console.Write("삭제하려고 찾는 파일 : " + fileUrl);
                Console.WriteLine(" 검색된 파일 : " + file.Name);
                if (fileUrl == file.Name)
                {
                    Console.WriteLine("---------찾앗음-----------삭제");
                    file.Delete();
                }
            }

            db.Close();
            return 1;
        }
    }
}
