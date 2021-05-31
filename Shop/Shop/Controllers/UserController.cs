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
        public IActionResult Cabinet()
        {
            return View(userService.GetUser(User.Identity.Name));
        }

        [Authorize]
        public IActionResult Cabinet(UserModel user)
        {
            if (ModelState.IsValid &&( userService.CheckUniqueEmail(user.Email) || user.Email.CompareTo(User.Identity.Name) == 0))
            {
                userService.ChangeData(user.Id, user.Email, null, user.Login);
                return View(user);
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
        public IActionResult ChangePassword(PasswordModel passwordModel)
        {
            var user = userService.GetUser(User.Identity.Name);
            if (ModelState.IsValid && userService.CheckPassword(user.Id,passwordModel.Password))
            {
               userService.ChangeData(user.Id, null, passwordModel.NewPassword, null);
                return RedirectToAction("Cabinet");
            }
            ModelState.AddModelError("", "Incorect data");
            return View(passwordModel);
        }
        [Authorize]
        public IActionResult  History(int page = 1)
        {
            var purchases = userService.GetPurchases(User.Identity.Name,page,2); 
            return View(purchases);
        }

        [HttpPost]
        public void ChangeProduct([FromBody] Data value)
        {
            List<ProductViewModel> cart = SessionHelper.GetObjectFromJson<List<ProductViewModel>>(HttpContext.Session, "cart") ?? new List<ProductViewModel>();
            var product = cart.FirstOrDefault(i => i.Id == int.Parse(value.Id));
            product.Amount = int.Parse(value.Amount);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
        }

        public IActionResult AddProduct(string id)
        {
            List<ProductViewModel> cart = SessionHelper.GetObjectFromJson<List<ProductViewModel>>(HttpContext.Session, "cart")?? new List<ProductViewModel>();
            var product = cart.FirstOrDefault(i => i.Id == int.Parse(id));
                if (product != null)
                {
                    product.Amount++;
                }
                else
                {
                    product = purchaseService.GetProductById(int.Parse(id));
                    cart.Add(product);
                }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Basket");
        }

        public IActionResult RemoveProduct(string id)
        {
            List<ProductViewModel> cart = SessionHelper.GetObjectFromJson<List<ProductViewModel>>(HttpContext.Session, "cart")?? new List<ProductViewModel>();
            
            cart.Remove(cart.FirstOrDefault(i => i.Id == int.Parse(id)));
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Basket");
        }

        public IActionResult Basket()
        {
            var cart = SessionHelper.GetObjectFromJson<List<ProductViewModel>>(HttpContext.Session, "cart")?? new List<ProductViewModel>();         
            
            ViewBag.total = cart.Sum(i => i.Price * i.Amount);
            
            return View(cart);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Buy()
        {
            List<ProductViewModel> cart = SessionHelper.GetObjectFromJson<List<ProductViewModel>>(HttpContext.Session, "cart");
            if(cart == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (User?.Identity?.Name == null)
            {
                return RedirectToAction("Login","Authorization");
            }
            purchaseService.Buy(User.Identity.Name, cart);
            return RedirectToAction("History");
        }
    }

}
