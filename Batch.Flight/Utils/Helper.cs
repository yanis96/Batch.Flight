using Batch.Flight.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Batch.Flight.Models.Utils
{
    public static class Helper
    {
        public static async Task<string> GetDataAsync(HttpClient client, string URL)
        {
            var data = string.Empty;
            var response = await client.GetAsync(URL);
            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadAsStringAsync();
            }
            return data;
        }

        public static void ClearTables(string connectionString)
        {
            Console.WriteLine("Clearing tables ...");
            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"delete from Avions";
                    conn.Open();
                    command.ExecuteNonQuery();
                }
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"delete from Vols";
                    command.ExecuteNonQuery();
                }
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"delete from Histories";
                    command.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Clearing tables DONE !");
        }

        public static void InsertToAvion(string connectionString, Data data)
        {
            Console.WriteLine("Inserting into AVION");
            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @" insert into Avions values(@numAvion, @model)";

                    if (data.Reg_number == null)
                    {
                        command.Parameters.AddWithValue("@numAvion", "");
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@numAvion", data.Reg_number);
                    }

                    if (data.Aircraft_icao == null)
                    {
                        command.Parameters.AddWithValue("@model", "");
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@model", data.Aircraft_icao);
                    }

                    conn.Open();
                    command.ExecuteNonQuery();

                }
            }
            Console.WriteLine("Inserting to AVION DONE !");
        }


        public static void InsertToVol(string connectionString, Data data)
        {
            Console.WriteLine("Inserting to Vols");
            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"insert into Vols values(@numVol, @code_camp, @numAvion, @dep, @arr, @statu)";
                    if (data.Flight_icao == null)
                    {
                        command.Parameters.AddWithValue("@numVol", "");
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@numVol", data.Flight_icao);
                    }

                    if (data.Aircraft_icao == null)
                    {
                        command.Parameters.AddWithValue("@code_camp", "");
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@code_camp", data.Aircraft_icao);
                    }

                    if (data.Reg_number == null)
                    {
                        command.Parameters.AddWithValue("@numAvion", "");
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@numAvion", data.Reg_number);
                    }

                    if (data.Dep_icao == null)
                    {
                        command.Parameters.AddWithValue("@dep", "");
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@dep", data.Dep_icao);
                    }

                    if (data.Arr_icao == null)
                    {
                        command.Parameters.AddWithValue("@arr", "");
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@arr", data.Arr_icao);
                    }

                    if (data.Status == null)
                    {
                        command.Parameters.AddWithValue("@statu", "");
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@statu", data.Status);
                    }

                    conn.Open();
                    command.ExecuteNonQuery();

                }
            }
            Console.WriteLine("Inserting to VOL DONE !");
        }

        public static void InsertToHistory(string connectionString, Data data)
        {
            Console.WriteLine("Inserting to HISTORY");
            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"insert into Histories values(@numVol, @dateHist, @lat, @long, @speed)";

                    if (data.Flight_icao == null)
                    {
                        command.Parameters.AddWithValue("@numVol", "");
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@numVol", data.Flight_icao);
                    }

                    DateTime now = DateTime.Now;
                    command.Parameters.AddWithValue("@dateHist", now);

                    if (data.Lat == null)
                    {
                        command.Parameters.AddWithValue("@lat", 0.00);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@lat", data.Lat);
                    }

                    if (data.Lng == null)
                    {
                        command.Parameters.AddWithValue("@long", 0.00);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@long", data.Lng);
                    }

                    if (data.Speed == null)
                    {
                        command.Parameters.AddWithValue("@speed", 0);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@speed", data.Speed);
                    }

                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Inserting to HISTORY DONE !");
        }
    }
}
