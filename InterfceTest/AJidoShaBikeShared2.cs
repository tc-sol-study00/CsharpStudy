using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfceTest {
    internal abstract class AJidoShaBikeShared2 {
        public void Display(string displaydata) {
            Console.WriteLine("{0}:{1}", DateTime.Now, displaydata);
        }
    }
}
