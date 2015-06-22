using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ConsoleApplication1
{
    class Response
    {
        
        private HttpListenerResponse res;
        
        public Response(HttpListenerResponse ares)
        {
            res = ares;
                   
        }

        public int SendRaw(String path,String type)
        {
            if (!String.IsNullOrEmpty(type)) res.ContentType = type;
            res.StatusCode = (int)HttpStatusCode.OK;
            return new ResponseWriter(res.OutputStream).WriteFile(path);
        }

        public int SendRaw(String path)
        {
            return SendRaw(path,null);
        }        

        public int SendText(String text)
        {
            res.StatusCode = (int)HttpStatusCode.OK;
            res.ContentType = Constants.texthtml;
            return new ResponseWriter(res.OutputStream).WriteString(text);
        }

        public int SendFile(String path)
        {           
            res.StatusCode = (int)HttpStatusCode.OK;
            res.ContentType = Constants.download;
            res.AddHeader("content-disposition", "attachment; filename=\"" + ResponseWriter.getFileName(path)+"\"");
            return new ResponseWriter(res.OutputStream).WriteFile(path);
        }

        public int NotFound(String path)
        {
            res.StatusCode = (int)HttpStatusCode.NotFound;
            res.ContentType = Constants.texthtml;
           
            return new ResponseWriter(res.OutputStream).WriteString(path);
        }

        public int NotFound()
        {
            res.StatusCode = (int)HttpStatusCode.NotFound;
            res.ContentType = Constants.texthtml;

            return new ResponseWriter(res.OutputStream).WriteString("Not Found");
        }

        public int Unauthorized(String cause)
        {
            res.StatusCode = (int)HttpStatusCode.Unauthorized;
            return new ResponseWriter(res.OutputStream).WriteString(cause);
        }

             public int NotAcceptable(String cause)
        {
            res.StatusCode = (int)HttpStatusCode.NotAcceptable;

            return new ResponseWriter(res.OutputStream).WriteString(cause);
        }

         public int InternalError(String cause)
        {
            res.StatusCode = (int)HttpStatusCode.InternalServerError;
            return new ResponseWriter(res.OutputStream).WriteString(cause);
        }

    }
}
