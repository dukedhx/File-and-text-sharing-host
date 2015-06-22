using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication1
{
  public static class FileIO
    {
       
        public static Boolean setVisibility(String loc, Boolean hidden)
        {            
            try{
            FileAttributes fa = File.GetAttributes(loc);
            File.SetAttributes(loc, hidden ? (fa | FileAttributes.Hidden) : (fa & ~FileAttributes.Hidden));
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public static String getType(String path)
        {
            String type = null; Constants.Types.TryGetValue(Path.GetExtension(path),out type);
            return type;
        }

        public static Boolean delete(String path)
        {
            try
            {
                if (File.Exists(path)) { File.Delete(path); return true; }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }
            return false;
        }

        public static Boolean chkdir(String path,Boolean create)
        {
            if (!String.IsNullOrEmpty(path))
            {
                if (Directory.Exists(path)) return true;
                if (create)
                    try
                    {
                        Directory.CreateDirectory(path);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
            }
            return false;
        }

        public static Boolean chkdir(String path) { return chkdir(path, true); }
    }
}
