namespace FlowChart {
    internal class Program {
        static void Main(string[] args) {

            //1から10まで足し算するプログラム
            //合計値
            int gokei=0;

            for(int i = 1; i <= 100; i++) {
                //gokei = gokei + i;
                gokei += i;
                Console.WriteLine(gokei);
            }

            Console.WriteLine(gokei);

            gokei = 0;
            int[] tbl = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            for(int j = 0; j < tbl.Length; j++) {
                gokei += tbl[j];
            }
            Console.WriteLine(gokei);

            //配列の１番目　→　行方向
            //配列の２番目　→　カラム方向
            int[,] tbl2 = new int[,] { { 1, 2, 3 }, { 3, 4, 5 } };

            int result = tbl2[0, 0];


        }
    }
}
