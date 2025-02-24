namespace GithubCommitHistory.Web.Models
{
    public class RepositoryInfoModel
    {
        public string Name { get; set; }
        public string Owner { get; set; }
        public List<CommitInfoModel>Commits { get; set; } = new List<CommitInfoModel>();
    }
}
