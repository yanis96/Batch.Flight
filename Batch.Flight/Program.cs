using Batch.Flight.Models;
using Batch.Flight.Models.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Timers;

namespace Batch.Flight
{
    class Program
    {
        private static string DATA_URL = ConfigurationManager.ConnectionStrings["flightAPI"].ConnectionString;
        private static string DB_CONNECTION = ConfigurationManager.ConnectionStrings["AzureDB"].ConnectionString;
        private static HttpClient client;
        private static Timer timer;
        private static List<Data> data;


        static void Main(string[] args)
        {
            client = new HttpClient();
            var json = Helper.GetDataAsync(client, DATA_URL).GetAwaiter().GetResult(); // get data from flight api
            data = new List<Data>();
            data = JObject.Parse(json).SelectToken("response").ToObject<List<Data>>();
            Console.WriteLine("deonnée recupérées !!");

            //Clearing tables
            Helper.ClearTables(DB_CONNECTION);

            Console.WriteLine("Inserting data ...");

            foreach (Data item in data)
            {
                Helper.InsertToAvion(DB_CONNECTION, item);
                Helper.InsertToVol(DB_CONNECTION, item);
                Helper.InsertToHistory(DB_CONNECTION, item);
            }

            Console.WriteLine("FINI !!!!");


            timer = new Timer();
            timer.Interval = 3600000;
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;

            Console.ReadLine();
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Inserting to HISTORY at {0}", e.SignalTime);
            foreach (Data item in data)
            {
                Helper.InsertToHistory(DB_CONNECTION, item);
            }
            Console.WriteLine("Inserting Done at{0}", e.SignalTime);
        }
    }
}
