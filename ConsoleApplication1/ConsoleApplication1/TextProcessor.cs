using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net; 

namespace ConsoleApplication1
{
    [Path("text")]
    class TextProcessor:Processor
    {
       

     
        public TextProcessor(HttpListenerContext ctx, String aUrlParts) : base(ctx, aUrlParts) { }

        public override void getDefault()
        {
           Text[] texts= new DBHelper().getTexts(0, 5);
           if (texts == null)
               new Response(res).NotFound("Error");
           else
           {
               StringBuilder sb = new StringBuilder("<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /></head><body>");
                   
                 sb.Append(String.Join("<hr>", (from text in texts select text.timestamp + " | <a href=\"del?id=" + text.id + "\">Del</a><br/><br/>" + Uri.UnescapeDataString(text.text)).ToArray<String>()));
               
               sb.Replace("+", "&nbsp;"); sb.Replace("%2B", "+"); sb.Replace("%5Cn", "<br/>"); sb.Replace("%5C", "\\");
               sb.Replace("%0D%0A", "<br/>"); sb.Replace("%3D", "="); sb.Replace("%2F", "/"); sb.Replace("%3A", ":"); sb.Replace("%3F", "?"); sb.Append("</body>");
               new Response(res).SendText(sb.ToString());
               
           }
        }



        [Path("add",true,Constants.post)]
        public void add()
        {
            String input = new System.IO.StreamReader(req.InputStream).ReadToEnd();
            if (input.StartsWith("text="))                           
                if( new DBHelper().add(new Text(input.Remove(0, 5))))
                    new Response(res).SendText("Added.");
                else new Response(res).SendText("Not Added.");            
            else new Response(res).NotAcceptable("Must have text!");
        }

        [Path("del")]
        public void del()
        {
            
            if (urlParts.StartsWith("?id="))
            {
                int id;if(Int32.TryParse(urlParts.Remove(0, 1).Split('=')[1],out id))                
                if(new DBHelper().del(id))
                    new Response(res).SendText("Removed.");
                else new Response(res).SendText("Not Removed.");
                else
                    new Response(res).NotAcceptable("ID must be numeral!");
            }
            else
                new Response(res).NotAcceptable("Must have id!");
           
        }   
        
    }
}
