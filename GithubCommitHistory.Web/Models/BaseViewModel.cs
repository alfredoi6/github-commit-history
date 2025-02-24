namespace GithubCommitHistory.Web.Models
{
    public class BaseViewModel
    {
        public string GitHubUser { get; set; }
        public bool IsAuthenticated => !string.IsNullOrEmpty(GitHubUser);
    }
}