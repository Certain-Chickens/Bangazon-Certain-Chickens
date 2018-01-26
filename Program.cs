using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BangazonAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();

      DateTime date1 = new DateTime(2009, 8, 1, 0, 0, 0);
      DateTime date2 = new DateTime(2009, 8, 1, 12, 0, 0);
      int result = DateTime.Compare(date1, date2);
      string relationship;

      if (result < 0)
         relationship = "is earlier than";
      else if (result == 0)
         relationship = "is the same time as";         
      else
         relationship = "is later than";

      Console.WriteLine("{0} {1} {2}", date1, relationship, date2);
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
