using System.Xml.Schema;

namespace CSharpStudy20240825 {
    internal class Program {

        int GlobgalStoreData;

        static void Main(string[] args) {       //ブロック

            int a;
            Console.WriteLine("Hello, World!");

            const int maxlen = 3;

            const float shohinzei = 1.08f;

            int[] dtt = new int[] { 1, 2, 3 };

            string dddd = "123123";

            string cccc = "123";
            cccc = "12313123";
 
            a = 2;

            string dt = "3";

            while (true) {

                int tempStoreData;

                tempStoreData = 1;

            }

            int ifd = 0;

            int[] datas = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            int[] datas2;

            foreach (int onedata in datas) {
                Console.WriteLine(onedata);
            }

            for (int i = 0; i < datas.Length; i++) {
                Console.WriteLine(datas[i]);
            }

            datas2 = datas;
            datas[0] = 10;
            Console.WriteLine(datas2[0]);

            const int maxlen2 = 10;
            int[] datalists = new int[maxlen2];

            int bbb = 0x_ff_ff_ff;
            bbb = 0b_11111111_11111111_11111111;

            ulong usg=123ul;

            double dv = 123e-100;  //123×10^-100   123×10^100

            double y = method1(1);

            method2("log1 処理開始");
            Console.WriteLine("log1 処理開始");
            Console.Write("log1 処理開始\n");
            Console.Write("log1 処理\"開始\n");

            Console.Write('\x0041');

            method2("log1 処理開始");

            double method1(double in1) {

                double a = in1 * 1.08;
                return a;
            }

            void method2(string in1) {
                Console.WriteLine(in1);
            }

        }
    }
}
