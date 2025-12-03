
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace WMUI.Pages;

[Authorize]
public class IndexModel : PageModel
{
    public IndexModel() { }

    public void OnGet()
    {

    }
}
