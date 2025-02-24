using GithubCommitHistory.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using System.Diagnostics;
using System.Xml.Linq;

namespace GithubCommitHistory.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Repos = new List<RepositoryInfoModel>();

            var baseModel = GetBaseViewModel();
            if (baseModel.IsAuthenticated)
            {
                var accessToken = HttpContext.Session.GetString("AccessToken");
                if (string.IsNullOrEmpty(accessToken))
                {
                    return RedirectToAction("Login", "Auth");
                }

                var client = new GitHubClient(new ProductHeaderValue("MyApp"))
                {
                    Credentials = new Credentials(accessToken)
                };

                try
                {
                    var repositories = await client.Repository.GetAllForCurrent();
                    if(repositories.Count > 0)
                    {
                        foreach (var repository in repositories)
                        {
                            RepositoryInfoModel repositoryInfo = new RepositoryInfoModel()
                            {
                                Name = repository.Name,
                                Owner = repository.Owner.Login
                            };

                            var commits = await client.Repository.Commit.GetAll(repository.Owner.Login, repository.Name);
                            foreach (var commit in commits)
                            {
                                CommitInfoModel commitInfo = new CommitInfoModel()
                                {
                                    Sha = commit.Sha.Substring(0, 7),
                                    Author = commit.Commit.Author.Name,
                                    CommitDate = commit.Commit.Author.Date.ToString("yyyy-MM-dd HH:mm"),
                                    CommitMesage = commit.Commit.Message
                                };
                                repositoryInfo.Commits.Add(commitInfo);
                            }
                            ViewBag.Repos.Add(repositoryInfo); 
                        }
                    }            
                }                
                catch (Octokit.AuthorizationException)
                {
                    ViewBag.Error = "Unauthorized access. Please log in again.";
                    return RedirectToAction("Login", "Auth");
                }
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
