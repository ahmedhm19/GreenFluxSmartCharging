using GreenFlux.Model;
using GreenFlux.Service.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GreenFlux.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            #region DB

            /*
             
            string connectionString = "Server=.\\sqlexpress_fs;Database=GreenFluxDB;MultipleActiveResultSets=true;User ID=alZahrawiApp;pwd=alZahrawiApp";

            //DbContext
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer(connectionString);
            var dbContext = new DatabaseContext(optionsBuilder.Options);
            
            if(!dbContext.Database.CanConnect())
            dbContext.Database.EnsureCreated();

           
            //Repos
            var groupRepo = new Repository<Group>(dbContext);
            var chargeStationRepo = new Repository<ChargeStation>(dbContext);
            var connectorRepo = new Repository<Connector>(dbContext);

            //UnitOfWorks
            var unitOfWork = new UnitOfWork(dbContext);
            unitOfWork.GroupRepository = groupRepo;
            unitOfWork.ChargeStationRepository = chargeStationRepo;
            unitOfWork.ConnectorRepository = connectorRepo;

            //Validators
            var commonValidator = new CommonValidator();
            var groupValidator = new GroupValidator(unitOfWork,commonValidator);
            var chargeStationValidator = new ChargeStationValidator(unitOfWork, commonValidator);
            var connectorValidator = new ConnectorValidator(unitOfWork, commonValidator);

            //Services
            var groupService = new GroupService(unitOfWork, groupValidator);
            var chargeStationService = new ChargeStationService(unitOfWork, chargeStationValidator);
            var connectorService = new ConnectorService(unitOfWork, connectorValidator);

            */

            #endregion


            #region Create connectors

            var connectors = new List<Connector>();

            for (int i = 0; i < 150; i++)
            {
                connectors.Add(new Connector { CK_Id = 103, ChargeStationId = 1, MaxCurrent = 2 });
                connectors.Add(new Connector { CK_Id = 104, ChargeStationId = 1, MaxCurrent = 5 });
                connectors.Add(new Connector { CK_Id = 105, ChargeStationId = 1, MaxCurrent = 4 });
                connectors.Add(new Connector { CK_Id = 102, ChargeStationId = 1, MaxCurrent = 3 });
                connectors.Add(new Connector { CK_Id = 106, ChargeStationId = 1, MaxCurrent = 3 });
                connectors.Add(new Connector { CK_Id = 107, ChargeStationId = 1, MaxCurrent = 6 });
                connectors.Add(new Connector { CK_Id = 108, ChargeStationId = 1, MaxCurrent = 1 });
            }
          //  connectors[7] = new Connector { CK_Id = 109, ChargeStationId = 1, MaxCurrent = 5 };


            #endregion


            float capacity = connectors.Sum(e => e.MaxCurrent);
            float target = 97;
            Console.WriteLine(Environment.NewLine + "Finding available space : ");
            Console.WriteLine("Connectors SUM : " + connectors.Sum(e => e.MaxCurrent));
            Console.WriteLine("Free space : " + (capacity - connectors.Sum(e => e.MaxCurrent)));
            Console.WriteLine("Target : " + target);



            List<Connector[]> combinations = new List<Connector[]>();
            List<ISuggestion> suggestions = new List<ISuggestion>();


            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            Suggester processor = new Suggester();
            suggestions = processor.GetSuggestions(connectors, target);
            //combinations = SuggestionProcessor.GetCombinations(connectors, target);


            sw.Stop();
            Console.WriteLine("****** Elapsed Time :  " + sw.Elapsed.TotalMilliseconds);

            Console.WriteLine("Solutions count : " + suggestions.Count);



            //foreach (var item in suggestions)
            //{
            //    Console.WriteLine("Suggestion : ");

            //    DisplaySuggestion(item);

            //    Console.WriteLine("END Suggestion");
            //    Console.WriteLine(Environment.NewLine);
            //}




            Console.WriteLine("End");
            Console.ReadLine();
        }


        public static void DisplaySuggestion(ISuggestion suggestion)
        {
            Console.WriteLine("------------------------");

            if(suggestion is SingleElementSuggestion)
            Console.WriteLine($"Connector.CK_ID : {((SingleElementSuggestion)suggestion).Element.CK_Id} -ChargeStationId : {((SingleElementSuggestion)suggestion).Element.ChargeStationId} -MaxCurrent : {((SingleElementSuggestion)suggestion).Element.MaxCurrent}");
            

            if(suggestion is MultipleElementSuggestion)
                foreach (var connector in ((MultipleElementSuggestion)suggestion).Elements)
                {
                    Console.WriteLine($"Connector.CK_ID : { connector.CK_Id} -ChargeStationId : {connector.ChargeStationId} -MaxCurrent : {connector.MaxCurrent}");
                }

            Console.WriteLine("------------------------");
        }

        public static void DisplaySuggestions(IList<ISuggestion> collection)
        {
            Console.WriteLine("------------------------");

            foreach (SingleElementSuggestion item in collection)
            {
                Console.WriteLine($"Connector.CK_ID : {item.Element.CK_Id} -ChargeStationId : {item.Element.ChargeStationId} -MaxCurrent : {item.Element.MaxCurrent}");
            }

            foreach (MultipleElementSuggestion item in collection)
            {
                foreach (var connector in item.Elements)
                {
                    Console.WriteLine($"Connector.CK_ID : { connector.CK_Id} -ChargeStationId : {connector.ChargeStationId} -MaxCurrent : {connector.MaxCurrent}");
                }
            }



            Console.WriteLine("------------------------");
        }


        public static void DisplayListConnections(IList<Connector> collection)
        {
            Console.WriteLine("------------------------");
            foreach (var item in collection)
            {
                Console.WriteLine($"Connector.CK_ID : {item.CK_Id} -ChargeStationId : {item.ChargeStationId} -MaxCurrent : {item.MaxCurrent}");
            }
            Console.WriteLine("Total deleted : " + collection.Sum(e=>e.MaxCurrent));

            Console.WriteLine("------------------------");
        }


    }
}
