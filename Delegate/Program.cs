using Delegate.UsingInterFace;

namespace Delegate {
    /// <summary>
    /// DelegateとジェネリックDelegateの勉強用
    /// </summary>
    internal class Program {
        static void Main(string[] args) {
            /*
             * DelegateとジェネリックDelegateの勉強用
             */
            Delegate.DelegateStudy();                       //Delegate勉強
            Delegate.GenericDelegateStudy();                //ジェネリックDelegate
            StudyToInterfaceInsteadOfDelegate.Method1();    //delegateが使えない前提でintergaceで実現した例
            /*
             * イベント処理勉強
             */
            DelegateOuYou.FuTsuuNoKumikata();               //delegateを利用しないイベント処理
            DelegateOuYou.DelegateNoKumikata();             //delegateを利用したイベント処理
        }
    }
}
