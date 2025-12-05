using System.Security.Claims;
using Application.Models;
using Application.Services.Interface;
using Domain.Exceptions;
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

        public async Task<IActionResult> OnGetAccountAsync(int id)
        {
            try
            {
                var acc = await _accountService.FindById(id);

                if (acc == null)
                {
                    return NotFound(new { message = "Tài khoản này không tồn tại." });
                }

                return new JsonResult(new { id = acc.Id, name = acc.Name })
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (BadRequestException e)
            {
                return BadRequest(new { message = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            if (id <= 0)
            {
                return new JsonResult(new { success = false, message = "ID không hợp lệ" });
            }

            try
            {
                var isDel = await _accountService.Delete(id);

                if (!isDel)
                {
                    return new JsonResult(new { success = false, message = $"Xóa thất bại" });
                }

                return new JsonResult(new { success = true, message = $"User {id} đã bị xóa" });
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }
    }
}
