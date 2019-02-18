using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueSunSystems.Model
{
    public class EveSystem
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<Adm> adms { get; set; }

    }
}
