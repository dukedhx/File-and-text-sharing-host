using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ConsoleApplication1
{
    [Path(Constants.rootPath)]
    class Processor:IDisposable
    {
        private Boolean disposed;
        protected HttpListenerRequest req;
        protected HttpListenerResponse res;
        protected String urlParts;
        public Processor(HttpListenerContext ctx, String aUrlParts)
        {
            req = ctx.Request;
            res = ctx.Response;
            urlParts = aUrlParts;
            Constants.conncount++;
            Console.WriteLine("Processor Initiated");
            disposed = false;            
        }

         public virtual void getDefault()
        {
            res.Redirect("/static/");
        }

         public virtual void getRaw()
        {
            new Response(res).SendText("Get Raw of : " + urlParts+ ", " + "from :" + this.GetType());
            Console.WriteLine("Default Root Processor getRaw called");
        }


         public virtual void notFound()
         {             
             new Response(res).NotFound("Not found: "+Util.checkNullOrEmpty(urlParts));
             Console.WriteLine("Default Root Processor notFound called");
         }

         public virtual void notAcceptable()
         {
             new Response(res).NotAcceptable(Constants.postOnly);
             Console.WriteLine("Default Root Processor notAcceptable called");
         }

         ~Processor()
         {     
            if(!disposed)
             Dispose();   
         }

         public virtual void Dispose() 
         {
             try
             {
                 res.Close();
             }
             catch (ObjectDisposedException ex)
             {
                 Console.WriteLine("[Disposed]"+ex.Message);
             }
             Constants.conncount--;
             Console.WriteLine(this.GetType()+"Processor Destroyed");
             disposed = true;
         }

        

    }
}
