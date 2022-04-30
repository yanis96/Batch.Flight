using System;
using System.Collections.Generic;
using System.Text;

namespace Batch.Flight.Models
{
    public class Data
    {
        public string Reg_number { get; set; } // numAvion
        public string Flag { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public int Alt { get; set; }
        public int Speed { get; set; }
        public string Flight_icao { get; set; } // numVol
        public string Dep_icao { get; set; } // dep pour depart
        public string Arr_icao { get; set; } // arr pour arrivée
        public string Airline_icao { get; set; } // code_comp
        public string Aircraft_icao { get; set; } // model
        public string Status { get; set; }
    }
}
