using Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WMUI.Pages.Accounts
{
    [Authorize]
    public class RemoveModel : PageModel
    {
        private readonly IAccountService _accountService;
        public RemoveModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public async Task OnGetAsync()
        {
            await _accountService.Delete(Id);
        }
    }
}
