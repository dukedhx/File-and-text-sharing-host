using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    static class  Util
    {
        static internal Boolean validateIE<T>(IEnumerable<T> ie)
        {
            return !(ie == null && ie.Count() > 0);
        }
    }
}
