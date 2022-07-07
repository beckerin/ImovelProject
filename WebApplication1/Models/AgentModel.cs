using System.ComponentModel.DataAnnotations;
using WebApplication.Engine;

namespace WebApplication1.Models
{
    public class AgentModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Picture { get; set; }


        public AgentModel()
        {
        }

        public AgentModel(Agent agent)
        {
            ID = agent.ID;
            Name = agent.Name;
            Email = agent.Email;    
            Phone = agent.Phone;    
            Address = agent.Address;
            Picture = agent.Picture;
        }

        public static AgentModel GetAgentModel(int id)
        {
            Agent agent = Agent.GetAgent(id);

            if (agent == null) return null;

            AgentModel model = new AgentModel(agent);

            return model;
        }

    }
}
