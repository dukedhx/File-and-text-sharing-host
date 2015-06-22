using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Exceptions
{
    [Serializable]
    class NoDefaultResolver:Exception
    {
        public NoDefaultResolver(String message) : base(message) { }

        public NoDefaultResolver(string format, params object[] args)
        : base(String.Format(format, args)) { }
    
    public NoDefaultResolver(string message, Exception innerException)
        : base(message, innerException) { }
    
    public NoDefaultResolver(string format, Exception innerException, params object[] args)
        : base(String.Format(format, args), innerException) { }
    }
}
