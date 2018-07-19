using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Net.Http;
using System.Web;
using System.Net;
using System.IO;
using Microsoft.Extensions.Configuration;
using ProjectSW.Business;

namespace ProjectSW
{
    public class Starship
    {
        static void Main(string[] args)
        {
            InitConfiguration();
            Console.WriteLine("Insert the distance: ");
            var distance = Convert.ToInt64(Console.ReadLine());
            var apiUrl = Starship.Configuration["ApiUrl"];
            var starshipBusiness = new StarshipBusiness();
            starshipBusiness.CalculateStarshipsStops(apiUrl, distance);
            Console.ReadLine();
        }

        public static IConfiguration Configuration { get; set; }

        public static void InitConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }
    }
}
