namespace InterfceTest {
    internal class Program {
        static void Main(string[] args) {

            IJidoShaBikeShared jidoSha = new JidoSha();
            IJidoShaBikeShared bike = new Bike();

            List<IJidoShaBikeShared> obj1;
            List<Bike> obj2;

            /*
            foreach obj1;
            foreach obj2;
            */


            obj.Add(jidoSha);
            obj.Add(bike);


            jidoSha.Hashiru();  
            bike.Hashiru();

            /*
            AJidoShaBikeShared jidoShaWithAbst = new JidoShaWithAbst();
            AJidoShaBikeShared bikeWithAbst = new BikeWithAbst();
            jidoShaWithAbst.Hashiru();
            bikeWithAbst.Hashiru();
            */

        }
    }
}
