using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGit
{
    class Model
    {
        public Guid Id { get; set; }
        public string Environment { get; set; }
        public string Market { get; set; }
        public string MethodName { get; set; }

        public int Duration { get; set; }


    }
}
