using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BBQInschrivingen.API.Models;
using BBQInschrivingen.API.Managers;
using System.Net;
using System.Net.Http.Headers;

namespace BBQInschrivingen.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BBQInschrijvingenController : ControllerBase
	{
		private IInschrijvingManager _inschrijvingManager;
		private IUserManager _userManager;
		public BBQInschrijvingenController(IInschrijvingManager inschrijvingManager, IUserManager userManager)
		{
			_inschrijvingManager = inschrijvingManager;
			_userManager = userManager;
		}

		[HttpPost]
		[Route("submit")]
		public IActionResult Submit(object jsonRequest)
		{
			try
			{
				var inschrijving = JsonConvert.DeserializeObject<Inschrijving>(jsonRequest.ToString());
				var result = _inschrijvingManager.Submit(inschrijving);

				return new JsonResult(new { success = true, data = result});

			}
			catch (Exception ex)
			{
				return new JsonResult(new { success = false, message = ex.Message });
			}
		}

		[HttpGet]
		[Route("GetAllInschrijvingen")]
		public IActionResult GetAllInschrijvingen()
		{
			try
			{
				var inschrijvingen = _inschrijvingManager.GetAllInschrijvingen();
				return new JsonResult(new { success = true, data = inschrijvingen });
			}
			catch (Exception ex)
			{
				return new JsonResult(new { succes = true, message = ex.Message });
			}
		}

		[HttpPost]
		[Route("GenerateTicketPDF")]
		public IActionResult GenerateTicketPDF(object inschrijvingJson)
		{
			try
			{
				var inschrijving = JsonConvert.DeserializeObject<Inschrijving>(inschrijvingJson.ToString());
				var ticketPDFBytes = _inschrijvingManager.GenerateInschrijvingTicket(inschrijving);

				return File(ticketPDFBytes, System.Net.Mime.MediaTypeNames.Application.Octet, $"{inschrijving.TimeStamp}_{inschrijving.Naam}.pdf");
				
			}
			catch (Exception ex)
			{
				return new JsonResult(new { success = false, message = ex.Message });
			}
		}

		[HttpPost]
		[Route("UpdateInschrijving")]
		public IActionResult UpdateInschrijving(object request)
		{
			try
			{
				var passwordedJsonRequest = JsonConvert.DeserializeObject<PasswordedJsonRequest<Inschrijving>>(request.ToString());

				if (!_userManager.ValidatePassword(passwordedJsonRequest.Password))
				{
					return new JsonResult(new { success = false, message = "Invalid Password" });
				}

				var inschrijving = passwordedJsonRequest.Model;
				_inschrijvingManager.UpdateInschrijving(inschrijving);

				return new JsonResult(new { success = true, data = inschrijving });
			}
			catch (Exception ex)
			{
				return new JsonResult(new { success = false, message = ex.Message });

			}
		}

		[HttpPost]
		[Route("DeleteInschrijving")]
		public IActionResult DeleteInschrijving(object request)
		{
			try
			{
				var passwordedJsonRequest = JsonConvert.DeserializeObject<PasswordedJsonRequest<int>>(request.ToString());

				if (!_userManager.ValidatePassword(passwordedJsonRequest.Password))
				{
					return new JsonResult(new { success = false, message = "Invalid Password" });
				}

				var id = passwordedJsonRequest.Model;
				_inschrijvingManager.DeleteInschrijving(id);

				return new JsonResult(new { success = true});
			}
			catch (Exception ex)
			{
				return new JsonResult(new { success = false, message = ex.Message });

			}
		}

		[HttpGet]
		[Route("ValidatePassword/{password}")]
		public IActionResult ValidatePassword(string password)
		{
			try
			{
				//var password = JsonConvert.DeserializeObject<string>(request.ToString());
				var isValidated = _userManager.ValidatePassword(password);

				return new JsonResult(new { success = true, isValidated = isValidated });
			}
			catch (Exception ex)
			{
				return new JsonResult(new { success = false, message = ex.Message });
			}
			

		}

	}
}