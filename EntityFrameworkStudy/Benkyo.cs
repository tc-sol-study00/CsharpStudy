using EntityFrameworkStudy.Data;
using EntityFrameworkStudy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkStudy {

    /// <summary>
    /// 2024/9/24に実施した座学＋演習の内容
    /// 主にEfcore
    /// </summary>
    public class Benkyo {

        private static EntityFrameworkStudyContext _context;

        public Benkyo(EntityFrameworkStudyContext context) {
            _context = context;
        }
        public void BenkyoMethod() {

            //問い合わせ

            if (false) {
                var rec =
                    from dt in _context.Education
                    select dt;

                foreach (var item in rec) {
                    Console.WriteLine("{0}:{1:D3}", item.ClassCode, item.SeitoNo);
                }

                //担任毎の、各教科の平均点
                //担任、国語平均、数学平均、理科平均

                //joinを使う、group by を使う
                var query = from Education edu in _context.Education
                            join ClassAttr cls in _context.ClassAttr on edu.ClassCode equals cls.ClassCode
                            group new { edu, cls } by new { cls.Tannin } into grp
                            select new {
                                Tannin = grp.Key.Tannin,
                                KokugoAvgScore = grp.Average(x => x.edu.KokugoScore),
                                SuugakuAvgScore = grp.Average(x => x.edu.SuugakuScore),
                                RikaAvgScore = grp.Average(x => x.edu.RikaScore),
                                Ninzu = grp.Count()
                            };

                foreach (var item in query) {
                    Console.WriteLine("{0,-10}:{1,4}:{2,4}:{3,4}:{4,4}",
                        item.Tannin, item.KokugoAvgScore, item.SuugakuAvgScore, item.RikaAvgScore, item.Ninzu);
                }

                var query3 = _context.Education.ToList();
                var query4 = _context.Education.Include(x => x.ClassAttr).ToList();

                //includeを使って結合した例

                query = from Education edu in _context.Education.Include(x => x.ClassAttr)
                        group new { edu } by new { edu.ClassAttr.Tannin } into grp
                        select new {
                            Tannin = grp.Key.Tannin,
                            KokugoAvgScore = grp.Average(x => x.edu.KokugoScore),
                            SuugakuAvgScore = grp.Average(x => x.edu.SuugakuScore),
                            RikaAvgScore = grp.Average(x => x.edu.RikaScore),
                            Ninzu = grp.Count()
                        };

                foreach (var item in query) {
                    Console.WriteLine("{0,-10}:{1,4}:{2,4}:{3,4}:{4,4}",
                        item.Tannin, item.KokugoAvgScore, item.SuugakuAvgScore, item.RikaAvgScore, item.Ninzu);
                }
            }

            //挿入
            ClassAttr classattr = new ClassAttr() { ClassCode = "G", Tannin = "Gさん" };

            ClassAttr? classattr_r;
            if ((classattr_r = _context.ClassAttr.AsNoTracking().Where(x => x.ClassCode == "G").SingleOrDefault()) != null) {
                _context.ClassAttr.Remove(classattr_r);
                _context.SaveChanges();
            }

            _context.ClassAttr.Add(classattr);
            _context.SaveChanges();

            //更新
            classattr.Tannin = "GGさん";
            _context.ClassAttr.Update(classattr);
            _context.SaveChanges();

            //削除
            _context.ClassAttr.Remove(classattr);
            _context.SaveChanges();


            //Addをトラッキングするには、List変換してはいけない
            //変更はListに変換してもＯＫ
            var classattrs = _context.ClassAttr;

            classattrs.Find("F").Tannin = "FFさん";

            if (_context.ClassAttr.AsNoTracking().Where(x => x.ClassCode == "G").SingleOrDefault() == null) {
                classattrs.Add(new ClassAttr() { ClassCode = "G", Tannin = "Gさん" });
            }

            //更新前になにが変更されたかチェックできる
            //savechangesされると消えるので、savechanges直前でチェック
            _context.ChangeTracker.DetectChanges();
            Console.WriteLine(_context.ChangeTracker.DebugView.LongView);

            _context.SaveChanges();

            //やったらダメなやつ

            List<ClassAttr> data = _context.ClassAttr.Where(x => x.ClassCode == "A").ToList();
            string tannin = data[0].Tannin;    //データがない時にアベンドするし、[0]がマジックナンバー、リストデータではない

            var data3 = _context.ClassAttr.Where(x => x.ClassCode == "A").ToList(); //var大好きな人ほど、データ型の認識がない

            //Nullや0件データの想定をしましょう
            if(data3.Count() <= 0) {
                //NULLの処置
            }
            //なんでもかんでもListにするのはやめましょう

            //かならず一意に戻るデータは、listで受けない
            //NULLがありえるなら、タイプの横に"?"をつけてnull許容型にする
            ClassAttr? data2 = _context.ClassAttr.Where(x => x.ClassCode == "A").SingleOrDefault();

            //Nullや0件データの想定をしましょう
            if (data2 == null) {
                //NULLの処置
            }


        }
    }
}
