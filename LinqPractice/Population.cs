using System;
using System.Collections.Generic;
using System.Linq;
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
        internal string CityName { get; set; }     // $"{CityId}市";
        internal int Population { get; set; }  //人口（ランダム機能でも使って
    }
    internal class PrefectureData {
        internal int PrefectureId { get; set; }             //県ID 1～49
        internal string PrefectureName { get; set; }     //$"{PrefectureId}県"
        internal int AverageWage { get; set; }          //県平均賃金（ランダム）
    }

    //講義用（この処理では使っていない）
    internal static class RandomStudy {
        internal static void RanSu() {
            Random r = new Random();

            r.Next(3); //0,1,2
            r.Next(4);  //0,1,2,3

            r.Next(100_000);
            r.Next(10_000, 100_000);
        }
    }

    internal class LinqPractice {
        /*
         * メンバー変数
         */
        //市データ
        IList<CityData> CityDatas { get; set; } = new List<CityData>() { };
        //県データ
        IList<PrefectureData> PrefectureDatas { get; set; } = new List<PrefectureData>() { };

        //リテラル
        const uint PrefecureMaxLength = 47;
        const uint CityMaxLength = 10;

        const int LineMaxSize = 80;
        static Action border = () => Console.WriteLine(new string('-', LineMaxSize));           //-----------←こういうの出す
        static Action<string> title = x => { border(); Console.WriteLine(x); border(); };       //引数にいれられた内容を出す


        /// <summary>
        /// 演習メソッド（演習１～５）
        /// </summary>
        internal void Enshu() {

            Random r = new Random();

            /* 演習１
             * CityDataを作成する
             */

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

            /* 演習２
             * CityDataからPrefectureDataを作成する
             */

            PrefectureDatas = (from cty in CityDatas
                               group cty by cty.PrefectureId into grp
                               select new PrefectureData {
                                   PrefectureId = grp.Key,
                                   PrefectureName = $"{grp.Key}県",
                                   AverageWage = r.Next(100_000, 1_000_000) //最低賃金,最高賃金
                               }).ToList();

            //foreach (var item in PrefectureDatas) {
            //    Console.WriteLine($"{item.PrefectureId},{item.PrefectureName},{item.AverageWage}");
            //}

            /* 演習３
             * 県単位の、人口と総収入を求める（CityData→PrefectureData）
             * 一覧表表示
             */

            var summaryPopWagePractice3 = (
                from cty in CityDatas
                join pre in PrefectureDatas
                on cty.PrefectureId equals pre.PrefectureId
                group new { cty, pre } by pre.PrefectureId into grp
                select new {
                    PrefectureId = grp.Key,
                    PrefectureName = grp.Max(x => x.pre.PrefectureName),
                    Population = grp.Sum(x => x.cty.Population),
                    AverageWage = grp.Min(x => x.pre.AverageWage),
                    TotalWage = (long)grp.Sum(x => x.cty.Population) * (long)grp.Min(x => x.pre.AverageWage)
                }).ToList();

            title("演習３ 県単位の、人口と総収入を求める（CityData→PrefectureData）");
            foreach (var prefectureData in summaryPopWagePractice3) {
                Console.WriteLine($"{prefectureData.PrefectureId},{prefectureData.PrefectureName},{prefectureData.Population},{prefectureData.AverageWage},{prefectureData.TotalWage}");
            }
            border();

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


            /* 演習５
             * 市単位の、人口と総収入を求める（PrefectureData→CityData）
             * 一覧表表示（フラット版）foreach１つのはず
             *　例）
             *　-東京 小金井   xxxx
             *　-東京 港区     xxxx  
             *　-神奈川 横浜   xxxx 
             */

            var summaryPopWagePractice5 = (
            from pre in PrefectureDatas
            from cty in CityDatas
            where pre.PrefectureId == cty.PrefectureId
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
