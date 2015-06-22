using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Collections;

namespace ConsoleApplication1
{
    class PathQualifier
    {
        private String url;
        private String spath;
        private String path;
        private ICollection keys;
        private HttpListenerContext context;
        public HttpListenerContext Context{get{return context;}}
        public PathQualifier(HttpListenerContext ctx, ICollection akeys)
        {
            url = ctx.Request.RawUrl;
            load(ctx,akeys);
        }

        public PathQualifier(HttpListenerContext ctx, String aurl, ICollection akeys)
        {
            url = aurl;
           load(ctx,akeys);
        }

        private void load(HttpListenerContext ctx, ICollection akeys)
        {
             context=ctx;
            keys = akeys;
            path = getPath();
            spath = String.IsNullOrEmpty(path) ? "" : new Regex(path).Replace(url, "", 1).RemoveLastIfExists(Constants.rootpath);
        }

        public String Spath
        {
            get { return spath;}
        }

        public String Path
        {
            get
            {
                return path;
            }
        }

        private String getPath()
        {
           
            if (String.IsNullOrEmpty(url) || url.Equals(Constants.rootPath))
                return "";
            foreach (String apath in keys)
                if (new Regex(apath + "((/?$|/.*)|\\?.*)", RegexOptions.IgnoreCase).IsMatch(url))
                    return apath;
            
            return null;
        }
    }
}
