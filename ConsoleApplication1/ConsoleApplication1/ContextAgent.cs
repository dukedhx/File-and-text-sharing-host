using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;


namespace ConsoleApplication1
{
    class ContextAgent
    {
       
        private String url;
        private HttpListenerContext context;
        public HttpListenerContext Context {get{return context;}}
        public String Url { get { return url; } }        
        public ContextAgent(String aurl, HttpListenerContext ctx)
        {           
            url = aurl;            
            context = ctx;
        }
    }
}
