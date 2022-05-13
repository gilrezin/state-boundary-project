using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForHugProject
{
    class Ethnicity
    {
        public string id;
        public string name;
        public Ethnicity(string name)
        {
            id = Guid.NewGuid().ToString();
            this.name = name;
        }
    }
}
