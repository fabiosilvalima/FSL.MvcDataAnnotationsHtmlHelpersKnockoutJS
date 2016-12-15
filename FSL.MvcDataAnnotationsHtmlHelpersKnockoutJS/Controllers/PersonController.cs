using FSL.MvcDataAnnotationsHtmlHelpersKnockoutJS.Models;
using System.Web.Mvc;

namespace FSL.MvcDataAnnotationsHtmlHelpersKnockoutJS.Controllers
{
    public class PersonController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new PersonViewModel();

            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(PersonViewModel viewModel)
        {
            return View(viewModel);
        }
    }
}