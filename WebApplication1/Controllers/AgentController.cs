using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Engine;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AgentController : Controller
    {
        // GET: AgentController
        public ActionResult List()
        {
            return View();
        }

        // GET: AgentController/Details/5
        public ActionResult Details(int id)
        {
            if (id > 0)
            {
                AgentModel model = AgentModel.GetAgentModel(id);
                Filter filter = new Filter()
                {
                    Limit = 6,
                    AgentID = model.ID,
                    Large = false,
                };

                filter.FillStatic(filter);
                
                if (model != null)
                    return View(model);

            }
            return NotFound();
        }

        // GET: AgentController/Create
        public ActionResult Create()
        {
            AgentModel model = new AgentModel();
            return View(model);
        }

        // POST: AgentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Agent agent = new Agent()
                {
                    Name= collection["name"],
                    Email = collection["email"],
                    Phone= collection["Phone"],
                    Address = collection["Address"],
                    Picture = collection["Picture"],
                };

                Agent.InserOrUpdateAgent(agent);
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }

        // GET: AgentController/Edit/5
        public ActionResult Edit(int id)
        {
            if (id > 0)
            {
                AgentModel model = AgentModel.GetAgentModel(id);
                if (model != null)
                   return View(model);

            }
            return NotFound();
        }

        // POST: AgentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                Agent agent = new Agent()
                {
                    ID = id,
                    Name= collection["Name"],
                    Email = collection["Email"],
                    Phone= collection["Phone"],
                    Address = collection["Address"],
                    Picture = collection["Picture"],
                };

                Agent.InserOrUpdateAgent(agent);
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var rs = Realestate.GetRealestate(id);
            var AgentID = rs.GetAgent().ID;

            rs.RemoveAgent();

            return RedirectToAction(nameof(Edit), new { id = AgentID });
        }
    }
}
