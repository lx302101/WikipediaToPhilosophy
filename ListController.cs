using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebsiteForWikiToPhil.Models;
using System.Web;

namespace WebsiteForWikiToPhil.Controllers
{
    public class ListController : Controller
    {
        public IActionResult WebList()
        {
            WebList test = new WebList();

            return View(test);
        }
        
        [HttpPost]
        public IActionResult WebList(WebList model) //takes input value.
        {
            model.createList();

            return View(model); 
        }

        

    }
}
