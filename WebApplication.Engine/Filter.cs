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
        public static Filter _filter { get; set; } = new Filter();

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
        public static List<int> StaticList { get; set; }

        public Filter()
        {
        }

        public static void FillStatic(Filter filter)
        {
            _filter.Address = filter.Address;
            if (filter.Large != null) _filter.Large = filter.Large;
            if (filter.Limit != null && filter.Limit > 0) _filter.Limit = filter.Limit;
            if (filter.MinRooms != null) _filter.MinRooms = filter.MinRooms;
            if (filter.MaxRooms != null) _filter.MaxRooms = filter.MaxRooms;
            if (filter.MinSalePrice != null) _filter.MinSalePrice = filter.MinSalePrice;
            if (filter.MaxSalePrice != null) _filter.MaxSalePrice = filter.MaxSalePrice;
            if (filter.MinRentPrice != null) _filter.MinRentPrice = filter.MinRentPrice;
            if (filter.MaxRentPrice != null) _filter.MaxRentPrice = filter.MaxRentPrice;
            if (filter.MinArea != null) _filter.MinArea = filter.MinArea;
            if (filter.MaxArea != null) _filter.MaxArea = filter.MaxArea;
            if (filter.OrderBy != null) _filter.OrderBy = filter.OrderBy;
            if (filter.AgentID != null) _filter.AgentID = filter.AgentID;
            if (filter.Limit != null) _filter.Limit = filter.Limit;
            if (filter.Page != null) _filter.Page = filter.Page;
            if (filter.Count != null) _filter.Count = filter.Count;
            if (filter.Editing != null) _filter.Editing = filter.Editing;
            if (filter.Home != null) _filter.Home = filter.Home;
            if (filter.Large != null) _filter.Large = filter.Large;
            if (filter.Option != null) _filter.Option = filter.Option;
        }
        public static Filter GetFilter()
        {
            return _filter;
        }
        public static void SetFilter(Filter value)
        {
            _filter = value;
        }
        public void Populate()
        {
            List = Realestate.GetRealestates(this);
        }

        public void Filtrar()
        {
            List<Realestate> rsList = Realestate.GetRealestates(this);

            //foreach (Realestate item in List)
            //{
            //    if (Address != null && !item.Address.ToLower().Contains(Address.ToLower()))
            //    {
            //        rsList.Remove(item);
            //        continue;

            //    }
            //    if (MaxRooms != 0 && MinRooms != 0)
            //    {
            //        if (item.Rooms > MaxRooms ||item.Rooms < MinRooms)
            //            rsList.Remove(item);
            //        continue;
            //    }
            //    else if (item.Rooms < MinRooms && MinRooms != 0)
            //    {
            //        rsList.Remove(item);
            //        continue;
            //    }
            //    else if (item.Rooms > MaxRooms && MaxRooms != 0)
            //    {
            //        rsList.Remove(item);
            //        continue;
            //    }

            //    if (Option.HasValue)
            //    {
            //        if (Option == OptionType.Undefined)
            //        {
            //            continue;
            //        }
            //        else if (item.SalePrice > 0 && Option != OptionType.Sale)
            //        {
            //            rsList.Remove(item);
            //            continue;
            //        }

            //        else if (item.RentPrice > 0 && Option != OptionType.Rent)
            //        {
            //            rsList.Remove(item);
            //            continue;
            //        }
            //    }

            //    if (AgentID.HasValue && AgentID != 0 && AgentID != item.AgentID)
            //    {
            //        rsList.Remove(item);
            //        continue;
            //    }
            //}
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
