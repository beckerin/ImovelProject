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
            filter.FillStatic(filter);
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

                if (collection.Files.Count > 0)
                {
                    foreach (IFormFile file in collection.Files)
                    {
                        //criamos o path do file
                        var tempFolder = IO.Path.Combine(IO.Path.GetTempPath(), id.ToString());

                        if (!IO.Directory.Exists(tempFolder))
                        {
                            IO.Directory.CreateDirectory(tempFolder);
                        }

                        var tempPath = IO.Path.Combine(tempFolder, file.FileName);

                        //vamos buscar a stream dele
                        using (IO.Stream stream = file.OpenReadStream())

                        //injetamos para um memoryStream para podermos manipular ou ir buscar todos os bytes de uma vez só
                        //using (var createdFile = IO.File.Create(tempPath))
                        //{
                        //    await stream.CopyToAsync(createdFile);
                        //}

                        using (IO.FileStream streamWriter = new IO.FileStream(tempPath, IO.FileMode.Create, IO.FileAccess.Write))
                        {
                            await stream.CopyToAsync(streamWriter);
                        }

                        rs.InsertPicture(tempPath);
                    }
                }

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

        public IActionResult RequestFilter(Filter filter)
        {
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
    }
}
