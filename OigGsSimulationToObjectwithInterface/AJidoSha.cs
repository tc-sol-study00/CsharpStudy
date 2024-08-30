using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OigGsSimulationToObjectwithInterface {
    /// <summary>
    /// 抽象クラス（説明用なので処理には関係ない）
    /// </summary>
    public abstract class AJidoSha {///
        //メンバー（プロパティ）
        public string CarName { get; set; }
        public double NenPi { get; set; }
        public double TankFull { get; set; }
        public double TankLimit { get; set; }
        public double TankZanryo { get; set; }
        /*
         * 走行
         */
        public abstract (double, double) Soukou(int argSoukouKyori);
        /*
         * ガス残量チェック
         */
        public abstract bool GsRemainCheck();
        /*
         * 家ーＧＳ間
         */
        public abstract double RunBetweenHomeToGs(double argMovedDist);
        /*
         * ガス欠チェック
         */
        public bool GasKetsuCheck() {
            if (TankZanryo <= 0) {
                Console.WriteLine("ガス欠です");
                return (true);
            }
            else {
                return (false);
            }
        }
    }
}

