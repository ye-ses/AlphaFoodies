using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlphaFoodies.Models;

namespace AlphaFoodies.Controllers
{
    public class AdminController : Controller
    {

        private  ModelContext model = new ModelContext();
        
        [HttpGet]
        public ActionResult AddMenuItem()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddMenuItem(MenuItem newItem, HttpPostedFileBase thePicture)
        {
            if (thePicture != null)
            {     
                newItem.Picture = new byte[thePicture.ContentLength];  //converts the image to binary
                thePicture.InputStream.Read(newItem.Picture, 0, thePicture.ContentLength);
            }
            model.MenuItems.Add(newItem);
            model.SaveChanges();
            return View();
        }

        /*View menu items, action methods*/

        public ActionResult ViewMenu()
        {
            return View(model.MenuItems.ToList());
        }

    }
}