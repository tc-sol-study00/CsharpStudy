using OilGsSimulationToObjectwithInterfaceNoHeri.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OigGsSimulationToObjectwithInterface {
    /// <summary>
    /// 自動車クラスのインターフェース
    /// オイル付き自動車クラスと自動車クラスで共通部分をコールするため作成
    /// </summary>
    public interface IJidoSha {
        //メンバー（プロパティ）
        public CarAttribute CarAttribute { get; }

        public TankAttr TankAttr { get; }

        /*
         * 走行
         */
        public double Soukou(int argSoukouKyori);
        /*
         * ガス残量チェック
         */
        public bool GsRemainCheck();
        /*
         * 家ーＧＳ間
         */
        public double RunBetweenHomeToGs(int argMovedDist);
        /*
         * ガス欠チェック
         */
        public bool GasKetsuCheck();
    }
}
