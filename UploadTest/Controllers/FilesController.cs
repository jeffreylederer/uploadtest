﻿using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace UploadTest.Controllers
{
    public class FilesController : ApiController
    {
        [HttpPost] // This is from System.Web.Http, and not from System.Web.Mvc
        public async Task<HttpResponseMessage> Upload()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            var streamProvider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(streamProvider);
            foreach (var ctnt in streamProvider.Contents)
            {
                // You would get hold of the inner memory stream here
                var len = ctnt.Headers.ContentLength;
                var stream = ctnt.ReadAsStreamAsync().Result;
                var buffer = new byte[(int) len];
                await stream.ReadAsync(buffer, 0, (int) len);
                var mediaType = ctnt.Headers.ContentType.MediaType;
                var fileName = ctnt.Headers.ContentDisposition.FileName;
                fileName = fileName.Replace("\"", "");


                //save results here
                /*
                var item  = db.DocumentTable.Create();
                item.MediaType = mediaType;
                item.filename = filename;
                item.contents = buffer;
                db.DocumentTable.Add(item);
                db.SaveChanges();


                */
            }


            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}