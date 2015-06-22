using System.Data.Linq;

using System.Linq;
using System.IO;
using System;using System.Collections;
using System.Data.Linq.Mapping;
using System.Collections.Generic;


namespace ConsoleApplication1
{
    

    class DBHelper
    {
        readonly static Object alock = new Object();
        public Text[] getTexts(int p,int cpp)
        {
            Text[] results=null;
            try
            {
                using (DBFile dbf = new DBFile())

                    if (dbf.Texts != null)

                        results = (from text in dbf.Texts select text).Skip(cpp*p).Take(cpp).ToArray<Text>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return results;
        }
      
        public Boolean add(Text text)
{
            Boolean rvalue=false;
             try
            {
    using (DBFile dbf = new DBFile())

        if (dbf.Texts != null)
        {
            dbf.Texts.InsertOnSubmit((text));
     dbf.submit(); rvalue = true;
     }
            }
             catch (Exception ex)
             {
                 Console.WriteLine(ex.Message);
             }
            return rvalue;
}

        public Boolean del(int id)
        {
            Boolean rvalue = false;
          try
            {
            using (DBFile dbf = new DBFile())            
                if (dbf.Texts != null)                
                    lock (alock)
                    {
                        Text text = dbf.Texts.FirstOrDefault(x => x.id == id);
                        if (text != null)
                        {
                            dbf.Texts.DeleteOnSubmit(text);
                            dbf.submit(); rvalue = true;
                        }
                    }                
            }
          catch (Exception ex)
          {
              Console.WriteLine(ex.Message);
          }            
            return rvalue;
        }


        class DBFile:IDisposable
        {            
            
            [Database]
        class DBContext : DataContext
        {

            public DBContext() : base("Data Source=|DataDirectory|\\"+Constants.dbfilename) { }

            public Table<Text> Texts=null;

        }            
            public void submit()
            {               
                if(ctx!=null&&!disposed)ctx.SubmitChanges();
            }
            Boolean disposed;
            FileStream fs;
            DBContext ctx;
         
            public Table<Text> Texts { get { return disposed||ctx==null?null:ctx.Texts; } }
            public DBFile()
            {
                disposed = false;
                try
                {
                    if (File.Exists(Constants.dbfilename))
                    {                        
                        fs = File.Open(Constants.dbfilename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        ctx = new DBContext();                        
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }             
            }            
            
         ~DBFile()
         {     
            if(!disposed)
             Dispose();   
         }

         public void Dispose() 
         {
             try
             {
                 if(fs!=null)
                 fs.Close();
                 if (ctx != null) ctx.Dispose();
             }
             catch (ObjectDisposedException ex)
             {
                 Console.WriteLine("[Disposed]"+ex.Message);
             }            
             disposed = true;
         }
        }
    }
}
