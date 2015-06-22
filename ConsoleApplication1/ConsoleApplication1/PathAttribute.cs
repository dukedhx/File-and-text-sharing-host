using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    [AttributeUsage(AttributeTargets.Method|AttributeTargets.Class|AttributeTargets.Field)]
    public class PathAttribute:Attribute
    {
        private String verb;
        private String pathname;
        private Boolean noParams;
        public Boolean NoParams { get { return noParams; } }
        public String Pathname
        {
            get { return pathname; }           
        }

        public String Verb
        {
            get { return verb; }
        }
       
        public PathAttribute(String pathname_in)
        {
            init(pathname_in, false, Constants.get);
        }
        public PathAttribute(String pathname_in,Boolean nps)
        {
            init(pathname_in, nps,Constants.get);
        }

        public PathAttribute(String pathname_in, Boolean nps, String averb)
        {
            init(pathname_in, nps,averb);           
        }

        public PathAttribute(String pathname_in,  String averb)
        {
            init(pathname_in, false, averb);
        }

        private void init(String pathname_in,Boolean nps,String averb)
        {
            pathname = pathname_in.ToNormalizedUrl();
            noParams = nps;
            verb = averb;
        }
    }
}
