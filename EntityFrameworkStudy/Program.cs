using EntityFrameworkStudy.Data;
using EntityFrameworkStudy.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Headers;
using static System.Net.Mime.MediaTypeNames;




namespace EntityFrameworkStudy {
    
    class Program {

        private const string connectionString = "Server=localhost;Port=5432;Username=study01;Password=study01;Database=postgres;";

        public static EntityFrameworkStudyContext _context;

        //DI用
        private static void ConfigureServices(IServiceCollection services) {
            // DbContextの登録
            services.AddDbContext<EntityFrameworkStudyContext>(options =>
                options.UseNpgsql(connectionString));
            // アプリケーションクラスの登録
            //services.AddTransient<DiTest>();
            services.AddTransient<Enshu>();
        }

        //DI使わない場合
        public static void OpenDB() {
            var contextOptions = new DbContextOptionsBuilder<EntityFrameworkStudyContext>()
                .UseNpgsql(connectionString)
                .Options;
            _context = new EntityFrameworkStudyContext(contextOptions);
        }

        static void Main() {

            const string selectFlg = "NODI";

            switch (selectFlg) {
                case "DI":
                    // サービスコレクションの設定
                    var serviceCollection = new ServiceCollection();
                    ConfigureServices(serviceCollection);

                    // サービスプロバイダーの作成
                    var serviceProvider = serviceCollection.BuildServiceProvider();

                    // サービスの取得と使用
                    var app = serviceProvider.GetService<Enshu>();
                    app.EnshuMethod();
                    break;

                case "NODI":
                    OpenDB();
                    new Enshu(_context).EnshuMethod();
                    break;

                default:
                    Console.WriteLine("No matching case found.");
                    break;
            }
        }
    }
}