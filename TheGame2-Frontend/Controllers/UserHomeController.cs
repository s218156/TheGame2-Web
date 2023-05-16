using Microsoft.AspNetCore.Mvc;
using TheGame2_Frontend.Services;
using TheGame2_Library.Exceptions;
using TheGame2_Library.Models;

namespace TheGame2_Frontend.Controllers
{
	public class UserHomeController : Controller
	{
		ApiCommunicationService apiService;

		public UserHomeController(ApiCommunicationService apiService)
		{
			this.apiService = apiService;
		}
		public async Task<IActionResult> Index()
		{
			string token = Request.Cookies["authToken"];
			try
			{
				Task<UserModel> user = apiService.GetUserData(token);
				return View(await user);
			}
			catch (TheGameWebException e)
			{
				return RedirectToAction("LoginE", "Login");
			}

		}

		public async Task<IActionResult> AccountDetails()
		{
			string token = Request.Cookies["authToken"];
			try
			{
				Task<UserModel> user = apiService.GetUserData(token);
				return View(await user);
			}
			catch (TheGameWebException e)
			{
				return RedirectToAction("LoginE", "Login");
			}
		}

		public async Task<IActionResult> ChangeTexture()
		{
			string token = Request.Cookies["authToken"];
			try
			{
				Task<UserModel> user = apiService.GetUserData(token);
				return View(await user);
			}
			catch (TheGameWebException e)
			{
				return RedirectToAction("LoginE", "Login");
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ChangeTexture(UserModel model)
		{
			string token = Request.Cookies["authToken"];
			try
			{
				Task<UserModel> user = apiService.GetUserData(token);
				UserModel userModel = await user;
				userModel.textureID = model.textureID;
				apiService.UpdateTextureID(token, userModel);
				return RedirectToAction("index", "UserHome");
			}
			catch (TheGameWebException e)
			{
				return RedirectToAction("LoginE", "Login");
			}
		}


		public async Task<IActionResult> Edit()
		{
			string token = Request.Cookies["authToken"];
			try
			{
				Task<UserModel> user = apiService.GetUserData(token);
				return View(await user);
			}
			catch (TheGameWebException e)
			{
				return RedirectToAction("LoginE", "Login");
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(UserModel model)
		{
			string token = Request.Cookies["authToken"];
			try
			{
				Task<UserModel> user = apiService.GetUserData(token);
				UserModel userModel = await user;
				userModel.fullname = model.fullname;
				apiService.UpdateUserData(token, userModel);
				return RedirectToAction("index", "UserHome");
			}
			catch (TheGameWebException e)
			{
				return RedirectToAction("LoginE", "Login");
			}
		}
	}
}
