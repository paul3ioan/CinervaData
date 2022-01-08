using DataTestConsole.SqlQueries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Cinerva.Data;
using Microsoft.EntityFrameworkCore;

namespace DataTestConsole
{
    class Program
    { 
       static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("app.json").Build();
            var connStr = configuration.GetConnectionString("Cinerva");

            var host = Host.CreateDefaultBuilder().ConfigureServices((x, y) => {
                y.AddDbContext<CinervaDBContext>(args => args.UseSqlServer(connStr));
                y.AddScoped<IQueries, Queries>();
                }
            ).Build();
            var query = host.Services.GetRequiredService<IQueries>();
            //var sql_2 = Query1To5.Query2();
            var x= query.Query2();
            //var sql_3 = Query1To5.Query3();
            
            //var sql_4 = Query1To5.Query4();
            
            //var sql_5 = Query1To5.Query5();
            
            //var sql_6 = Query6To10.Query6();
            
            //var sql_7 = Query6To10.Query7();
            
            //var sql_8 = Query6To10.Query8();
            
            //var sql_9 = Query6To10.Query9();
            
            //var sql_10 = Query6To10.Query10();

            //var sql_11 = Queries11To15.Query11();

            //var sql_12 = Queries11To15.Query12();

            //var sql_13 = Queries11To15.Query13();

            //var sql_14 = Queries11To15.Query14();

            //var sql_15 = Queries11To15.Query15();
        }
    }
}
