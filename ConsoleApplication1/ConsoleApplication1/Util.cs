using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Net;
//using System.Net.Sockets;
//using System.Net.NetworkInformation;
//using Microsoft.Win32;


namespace ConsoleApplication1
{
    public static class Util

    {

      

        static public String getFromDict(String key, Dictionary<String, String> dict)
        {
            String value;
            return dict.TryGetValue(key, out value) ? value : "";
        }
        public static Boolean compareEnums<T>(IEnumerable<T> a, IEnumerable<T> b)
        {
            int size = a.Count();
            if (size != b.Count()) return false;
            for (int i = 0; i < size; i++)
                if (!a.ElementAt(i).Equals( b.ElementAt(i))) return false;
            return true;
        }

        static public int? IndexOf(byte[] arrayToSearchThrough, byte[] patternToFind)
    {
        return IndexOf(arrayToSearchThrough, patternToFind, 0);
    }
        static public int? IndexOf(byte[] arrayToSearchThrough, byte[] patternToFind, int i)
        {

            int length = patternToFind.Length;
            for (; i < arrayToSearchThrough.Length - length; i++)
            {
                Boolean found = true;
                int j = 0;
                for (j = 0; j < length; j++)

                    if (arrayToSearchThrough[i + j] != patternToFind[j])
                    {
                        found = false;
                        break;
                    }

                if (found)
                    return i;                
            }
            return null;
        }

        static public String addPrefix(this String path)
        {
            return String.IsNullOrEmpty(path) ? "" : path.prepend("/");
        }

        static public String prepend(this String self, String header)
        {
            return self.prepend(header, false);
        }

        static public String prepend(this String self, String header, Boolean dedup)
        {
            return String.IsNullOrEmpty(self) ? header : String.IsNullOrEmpty(header)||(dedup && self.StartsWith(header)) ? self : header.append(self);
        }

        static public String append(this String self, String[] appx)
        {            
            return self.append(String.Concat(appx),false);
        }

        static public String append(this String self, String appx)
        {
            return self.append(appx, false);
        }

        static public String append(this String self, String appx, Boolean dedup)
        {
            return String.IsNullOrEmpty(self) ? appx : dedup && self.EndsWith(appx) ? self : new StringBuilder(self).Append(appx).ToString();
        }

        static public String ToNormalizedPath(this String path)
        {
            return String.IsNullOrEmpty(path) ? "" : path.Replace(Constants.rootPath, Constants.dirPart);
        }



        static public String getValidPath(String[] parts)
        {

            return parts == null || parts.Length == 0 ? "" : Uri.UnescapeDataString(String.Concat(parts.Select((v, i) => String.IsNullOrEmpty(v) || i == parts.Length - 1 ? v.RemoveFirstIfExists(Constants.dirpart) : v.RemoveFirstIfExists(Constants.dirpart).append(Constants.dirPart, true)).ToArray()));
        }

        static public String ToNormalizedUrl(this String value)
        {
            return String.IsNullOrEmpty(value) ? "" : value.Replace(Constants.dirPart, Constants.rootPath).prepend(Constants.rootPath, true).RemoveLastIfExists(Constants.rootpath);
        }

        static public String RemoveLastIfExists(this String value, char target)
        {            
            int index = value.Length - 1;
            return index>0&&value[index] == target ? value.Remove(index) : value;
        }

        static public String RemoveFirstIfExists(this String value, char target)
        {            
            return !String.IsNullOrEmpty(value)&&value[0] == target ? value.Remove(0,1) : value;
        }

        static public String[] splitUrl(this String rawUrl)
        {
            return Uri.UnescapeDataString(rawUrl).Split(new String[] { Constants.rootPath }, StringSplitOptions.RemoveEmptyEntries);
        }

        static public String checkNullOrEmpty(String value)
        {
            return String.IsNullOrEmpty(value) ? "" : value;
        }

        static public String getTimeStamp()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssffff");
        }

        static public String getTimeSimpleStamp()
        {
            return DateTime.Now.ToString("ssffff");
        }
    }
}

