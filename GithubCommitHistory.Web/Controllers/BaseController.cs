using GithubCommitHistory.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace GithubCommitHistory.Web.Controllers
{
    public class BaseController : Controller
    {
        protected BaseViewModel GetBaseViewModel()
        {
            var model = new BaseViewModel
            {
                GitHubUser = HttpContext.Session.GetString("GitHubUser")
            };
            return model;
        }

        // Optional: Override View to always include BaseViewModel
        public override ViewResult View(string viewName, object model)
        {
            ViewBag.BaseModel = GetBaseViewModel();
            return base.View(viewName, model);
        }
    }
}