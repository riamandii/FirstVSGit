using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<int> b = new List<int>();
            IEnumerable<object> d = b;
        }
    }
}
