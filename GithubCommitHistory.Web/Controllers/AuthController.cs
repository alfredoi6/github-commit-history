using GithubCommitHistory.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Octokit;

namespace GithubCommitHistory.Web.Controllers
{

    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly GitHubSettings _githubSettings;

        public AuthController(IOptions<GitHubSettings> githubSettings)
        {
            _githubSettings = githubSettings.Value;
        }

        // Step 1: Redirect user to GitHub for authentication
        [HttpGet("login")]
        public IActionResult Login()
        {
            var client = new GitHubClient(new ProductHeaderValue("SampleGithubCommitHistoryApp"));
            var request = new OauthLoginRequest(_githubSettings.ClientId)
            {
                Scopes = { "user", "repo" } // Define required scopes
            };

            var loginUrl = client.Oauth.GetGitHubLoginUrl(request);
            return Redirect(loginUrl.ToString());
        }

        // Step 2: Handle callback from GitHub
        [HttpGet("callback")]
        public async Task<IActionResult> Callback(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return BadRequest("No code provided.");
            }

            var client = new GitHubClient(new ProductHeaderValue("MyApp"));
            var tokenRequest = new OauthTokenRequest(_githubSettings.ClientId, _githubSettings.ClientSecret, code);
            var token = await client.Oauth.CreateAccessToken(tokenRequest);

            // Authenticate the client with the access token
            client.Credentials = new Credentials(token.AccessToken);

            // Fetch user info (optional)
            var user = await client.User.Current();
            HttpContext.Session.SetString("GitHubUser", user.Login);
            HttpContext.Session.SetString("AccessToken", token.AccessToken);

            return RedirectToAction("Index", "Home");
        }

        // Logout (optional)
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}