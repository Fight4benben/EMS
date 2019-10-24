using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;

namespace EMS.UI.Controllers.Setting
{
    [Authorize]
    public class SvgFileController : ApiController
    {
        [HttpPost]
        public object Upload()
        {
            try
            {
                var request = HttpContext.Current.Request;
                var formData = request.Form;
                string svgId = formData["svgId"];
                string fileName = svgId + ".svg";

                if (request.Files.Count > 0)
                {
                    var file = request.Files[0];
                    string filePath = HttpContext.Current.Server.MapPath("~/App_Data/SvgFiles/");
                    file.SaveAs(Path.Combine(filePath, fileName));
                }
                
                return new { Result = "上传成功！", Flag = true };
            }
            catch (Exception e)
            {
                return new { Error = e.Message, Flag = false };
            }
        }

        public HttpResponseMessage Get(string id)
        {
            string filePath = HttpContext.Current.Server.MapPath("~/App_Data/SvgFiles/");
            string fileName = id.ToUpper() + ".svg";

            string path = Path.Combine(filePath, fileName);
            try
            {
                var stream = new FileStream(path, FileMode.Open);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(stream);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };

                return response;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
        }

        public string Get(string name,string type)
        {
            string filePath = HttpContext.Current.Server.MapPath("~/app/img/");
            string fileName = "aircondition.svg";
            string svgview = "";

            string path = Path.Combine(filePath, fileName);


            svgview = File.ReadAllText(path, Encoding.UTF8);

            return svgview;
        }
    }
}
