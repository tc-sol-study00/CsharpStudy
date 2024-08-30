using OilGsSimulationToObject;

namespace OigGsSimulationToObjectwithInterface {
    class Programs {

        //自動車クラスのインターフェースをタイプにする
        private static List<IJidoSha> JidoShas = new List<IJidoSha>();    //共変性を利用し、基底・派生クラス両方使えるようにする
       
        private static GsStation gsStation = new GsStation();
        static void Main() {

            //２つのオブジェクトが扱える
            JidoShas.Add(new JidoSha("A車", 20, 60, 0.2));
            JidoShas.Add(new JidoShawithOil("B車", 20, 60, 0.2, 0));

            foreach (IJidoSha aCar in JidoShas) {
                /*
                 * 走行距離入力で、"E"あるいは"e"が入力されるまで処理を継続する
                 * ガス欠の場合は、処理終了
                 */

                Console.WriteLine("{0}のシミュレーション", aCar.CarName);

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

                    //インターフェース利用なので、aCarのオブジェクトでコールするメソッドを切り分ける
                    //aCarが、JidoShaであれば、JidoSha.Sokouをコール、JidoShawithOilであれば、JidoShawithOil.Sokouをコール

                    aCar.Soukou(souKouKyori ?? 0);    //走行距離インプット・タンク残量計算
                    if (aCar.GasKetsuCheck()) break;   //ガス欠チェック
                    /*
                     * チェック
                     */
                    //残量チェック　（OK=true/NG=false)
                    bool checkStatus = aCar.GsRemainCheck();

                    //オイル交換時期チェック（OK=true/NG=false)
                    //自動車クラスの場合は、オイル交換時期は問題なし扱い
                    bool oilStatus;
                    if (aCar is JidoShawithOil jidoShaWithOil) {
                        oilStatus = jidoShaWithOil.OilCheck();
                    }
                    else {
                        oilStatus = true;
                    }

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
                        aCar.RunBetweenHomeToGs(5); //インターフェース利用で、各オブジェクトのRunBetweenHomeToGsをコール
                        if (aCar.GasKetsuCheck()) break;    //インターフェース利用

                        /*
                         * GS
                         */
                        //満タン
                        //必要な供給量
                        gsStation.FuelCharge(aCar);

                        if (aCar is JidoShawithOil jidoShaWithOil2) {
                            //オイルチェック
                            if (!jidoShaWithOil2.OilCheck()) {
                                if (!flgDisplayed) Console.WriteLine("オイル交換必要です。");
                                //オイル交換
                                gsStation.OilCharge(jidoShaWithOil2);
                            }
                        }
                        //家に戻る
                        aCar.RunBetweenHomeToGs(5);
                        if (aCar.GasKetsuCheck()) break;    //インターフェース利用
                    }

                }

            }
        }
    }
}
