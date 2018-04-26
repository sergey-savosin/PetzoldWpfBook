using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petzold.ListSystemParameters
{
    public class SystemParam
    {
        public string Name
        {
            set; get;
        }

        public object Value
        {
            set; get;
        }

        public override string ToString()
        {
            return Name + "=" + Value;
        }
    }
}
