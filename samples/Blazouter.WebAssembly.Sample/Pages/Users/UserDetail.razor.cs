namespace Blazouter.WebAssembly.Sample.Pages.Users
{
    public partial class UserDetail
    {
        private string? _userId;
        private UserModel? _user;

        protected override void OnInitialized()
        {
            _userId = RouterState.GetParam("id");

            if (int.TryParse(_userId, out int id))
            {
                _user = GetUserById(id);
            }
        }

        private UserModel? GetUserById(int id)
        {
            List<UserModel> users =
        [
            new() { Id = 1, Name = "John Doe", Email = "john@example.com", Role = "Administrator", Department = "IT", Initials = "JD" },
            new() { Id = 2, Name = "Jane Smith", Email = "jane@example.com", Role = "Developer", Department = "Engineering", Initials = "JS" },
            new() { Id = 3, Name = "Mike Johnson", Email = "mike@example.com", Role = "Designer", Department = "Design", Initials = "MJ" },
            new() { Id = 4, Name = "Sarah Williams", Email = "sarah@example.com", Role = "Product Manager", Department = "Product", Initials = "SW" },
            new() { Id = 5, Name = "Tom Brown", Email = "tom@example.com", Role = "Developer", Department = "Engineering", Initials = "TB" },
            new() { Id = 6, Name = "Emily Davis", Email = "emily@example.com", Role = "QA Engineer", Department = "Quality", Initials = "ED" }
        ];

            return users.FirstOrDefault(u => u.Id == id);
        }

        private class UserModel
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public string Email { get; set; } = "";
            public string Role { get; set; } = "";
            public string Department { get; set; } = "";
            public string Initials { get; set; } = "";
        }
    }
}