using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
namespace ConsoleApplication1
{
    class Resolver : Handler<ProcDir, ContextAgent>
    {

        private Type type;
        internal Resolver(Type atype)
            : base()
        {
            type = atype;
            foreach (var method in type.GetMethods())
                foreach (Attribute attr in method.GetCustomAttributes(true))
                    if (attr is PathAttribute) { PathAttribute pa = attr as PathAttribute; ProcDir pd = new ProcDir(pa, method); mapper.Add(pa.Pathname, pd); }
         
        }



       
        protected override PathQualifier getPQ(ContextAgent agent)
        {
            return new PathQualifier(agent.Context, agent.Url, keys);
        }

        protected internal override void process(HttpListenerContext ctx, String path, String spath)
        {
            ThreadPool.QueueUserWorkItem((w) =>
                   {
                       using (Processor processor =  Activator.CreateInstance(type, ctx, spath) as Processor)
                       {
                           String verb = ctx.Request.HttpMethod;
                           switch (path)
                           {

                               case "": if (valVerb(processor, verb, Constants.get) == ReturnCode.ok) { String url = ctx.Request.RawUrl;
            if (!url.EndsWith(Constants.rootPath)) ctx.Response.Redirect(url+Constants.rootPath); else processor.getDefault(); } break;
                               case null: if (valVerb(processor, verb, Constants.get) == ReturnCode.ok) processor.getRaw(); break;
                               default: ProcDir pd = mapper[path]; PathAttribute pa = pd.PaAttr; if (valVerb(processor, verb, pa.Verb) == ReturnCode.ok) { if (!String.IsNullOrEmpty(spath)) if (pa.NoParams) { processor.notFound(); return; } pd.invoke(processor); } break;
                           }
                       }

                   });
        }


        private ReturnCode valVerb(Processor proc,String hverb,String rverb)
        {
            if (!hverb.Equals(rverb))
            {
                proc.notAcceptable();
                return ReturnCode.lv1Err;
            }           
                return ReturnCode.ok;
        }
        }
}
