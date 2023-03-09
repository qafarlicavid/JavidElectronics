using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JavidElectronics.Database;

namespace JavidElectronics.Areas.Admin.ViewComponents.Navbar
{
    [ViewComponent(Name = "NavbarHeader")]
    public class NavbarHeaderViewComponent : ViewComponent
    {
        private readonly DataContext _dataContext;
        public NavbarHeaderViewComponent(DataContext applicationDbContext)
        {
            _dataContext = applicationDbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = _dataContext.Navbars
                .Include(n => n.subNavbars.OrderBy(sn => sn.Order))
                .Where(n => n.IsOnHeader).OrderBy(n => n.Order).ToList();

            return View(model);

        }
    }
}
