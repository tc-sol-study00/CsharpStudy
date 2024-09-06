using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate.UsingInterFace {
    internal class Plus1 : IPlusCalcs {
        public int SharedMethod(int argData) {
            return argData + 1;
        }

    }
    internal class Plus2 : IPlusCalcs {
        public int SharedMethod(int argData) {
            return argData + 2;
        }

    }

    internal static class StudyToInterfaceInsteadOfDelegate {
        public static void Method1() {
            IPlusCalcs plus;

            Console.WriteLine("----インターフェースでdelegateっぽいことをやる");
            //Plus1（＋１をするメソッド）セット
            plus = new Plus1();
            Console.WriteLine($"{plus.SharedMethod(1)}");

            //Plus1（＋２をするメソッド）セット
            plus = new Plus2();
            Console.WriteLine($"{plus.SharedMethod(1)}");//使い方は上記と違うが、plusの中の中身が違う
        }
    }
}
