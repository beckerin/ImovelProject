using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static WebApplication.Engine.Filter;

namespace WebApplication.Engine
{
    public class Realestate
    {
        private static List<Realestate> Database { get; } = new List<Realestate>()
        {

             new Realestate
             {
                  ID =1,
                  Title = "Moradia T1 ID1",
                  Description ="Moradia ao lado da praia",
                  Address ="Rua Juscelino",
                  Area = 400,
                  RentPrice = 500,
                  Rooms = 1,
                  AgentID = 1,
                  Pictures = new List<string>(){ "C:\\Users\\diego.paiva\\AppData\\Local\\Temp\\1\\1.jfif" }
             },new Realestate
             {
                  ID =2,
                  Title = "Moradia T2 ID2",
                  Description ="Moradia ao lado da praia",
                  Address ="Rua Dr. Nathalie",
                  Area = 400,
                  RentPrice = 500,
                  SalePrice = 500.000,
                  Rooms = 2,
                  AgentID = 1,
                  Pictures = new List<string>(){ "C:\\Users\\diego.paiva\\AppData\\Local\\Temp\\1\\2.jfif" }
             },
             new Realestate
             {
                  ID =3,
                  Title = "Moradia T3 ID3",
                  Description ="Moradia ao lado da praia",
                  Address ="Avenida Segundaria, Caldas da Rainha",
                  Area = 600,
                  SalePrice = 1000,
                  Rooms = 3,
                  AgentID = 1,
                  Pictures = new List<string>(){ "C:\\Users\\diego.paiva\\AppData\\Local\\Temp\\1\\3.jfif" }
             },

             new Realestate
             {
                  ID =4,
                  Title = "Moradia T4 ID4",
                  Description ="Moradia ao lado da praia",
                  Address ="Rua Dr. Mendes",
                  Area = 400,
                  RentPrice = 500,
                  Rooms = 4,
                  AgentID = 1,
                  Pictures = new List<string>(){ "C:\\Users\\diego.paiva\\AppData\\Local\\Temp\\1\\4.jfif" }
             },new Realestate
             {
                  ID =5,
                  Title = "Moradia T5 ID",
                  Description ="Moradia ao lado da praia",
                  Address ="Rua Dr. Frederico",
                  Area = 400,
                  RentPrice = 500,
                  SalePrice = 500.000,
                  Rooms = 5,
                  AgentID = 1,
                  Pictures = new List<string>(){ "C:\\Users\\diego.paiva\\AppData\\Local\\Temp\\1\\5.jfif" }
             },
             new Realestate
             {
                  ID =6,
                  Title = "Moradia T5 Algarve ID6",
                  Description ="Moradia ao lado da praia",
                  Address ="Rua Dr. Frederico Ramos Mendes",
                  Area = 600,
                  SalePrice = 1000,
                  Rooms = 5,
                  AgentID = 1,
                  Pictures = new List<string>(){ "C:\\Users\\diego.paiva\\AppData\\Local\\Temp\\1\\6.jfif" }
             },
             new Realestate
             {
                  ID =7,
                  Title = "Moradia T1 ID7",
                  Description ="Moradia ao lado da praia",
                  Address ="Rua Juscelino",
                  Area = 400,
                  RentPrice = 500,
                  Rooms = 1,
                  AgentID = 1,
                  Pictures = new List<string>(){ "C:\\Users\\diego.paiva\\AppData\\Local\\Temp\\1\\1.jfif" }
             },new Realestate
             {
                  ID =8,
                  Title = "Moradia T2 ID8",
                  Description ="Moradia ao lado da praia",
                  Address ="Rua Dr. Nathalie",
                  Area = 400,
                  RentPrice = 500,
                  SalePrice = 500.000,
                  Rooms = 2,
                  AgentID = 1,
                  Pictures = new List<string>(){ "C:\\Users\\diego.paiva\\AppData\\Local\\Temp\\1\\2.jfif" }
             },
             new Realestate
             {
                  ID =9,
                  Title = "Moradia T3 ID9",
                  Description ="Moradia ao lado da praia",
                  Address ="Avenida Segundaria, Caldas da Rainha",
                  Area = 600,
                  SalePrice = 1000,
                  Rooms = 3,
                  AgentID = 1,
                  Pictures = new List<string>(){ "C:\\Users\\diego.paiva\\AppData\\Local\\Temp\\1\\3.jfif" }
             },

             new Realestate
             {
                  ID =10,
                  Title = "Moradia T4 ID10",
                  Description ="Moradia ao lado da praia",
                  Address ="Rua Dr. Mendes",
                  Area = 400,
                  RentPrice = 500,
                  Rooms = 4,
                  AgentID = 1,
                  Pictures = new List<string>(){ "C:\\Users\\diego.paiva\\AppData\\Local\\Temp\\1\\4.jfif" }
             },new Realestate
             {
                  ID =11,
                  Title = "Moradia T5 ID11",
                  Description ="Moradia ao lado da praia",
                  Address ="Rua Dr. Frederico",
                  Area = 400,
                  RentPrice = 500,
                  SalePrice = 500.000,
                  Rooms = 5,
                  AgentID = 1,
                  Pictures = new List<string>(){ "C:\\Users\\diego.paiva\\AppData\\Local\\Temp\\1\\5.jfif" }
             },
             new Realestate
             {
                  ID =12,
                  Title = "Moradia T5 Algarve ID12",
                  Description ="Moradia ao lado da praia",
                  Address ="Rua Dr. Frederico Ramos Mendes",
                  Area = 600,
                  SalePrice = 1000,
                  Rooms = 5,
                  AgentID = 1,
                  Pictures = new List<string>(){ "C:\\Users\\diego.paiva\\AppData\\Local\\Temp\\1\\6.jfif" }
             },
        };

