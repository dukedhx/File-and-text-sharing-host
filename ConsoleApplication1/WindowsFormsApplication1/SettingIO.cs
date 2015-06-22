using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace WindowsFormsApplication1
{
    static class SettingIO
    {
        static readonly public String pathname = "path", addrname = "addr", smtp ="smtp",pop3="pop3";
        static readonly public String[] names = new String []{ pathname, addrname, smtp, pop3 };        
        static private String name;

        static public Boolean checkDB()
        {

            try
            {
                if (!File.Exists(Constants.dbfilename))                 
                      File.WriteAllBytes(Constants.dbfilename,WindowsFormsApplication1.Properties.Resources.Database1);                    
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
        static public String Name { get { if(String.IsNullOrEmpty(name))name=System.AppDomain.CurrentDomain.FriendlyName.Split(Constants.dot)[0] + ".ini"; return name;} }
        static public Dictionary<String,String> load()
        {
            try
            {
                if (File.Exists(Name))
                {
                    Dictionary<String, String> dict = new Dictionary<String, String>();
                    using(StreamReader sr = new StreamReader(Name)){
                    String line; 
                    var namelist = new List<String>(names);                  
                    while (!String.IsNullOrEmpty((line = sr.ReadLine()))&& namelist.Count>0)
                    {
                        String[] parts=line.Split(Constants.equal);
                        if (parts.Length > 1)
                        {
                            String vname = parts[0]; int id;
                            if ((id=namelist.IndexOf(vname)) > -1)
                            {
                                if (dict.ContainsKey(vname)) dict[vname] = parts[1];
                                else dict.Add(vname,parts[1]);
                                namelist.RemoveAt(id);
                            }
                        }
                    }
                }
                    return dict;
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        static public void save(String name, String value)
        {
            try
            {
                Dictionary<String, String> dict = load();
                if (dict.ContainsKey(name)) dict[name] = value;
                else dict.Add(name,value);
                save(dict);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static public void save(Dictionary<String, String> dict)
        {
            try
            {
                if(Util.validateIE(dict)&&!String.IsNullOrEmpty(name))
                File.WriteAllText(name,String.Join("\n",dict.Select(x=>x.Key+Constants.equal+x.Value).ToArray()));  
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
