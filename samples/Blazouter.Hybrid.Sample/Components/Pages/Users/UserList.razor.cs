using Blazouter.Extensions;

namespace Blazouter.Hybrid.Sample.Components.Pages.Users
{
    public partial class UserList
    {
        private List<User> _users =
    [
        new User { Id = 1, Name = "John Doe", Email = "john@example.com", Role = "Administrator", Initials = "JD", JoinDate = new DateTime(2020, 1, 15) },
        new User { Id = 2, Name = "Jane Smith", Email = "jane@example.com", Role = "Developer", Initials = "JS", JoinDate = new DateTime(2021, 3, 22) },
        new User { Id = 3, Name = "Mike Johnson", Email = "mike@example.com", Role = "Designer", Initials = "MJ", JoinDate = new DateTime(2019, 8, 10) },
        new User { Id = 4, Name = "Sarah Williams", Email = "sarah@example.com", Role = "Product Manager", Initials = "SW", JoinDate = new DateTime(2022, 5, 30) },
        new User { Id = 5, Name = "Tom Brown", Email = "tom@example.com", Role = "Developer", Initials = "TB", JoinDate = new DateTime(2021, 11, 5) },
        new User { Id = 6, Name = "Emily Davis", Email = "emily@example.com", Role = "QA Engineer", Initials = "ED", JoinDate = new DateTime(2023, 2, 18) }
    ];

        private List<User> _displayedUsers = [];
        private string _queryInfo = "";

        protected override void OnInitialized()
        {
            // NEW: Using typed query parameter extensions for better type safety
            string? sort = RouterState.GetQuery("sort");
            string? order = RouterState.GetQuery("order");
            string? filter = RouterState.GetQuery("filter");
            int page = RouterState.GetQueryInt("page", 1); // Default to page 1 if not specified
            bool showInactive = RouterState.GetQueryBool("showInactive", false); // NEW: Boolean parameter

            // Build query info message - show ALL query parameters for demonstration
            List<string> queryParts = [];

            // Get all query parameters
            Dictionary<string, string> allParams = RouterState.GetAllQueryParams();

            if (allParams.Count > 0)
            {
                // Display all query parameters
                foreach (KeyValuePair<string, string> kvp in allParams.OrderBy(x => x.Key))
                {
                    queryParts.Add($"{kvp.Key}: {kvp.Value}");
                }
            }

            if (queryParts.Count > 0)
            {
                _queryInfo = string.Join(" | ", queryParts);
            }

            // Apply sorting
            _displayedUsers = _users.ToList();

            if (!string.IsNullOrEmpty(sort))
            {
                bool ascending = order?.ToLower() != "desc";

                _displayedUsers = sort.ToLower() switch
                {
                    "name" => ascending
                        ? _displayedUsers.OrderBy(u => u.Name).ToList()
                        : _displayedUsers.OrderByDescending(u => u.Name).ToList(),
                    "date" => ascending
                        ? _displayedUsers.OrderBy(u => u.JoinDate).ToList()
                        : _displayedUsers.OrderByDescending(u => u.JoinDate).ToList(),
                    _ => _displayedUsers
                };
            }

            // Apply filter (show only active users, unless showInactive is true)
            if (filter?.ToLower() == "active" && !showInactive)
            {
                _displayedUsers = _displayedUsers.Where(u => u.Id % 2 != 0).ToList();
            }
        }

        private class User
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public string Email { get; set; } = "";
            public string Role { get; set; } = "";
            public string Initials { get; set; } = "";
            public DateTime JoinDate { get; set; }
        }

        private void ClearFilters()
        {
            NavService.NavigateToWithClearedQuery(RouterState);
        }
    }
}