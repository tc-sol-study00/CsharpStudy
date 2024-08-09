using System.ComponentModel.Design;

namespace Merge {
    /// <summary>
    /// 
    /// </summary>
    internal class Program {
        static void Main(string[] args) {
            /*
             * 初期化
             */
            int[] aArray = new int[] { 6, 8, 9, 9, 10, 11 };
            int[] bArray = new int[] { 1, 5, 6, 7, 10, 12,13,20 };
            int[] rArray = new int[20];

            int ap = 0, bp = 0, rp=0;
            /*
             * マージ処理
             */
            while (true) {
                if (ap < aArray.Length) {           //AポイントがA配列の範囲内の場合
                    if (bp < bArray.Length) { //ap < aArray.Length && bp < bArray.Length 
                        if (aArray[ap] < bArray[bp]) {  //A配列の内容が小さい場合
                            rArray[rp] = aArray[ap++];  
                        }
                        else {                          //B配列の内容が小さいあるいは等しい場合
                            rArray[rp] = bArray[bp++];
                        }
                    }
                    else {  //ap < aArray.Length && bp >= bArray.Length →A配列が余った時
                        rArray[rp] = aArray[ap++];
                    }
                }
                else { //ap >= aArray.Length　→//AポイントがA配列の範囲外の場合
                    if (bp < bArray.Length) { // ap >= aArray.Length && bp < bArray.Length　→B配列が余った時 　
                        rArray[rp] = bArray[bp++];
                    }
                    else { //ap >= aArray.Length && bp >= bArray.Length
                        //終了
                        break;
                    }
                }
                rp++;   
            }
            /*
             * 結果表示
             */
            for (int i = 0; i < rp; i++) {
                Console.WriteLine(rArray[i]);
            }

            /*
             * 実は、一行で書ける
             */
            aArray.Concat(bArray).Order().ToList().ForEach(x => Console.WriteLine(x));
        }
    }
}
