using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Reflection;
using ConsoleApplication1.Exceptions;

namespace ConsoleApplication1
{
    class Router : Handler<Resolver,IAsyncResult>
    {            
        
        public static Router Instance { get { if (instance == null)instance = new Router(); return instance; } }
        private static Router instance;
        private Router()
            : base()
        {
            String path;
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
                if (typeof(Processor).IsAssignableFrom(type))
                    foreach (Attribute attr in type.GetCustomAttributes(true))
                        if (attr is PathAttribute)
                            if ((path = (attr as PathAttribute).Pathname).Equals(Constants.rootPath))
                                defaultHandler = new Resolver(type);
                            else
                                mapper.Add(path, new Resolver(type));
            if (defaultHandler == null)
                throw new NoDefaultResolver("Default Resolver Not Found");
        }     
                
        protected override PathQualifier getPQ(IAsyncResult agent)
        {
            
            HttpListener lsnr = (HttpListener)agent.AsyncState;
            return lsnr.IsListening&&Constants.Start?new PathQualifier(lsnr.EndGetContext(agent),keys):null;
        }

        protected internal override void process(HttpListenerContext ctx, string path, string spath)
        {
            Console.WriteLine(ctx.Request.RawUrl);
            if (String.IsNullOrEmpty(path))
                defaultHandler.process(ctx, path, null);
            else
                mapper[path].dispatch(new ContextAgent(spath, ctx));
        }
    }
}
