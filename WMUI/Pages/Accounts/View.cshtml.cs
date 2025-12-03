using Application.Models;
using Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WMUI.Pages.Accounts
{
    [Authorize]
    public class ViewModel : PageModel
    {
        private readonly IAccountService _accountService;

        public ViewModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public AccountModel AccountModel { get; set; } = new();
        public async Task<IActionResult> OnGet()
        {
            var acc = await _accountService.FindById(Id);

            if (acc == null)
            {
                return RedirectToPage("Accounts/Index");
            }

            AccountModel = AccountModel.ToModel(acc);

            return Page();
        }
    }
}
