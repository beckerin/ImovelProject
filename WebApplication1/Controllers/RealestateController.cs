using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using IO = System.IO;
using System.Reflection;
using WebApplication.Engine;
using WebApplication1.Models;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System;
using Microsoft.Extensions.Primitives;

namespace WebApplication1.Controllers
{
    public class RealestateController : Controller
    {

        // GET: RealestateController
        public ActionResult List(Filter filter)
        {
            if (filter == null)
            {
                filter = new Filter();
            }

            filter.Limit = 4;
            filter.Large = true;
            filter.Home = false;
            filter.Page = 0;

            filter.Populate();

            return View(filter);
        }

        // GET: Realestate/Details/5
        public ActionResult Details(int realstateID)
        {
            return View(Realestate.GetRealestate(realstateID));
        }

        // GET: RealestateController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Realestate/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                _ = int.TryParse(collection["Rooms"], out int rooms);
                _ = int.TryParse(collection["SalePrice"], out int salePrice);
                _ = int.TryParse(collection["RentPrice"], out int rentPrice);
                _ = int.TryParse(collection["Area"], out int area);
                _ = int.TryParse(collection["AgentID"], out int agentID);
                List<string> pics = new List<string>();



                Realestate rs = new()
                {
                    Title = collection["Title"],
                    Description = collection["Description"],
                    Rooms = rooms,
                    SalePrice = salePrice,
                    RentPrice = rentPrice,
                    Area = area,
                    Address  = collection["Address"],
                    AgentID = agentID
                };

                rs.PopulateImages(collection.Files);

                Realestate.InsertOrUpdate(rs);
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }

        // GET: RealestateController/Edit/5
        public ActionResult Edit(int id)
        {
            Realestate realesate = Realestate.GetRealestate(id);

            if (realesate == null)
            {
                return NotFound();
            }

            RealestateModel model = new RealestateModel(realesate);

            return View(model);
        }

        // POST: RealestateController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, IFormCollection collection)
        {
            try
            {
                _ = int.TryParse(collection["Rooms"], out int rooms);
                _ = int.TryParse(collection["SalePrice"], out int salePrice);
                _ = int.TryParse(collection["RentPrice"], out int rentPrice);
                _ = int.TryParse(collection["Area"], out int area);
                _ = int.TryParse(collection["AgentID"], out int agentID);
                List<string> pics = new List<string>();

                Realestate rs = Realestate.GetRealestate(id);

                if (rs == null)
                {
                    return NotFound();
                }

                rs.PopulateImages(collection.Files);

                rs.Title = collection["Title"];
                rs.Description = collection["Description"];
                rs.Rooms = rooms;
                rs.SalePrice = salePrice;
                rs.RentPrice = rentPrice;
                rs.Area = area;
                rs.Address  = collection["Address"];

                rs.AgentID = agentID;


                //Realestate.InsertOrUpdate(rs);
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult RequestFilter(IFormCollection collection)
        {
            Filter filter = new Filter();
            FillBasedOnRequest(filter, collection);
            filter.Populate();
            return ViewComponent("RealestateFilter", filter);
        }
        public IActionResult RequestList(Filter filter)
        {
            return ViewComponent("RealestateList", filter);
        }

        public IActionResult RequestPagination(Filter filter)
        {
            return ViewComponent("RealestatePagination", filter);
        }

        public IActionResult GetRealestatePicture(int id)
        {
            Realestate rs = Realestate.GetRealestate(id);

            if (rs==null)
                return NoContent();


            var firstPicture = rs.GetPicturePaths().FirstOrDefault();


            if (string.IsNullOrEmpty(firstPicture))
                return NoContent();

            byte[] fileBytes = IO.File.ReadAllBytes(firstPicture);

            return File(fileBytes, "image/jpg");
        }


        public void FillBasedOnRequest(object atype, IFormCollection collection)
        {
            Type t = atype.GetType();
            PropertyInfo[] props = t.GetProperties();
            foreach (PropertyInfo prp in props)
            {
                if (!string.IsNullOrEmpty(collection[prp.Name]))
                {
                    var target = t.GetProperty(prp.Name);
                    var type = target.PropertyType.FullName;

                    if (type.Contains("String"))
                        target.SetValue(atype, collection[prp.Name].ToString(), null);
                    if (type.Contains("Int32"))
                    {
                        _= int.TryParse(collection[prp.Name], out int value);
                        target.SetValue(atype, value, null);
                    }
                    if (type.Contains("Boolean"))
                    {
                        _= bool.TryParse(collection[prp.Name], out bool value);
                        target.SetValue(atype, value, null);
                    }
                    if (type.Contains("OptionType"))
                    {
                        target.SetValue(atype, Filter.ParseOption(collection[prp.Name]), null);
                    }

                }
            }
        }
    }
}
