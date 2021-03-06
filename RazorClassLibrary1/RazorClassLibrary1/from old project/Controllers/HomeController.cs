﻿using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
	/// <summary>
	///	This controller provides HTML views.
	/// </summary>
	[ResponseCache(Duration = 60)]
	public class HomeController : Controller
	{
		public void Add()
		{
			using (var notesController = new PostsController())
				notesController.Add(Request.HttpContext);
			Helpers.LoadPageInResponse(HttpContext, "confirmation");
		}

		public void Authentificate()
		{
			Helpers.LoadPageInResponse(HttpContext, "authPage");
		}

		public void Check()
		{
			var controller = new UsersController();
			var form = Request.Form;
			var login = form["login"];
			var password = form["password"];
			if (!controller.Contains(login))
			{
				controller.Add(login, password);
				Response.Cookies.Append("login", "ok");
				Response.Cookies.Append("userId", login);
				Response.Redirect("/Home");
			}
			else if (controller.Contains(login) && controller.Check(login, password))
			{
				Response.Cookies.Append("login", "ok");
				Response.Cookies.Append("userId", login);
				Response.Redirect("/Home");
			}

		}

		public void Index()
		{
			var htmlPage = System.IO.File.ReadAllText("Views/Home.html");
			htmlPage = htmlPage.Replace("@List", Helpers.GetPosts(Request.HttpContext));
			using (var streamWriter = new StreamWriter(Response.Body))
				streamWriter.WriteAsync(htmlPage);
		}

		public void Notes()
		{
			Helpers.LoadPageInResponse(HttpContext, "notes");
		}
	}
}