using Microsoft.AspNetCore.Mvc;
using Shop.Interfaces;
using Shop.Models;
using Shop.Models.ViewModels;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        IShopService service;
        public HomeController(IShopService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index(int? type, string name, int page = 1, SortState sort = SortState.PriceAsc)
        {
            var viewModel = await service.LoadProductsAsync(type,name,page ,sort, 3);
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
