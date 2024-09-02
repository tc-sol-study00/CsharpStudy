using OilGsSimulationToObjectwithInterfaceNoHeri.Data;
using System.Transactions;

namespace OigGsSimulationToObjectwithInterface {

    /// <summary>
    /// 自動車・オイル付き自動車インターフェースクラス対応につき、virtual記述追加
    /// 基底クラスはvirtualと書く
    /// </summary>
    public class JidoSha : IJidoSha {
        //メンバー

        public CarAttribute CarAttribute { get; }

        public TankAttr TankAttr { get; }

        //コンストラクタ 

        public JidoSha() {
        }
        public JidoSha(string CarName, double NenPi, double TankFull, double LimitPct) {
            this.CarAttribute = new CarAttribute();
            this.TankAttr = new TankAttr();

            this.CarAttribute = new CarAttribute();
            this.TankAttr = new TankAttr();
            this.CarAttribute.CarName = CarName;
            this.TankAttr.NenPi = NenPi;
            this.TankAttr.TankFull = TankFull;
            this.TankAttr.TankLimit = TankFull * LimitPct;
            this.TankAttr.TankZanryo = TankFull;
        }

        //メソッド
        /*
         * 走行
         */
        public double Soukou(int argSoukouKyori) {
            TankAttr.TankZanryo=(this.Soukou(argSoukouKyori, TankAttr.TankZanryo, TankAttr.NenPi));
            return (TankAttr.TankZanryo);
        }

        public double Soukou(int argSoukouKyori, double argTankZanryo, double argNenPi) {
            double retTankZanryo = argTankZanryo - (argSoukouKyori / argNenPi);
            Console.WriteLine("走行距離= {0:D5}", argSoukouKyori);
            return retTankZanryo;
        }
        /*
         * ガス残量チェック
         */

        public bool GsRemainCheck() {
            return GsRemainCheck(TankAttr.TankZanryo, TankAttr.TankLimit);
        }

        public bool GsRemainCheck(double argTankZanryo, double argTankLimit) {
            string displayStatus = "";
            bool checkStatus = argTankZanryo >= argTankLimit;

            if (checkStatus) {
                displayStatus = "OK";
            }
            else {
                displayStatus = "NG";
            }
            //上記のifたちと同意
            displayStatus = checkStatus ? "OK" : "NG";

            Console.WriteLine("残量チェック {0:F2} 結果={1}", argTankZanryo, displayStatus);

            return checkStatus;
        }
        /*
         * 家ーＧＳ間
         */
        public double RunBetweenHomeToGs(int argMovedDist) {
            double tankZanryo = TankAttr.TankZanryo - (argMovedDist / TankAttr.NenPi);

            TankAttr.TankZanryo = tankZanryo;

            return TankAttr.TankZanryo;
        }
        /*
         * ガス欠チェック
         */
        public bool GasKetsuCheck() {
            return GasKetsuCheck(TankAttr.TankZanryo);
        }
        public bool GasKetsuCheck(double argTankZanryo) {
            if (argTankZanryo <= 0) {
                Console.WriteLine("ガス欠です");
                return (true);
            }
            else {
                return (false);
            }
        }

    }
}
