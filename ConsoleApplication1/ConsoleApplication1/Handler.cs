using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections;


namespace ConsoleApplication1
{
    abstract class Handler<Handle,Agent>
    {
        protected Handle defaultHandler;
        protected ICollection keys;
        protected Dictionary<String, Handle> mapper;
              
        protected Handler()
        {
            
            mapper = new Dictionary<String, Handle>();
            keys = mapper.Keys;
        }






        protected internal abstract void process(HttpListenerContext ctx, String path, String spath);
         public void dispatch(Agent agent)
        {
            PathQualifier pp = getPQ(agent);
            if (pp == null) return;
            process(pp.Context,pp.Path,pp.Spath);
             
        }

         protected abstract PathQualifier getPQ(Agent agent);

    }
}
