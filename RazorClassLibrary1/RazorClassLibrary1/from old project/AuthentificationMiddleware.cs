using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApplication1
{
	/// <summary>
	/// Used to authentificate users in system.
	/// </summary>
	public class AuthentificationMiddleware
	{
		public AuthentificationMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			if (!context.Request.Cookies.ContainsKey("login") &&
			    !context.Request.Path.StartsWithSegments(new PathString("/Home/Authentificate")))
				context.Response.Redirect("/Home/Authentificate");
			if (context.Request.Cookies.ContainsKey("login") &&
			    context.Request.Path.StartsWithSegments(new PathString("/Home/Authentificate")))
				context.Response.Redirect("/Home");
			await _next.Invoke(context);
		}

		private readonly RequestDelegate _next;
	}
}