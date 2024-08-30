using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfceTest {
    internal class Bike : IJidoShaBikeShared {

        public void Hashiru() {
            Console.WriteLine("インターフェース版バイクが走る");
        }

    }
    internal class BikeWithAbst : AJidoShaBikeShared {

        public override void Hashiru() {
            Console.WriteLine("抽象クラス版バイクが走る");
        }

    }
}
