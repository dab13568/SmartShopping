using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShopping
{
    public abstract class ListedVM<T>
    {
        public abstract List<T> SourceList { get; set; }
    }
}
