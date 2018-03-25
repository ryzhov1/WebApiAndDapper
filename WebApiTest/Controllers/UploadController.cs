using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApiTest.Controllers
{
    public class UploadController : ApiController
    {
        public async Task<IHttpActionResult> Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest();
            }
            var provider = new MultipartMemoryStreamProvider();
            
            string root = System.Web.HttpContext.Current.Server.MapPath("~/Files/");
            await Request.Content.ReadAsMultipartAsync(provider);

            string newFilename = "";
            foreach (var file in provider.Contents)
            {
                var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
                byte[] fileArray = await file.ReadAsByteArrayAsync();

                byte[] hash;
                using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
                {
                    hash = sha1.ComputeHash(fileArray);
                }

                StringBuilder hashString = new StringBuilder(hash.Length * 2);
                for (int i = 0; i < hash.Length; i++)
                    hashString.Append(hash[i].ToString(true ? "X2" : "x2"));

                newFilename = hashString.ToString();

                if (!System.IO.File.Exists(root + newFilename))
                {
                    using (System.IO.FileStream fs = new System.IO.FileStream(root + newFilename, System.IO.FileMode.Create))
                    {
                        await fs.WriteAsync(fileArray, 0, fileArray.Length);
                    }
                }
            }

            return Json(new { redirecturl = "/Home/Preview/" + newFilename });
        }
    }
}
