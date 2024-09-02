using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OilGsSimulationToObjectwithInterfaceNoHeri;
using OilGsSimulationToObjectwithInterfaceNoHeri.Data;

namespace OigGsSimulationToObjectwithInterface {
    public class GsStation {
        /*
         * 満タン
         */
        public double FuelCharge(IJidoSha argJidoSha) {
            var TankZanryoBeforeCharge = argJidoSha.TankAttr.TankZanryo;

            var requiredFuel = argJidoSha.TankAttr.TankFull - argJidoSha.TankAttr.TankZanryo;
            argJidoSha.TankAttr.TankZanryo += requiredFuel;
            Console.WriteLine("現在の残量={0:F2} {1:F2} 給油して {2:F2}リットル 満タンにしました", TankZanryoBeforeCharge, requiredFuel, argJidoSha.TankAttr.TankZanryo);

            return argJidoSha.TankAttr.TankZanryo;
        }

        /*
         * オイル交換
         */
        public bool OilCharge(OilChangeManage argOilChangeManage, CarAttribute argCarAttribute) {
            bool flgCharged = false;
            Console.Write("オイル交換しますか？");
            string? strKoukanWillness = Console.ReadLine();
            if (strKoukanWillness == "y" || strKoukanWillness == "Y") {
                argOilChangeManage.SouSoukouKyoriAtLastOilChange = argOilChangeManage.SouSoukouKyori;
                argOilChangeManage.SouSoukouKyoriAtNextOilChange = argOilChangeManage.SouSoukouKyoriAtLastOilChange + argOilChangeManage.OilChangeInterval;
                flgCharged = true;
                Console.WriteLine("オイル交換しました");
            }
            return flgCharged;
        }
    }
}
