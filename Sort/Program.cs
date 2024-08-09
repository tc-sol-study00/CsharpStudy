using System.Net.Http.Headers;

namespace Sort {

    /// <summary>
    /// 
    /// </summary>
    internal class Program {
        static void Main(string[] args) {

            int[] list = new int[] { 4, 1, 3, 5, 2,4,5,67,89,12,-2,34,12,-5 };

            for(int setP=0; setP <= list.Length; setP++) {
                int swap2=list.Length - 1; ;
                bool swapped = false;
                for (int swap1=swap2-1; swap1 >= setP;swap2=(--swap1)+1) {
                    if (list[swap2] < list[swap1]) {
                        (list[swap1], list[swap2]) = (list[swap2], list[swap1]);
                        swapped = true;
                    }
                }
                if (!swapped) break;
            }

            for(int i = 0; i<list.Length; i++) {
                Console.Write("{0,3} ",list[i]);
            }
            Console.Write("\n");

            /*
             * --------------------------------------------------------
             * 終わり
             */

            /*
             *  Appendix
             */

            //ソートは今では一発で出来る
            foreach (var item in list.Order())
            {
                Console.Write("{0,3} ", item);
            };
            Console.Write("\n");

            //もっと短く書ける
            list.Order().ToList().ForEach(x => Console.Write("{0,3} ",x));
        }
    }
}
