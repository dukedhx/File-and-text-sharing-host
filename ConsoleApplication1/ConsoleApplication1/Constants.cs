using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public enum ReturnCode { ok=0, lv1Err=-1,lv2Err=-2,okWithLv1Err=1,okWithLv2Err=2}
    static class Constants
    {
      public  const string dbfilename = "Database1.sdf";
        private const String defaultbasedir = ".";//Uri.UnescapeDataString(System.IO.Path.GetDirectoryName((new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath));   
        public static String defaultBasedir { get { return defaultbasedir; } }
        private const String defaultfilepath="file";
        public static String defaultFilePath { get { if (String.IsNullOrEmpty(defaultFFP))defaultFFP = Util.getValidPath(new String[] { defaultbasedir, defaultfilepath }); return FileIO.chkdir(defaultFFP) ? defaultFFP : defaultbasedir; } }
        private static String defaultFFP = "rec";
        private const String defaultTextPath="textdat";
        public const Char rootpath = '/';
        public const String dirPart = "\\";
            public const Char dirpart ='\\';
        public const String rootPath = "/";        
        public const int writeSuccess = 1;
        public const int writeFailure = -1;
        public const int fileNotFound = -2;
        private static int defaultBuffer = 10000000;
        private static int? customBuffer;
        private static Boolean start = false;
        public static Boolean Start{get{return start;} }
        internal static readonly String texthtml = "text/html", textplain = "text/plain", download = "application/force-download", css = "text/css", js = "text/javascript", xml = "text/xml", jpg = "image/jpeg", png = "image/png", gif = "image/gif";
        public static int Buffer { get { return customBuffer ?? defaultBuffer; } }
        public static void setBuffer(int buffer) { if(!start)customBuffer = buffer; }
        public static void serverStop() { start=false;}
        public static void serverStart() {start=true;}
        public static int conncount = 0;
        public static void resetCount() { conncount=0;}
        public static String userPath = null;
        public static String filePath {get{ return String.IsNullOrEmpty(userPath)?defaultFilePath:userPath;}}
        internal const String post = "POST";
        internal const String get = "GET";
        internal const String getOnly = "Must be GET";
        internal const String postOnly = "Must be POST";
        public static readonly Dictionary<String, String> Types = new Dictionary<String, String> { { "html", texthtml }, { "htm", texthtml }, { "txt", textplain }, { "log", textplain }, { "ini", textplain }, { "css", css }, { "js", js }, { "xml", xml }, { "jpg", jpg }, { "gif", gif },{ "png", png } };
    }
}
