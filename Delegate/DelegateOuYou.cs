using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate {
    static internal class DelegateOuYou {
        //共通
        static private void Tobu() {
            Console.WriteLine("飛ぶ");
        }
        static private void Hashiru() {
            Console.WriteLine("走る");
        }
        static private void Aruku() {
            Console.WriteLine("歩く");
        }
        //普通の組み方
        static public void FuTsuuNoKumikata() {

            Console.WriteLine("イベント処理普通の組み方");
            //ボタンクリックシュミレーション
            Bottun0_Pushed_Futu();
            Bottun1_Pushed_Futu();
            Bottun2_Pushed_Futu();
        }

        static void Bottun0_Pushed_Futu() {
            FuTsuuNoKumikataHandller(0);
        }
        static void Bottun1_Pushed_Futu() {
            FuTsuuNoKumikataHandller(1);
        }

        static void Bottun2_Pushed_Futu() {
            FuTsuuNoKumikataHandller(2);
        }

        static private void FuTsuuNoKumikataHandller(int argClickbottunNo) {
            if(argClickbottunNo == 0) {
                Tobu();
            }else if(argClickbottunNo == 1) {
                Hashiru();
            }else if(argClickbottunNo == 2) {
                Aruku();
            }

            //その後の共通処理
        }

        //普通の組み方

        static Action selectedmethod;
        static public void DelegateNoKumikata() {
            Console.WriteLine("イベント処理delegateでの組み方");
            //ボタンクリックシュミレーション
            Bottun0_Pushed_Dele();
            Bottun1_Pushed_Dele();
            Bottun2_Pushed_Dele();
        }

        static void Bottun0_Pushed_Dele() {
            DelegateNoKumikataHandller(Tobu);
        }
        static void Bottun1_Pushed_Dele() {
            DelegateNoKumikataHandller(Hashiru);
        }

        static void Bottun2_Pushed_Dele() {
            DelegateNoKumikataHandller(Aruku);
        }

        static private void DelegateNoKumikataHandller(Action argSelectedmethod) {
            argSelectedmethod();

            //その後の共通処理

        }



    }
}
