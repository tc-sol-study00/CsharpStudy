namespace KaiZyo {

    /// <summary>
    /// 階乗演習
    /// </summary>
    internal class Program {
        static void Main(string[] args) {
            int atai=10,x=0,y=1;
            while (x < atai) {
                y*=++x;
            }
            Console.WriteLine("{0}! = {1:N0}",x,y);

            /*
             * 実は一行で書ける
             */

            Console.WriteLine("{0}! = {1:N0}", 10,10!);
        }



    }
}