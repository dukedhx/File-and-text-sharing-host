using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ConsoleApplication1
{
    [Path("file")]    
     class FileProcessor : Processor
    {
        
        private String filePath;

        public FileProcessor(HttpListenerContext ctx, String aUrlParts)
            : base(ctx, aUrlParts)
        {
            filePath = urlParts.ToNormalizedPath();
        }

        public override void  getDefault()
{
    res.Redirect("/file/list");

        }

        [Path("del")]
        public void del()
        {

            if (FileIO.delete(Util.getValidPath(new String[] { Constants.filePath, filePath }))) new Response(res).SendText("Deleted."); else new Response(res).NotFound();
        }

        [Path("get")]
        public void getFiles()
        {
               Response r =  new Response(res);
               if (r.SendFile(Util.getValidPath(new String[]{Constants.filePath,filePath}))== Constants.fileNotFound) r.NotFound(filePath);        
        }

        [Path("getraw")]
        public void getRawFiles()
        {
            Response r = new Response(res);
            if (r.SendRaw(Util.getValidPath(new String[] { Constants.filePath, filePath })) == Constants.fileNotFound) r.NotFound(filePath);
        }

        [Path("list",true)]
        public void getList()
        {

            new Response(res).SendText("\n<a href='../ctrl/exit'>Exit</a><br/>" + req.UserAgent + 

                String.Concat((from path in Directory.GetFiles(Constants.filePath) let fn= ResponseWriter.getFileName(path) select "<br/>>>> <a href=\"/file/get/"+fn+"\">"+fn+"</a> | <a href=\"/file/getraw/"+fn+"\">Raw</a> | <a href=\"/file/del/"+fn+"\">Del</a>").ToArray() ));
            
        }

        [Path("upload", true, Constants.post)]
        public void upload()
        {
            
            new UploadSaver(req.InputStream, req.ContentType.Split(new String[] { "boundary=" }, StringSplitOptions.RemoveEmptyEntries)[1]).saveFiles();
        }
    }
}
