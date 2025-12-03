using System.Security.Claims;
using Application.Models;
using Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WMUI.Pages.Accounts
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAccountService _accountService;
        public IndexModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public List<AccountModel> AccountModels { get; set; } = new();

        public async Task OnGet()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var accounts = await _accountService.FindAll();
            AccountModels = accounts
                .Where(p => p.Id != userId)
                .Select(AccountModel.ToModel)
                .ToList();
        }

        public async Task<IActionResult> OnGetAccount(int id)
        {


            var model = new AccountModel();



            return new JsonResult(model);
        }
    }
}
