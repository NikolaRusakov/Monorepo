public class UserService : IUserService
{
	private static readonly List<User> _users = new()
	{
		new(1, "John Doe", "john@example.com", DateTime.UtcNow.AddDays(-30)),
		new(2, "Jane Smith", "jane@example.com", DateTime.UtcNow.AddDays(-15))
	};

	public Task<IEnumerable<User>> GetAllUsersAsync()
	{
		return Task.FromResult<IEnumerable<User>>(_users);
	}

	public Task<User?> GetUserByIdAsync(int id)
	{
		var user = _users.FirstOrDefault(u => u.Id == id);
		return Task.FromResult(user);
	}

	public Task<User> CreateUserAsync(User user)
	{
		var newId = _users.Max(u => u.Id) + 1;
		var newUser = user with { Id = newId, CreatedAt = DateTime.UtcNow };
		_users.Add(newUser);
		return Task.FromResult(newUser);
	}
}