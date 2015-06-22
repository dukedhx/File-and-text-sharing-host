using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ConsoleApplication1
{
    class ProcDir
    {
        
        private MethodInfo method;
        private PathAttribute pa;
        public PathAttribute PaAttr { get { return pa; } }
        public ProcDir(PathAttribute apa, MethodInfo aMethod)
        {
            
            method = aMethod;
            pa = apa;
        }


        public void invoke(Processor proc)
        {
            method.Invoke(proc,null);
        }

     
    }
}
