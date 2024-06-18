using LoginApi.Models;
using LoginApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoginApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly UserService _userService;

		public LoginController(UserService userService)
		{
			_userService = userService;
		}

		[HttpPost("register")]
		public ActionResult<User> Register(User user)
		{
			var existingUser = _userService.GetByUsername(user.Username);
			if (existingUser != null)
			{
				return Conflict("Username already taken");
			}

			_userService.Create(user);
			return Ok(user);
		}

		[HttpPost("login")]
		public ActionResult<User> Login(User login)
		{
			var user = _userService.GetByUsername(login.Username);
			if (user == null || user.Password != login.Password)
			{
				return Unauthorized("Invalid username or password");
			}

			return Ok(user);
		}
	}
}
