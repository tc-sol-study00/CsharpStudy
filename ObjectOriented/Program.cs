using System.Xml.Linq;

namespace ObjectOriented {
    internal class Program {
        static void Main(string[] args) {

            Dog dogA = new Dog("いぬＡ", 5);   //いぬＡの生成化・実態（インスタンシエーション）
            Dog dogB = new Dog("いぬＢ", 20);   //いぬＢの生成化・実態（インスタンシエーション）

            dogA.Gohan();
            dogA.Gohan();
            dogA.Gohan();

            Console.WriteLine(dogA.leglength);

            dogB.Gohan();
            dogB.Gohan();

            Console.WriteLine(dogA.leglength);

            dogA.Naku();
            dogB.Naku();

            dogB.Gohan();
            dogB.Gohan();
            dogB.Gohan();
            dogB.Gohan();

            dogB.Naku();

            SuperDog sdogC = new SuperDog("SいぬC", 100,0);
            sdogC.Tobu(100);
            sdogC.Tobu(100.0);
            sdogC.Tobu();
            sdogC.Tobu("100");

            Console.WriteLine(sdogC.fliedDistance);

            sdogC.Gohan();

            Console.WriteLine(sdogC.leglength);
            Console.WriteLine(sdogC.WingLength);

            /*
             * .    ピリオド
             * ,    コンマ、カンマ
             * :    コロン
             * ;    セミコロン
             */
        }
    }

    public class Dog {  //スーパークラス・基底クラス・ベースクラス

        //プロパティ
        public string name { get; set; }
        public double leglength { get; set; }

        public string nakikata { get; set; } = "わんわん";

        //コンストラクター
        public Dog(string inName, double inLegLength) {
            name = inName;
            leglength = inLegLength;
        }

        //メソッド
        public void Naku() {
            Console.WriteLine(nakikata);
        }

        //
        public double Gohan() {
            leglength += 1;
            if (leglength >= 25) {
                nakikata = "バウワウ";
            }
            return leglength;
        }
    }
    public class SuperDog : Dog { //サブクラス   //extents (クラス) implements(I/F)
        public string name { get; set; }
        public double fliedDistance { get; set; }

        public double WingLength { get; set; } = 5;

        public SuperDog(string inName, double inLegLength, double infliedDistance) : base(inName, inLegLength) {
            fliedDistance = infliedDistance;
            this.name = inName;
            base.name = inName; //Javaの場合は、super
        }

        public void Tobu(double inTondaKyori) {
            fliedDistance += inTondaKyori;
        }

        public void Tobu(int inTondaKyori) {    //オーバーロード
            fliedDistance += inTondaKyori;
        }

        public void Tobu(string inStrTondaKyori) {    //オーバーロード
            double inTondaKyori = int.Parse(inStrTondaKyori);
            this.Tobu((int)inTondaKyori);               //Ｃａｓｔ（型変換）
        }

        public void Tobu() {    //オーバーロード
            this.Tobu(5.0);
        }

        public double Gohan() { //オーバライド
            WingLength += 1;

            double leglength=base.Gohan();
            return (leglength);
        }

    }
