using System;
using System.Collections.Generic;

#nullable disable

namespace AirlinesDb.Model
{
    public partial class Company
    {
        public Company()
        {
            Trips = new HashSet<Trip>();
        }

        public int IdComp { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Trip> Trips { get; set; }
    }
}
