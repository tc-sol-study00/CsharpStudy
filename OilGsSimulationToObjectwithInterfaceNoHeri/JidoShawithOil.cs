using OilGsSimulationToObjectwithInterfaceNoHeri;
using OilGsSimulationToObjectwithInterfaceNoHeri.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace OigGsSimulationToObjectwithInterface {

    /// <summary>
    /// 自動車・オイル付き自動車インターフェースクラス対応につき、orverride記述追加
    /// 基底クラスはorverrideと書く
    /// </summary>
    public class JidoShawithOil : IJidoSha {

        const int OILCHANGERETRIVAL = 5000; 

        public CarAttribute CarAttribute { get; set; }
        public TankAttr TankAttr { get; set; }

        public OilChangeManage OilChangeManage { get; }

        private JidoSha JidoSha;

        public JidoShawithOil(string CarName, double NenPi, double TankFull, double LimitPct, double argSouSoukouKyori) {

            JidoSha = new JidoSha(); //関数のみ使うため

            this.CarAttribute = new CarAttribute() {
                CarName = CarName
            };

            this.TankAttr = new TankAttr() {
                NenPi = NenPi,
                TankFull = TankFull,
                TankLimit = TankFull * LimitPct,
                TankZanryo = TankFull
            };

            this.OilChangeManage = new OilChangeManage() {
                SouSoukouKyori = argSouSoukouKyori,                                 //総走行距離（新車納品時は0とする）
                OilChangeInterval = OILCHANGERETRIVAL,                               
                SouSoukouKyoriAtLastOilChange = argSouSoukouKyori,                       //前回のオイル交換時の総走行距離
                SouSoukouKyoriAtNextOilChange = argSouSoukouKyori + OILCHANGERETRIVAL    //次回のオイル交換時の総走行距離
            };
        }                                                                                       

        //走る（オーバライド）
        public double Soukou(int argSouKouKyori) {
            TankAttr.TankZanryo = JidoSha.Soukou(argSouKouKyori, TankAttr.TankZanryo, TankAttr.NenPi);
            OilChangeManage.SouSoukouKyori += (double)argSouKouKyori;
            return TankAttr.TankZanryo;
        }
        //走る（家ーＧＳ間）
        public double RunBetweenHomeToGs(int argDist) {
            TankAttr.TankZanryo = JidoSha.Soukou(argDist, TankAttr.TankZanryo, TankAttr.TankLimit);
            OilChangeManage.SouSoukouKyori += argDist;
            return TankAttr.TankZanryo;
        }
        //オイルチェック(サブクラス用）
        public bool OilCheck() {
            return (OilChangeManage.SouSoukouKyori < OilChangeManage.SouSoukouKyoriAtNextOilChange);
        }

        /*
 * ガス残量チェック
 */
        public bool GsRemainCheck() {
            return (JidoSha.GsRemainCheck(TankAttr.TankZanryo, TankAttr.TankLimit));
        }
        /*
         * 家ーＧＳ間
         */

        public bool GasKetsuCheck() {
            return JidoSha.GasKetsuCheck(TankAttr.TankZanryo);
        }
    }
}

