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

        [HttpGet]
        public ActionResult add_Staff()
        {
            return View();
        }

        [HttpPost]
        public ActionResult add_Staff(HttpPostedFileBase thePicture)
        {
            var firstname = Request.Form["firstname"];
            var lastname = Request.Form["lastname"];
            var email_Address = Request.Form["email_address"];
            var phone = Request.Form["phone_number"];
            var type = Request.Form["user_type"];

            /*The objects are different, hence we need to verify what type of a user is this one being created*/
            if (type.Equals("waiter"))
            {   
                Waiter new_waiter = new Waiter();
                new_waiter.Firstname = firstname;
                new_waiter.Lastname = lastname;
                new_waiter.Email_Address = email_Address;
                new_waiter.Phone = phone;
                if (thePicture != null)
                {
                    new_waiter.Image = new byte[thePicture.ContentLength];  /*Convert the image to a byte*/
                    thePicture.InputStream.Read(new_waiter.Image, 0, thePicture.ContentLength);
                }
                model.Waiters.Add(new_waiter);
                model.SaveChanges();
            }
            else
            {
                Chef new_Chef = new Chef();
                new_Chef.Firstname = firstname;
                new_Chef.Lastname = lastname;
                new_Chef.Email_Address = email_Address;
                new_Chef.Phone = phone;
                new_Chef.Password = Request.Form["password"];
                if (thePicture != null)
                {
                    new_Chef.Image = new byte[thePicture.ContentLength];
                    thePicture.InputStream.Read(new_Chef.Image, 0, thePicture.ContentLength);
                }
                model.Chefs.Add(new_Chef);
                model.SaveChanges();
            }

            return View();
        }
    }
}