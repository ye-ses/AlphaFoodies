using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlphaFoodies.Models;
namespace AlphaFoodies.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        private ModelContext context = new ModelContext();
        public ActionResult Index()
        {
            return View(context.MenuItems.ToList());
        } 
       
    }
}