        public int ID { get; set; }

        public int? Rooms { get; set; }
        public double SalePrice { get; set; }
        public double RentPrice { get; set; }
        public int Area { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }

        private List<string> Pictures { get; set; } = new List<string>();

        public int? AgentID { get; set; }


        public Agent GetAgent()
        {
            if (!AgentID.HasValue || AgentID <= 0)
            {
                return null;
            }

            return Agent.FindAgentByID(AgentID.Value);
        }
        public static List<Realestate> FindRealestateByAgentID(int agentID)
        {
            return Database.FindAll(x => x.AgentID == agentID).ToList();
        }

        public void InsertPicture(string value)
        {
            Pictures.Add(value);
            _RealestatePictures = null;
        }

        public void InsertPictures(List<string> values)
        {
            Pictures.AddRange(values);
            _RealestatePictures = null;
        }

        private List<string> _RealestatePictures;

        public List<string> GetPicturePaths() => Pictures;

        public List<string> GetBase64Pictures()
        {
            if (_RealestatePictures != null)
            {
                return _RealestatePictures;
            }

            _RealestatePictures = new List<string>();

            foreach (string path in Pictures)
            {
                if (!File.Exists(path))
                    continue;

                byte[] bytes = File.ReadAllBytes(path);

                string base64 = Convert.ToBase64String(bytes);
                _RealestatePictures.Add(string.Concat("data:image/png;base64,", base64));
            }

            return _RealestatePictures;

        }


        public static void InsertOrUpdate(Realestate realestate)
        {
            if (realestate.ID > 0)
            {
                //Security needed
                UpdateRealstate(realestate);
            }
            else
            {
                InsertRealestate(realestate);
            }

        }
        private static void InsertRealestate(Realestate realestate)
        {
            int lastID = Database.OrderBy(x => x.ID).Select(x => x.ID).LastOrDefault();

            if (lastID == 0)
                lastID = 1;
            else
                lastID++;

            realestate.ID = lastID;

            Database.Add(realestate);
        }

        public static Realestate GetRealestate(int RealestateID)
        {
            return Database.SingleOrDefault(x => x.ID.Equals(RealestateID));
        }

        public static List<Realestate> GetRealestates(Filter filter)
        {
            IQueryable<Realestate> realestates = Database.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Address))
            {
                realestates = realestates.Where(x => x.Address.Contains(filter.Address, StringComparison.OrdinalIgnoreCase));
            }
            if (filter.MaxRooms > 0 && filter.MinRooms > 0)
            {
                realestates = realestates.Where(x => x.Rooms >= filter.MinRooms && x.Rooms <=filter.MaxRooms);
            }

            if (filter.Option.HasValue && filter.Option != Filter.OptionType.Undefined)
            {
                if (filter.Option == OptionType.Sale)
                    realestates = realestates.Where(x => x.SalePrice > 0);
                else
                    realestates = realestates.Where(x => x.RentPrice > 0);
            }

            if (filter.AgentID.HasValue && filter.AgentID != 0)
                realestates = realestates.Where(x => x.AgentID == filter.AgentID);

            return realestates.ToList();
        }

        private static void UpdateRealstate(Realestate realestate)
        {
            Realestate currentRealstate = Database.Single(x => x.ID == realestate.ID);

            if (Database.Remove(currentRealstate))
            {
                Database.Add(realestate);
            }
        }


        public void RemoveAgent()
        {
            AgentID = null;
        }
    }
}
