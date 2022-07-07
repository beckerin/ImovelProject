using System.ComponentModel.DataAnnotations;
using WebApplication.Engine;

namespace WebApplication1.Models
{
    public class RealestateModel
    {
        public int ID { get; set; }

        public int? Rooms { get; set; }
        public double SalePrice { get; set; }
        public double RentPrice { get; set; }
        public int Area { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        
        public int? AgentID { get; set; }


        public RealestateModel()
        {
        }

        public RealestateModel(Realestate realestate)
        {
            ID = realestate.ID;
            Title = realestate.Title;
            Description = realestate.Description;
            Rooms = realestate.Rooms;
            Area = realestate.Area; 
            Address = realestate.Address;
            SalePrice = realestate.SalePrice;
            RentPrice = realestate.RentPrice;
            AgentID = realestate.AgentID;

        }

    }
}
