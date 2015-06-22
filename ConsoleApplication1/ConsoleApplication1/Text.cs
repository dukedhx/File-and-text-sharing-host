using System;
using System.Data.Linq.Mapping;

namespace ConsoleApplication1
{
    [Table(Name = "text")]
    class Text
    {
        public Text()
        {
            text = "";
            setup();
        }

        private void setup()
        {
            type = 0;
            timestamp = DateTime.Now;
        }

       public Text(String t)
        {
            text = t;
            setup();
        }

        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int id { get; set; }
        [Column]
        public DateTime timestamp { get; set; }
        [Column]
        public String text { get; set; }
        [Column]
        public int type { get; set; }
        [Column]
        public int? pid { get; set; }
        
    }
}
