using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Engine
{
    public class Agent
    {
        private static List<Agent> Database { get; } = new List<Agent>()
        {
            new Agent
            {
                ID = 1,
                Name = "Carlinos Brow",
                Email = "Carlinhos@brown.pt",
                Phone = "913 321 412",
                Address = "Rua das Pedras, Lote 13 F, Leiria",
                Picture = "Rua das Pedras, Lote 13 F, Leiria"
            },
            new Agent
            {
                ID = 2,
                Name = "Santigo Coupe",
                Email = "Santigo@Coupe.pt",
                Phone = "321 412 913",
                Address = "Rua das Cimento, Lote 13 F, Porto",
                Picture = "Rua das Cimento, Lote 13 F, Porto"
            },
            new Agent
            {
                ID = 3,
                Name = "Ketlhen Nascimento",
                Email = "Ketlhen@Nascimento.pt",
                Phone = "913 412 797",
                Address = "Rua das Folhas, Lote 13 F, Lisboa",
                Picture = "Rua das Folhas, Lote 13 F, Lisboa"
            },
            new Agent
            {
                ID = 4,
                Name = "Lucia Pereira",
                Email = "Lucia@Pereira.pt",
                Phone = "913 648 485",
                Address = "Rua das Local, Lote 13 F, Caldas da Rainha",
                Picture = "Rua das Local, Lote 13 F, Caldas da Rainha"
            },
        };

        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Picture { get; set; }


        public List<Realestate> Realestates() => Realestate.FindRealestateByAgentID(ID);

        public static Agent FindAgentByID(int agentID) => Database.Where(x => x.ID == agentID).SingleOrDefault();

        public static void InserOrUpdateAgent(Agent agent)
        {
            if (agent.ID > 0)
            {
                UpdateAgent(agent);
            }
            else
            {
                InsertAgent(agent);
            }
        }
        private static void InsertAgent(Agent agent)
        {
            int lastID = Database.OrderBy(x => x.ID).Select(x => x.ID).LastOrDefault();

            if (lastID == 0)
                lastID = 1;
            else
                lastID++;

            agent.ID = lastID;

            Database.Add(agent);
        }

        private static void UpdateAgent(Agent agent)
        {
            Agent currentAgent = Database.Single(item => item.ID == agent.ID);

            if (Database.Remove(currentAgent))
            {
                Database.Add(agent);
            }

        }

        public static Agent GetAgent(int id)
        {
            return Database.SingleOrDefault(item => item.ID.Equals(id));
        }

        public static List<Agent> GetAgents()
        {
            List<Agent> agents = new List<Agent>();

            Database.ForEach(item => agents.Add(item));

            return agents;
        }

    }
}
