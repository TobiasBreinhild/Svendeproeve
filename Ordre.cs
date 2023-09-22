using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvendeproeveProjekt
{
    internal class Ordre
    {
        public Ordre(int id, string itemname, string customername)
        {
            this.Id = id;
            this.Itemname = itemname;
            this.Customername = customername;
        }

        public int Id { get; set; }
        public string Itemname { get; set; }
        public string Customername { get; set; }
    }
}
