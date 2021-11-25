using AirlinesDb.Connection;
using AirlinesDb.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AirlinesDb
{
    class Program
    {
        public static List<string> companyNames = new List<string>() 
        { 
            "TinyAir", 
            "TulaAir", 
            "AirTravel" 
        };

        public static IEnumerable<string> planeNames = new List<string>()
        {
            "Boeing",
            "Tu"
        };

        public static IEnumerable<string> townsFrom = new List<string>()
        {
            "Курумоч",
            "Москва"
        };

        public static IEnumerable<string> townsTo = new List<string>()
        {
            "Санкт_Петербург",
            "Тула",
            "Сочи",
            "Тюмень"
        };

        public static IEnumerable<char> places = new List<char>()
        {
            'a',
            'b',
            'c',
            'd',
            'e',
            'f'
        };

        public static IEnumerable<string> psgNames = new List<string>
        {
            "Andrey",
            "Aleksandr",
            "Nikolai",
            "Dmitri"
        };

        static void Main(string[] args)
        {
            FillMockDate();
            ChangeTownFromName();
            DeleteCompanyTrips();
        }

        static AirlinesContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AirlinesContext>();
            var options = optionsBuilder
                .UseSqlServer(new ConnectionStringConfig().ConnectionString)
                .Options;

            return new AirlinesContext(options);
        }

        static void FillMockDate(int seed = 0)
        {
            using var context = CreateContext();
            {
                var transaction = context.Database.BeginTransaction();
                try
                {
                    var random = new Random(seed);
                    var companyIds = new List<int>();
                    var tripNo = new List<int>();
                    var passId = new List<int>();

                    for (int i = 0; i < 3; i++)
                    {
                        companyIds.Add(random.Next());
                        context
                            .Companies
                            .Add(new Company()
                            {
                                IdComp = companyIds[i],
                                Name = companyNames[i]
                            });
                    }

                    context.SaveChanges();

                    for (int i = 0; i < 7; i++)
                    {
                        tripNo.Add(random.Next());
                        var trip = new Trip()
                        {
                            TripNo = tripNo[i],
                            IdComp = companyIds.ElementAt(random.Next(3)),
                            Plane = planeNames.ElementAt(random.Next(2)),
                            TownFrom = townsFrom.ElementAt(random.Next(2)),
                            TownTo = townsTo.ElementAt(random.Next(4)),
                            TimeOut = DateTime.Now,
                            TimeIn = DateTime.Now.AddHours(random.Next(5))
                        };
                        trip
                            .PassInTrips
                            .Add(new PassInTrip() 
                            {
                                TripNo = tripNo[i],
                                Date = DateTime.Now,
                                IdPsg = random.Next(),
                                Place = random.Next(51).ToString() + places.ElementAt(random.Next(6))
                            });
                        var passenger = new Passenger()
                        {
                            IdPsg = trip.PassInTrips.First().IdPsg,
                            Name = psgNames.ElementAt(random.Next(4))
                        };
                        context.Passengers.Add(passenger);
                        context.Trips.Add(trip);

                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    transaction.Rollback();
                }
            }
        }

        static void ChangeTownFromName()
        {
            using var context = CreateContext();
            {
                var trips = context
                    .Trips
                    .Where(t => t.TownFrom == "Курумоч")
                    .ToList();

                foreach (var trip in trips)
                {
                    trip.TownFrom = "Самара";
                }
                context.SaveChanges();
            }
        }

        static void DeleteCompanyTrips(int seed = 0)
        {
            var random = new Random(seed);
            var range = random.Next(3);

            using var context = CreateContext();
            {
                var findComp = context
                    .Companies
                    .First(c => c.Name == companyNames.ElementAt(range));
                var compTrips = context
                    .Trips
                    .Where(t => t.IdComp == findComp.IdComp);

                context.Trips.RemoveRange(compTrips);
                context.SaveChanges();
            }
        }
    }
}
