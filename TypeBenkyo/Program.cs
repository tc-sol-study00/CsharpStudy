namespace TypeBenkyo {
    internal class ChumonData {
        public string ChumonId { get; set; }
        public int Price { get; set; }

        internal ChumonData(string ChumonId, int Price) {
            this.ChumonId = ChumonId;
            this.Price = Price;
        }
    }

    /*
     * コレクション(集合)
     */
    internal class Program {
        static void Main(string[] args) {
            //public class List<T> : IList<T>, IList, IReadOnlyList<T>
            List<ChumonData> chumonData = new List<ChumonData>();

            for (int i = 1; i <= 100; i++) {
                chumonData.Add(new ChumonData($"20240926-{i:000}", i));
            }

            //public interface IEnumerable<out T> : IEnumerable
            IEnumerable<ChumonData> iEnumerableChumonData = chumonData;

            //public interface ICollection<T> : IEnumerable<T>, IEnumerable
            ICollection<ChumonData> iCollectionChumonData = chumonData;

            // public interface IList<T> : ICollection<T>, IEnumerable<T>, IEnumerable 
            IList<ChumonData> iListChumonData = chumonData;

            //IEnumerable
            //public interface IEnumerable<out T> : IEnumerable
            //GetEnumerator
            //Foreach型ともいわれている
            //基本ＬＩＮＱは、IEnumerable型を通して利用されている
            var enumerator = iEnumerableChumonData.GetEnumerator();
            while (enumerator.MoveNext()) {
                var rec1 = enumerator.Current;
            }

            foreach (var rec2 in iEnumerableChumonData) {
                var rec3 = rec2;
            }

            iEnumerableChumonData.FirstOrDefault();
            iEnumerableChumonData.GroupBy(x => x.ChumonId.Substring(9, 3));
            iEnumerableChumonData.Where(x => x.ChumonId == "xxxxxxx");
            chumonData.ToList();

            //Collection
            //追加削除が使える
            //public interface ICollection<T> : IEnumerable<T>, IEnumerable
            var cnt = iCollectionChumonData.Count;

            var data = new ChumonData("20240926-200", 200);

            iCollectionChumonData.Add(data);
            //iCollectionChumonData.Clear();
            if (iCollectionChumonData.Contains(data)) {
                Console.WriteLine("Found");
            }

            ChumonData[] chumontbls = new ChumonData[iCollectionChumonData.Count];
            iCollectionChumonData.CopyTo(chumontbls, 0);
            iCollectionChumonData.Remove(data);

            //IList
            // public interface IList<T> : ICollection<T>, IEnumerable<T>, IEnumerable 
            //インデクサが使える
            var found2 = iListChumonData.IndexOf(data);
            iListChumonData.Insert(found2 + 1, new ChumonData("20240926-201", 201));
            iListChumonData.RemoveAt(found2 + 1);
            var dataAtZero = iListChumonData[0];

            //IReadOnly型
            IReadOnlyList<ChumonData> iReadOnlyListChumonData = chumonData;
            var fdata3 = iReadOnlyListChumonData.First(x => x.ChumonId == "20240926-200");
            fdata3.Price = 300;

            //Generic型
            Func<string> a = new Func<string>();
            a.func("1", "2");

            ICollection<ChumonData> xxx = new List<ChumonData>();

        }
        List<ChumonData> xxxx;
    }
    class Func<T> {
        public Func (){}
        public int func(T argX1, T argX2) {

            int x1 = default, x2 = default;

            if (typeof(T) == typeof(string)) {
                x1 = int.Parse(argX1 as string);
                x2 = int.Parse(argX2 as string);
            }
            else if (typeof(T) == typeof(int)) {
                x1 = Convert.ToInt32(argX1);
                x2 = Convert.ToInt32(argX2);
            }
            return x1 + x2;
        }
    }
}
