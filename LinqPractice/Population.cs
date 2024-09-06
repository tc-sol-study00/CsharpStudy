using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

/*
 * Linq演習
 * 県データ、市データの集計
 * 
 */

namespace LinqPractice {
    internal class CityData {
        internal int PrefectureId { get; set; } //県ID 1～49
        internal int CityId { get; set; }       //県別に10市(1～10)
        internal string CityName { get; set; } = string.Empty;     // $"{CityId}市";
        internal int Population { get; set; }  //人口（ランダム機能でも使って
    }
    internal class PrefectureData {
        internal int PrefectureId { get; set; }             //県ID 1～49
        internal string PrefectureName { get; set; } = string.Empty;     //$"{PrefectureId}県"
        internal int AverageWage { get; set; }          //県平均賃金（ランダム）
    }
    internal static class LinqPractice {

        delegate void EnShuAction();    //演習メソッド用のタイプ宣言

        private class ExecuteMenu {
            internal int EnshuNo { get; set; }          //演習番号
            internal EnShuAction Action { get; set; }   //演習メソッド格納
            public ExecuteMenu(int EnshuNo, EnShuAction Action) {
                this.EnshuNo = EnshuNo;
                this.Action = Action;
            }
        }
        private static List<ExecuteMenu> ExecuteMenuList 
            = new List<ExecuteMenu>(){
                new ExecuteMenu(1, Enshu1),
                new ExecuteMenu(2, Enshu2),
                new ExecuteMenu(3, Enshu3),
                new ExecuteMenu(4, Enshu4),
                new ExecuteMenu(5, Enshu5)
        };
        /*
         * メンバー変数
         */
        //市データ
        private static IList<CityData> CityDatas { get; set; } = new List<CityData>() { };
        //県データ
        private static IList<PrefectureData> PrefectureDatas { get; set; } = new List<PrefectureData>() { };
        //リテラル
        private const uint PrefecureMaxLength = 47;
        private const uint CityMaxLength = 10;
        //ログ関係
        private const int LineMaxSize = 80;
        private static Action border = () => Console.WriteLine(new string('-', LineMaxSize));           //-----------←こういうの出す
        private static Action<string> title = x => { border(); Console.WriteLine(x); border(); };       //引数にいれられた内容を出す
        //乱数
        private static Random r = new Random();
        //必須で起動させる演習番号
        private static List<uint> FixedMenu = new List<uint>() { 1, 2 };
        //オプションで選択する演習メニュー
        private static List<uint> SelectiveExecuteList = new List<uint> { 3,4,5 };

        /// <summary>
        /// 演習メソッド（演習１～５）
        /// </summary>
        internal static void Enshu() {

            //オプションで選択する演習メニューから起動する演習用メソッドをコールする
            foreach (int menu in FixedMenu.Concat(SelectiveExecuteList)) {
                //演習番号により対応する演習メソッドを探す
                EnShuAction? enShuAction = ExecuteMenuList.Where(x => x.EnshuNo == menu).Select(x => x.Action).SingleOrDefault();
                if (enShuAction != null) {
                    //演習メソッド起動
                    enShuAction();          //delegate利用
                }
                else {
                    Console.WriteLine("演習メニュー選択エラー");
                }
            }
        }
        /* 演習１
         * CityDataを作成する
         */
        private static void Enshu1() {
            for (int prefectureCounter = 1; prefectureCounter <= PrefecureMaxLength; prefectureCounter++) {
                for (int cityCounter = 1; cityCounter <= CityMaxLength; cityCounter++) {
                    CityDatas.Add(new CityData() {
                        PrefectureId = prefectureCounter,
                        CityId = cityCounter,
                        CityName = $"{prefectureCounter}-{cityCounter}市",
                        Population = r.Next(5000, 100_000)  //最低人口,最高人口
                    }
                    );
                }
            }
            title("演習１ CityDataを作成する");
            CityDatas.ToList().ForEach(x => Console.WriteLine($"{x.PrefectureId},{x.CityId},{x.CityName},{x.Population}"));
            border();
        }
        /* 演習２
         * CityDataからPrefectureDataを作成する
         */
        private static void Enshu2() {
            PrefectureDatas = (from cty in CityDatas
                               group cty by cty.PrefectureId into grp
                               select new PrefectureData {
                                   PrefectureId = grp.Key,
                                   PrefectureName = $"{grp.Key}県",
                                   AverageWage = r.Next(100_000, 1_000_000) //最低賃金,最高賃金
                               }).ToList();

            title("演習２ CityDataからPrefectureDataを作成する");
            PrefectureDatas.ToList().ForEach(x => Console.WriteLine($"{x.PrefectureId},{x.PrefectureName},{x.AverageWage}" ));
            border();
        }

