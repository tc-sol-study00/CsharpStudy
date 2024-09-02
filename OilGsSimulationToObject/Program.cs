using OilGsSimulationToObject;
using System.Reflection.Metadata.Ecma335;

namespace OilGsSimulationToObject {
    /// <summary>
    /// GSシミュレーション
    /// </summary>
    class Programs {
        private static List<JidoSha> JidoShas = new List<JidoSha>();    //共変性を利用し、基底・派生クラス両方使えるようにする
        private static GsStation gsStation = new GsStation();
        static void Main() {

            //２つのオブジェクトが扱える
            JidoShas.Add(new JidoSha("A車", 20, 60, 0.2));
            JidoShas.Add(new JidoShawithOil("B車", 20, 60, 0.2, 0));

            foreach (JidoSha aCar in JidoShas) {
                /*
                 * 走行距離入力で、"E"あるいは"e"が入力されるまで処理を継続する
                 * ガス欠の場合は、処理終了
                 */
                Console.WriteLine("{0}のシミュレーション",aCar.CarName);
                
                while (true) {
                    /*
                     * 自宅
                     */

                    // 走行

                    //走行距離入力
                    Console.Write("走行距離=");
                    string? strSoKouKyori = Console.ReadLine();
                    if (strSoKouKyori == "e" || strSoKouKyori == "E") break;
                    int? souKouKyori = Int32.Parse(strSoKouKyori);

                    //走行・移動系で各自動車クラスでメソッドの呼び方が違うための対応
                    Action<int>? souKou = null;
                    Action<double>? runBetweenHomeToGs = null;
                    Func<bool>? oilCheck = null;

                    //デリゲートとラムダ式で２つのオブジェクト用のメソッドを切り分ける
                    if (aCar is JidoShawithOil jidoShaWithOil) {
                        souKou = kyori => jidoShaWithOil.Soukou(kyori); 
                        runBetweenHomeToGs = kyori => jidoShaWithOil.RunBetweenHomeToGs(kyori);
                        oilCheck = () => jidoShaWithOil.OilCheck();
                    }
                    else if (aCar is JidoSha jidoSha) {
                        souKou = kyori => jidoSha.Soukou(kyori);
                        runBetweenHomeToGs = kyori => jidoSha.RunBetweenHomeToGs(kyori);
                        oilCheck = () => true;
                    }

                    souKou(souKouKyori??0);    //走行距離インプット・タンク残量計算
                    if (aCar.GasKetsuCheck()) break;   //ガス欠チェック
                    /*
                     * チェック
                     */
                    //残量チェック　（OK=true/NG=false)
                    bool checkStatus = aCar.GsRemainCheck();

                    //オイル交換時期チェック（OK=true/NG=false)
                    //自動車クラスの場合は、オイル交換時期は問題なし扱い
                    bool oilStatus = oilCheck();

                    //上記２つのチェックがＯＫであれば、次の走行へ
                    if (checkStatus && oilStatus) continue;

                    /*
                     * ＧＳへ行く？
                     */
                    bool flgDisplayed = false;
                    if (!oilStatus) {
                        Console.WriteLine("オイル交換必要です。走行距離={0:F2}, 次回交換時期={1:F2} ", ((JidoShawithOil)aCar).SouSoukouKyori, ((JidoShawithOil)aCar).SouSoukouKyoriAtNextOilChange);
                        flgDisplayed = true;
                    }
                    Console.Write("給油しますか？(y/n)=");
                    var strFuelWillness = Console.ReadLine();
                    if (strFuelWillness == "y") {   //GSに行くと指示された
                        //ＧＳへ
                        runBetweenHomeToGs(5);
                        if (aCar.GasKetsuCheck()) break;

                        /*
                         * GS
                         */
                        //満タン
                        //必要な供給量
                        gsStation.FuelCharge(aCar);

                        if (aCar is JidoShawithOil jidoShaWithOil2) {
                            //オイルチェック
                            if (!oilCheck()) {
                                if (!flgDisplayed) Console.WriteLine("オイル交換必要です。");
                                //オイル交換
                                gsStation.OilCharge(jidoShaWithOil2);
                            }
                        }
                        //家に戻る
                        runBetweenHomeToGs(5);
                        if (aCar.GasKetsuCheck()) break;
                    }

                }

            }
        }
    }
}