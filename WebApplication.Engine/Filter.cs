using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Engine
{
    public class Filter
    {
        public string? Address { get; set; }
        public int? MinRooms { get; set; }
        public int? MaxRooms { get; set; }
        public int? MinSalePrice { get; set; }
        public int? MaxSalePrice { get; set; }
        public int? MinRentPrice { get; set; }
        public int? MaxRentPrice { get; set; }
        public int? MinArea { get; set; }
        public int? MaxArea { get; set; }

        public bool? OrderBy { get; set; }

        public int? AgentID { get; set; }

        public int? Limit { get; set; }
        public int? Page { get; set; }
        public int? Count { get; set; }
        public bool? Editing { get; set; }
        public bool? Home { get; set; }
        public bool? Large { get; set; }
        public OptionType? Option { get; set; }

        [Flags]
        public enum OptionType
        {
            Undefined = 0,
            Sale = 1,
            Rent = 2,
        }
        public static OptionType ParseOption(string value)
        {
            bool hasOption = OptionType.TryParse(value, out OptionType _option);

            if (!hasOption)
                return OptionType.Undefined;

            return _option;
        }


        public List<Realestate> List { get; set; }
        public List<int> StaticList { get; set; }

        public Filter()
        {
        }

        public void FillStatic(Filter filter)
        {
            Address = filter.Address;
            if (filter.Large != null) Large = filter.Large;
            if (filter.Limit != null && filter.Limit > 0) Limit = filter.Limit;
            if (filter.MinRooms != null) MinRooms = filter.MinRooms;
            if (filter.MaxRooms != null) MaxRooms = filter.MaxRooms;
            if (filter.MinSalePrice != null) MinSalePrice = filter.MinSalePrice;
            if (filter.MaxSalePrice != null) MaxSalePrice = filter.MaxSalePrice;
            if (filter.MinRentPrice != null) MinRentPrice = filter.MinRentPrice;
            if (filter.MaxRentPrice != null) MaxRentPrice = filter.MaxRentPrice;
            if (filter.MinArea != null) MinArea = filter.MinArea;
            if (filter.MaxArea != null) MaxArea = filter.MaxArea;
            if (filter.OrderBy != null) OrderBy = filter.OrderBy;
            if (filter.AgentID != null) AgentID = filter.AgentID;
            if (filter.Limit != null) Limit = filter.Limit;
            if (filter.Page != null) Page = filter.Page;
            if (filter.Count != null) Count = filter.Count;
            if (filter.Editing != null) Editing = filter.Editing;
            if (filter.Home != null) Home = filter.Home;
            if (filter.Large != null) Large = filter.Large;
            if (filter.Option != null) Option = filter.Option;
        }

        public void Populate()
        {
            List<Realestate> rsList = Realestate.GetRealestates(this);

            if (OrderBy.HasValue)
                rsList.OrderBy(item => item.ID);


            List<int> tempList = rsList.Select(x => x.ID).ToList();
            
            StaticList = tempList;

            if (StaticList.ToString() !=  tempList.ToString())
                Page = 0;

            Count = rsList.Count;
            
            if (Limit.HasValue && Page.HasValue && rsList.Count > 0)
            {
                int startIndex = ((int)Page * (int)Limit);
                int checkMaxCount = (startIndex + (int)Limit) - (int)Count - 1;
                int countItems = checkMaxCount > 0 ? checkMaxCount : (int)Limit;

                rsList = rsList.GetRange(startIndex, countItems);
            }

            List = rsList;
        }
    }
}
