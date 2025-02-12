# Git Log to Invoicing Notes (ASP.NET MVC Core 9)

This project is a prototype ASP.NET MVC Core (.NET 9) application designed for **IT consultancies** that use **GitHub repositories** to generate detailed invoicing notes. It integrates **GitHub SSO** via Octokit and allows users to fetch repository commit history within a given date range.

## Features

- **GitHub SSO Authentication** using Octokit.
- **Retrieve Repository Commits** for a specified date range via GitHub API.
- **Generate Invoice Notes** from commit logs, formatted for billing purposes.
- **User-Friendly Web UI** to configure repositories and export data.
- **ASP.NET MVC Core 9** backend for seamless integration.

---

## Prerequisites

- **.NET 9 SDK** installed.
- **GitHub Developer Account** with OAuth app registered.
- **GitHub Personal Access Token (PAT)** for API calls.
- **SQL Server or SQLite** for user session storage.

---

## Installation

1. **Clone the repository:**

   ```sh
   git clone https://github.com/your-username/git-invoicing-notes.git
   cd git-invoicing-notes
   ```

2. **Set up environment variables:**

   ```sh
   export GITHUB_CLIENT_ID=your_client_id
   export GITHUB_CLIENT_SECRET=your_client_secret
   export GITHUB_PAT=your_personal_access_token
   ```

3. **Update `appsettings.json`:**

   ```json
   {
     "GitHub": {
       "ClientId": "your_client_id",
       "ClientSecret": "your_client_secret",
       "Pat": "your_personal_access_token"
     }
   }
   ```

4. **Restore dependencies and run the application:**

   ```sh
   dotnet restore
   dotnet run
   ```

5. **Open in browser:**  
   Navigate to `https://localhost:5001`

---

## Usage

### 1️⃣ GitHub SSO Login  
Users authenticate via GitHub OAuth using Octokit.

### 2️⃣ Select Repository  
After login, users can select a repository they have access to.

### 3️⃣ Fetch Commits  
Users specify a **start and end date**, and the app retrieves all commits within that range.

### 4️⃣ Export for Invoicing  
The commit details (dates, messages, and files changed) are formatted into an **exportable report** for invoicing.

---

## Example API Calls

### Authenticate with GitHub

```csharp
var github = new GitHubClient(new ProductHeaderValue("GitInvoicingApp"));
var tokenAuth = new Credentials("your_personal_access_token");
github.Credentials = tokenAuth;
```

### Get Repository Commits

```csharp
var commits = await github.Repository.Commit.GetAll("your-username", "your-repo", 
    new CommitRequest { Since = new DateTime(2025, 01, 11), Until = new DateTime(2025, 01, 31) });
```

### Generate Invoice Notes

```csharp
foreach (var commit in commits)
{
    Console.WriteLine($"{commit.Commit.Author.Date}: {commit.Commit.Message}");
    Console.WriteLine($"Files Changed: {string.Join(", ", commit.Files.Select(f => f.Filename))}");
}
```

---

## Roadmap

- [ ] Implement UI for commit selection.
- [ ] Enable PDF/CSV export of invoice notes.
- [ ] Add integration with time-tracking services.
- [ ] Improve security with OAuth token management.

---

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request.

---

## License

This project is licensed under the **MIT License**.

🚀 **Transform your Git commits into meaningful invoice notes!**  
