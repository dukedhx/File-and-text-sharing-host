using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace ConsoleApplication1
{
   
    internal class UploadSaver
    {
        private Stream input;

        private static Object lockObj = new Object();
        private static readonly Encoding enc = Encoding.UTF8;        
        private Byte[] ba;
        private int basize;
        private static readonly String attrname = "filename";
        private static readonly Byte[] end = enc.GetBytes("--\r\n");
        private static readonly int endsize = end.Length;
        internal UploadSaver(String path, String Boundary)
        {
            init(Boundary);
            input = File.Open(path, FileMode.Open);
        }

        internal UploadSaver(Stream Input, String Boundary)
        {
            init(Boundary);
            input = Input;
        }

        private void init(String Boundary)
        {   
            ba = enc.GetBytes(Boundary.prepend("\r\n--", false));
            basize = ba.Length;
        }


        internal ReturnCode saveFiles()
        {
            try
            {
                int bsize = Constants.Buffer,read = 0,current = 0;
                Byte[] buffer=null;              
                Boolean reading = true;
                UplHeader uplh = null;
                FileStream fs = null;
                try
                {
                    while (true)
                    {
                        if (reading)
                        {
                            Byte[] temp = new Byte[bsize];
                            int tsize = 0;
                            if (!(input.CanRead && (tsize = input.Read(temp, 0, bsize)) > 0)) reading = false;
                            if (reading)
                                if (current > 0)
                                {
                                    int residue=read-current;
                                    buffer = buffer.Skip(current).Take(residue).Concat(temp).ToArray();
                                    current = 0;
                                    read = tsize+residue;
                                }
                                else { buffer = temp; read = tsize; }
                        }
                        if (uplh==null)
                        {
                            uplh = new UplHeader(buffer, current, attrname);
                            current = uplh.Residue;
                            lock (lockObj)
                                fs = getTmpFile();                            
                        }
                        if (current < 1 && !reading) throw new Exception("Unexpected end of input stream.");
                        current=writeData(buffer,current,read,fs);
                        if (current > 0)
                        {                            
                            String tmp = fs.Name;
                            fs.Close();
                            String fn=uplh.Name;
                            uplh = null;
                            FileIO.setVisibility(tmp, false);
                            lock (lockObj)
                            {
                                while (File.Exists(fn = Util.getValidPath(new String[] { Constants.filePath, fn }))) { fn = Path.GetFileNameWithoutExtension(fn).append(new String[] { Util.getTimeSimpleStamp(), Path.GetExtension(fn) }); }
                                File.Move(tmp, fn);
                            }
                            current += basize;
                            if (read - current == endsize) if (Util.compareEnums(buffer.Skip(current).Take(endsize), end)) break;                            
                        }
                    }
                  
                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine(ex.Message);
                    fs.Close();
                    return ReturnCode.lv2Err;
                }
                input.Close();
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); return ReturnCode.lv1Err; }
          
            return ReturnCode.ok;
        }

        private int writeData(Byte[] buffer, int offset,  int read,Stream stream)
        {
            int result;
            if (offset < 0) { if (Util.compareEnums(buffer.Take(-offset), ba.Skip(basize + offset))) return basize + offset; else stream.Write(ba,-offset,basize+offset); }
            
            int? index=Util.IndexOf(buffer,ba,offset);
            if (index > -1) result = (int)index; 
            else
            {
                int temp = basize-1;
                for (int i = read-basize+1; i <read; i++)
                    if (Util.compareEnums(buffer.Skip(i).Take(temp), ba.Take(temp))) { result = -temp ; break; } else  temp--;
                result = 0;
            }
                   stream.Write(buffer,offset,result==0?read-offset:(result>0?result-offset:read-offset+result));
            return result;
        }

      

        private FileStream getTmpFile()
        {
           
            String path =  Util.getValidPath(new String[] { Constants.filePath,"\\tmp"});
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
                     String fn;
            while(File.Exists(fn = Util.getValidPath(new String[] { path, Util.getTimeStamp() }))){  }
            return File.Create(fn);           
        }



       


      
    }
}
