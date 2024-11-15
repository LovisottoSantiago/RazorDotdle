using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDotdle.Models;

namespace RazorDotdle.Pages
{
    public class InputPageModel : PageModel
    {
        [BindProperty]
        public required InputModel InputModel { get; set; }
        public void OnGet()
        {
            InputModel = new InputModel();
        }

        public void OnPost() 
        {
            var input = InputModel.UserInput;
        }
    }
}
