#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication.Engine;
using WebApplication1.Models;

namespace WebApplication1.Components
{
    [ViewComponent(Name = "RealestateFilter")]
    public class RealestateFilterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Filter filter = Filter.GetFilter();
            filter.Populate();

            filter.Filtrar();

            return View(filter);
        }
    }

    [ViewComponent(Name = "RealestateList")]
    public class RealestateListViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(Filter.GetFilter());
        }
    }

    [ViewComponent(Name = "RealestatePagination")]
    public class RealestatePaginationViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(Filter.GetFilter());
        }
    }


}

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously