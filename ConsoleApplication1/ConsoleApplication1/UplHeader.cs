using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    internal class UplHeader
    {
        
        private String name;
        private int residue;
        private static readonly Encoding enc = Encoding.UTF8;      
        private static readonly Byte[] headerend = enc.GetBytes("\r\n\r\n");
        private static readonly int hesize = headerend.Length;
        internal UplHeader(Byte[] buff, int offset, String sept)
        {
            int? result = Util.IndexOf(buff,headerend,offset);
            if (result==null||result < 0) throw new Exception("Invalid header with unexpected ending");
            int index = (int)result;
             Match m = new Regex("("+sept+@"="")(?<name>.+)("")").Match(enc.GetString(buff.Skip(offset).Take(index-offset).ToArray()));
             if (!m.Success) throw new Exception("Invalid header with no name"); name = m.Groups["name"].Value;
             residue = index + hesize;
        }


        internal int Residue { get { return residue; } }
        internal String Name { get { return name; } }
       
    }
}
