using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CedFCIC.Models;


namespace CedFCIC.ViewComponents
{
    public class PagerViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PagedResultBase result)
        {
            return Task.FromResult((IViewComponentResult)View("Default", result));
        }
    }
}
