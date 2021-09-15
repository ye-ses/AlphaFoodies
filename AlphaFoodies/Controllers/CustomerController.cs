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
        [HttpGet]
        public ActionResult addItem(int id)
        {
            var list = from a in context.MenuItems
                       where a.Item_Code.Equals(id)
                       select a;
            MenuItem curItem = list.FirstOrDefault();
            if (curItem!=null)
            {
                List<OrderItem> orders;
                if (Session["cart"] == null)
                {
                    orders = new List<OrderItem>(); 
                    OrderItem item = new OrderItem
                    {
                        MenuItem = curItem,
                        Item_Code = curItem.Item_Code,
                        Quantity = 1
                    };
                    //context.OrderItems.Add(item); 
                    Session["cart"] = orders; 
                }
                else
                {
                    bool yes = false;
                    orders = (List<OrderItem>)Session["cart"];
                    for (int i = 0; i < orders.Count; i++)
                    {
                        if (orders[i].MenuItem.Item_Name.Equals(curItem.Item_Name))
                        {
                            orders[i].Quantity += 1;
                            yes = true;
                            break;
                        }
                    }
                    if (!yes)
                    {
                        OrderItem item = new OrderItem
                        {
                            MenuItem = curItem,
                            Item_Code = curItem.Item_Code,
                            Quantity = 1
                        };
                        orders.Add(item);
                        //context.OrderItems.Add(item);
                    }
                }
                    //context.Entry(curItem).State = System.Data.Entity.EntityState.Modified;
                    //context.SaveChanges();
                }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult viewCart()
        {
            return View((List<OrderItem>)Session["cart"]);
        }
        public ActionResult increament(int id)
        {
            var list = from a in context.MenuItems
                       where a.Item_Code.Equals(id)
                       select a;
            MenuItem curItem = list.FirstOrDefault();
            bool found = false;
            int trace = -1;
            List<OrderItem> orders = (List<OrderItem>)Session["cart"];
            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i].MenuItem.Item_Name.Equals(curItem.Item_Name))
                {
                    orders[i].Quantity += 1;
                    trace = i;
                    found = true;
                    break;
                }
            } 
            Session["cart"] = orders;
            return RedirectToAction("viewCart");
        } 
        public ActionResult Remove(int id)
        {
            var list = from a in context.MenuItems
                       where a.Item_Code.Equals(id)
                       select a;
            MenuItem curItem = list.FirstOrDefault();
            bool found = false;
            int trace = -1;
            List<OrderItem> orders = (List<OrderItem>)Session["cart"];
            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i].MenuItem.Item_Name.Equals(curItem.Item_Name))
                {
                    orders[i].Quantity -= 1;
                    trace = i;
                    found = true;
                    break;
                }
            }
            if (found)
            {
                if (orders[trace].Quantity == 0)
                {
                    orders.RemoveAt(trace);
                }
            }
            Session["cart"] = orders;
            return RedirectToAction("viewCart");
        } 
        public ActionResult RemoveAll(OrderItem item)
        { 
            List<OrderItem> orders = (List<OrderItem>)Session["cart"];
            orders.Clear();
            Session["cart"] = orders;
            return RedirectToAction("viewCart");
        }
    }
}