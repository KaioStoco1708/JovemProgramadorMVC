using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JovemProgramadorMVC.Controllers
{
    public class FiltrosController : Controller
    {
        public FiltrosController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }

        
    }
}
