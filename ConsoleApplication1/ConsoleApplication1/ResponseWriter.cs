using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication1
{
    public class ResponseWriter
    {
        private Stream stream;
        private int wf = Constants.writeFailure;
        private int ws = Constants.writeSuccess;
        private int nf = Constants.fileNotFound;
        private int buffer = Constants.Buffer;
        public ResponseWriter(Stream aStream)
        {
            stream = aStream;
        }

        private int WriteReponse(Byte[] bytes)
        {
            return WriteReponse(bytes,bytes.Length);
        }

        private int WriteReponse(Byte[] bytes, int length)
        {
            try
            {
                
                long current=0;
                long rest = length;
                while (current<length) {
                    if (stream.CanWrite) stream.Write(bytes, 0, buffer > rest ? (int)rest : buffer); else throw new Exception("Write Failure"); 
                    current += buffer;
                    rest -= buffer;                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Write] "+ex.Message);
                return wf;
            }
            return ws;
        }

        public int WriteFile(String path)
        {
            Console.WriteLine(path);            
            if (!File.Exists(path)) return nf;            
            try
            {

                using( FileStream fs = new FileStream(path,FileMode.Open,FileAccess.Read,FileShare.Read)){
                   
                     byte[] bytes = new byte[buffer]; 
                     long current = 0;                   
                     while(fs.Length>current)
                     {
                     int read=fs.Read(bytes, 0, buffer);
                     if (WriteReponse(bytes,read)==wf)return wf;
                     current += buffer;                     
                     }
                    
            }
            }
            catch(Exception ex)
            {
                Console.WriteLine("[WriteFile] "+ex.Message);
                return wf;
            }
            return ws;
        }

        public int WriteString(String text)
        {            
             return WriteReponse(Encoding.UTF8.GetBytes(text));            
        }

        public static String getFileName(String path)
        {
            return Path.GetFileName(path);
        }

       
    }
}
