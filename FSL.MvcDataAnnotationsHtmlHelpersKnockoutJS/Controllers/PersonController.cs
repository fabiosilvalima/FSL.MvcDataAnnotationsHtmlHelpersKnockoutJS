using System.Web.Mvc;

namespace FSL.MvcDataAnnotationsHtmlHelpersKnockoutJS.Controllers
{
    public class PersonController : Controller
    {
        // GET: Person
        public ActionResult Index()
        {
            var viewModel = new Models.PersonViewModel();

            return View(viewModel);
        }
    }
}