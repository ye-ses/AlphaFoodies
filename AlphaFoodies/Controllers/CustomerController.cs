﻿using System;
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
        [HttpGet]
        public ActionResult Index(string text)
        {
            ViewBag.text = text; 
            var list = context.MenuItems.Where(x => x.Item_Name.Contains(text) || text == null).ToList();//a list of searched items or all items 
            ViewBag.found = "Search results for '" + text + "' .";//message to be displayed if searched items were found 
            if (list.Count() > 0)
            {
                return View(list); //returns a customer view and selected items
            }
            else
            {
                ViewBag.notfound = "The searched item '" /*message to be displated if no items were found while searching*/
                + text
                + "' is either not available or not sold in this resturant, keep browsing the menu to see available items";
                TempData["notFound"] = "The searched item '" /*message to be displated if no items were found while searching*/
                + text
                + "' is either not available or not sold in this resturant, keep browsing the menu to see available items"; 
                return RedirectToAction("Index");
            }
        } 
       
    }
}