using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ConsoleApplication1
{
    [Path("ctrl")]
    class ControlProcessor : Processor
    {


        public ControlProcessor(HttpListenerContext ctx, String aUrlParts)
            : base(ctx, aUrlParts)
        {

        }

        [Path("exit")]
        public void exit()
        {

            res.StatusCode = (int)HttpStatusCode.OK;
            res.ContentType = "text/html";
           

            Byte[] response = Encoding.ASCII.GetBytes("\nServer Down");
            res.OutputStream.Write(response, 0, response.Length);
          
            Constants.serverStop();
        }



    }
}
