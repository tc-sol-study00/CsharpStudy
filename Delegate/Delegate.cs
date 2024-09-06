using Delegate.UsingInterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate {
    internal static class Delegate {

        //delegate 戻り値の型 デリゲート名 (関数が引き受ける仮引数);
        //変数を設定しているのではなく、タイプを設定しているイメージ
        //delagateは、クラス内でかつメソッド外に書く

        delegate int Culc(int argData);

        static public void DelegateStudy() {

            Culc culc;
            Console.WriteLine("----delegate利用");
            //Newキーワードによる生成
            //他オブジェクト参照
            culc = new Culc(new Plus1().SharedMethod);
            Console.WriteLine($"{culc(1)}");

            //メソッドからデリゲートへの暗黙変換(C#2.0より)
            //new使わなくて良い
            //同クラス内参照
            culc = PlusCulc;
            Console.WriteLine($"{culc(1)}");

            //匿名メソッドによる生成
            culc = delegate (int argData) {
                return argData + 2;
            };
            Console.WriteLine($"{culc(1)}");

            //ラムダ式使用
            culc = argData => { return argData + 3; };
            Console.WriteLine($"{culc(1)}");
        }
        static public int PlusCulc(int argData) {
            return (argData + 1);
        }

        /// <summary>
        /// ジェネリックデリゲート利用(Func)
        /// </summary>
        static public void GenericDelegateStudy() {
            //これはタイプ宣言ではなく、Func<int, int>がタイプとなる
            //Func<int, int>がタイプの、領域を動的に確保している
            Func<int, int> culcLambda;

            Console.WriteLine("----ジェネリックdelegate利用");

            //Newキーワードによる生成
            //他オブジェクト参照
            culcLambda = new Func<int, int>(new Plus1().SharedMethod);
            Console.WriteLine($"{culcLambda(1)}");

            //メソッドからデリゲートへの暗黙変換(C#2.0より)
            //new使わなくて良い
            //同クラス内参照
            culcLambda = PlusCulc;
            Console.WriteLine($"{culcLambda(1)}");

            //匿名メソッドによる生成
            culcLambda = delegate (int argData) {
                return argData + 2;
            };
            Console.WriteLine($"{culcLambda(1)}");

            //ラムダ式使用
            culcLambda = argData => { return argData + 3; };
            Console.WriteLine($"{culcLambda(1)}");

        }
    }
}
