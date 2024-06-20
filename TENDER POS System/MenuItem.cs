using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TENDER_POS_System
{
    public class MenuItem
    {
        public int Item_ID { get; set; }
        public string Item_Name { get; set; }
        public string Category_ID { get; set; }
        public string Item_Description { get; set; }
        public decimal Item_Price { get; set; }
        public string Item_Image { get; set; }
    }
}
