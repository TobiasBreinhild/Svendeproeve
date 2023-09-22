using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvendeproeveProjekt
{
    internal class Vare
    {
        public Vare(string name, string status)
        {
            this.Name = name;
            this.Status = status;
        }

        public string Name { get; set; }
        public string Status { get; set; }
    }
}
