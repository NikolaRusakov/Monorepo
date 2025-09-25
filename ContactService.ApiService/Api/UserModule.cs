using Carter;

namespace ContactsApi.Modules;

public class UserModule : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/api/users", GetUsers);
		app.MapGet("/api/users/{id:int}", GetUser);
		app.MapPost("/api/users", CreateUser);
	}

	public static async Task<IResult> GetUsers(IUserService userService)
	{
		var users = await userService.GetAllUsersAsync();
		return Results.Ok(users);
	}

	public static async Task<IResult> GetUser(int id, IUserService userService)
	{
		var user = await userService.GetUserByIdAsync(id);
		return user is not null ? Results.Ok(user) : Results.NotFound();
	}

	public static async Task<IResult> CreateUser(User user, IUserService userService)
	{
		var createdUser = await userService.CreateUserAsync(user);
		return Results.Created($"/api/users/{createdUser.Id}", createdUser);
	}
}
