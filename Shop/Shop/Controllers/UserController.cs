using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Extension;
using Shop.Interfaces;
using Shop.Models;
using Shop.Models.ViewModels;
using Shop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class Data
    {
        public string Id { get; set; }
        public string Amount { get; set; }
    }


    public class UserController : Controller
    {

        IPurchaseService purchaseService;
        IUserService userService;

        public UserController(IPurchaseService service, IUserService userService)
        {
            this.purchaseService = service;
            this.userService = userService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Cabinet()
        {
            return View( await userService.GetUserAsync(User.Identity.Name));
        }

        [Authorize]
        public async Task<IActionResult> Cabinet(UserModel user)
        {
            if (ModelState.IsValid)
            {
                if(!await userService.ChangeDataAsync(user.Email, null, user.Login , User.Identity.Name))
                {
                    ModelState.AddModelError("", "Incorect data. Change!!!");
                }
                else
                {
                    return View(user);
                }
            }
            ModelState.AddModelError("", "Incorect data");
            return View(user);
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(PasswordModel passwordModel)
        {
            if (ModelState.IsValid)
            {
                if(await userService.ChangeDataAsync(null, passwordModel.NewPassword, null, User.Identity.Name))
                {
                    ModelState.AddModelError("", "Incorect data. Repeat again!");
                }
                else
                {
                    return RedirectToAction("Cabinet");
                } 
            }
            ModelState.AddModelError("", "Incorect data");
            return View(passwordModel);
        }
        [Authorize]
        public async Task<IActionResult>  History(int page = 1)
        {
            var purchases = await userService.GetPurchasesAsync(User.Identity.Name,page,2); 
            return View(purchases);
        }

        [HttpPost]
        public async Task ChangeProduct([FromBody] Data value)
        {
            List<ProductViewModel> cart = await SessionHelper.GetObjectFromJsonAsync<List<ProductViewModel>>(HttpContext.Session, "cart") ?? new List<ProductViewModel>();
            var product = cart.FirstOrDefault(i => i.Id == int.Parse(value.Id));
            product.Amount = int.Parse(value.Amount);
            await SessionHelper.SetObjectAsJsonAsync(HttpContext.Session, "cart", cart);
        }

        public async Task<IActionResult> AddProduct(string id)
        {
            List<ProductViewModel> cart = await SessionHelper.GetObjectFromJsonAsync<List<ProductViewModel>>(HttpContext.Session, "cart")?? new List<ProductViewModel>();
            var product = cart.FirstOrDefault(i => i.Id == int.Parse(id));
                if (product != null)
                {
                    product.Amount++;
                }
                else
                {
                    product = await purchaseService.GetProductByIdAsync(int.Parse(id));
                    cart.Add(product);
                }
            await SessionHelper.SetObjectAsJsonAsync(HttpContext.Session, "cart", cart);
            return RedirectToAction("Basket");
        }

        public async Task<IActionResult> RemoveProduct(string id)
        {
            List<ProductViewModel> cart = await SessionHelper.GetObjectFromJsonAsync<List<ProductViewModel>>(HttpContext.Session, "cart") ?? new List<ProductViewModel>();
            
            cart.Remove(cart.FirstOrDefault(i => i.Id == int.Parse(id)));
            await SessionHelper .SetObjectAsJsonAsync(HttpContext.Session, "cart", cart);
            return RedirectToAction("Basket");
        }

        public async Task<IActionResult> Basket()
        {
            var cart = await SessionHelper.GetObjectFromJsonAsync<List<ProductViewModel>>(HttpContext.Session, "cart")?? new List<ProductViewModel>();         
            
            ViewBag.total = cart.Sum(i => i.Price * i.Amount);
            
            return View(cart);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Buy()
        {
            List<ProductViewModel> cart = await SessionHelper.GetObjectFromJsonAsync<List<ProductViewModel>>(HttpContext.Session, "cart");
            if(cart == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (User?.Identity?.Name == null)
            {
                return RedirectToAction("Login","Authorization");
            }
            await purchaseService.BuyAsync(User.Identity.Name, cart);
            return RedirectToAction("History");
        }
    }

}
