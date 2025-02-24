namespace GithubCommitHistory.Web.Models
{
    public class CommitInfoModel
    {
        public string Sha {  get; set; }
        public string CommitMesage { get; set; }
        public string CommitDate { get; set; }
        public string Author { get; set; }
    }
}
