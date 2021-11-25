using System;
using System.Collections.Generic;

#nullable disable

namespace AirlinesDb.Model
{
    public partial class Passenger
    {
        public Passenger()
        {
            PassInTrips = new HashSet<PassInTrip>();
        }

        public int IdPsg { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PassInTrip> PassInTrips { get; set; }
    }
}
