using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop
{

    //  »нтернет магазин
    //  —писок товаров и категории создать руками в базе данных.
    //-	¬озможность просматривать список товаров, категории. 
    //-	¬озможность сортировки по цене.
    //-	¬озможность регистрации/логина.
    //-	¬озможность просмотра истории заказов дл€ авторизованных пользователей.
    //-	–едактирование собственного профил€.
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