        /* 演習３
         * 県単位の、人口と総収入を求める（CityData→PrefectureData）
         * 一覧表表示
         */
        private static void Enshu3() {
            var summaryPopWagePractice3 = (
                from cty in CityDatas
                join pre in PrefectureDatas
                on cty.PrefectureId equals pre.PrefectureId
                group new { cty, pre } by new { pre.PrefectureId, pre.PrefectureName,pre.AverageWage } into grp
                select new {
                    PrefectureId = grp.Key.PrefectureId,
                    PrefectureName = grp.Key.PrefectureName,
                    Population = grp.Sum(x => x.cty.Population),
                    AverageWage = grp.Key.AverageWage,
                    TotalWage = (long)grp.Sum(x => x.cty.Population) * (long)grp.Key.AverageWage
                }).ToList();

            title("演習３ 県単位の、人口と総収入を求める（CityData→PrefectureData）");
            foreach (var prefectureData in summaryPopWagePractice3) {
                Console.WriteLine($"{prefectureData.PrefectureId},{prefectureData.PrefectureName},{prefectureData.Population},{prefectureData.AverageWage},{prefectureData.TotalWage}");
            }
            border();
        }
        /* 演習４
         * 市単位の、人口と総収入を求める（PrefectureData→CityData）
         * 一覧表表示（ブレーク版）foreach２つのはず
         * 　例）
         * 　-東京
         * 　 -小金井　xxxx
         * 　 -港区    xxxxx
         * 　-神奈川
         * 　 -横浜    xxxxxx
         */
        private static void Enshu4() {
            var summaryPopWagePractice4 = (
                from pre in PrefectureDatas
                join cty in CityDatas
                on pre.PrefectureId equals cty.PrefectureId into grp
                select new {
                    PrefectureId = pre.PrefectureId,
                    PrefectureName = pre.PrefectureName,
                    CityDatas = grp.Select(cty => new { cty.CityId, cty.CityName, cty.Population, TotalWage = (long)cty.Population * (long)pre.AverageWage })
                }
               ).ToList();

            title("演習４ 市単位の、人口と総収入を求める（PrefectureData→CityData）");
            foreach (var prefactureData in summaryPopWagePractice4) {
                Console.WriteLine($"{prefactureData.PrefectureId},{prefactureData.PrefectureName} ");

                foreach (var cityData in prefactureData.CityDatas) {
                    Console.WriteLine($"   {cityData.CityId},{cityData.CityName},{cityData.Population},{cityData.TotalWage}");
                }

            }
            border();
        }
        /* 演習５
         * 市単位の、人口と総収入を求める（PrefectureData→CityData）
         * 一覧表表示（フラット版）foreach１つのはず
         *　例）
         *　-東京 小金井   xxxx
         *　-東京 港区     xxxx  
         *　-神奈川 横浜   xxxx 
         */
        private static void Enshu5() {
            var summaryPopWagePractice5 = (
            from pre in PrefectureDatas
            join cty in CityDatas
            on pre.PrefectureId equals cty.PrefectureId
            select new {
                PrefectureId = pre.PrefectureId,
                PrefectureName = pre.PrefectureName,
                cty.CityId,
                cty.CityName,
                cty.Population,
                pre.AverageWage,
                TotalWage = (long)cty.Population * (long)pre.AverageWage
            }
            ).ToList();

            title("演習５ 市単位の、人口と総収入を求める（PrefectureData→CityData）");
            foreach (var flatData in summaryPopWagePractice5) {
                Console.WriteLine($"{flatData.PrefectureId},{flatData.PrefectureName},{flatData.CityId},{flatData.CityName},{flatData.Population},{flatData.AverageWage},{flatData.TotalWage}");
            }
            border();
        }

    }
}