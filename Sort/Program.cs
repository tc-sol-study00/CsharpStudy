using System.Net.Http.Headers;

namespace Sort {

    /// <summary>
    /// プログラム名：バブルソート演習
    /// 担当者：戸尾
    /// 作成日付：2024/08/17
    /// </summary>
    internal class Program {
        /// <summary>
        /// メインプログラム
        /// </summary>
        /// <param name="args">引数不要</param>
        /// 
        static readonly int[] list = new int[] { 4, 1, 3, 5, 2, 4, 5, 67, 89, 12, -2, 34, 12, -5 };

        static Action<string> printm = (status) 
            => { Console.Write("{0,-10}:", status); for (int i = 0; i < list.Length; i++) Console.Write("{0,3} ", list[i]); Console.Write("\n"); };

        static void Main(string[] args) {

            /*
             * バブルソート本処理
             * 　引数用途
             *   
             * setPointer:データを追い込む先のポインタ（小さい数字を左側に追い込む）
             * swapPointer　：スワップ候補（右側）
             * swapPointer-1：スワップ候補（左側）
             */

            for (int setPointer=0; setPointer<list.Length-1;setPointer++) {                 //データ追い込み用ループ（左側に詰めながら追い込む）
                bool everswapped = false;                                                   //スワップを一回でも行えばtrue。一回もスワップしなかった場合false
                for(int swapPointer=list.Length-1;swapPointer>setPointer;swapPointer--) {   //データスワップ用ループ
                    if (list[swapPointer-1] > list[swapPointer]) {                          //スワップ候補（左側）よりスワップ候補（右側）が小さい場合
                        (list[swapPointer-1], list[swapPointer]) = (list[swapPointer], list[swapPointer-1]);    //スワップ候補（左側）とスワップ候補（右側）をスワップ
                        everswapped = true;                                                 //スワップを一回でも行った
                    }
                }
                if (!everswapped) break;                                                    //データ追い込み時に、一回でもスワップしなかった場合は、終了
                printm("In Process");
            }

            printm("Finished");

            /*
             * --------------------------------------------------------
             * 終わり
             */

            /*
             *  Appendix
             */

            //ソートは今では一発で出来る
            foreach (var item in list.Order())
            {
                Console.Write("{0,3} ", item);
            };
            Console.Write("\n");

            //もっと短く書ける
            list.Order().ToList().ForEach(x => Console.Write("{0,3} ",x));
        }
    }
}
