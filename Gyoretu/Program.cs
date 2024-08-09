using System.Reflection.Metadata.Ecma335;

namespace Gyoretu {
    internal class Program {
        static void Main(string[] args) {

            int[,] aArray = new int[,] { { 1, 3, 5 }, { 3, 23, 23 }, { 5, 12, 23 }, { 21, 435, 234 }, { 12, 45, 234 }, { 45, 234, 456 }, { 12, 23, 123 } };
            int[,] bArray = new int[,] { { 3, 234, 212 }, { 435, 23, 23 }, { 34, 534, 12 }, { 23, 345, 234 }, { 45, 1235, 21 }, { 56, 12, 211 }, { 23, 43, 21 } };
            int[,] rArray = new int[7, 3];

            int row, col;

            for (row = 0; row < aArray.GetLength(0); row++) {
                for (col = 0; col < aArray.GetLength(1); col++) {
                    rArray[row, col] = aArray[row, col] + bArray[row, col];
                }
            }

            for (row = 0; row < aArray.GetLength(0); row++) {
                for (col = 0; col < aArray.GetLength(1); col++) {
                    Console.Write("{0,5} ", rArray[row, col]);
                }
                Console.Write("\n");
            }

            rArray = new int[7, 3];

            /*
             * こんな風に書ける
             */
            //行列足し算
            Enumerable.Range(0, aArray.GetLength(0)).SelectMany(row => Enumerable.Range(0, aArray.GetLength(1))
            .Select(col => new { row, col, val = aArray[row, col] + bArray[row, col] }))
                .ToList().ForEach(d => rArray[d.row, d.col] = d.val);

            //表示
            Enumerable.Range(0, aArray.GetLength(0)).SelectMany(row => Enumerable.Range(0, aArray.GetLength(1))
            .Select(col => new { row, col })).ToList().ForEach(x => { Console.Write("{0,5} ",rArray[x.row, x.col]); if (x.col >= aArray.GetLength(1)-1) Console.Write("\n"); });
        }
    }
}
