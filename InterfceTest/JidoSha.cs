using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfceTest {
    internal class JidoSha : IJidoShaBikeShared {

        public void Hashiru() {
            Console.WriteLine("インターフェース版自動車が走る");
        }
    }

    internal class JidoShaWithAbst : AJidoShaBikeShared {

        public override void Hashiru() {
            Console.WriteLine("抽象クラス版自動車が走る");
        }
    }
}
