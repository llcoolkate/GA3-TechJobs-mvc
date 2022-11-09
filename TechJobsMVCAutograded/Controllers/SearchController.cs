using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechJobsMVCAutograded.Data;
using TechJobsMVCAutograded.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechJobsMVCAutograded.Controllers
{
    public class SearchController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.columns = ListController.ColumnChoices;
            ViewBag.jobs = new List<Job>();
            
            return View();
        }

        // TODO #3 - Create an action method to process a search request and render the updated search views.
        [HttpPost]
        public IActionResult Results(string searchType, string searchTerm)
        {
            ViewBag.jobs = new List<Job>();
            if(searchTerm == "all" || searchTerm == string.Empty)
            {
                ViewBag.jobs = JobData.FindAll();
            }
            else
            {
                ViewBag.jobs = JobData.FindByColumnAndValue(searchType, searchTerm);
            }
            ViewBag.title = $"Search by {ViewBag.columns[searchType]} for: '{searchTerm}'";
            ViewBag.columns = ListController.ColumnChoices;
            ViewBag.searchTerm = searchTerm;
            ViewBag.searchType = searchType;
            return View("Index");
        }
    
    
    }

}
