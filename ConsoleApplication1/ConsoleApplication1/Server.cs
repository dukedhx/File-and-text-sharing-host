using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
    


namespace ConsoleApplication1
{


    public class Server
    {


        public String filePath { get {return Constants.filePath;}}
       
        public  Boolean isStopped {get{return isstopped;}}
        private Boolean isstopped;
        private HttpListener listener;

        private void init()
        {
            isstopped = true;
            listener = null;
        }

        public Server(String path)
        {
            init();
            setPath(path);
        }

        public Server()
        {            
            init();
        }

        public Boolean setPath(String path)
        {
            if (FileIO.chkdir(path,false)) { Constants.userPath = path; return true; }
            return false;
        }

        public void Start(String port,Boolean block)
{
    
    
    Console.WriteLine("[Control] Server Starting");     
            try
            {
                listener = new HttpListener();
                listener.IgnoreWriteExceptions = true;  
                listener.Prefixes.Add("http://" + port + "/");
                
                    listener.Start();
                           
                   ThreadPool.QueueUserWorkItem((w) =>
                    {
                        Constants.serverStart();
                    while (Constants.Start)                    
                       listener.BeginGetContext(new AsyncCallback(Router.Instance.dispatch), listener).AsyncWaitHandle.WaitOne();
                    
                    });
                    isstopped = false;
                    Console.WriteLine("[Control] Server Started");
                    if (block)
                    {
                        while (Constants.Start) { Thread.Sleep(1); }
                        Stop(false);
                    }                
            }
             catch (Exception e)
            {
                Console.WriteLine("[Error] " + e.Message);
                Console.ReadLine();
                Stop(true);
            }  
}      

        public void Stop(Boolean force)
        {
            if (isstopped) return;
            Constants.serverStop();           
            listener.Stop();
            if (!force) while (Constants.conncount > 0) { Thread.Sleep(1); } else Constants.resetCount();
            listener.Abort();            
            Console.WriteLine("[Control] Server Terminated "+(force?"Forcefully":""));
            isstopped = true;
            
        }
        
    }
}
