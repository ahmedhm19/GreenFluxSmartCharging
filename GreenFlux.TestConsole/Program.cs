using GreenFlux.Model;
using GreenFlux.Service.Tools;
using System;
using System.Collections.Generic;

namespace GreenFlux.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");




            float target = 9;

            var connectorsList = new List<Connector>();
            connectorsList.Add(new Connector() { MaxCurrent = 6, ChargeStationId = 1, CK_Id = 1 });
            connectorsList.Add(new Connector() { MaxCurrent = 5, ChargeStationId = 2, CK_Id = 2 });
            connectorsList.Add(new Connector() { MaxCurrent = 4, ChargeStationId = 3, CK_Id = 3 });
            connectorsList.Add(new Connector() { MaxCurrent = 3, ChargeStationId = 4, CK_Id = 4 });
            connectorsList.Add(new Connector() { MaxCurrent = 3, ChargeStationId = 7, CK_Id = 7 });
          //  connectorsList.Add(new Connector() { MaxCurrent = 3, ChargeStationId = 5, CK_Id = 5 });
          //  connectorsList.Add(new Connector() { MaxCurrent = 1, ChargeStationId = 6, CK_Id = 6 });

            //connectorsList.Add(new Connector() { MaxCurrent = 33, ChargeStationId = 1, CK_Id = 1 });
            //connectorsList.Add(new Connector() { MaxCurrent = 20, ChargeStationId = 2, CK_Id = 2 });
            //connectorsList.Add(new Connector() { MaxCurrent = 19, ChargeStationId = 3, CK_Id = 3 });
            //connectorsList.Add(new Connector() { MaxCurrent = 17, ChargeStationId = 4, CK_Id = 4 });
            //connectorsList.Add(new Connector() { MaxCurrent = 16, ChargeStationId = 4, CK_Id = 4 });
            //connectorsList.Add(new Connector() { MaxCurrent = 10, ChargeStationId = 5, CK_Id = 5 });
            //connectorsList.Add(new Connector() { MaxCurrent = 9, ChargeStationId = 6, CK_Id = 6 });
            //connectorsList.Add(new Connector() { MaxCurrent = 3, ChargeStationId = 7, CK_Id = 7 });
            //connectorsList.Add(new Connector() { MaxCurrent = 2, ChargeStationId = 8, CK_Id = 8 });
            //connectorsList.Add(new Connector() { MaxCurrent = 17, ChargeStationId = 9, CK_Id = 9 });





            ///////


            var watch2 = System.Diagnostics.Stopwatch.StartNew();

            var suggestions2 = ConnectorRemovalSuggestionService.SuggestConnectors(connectorsList, target);

            watch2.Stop();
            var elapsedMs2 = watch2.ElapsedMilliseconds;

            Console.WriteLine("Suggestions count : " + suggestions2.Count);
            Console.WriteLine("Time : " + elapsedMs2);

            foreach (var item in suggestions2)
            {
                Console.WriteLine("Suggestion Start : ***********");
                foreach (var element in item)
                {
                    Console.WriteLine($"ID : {element.ConnectorId} chargeStationID : {element.StationId} MaxCurrent : {element.ConnectorCurrent}");
                }
                Console.WriteLine("Suggestion End  **************");
            }





            Console.WriteLine("\nThe Other Solution  //////////////////////////**************");
            Console.WriteLine("**************\n");


            var suggester = new Suggester();

            var watch = System.Diagnostics.Stopwatch.StartNew();

            var suggestions = suggester.GetSuggestions(connectorsList, target);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            Console.WriteLine("My Suggestions count : " + suggestions.Count);
            Console.WriteLine("My Time : " + elapsedMs);

            foreach (var item in suggestions)
            {
                Console.WriteLine("Suggestion Start : ***********");
                if (item is SingleElementSuggestion)
                {
                    var sinSuggestion = (SingleElementSuggestion)item;
                    Console.WriteLine($"ID : {sinSuggestion.Element.CK_Id} chargeStationID : {sinSuggestion.Element.ChargeStationId} MaxCurrent : {sinSuggestion.Element.MaxCurrent}");
                }
                else
                {
                    var muSuggestion = (MultipleElementSuggestion)item;
                    foreach (var element in muSuggestion.Elements)
                    {
                        Console.WriteLine($"ID : {element.CK_Id} chargeStationID : {element.ChargeStationId} MaxCurrent : {element.MaxCurrent}");
                    }
                }
                Console.WriteLine("Suggestion End  **************");
            }







            Console.ReadKey();
        }

    }
}
