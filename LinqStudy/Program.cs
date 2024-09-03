using LinqStudy.Data;
using System.Collections.Generic;

namespace LinqStudy {
    /// <summary>
    /// LINQ勉強用
    /// </summary>
    internal class Program {
        static void Main(string[] args) {

            IList<M_CityMonthlyTemperattures> m_CityMonthlyTemperattureList
                = new List<M_CityMonthlyTemperattures>() {
                    new M_CityMonthlyTemperattures(){ CityName = "TOKYO", MaxTemp = 30.0 },
                     new M_CityMonthlyTemperattures(){ CityName = "TOKYO", MaxTemp = 29.0 },
                     new M_CityMonthlyTemperattures(){ CityName = "TOKYO", MaxTemp = 35.0 },
                     new M_CityMonthlyTemperattures(){ CityName = "TOKYO", MaxTemp = 20.0 },
                    new M_CityMonthlyTemperattures(){ CityName = "OOSAKA", MaxTemp = 32.0 }
                };

            List<CityData> m_cities =
                new List<CityData>() {
                    new CityData(){CityName="TOKYO",JapaneseName="東京"},
                    new CityData(){CityName="OOSAKA",JapaneseName="大阪"},
                     new CityData(){CityName="NAGOYA",JapaneseName="名古屋"},
                     new CityData(){CityName="OOITA",JapaneseName="大分"},
                     new CityData(){CityName="FUKUOKA",JapaneseName="福岡"},
                };

            /*
                        M_CityMonthlyTemperattures m_CityMonthlyTemperatture 
                            = new M_CityMonthlyTemperattures() { CityName = "TOKYO", MaxTemp = 30.0 };

                        m_CityMonthlyTemperattureList.Add(m_CityMonthlyTemperatture);

                        m_CityMonthlyTemperatture
                            = new M_CityMonthlyTemperattures() { CityName = "OOSAKA", MaxTemp = 32.0 };

                        m_CityMonthlyTemperattureList.Add(m_CityMonthlyTemperatture);
            */

            const string cityName = "TOKYO";
            const double bordertemp = 25.0;

            //クエリ構文
            var query =
                from M_CityMonthlyTemperattures c in m_CityMonthlyTemperattureList
                where c.CityName == cityName && c.MaxTemp >= bordertemp
                orderby c.MaxTemp descending
                select c;

            //メソッド構文
            var query2 = m_CityMonthlyTemperattureList
                .Where(c => c.CityName == cityName && c.MaxTemp >= bordertemp)
                .OrderByDescending(c => c.MaxTemp)
                .Select(c => c);

            foreach (var item in query) {
                Console.WriteLine("{0} - {1:F2}", item.CityName, item.MaxTemp);
            }

            foreach (var item in m_CityMonthlyTemperattureList.OrderByDescending(x => x.MaxTemp)) {
                if (item.CityName == cityName && item.MaxTemp == bordertemp) {
                    Console.WriteLine("{0} - {1:F2}", item.CityName, item.MaxTemp);
                }
            }

            Console.WriteLine("---------------");
            var query3 =
                from M_CityMonthlyTemperattures c in m_CityMonthlyTemperattureList
                    //where c.CityName == cityName && c.MaxTemp >= bordertemp
                group c by new { c.CityName } into g
                select new { CityName = g.Key.CityName, AverageMaxTemp = g.Average(c => c.MaxTemp) };

            foreach (var item in query3) {
                Console.WriteLine("{0} - {1:F2}", item.CityName, item.AverageMaxTemp);
            }

            Console.WriteLine("---------------");
            var query4 =
            from M_CityMonthlyTemperattures c in m_CityMonthlyTemperattureList

                //where c.CityName == cityName && c.MaxTemp >= bordertemp
            orderby c.MaxTemp descending
            join CityData city in m_cities on c.CityName equals city.CityName
            select new { CityName = c.CityName, JapanseName = city.JapaneseName, MaxTemp = c.MaxTemp };

            foreach (var item in query4) {
                Console.WriteLine("{0}-{1} - {2:F2}", item.CityName, item.JapanseName, item.MaxTemp);
            }

            Console.WriteLine("---------------");
            var query5 =
            from M_CityMonthlyTemperattures c in m_CityMonthlyTemperattureList
                //where c.CityName == cityName && c.MaxTemp >= bordertemp
            orderby c.MaxTemp descending
            join CityData city in m_cities on c.CityName equals city.CityName
            group new { c, city } by new { c.CityName, city.JapaneseName } into g
            select new {
                CityName = g.Key.CityName,
                JapanseName = g.Key.JapaneseName,
                AverageMaxTemp = g.Average(x => x.c.MaxTemp)
            };

            foreach (var item in query5) {
                Console.WriteLine("{0}-{1} - {2:F2}", item.CityName, item.JapanseName, item.AverageMaxTemp);
            }

            var query6 = m_CityMonthlyTemperattureList
            .OrderByDescending(c => c.MaxTemp)
            .Join(m_cities,
                  c => c.CityName,
                  city => city.CityName,
                  (c, city) => new { c, city })
            .GroupBy(x => new { x.c.CityName, x.city.JapaneseName })
            .Select(g => new {
                CityName = g.Key.CityName,
                JapaneseName = g.Key.JapaneseName,
                AverageMaxTemp = g.Average(x => x.c.MaxTemp)
            });

            Console.WriteLine("---------------");
            var query10 =
                from a in m_cities
                from b in m_cities
                where a.CityName.CompareTo(b.CityName) < 0 // a.Cityname < b.Cityname
                select new {
                    LeftCityName = a.CityName,
                    LeftJapaneseName = a.JapaneseName,
                    RightCityName = b.CityName,
                    RightJapaneseName = b.JapaneseName
                };

            int i = 1;
            foreach (var item in query10) {
                Console.WriteLine("{0:D2} {1}:{2} - {3}:{4}", i++,item.LeftCityName, item.LeftJapaneseName, 
                                                    item.RightCityName, item.RightJapaneseName);
            }
            Console.WriteLine("---------------");
            Console.WriteLine("A".CompareTo("B"));  //-1 "A"<"B"
            Console.WriteLine("B".CompareTo("A"));  //1  "B">"A"
            Console.WriteLine("A".CompareTo("A"));  //0  "A"="A"


        }
    }
}   
