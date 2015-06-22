using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net; 

namespace ConsoleApplication1
{
    [Path("static")]
    class StaticProcessor:Processor
    {

        public StaticProcessor(HttpListenerContext ctx, String aUrlParts) : base(ctx, aUrlParts) { }

        public override void getRaw()
        {

            Response r = new Response(res);
            String path = Util.getValidPath(new String[] {  req.RawUrl.ToNormalizedPath() });
            Console.WriteLine(req.RawUrl.ToNormalizedPath());
            if (r.SendRaw(path,FileIO.getType(path)) == Constants.fileNotFound) r.NotFound();
        }

        public override void getDefault()
        {           
            Response r = new Response(res);
            String path = Util.getValidPath(new String[] { "static", "index.html"});
            if (r.SendRaw(path, Constants.texthtml) == Constants.fileNotFound) r.NotFound();
        }
    }
}
