using System.ComponentModel.DataAnnotations;
using Application.Models;
using Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WMUI.Pages.Accounts
{
    [Authorize]
    public class AddModel : PageModel
    {
        private readonly IAccountService _accountService;
        public AddModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        [Required]
        [Display(Name = "Ảnh cá nhân")]
        public required IFormFile Avatar { get; set; }

        [BindProperty]
        [Required]
        [MinLength(3)]
        public required string Username { get; set; }


        [BindProperty]
        [Required]
        [MinLength(6)]
        public required string Password { get; set; }

        [BindProperty]
        public bool IsActived { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var model = new AccountModel();

                model.Name = Username;
                model.Password = Password;
                model.IsActived = IsActived;

                if (Avatar != null)
                {
                    model.ImageUrl = await Utils.UploadFile.File(Avatar, "Avatars");
                }

                await _accountService.Create(model);

                return RedirectToPage("/Accounts/Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", $"{e.Message}");
                return Page();
            }
        }
    }
}
