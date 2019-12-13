using Microsoft.AspNetCore.Mvc;

namespace Note.MVCWebApp.Controllers.Base
{
    public class ManageController : Controller
    {
        protected IActionResult BackWithError(string errorTitle, string errorMessage)
        {
            TempData["HasError"] = true;
            TempData["ErrorTitle"] = errorTitle;
            TempData["ErrorMessage"] = errorMessage;

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}