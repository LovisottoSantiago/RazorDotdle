using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDotdle.Models;

namespace RazorDotdle.Pages {
    public class InputPageModel : PageModel {
        [BindProperty]
        public InputModel InputModel { get; set; }

        [BindProperty]
        public string Word { get; set; }

        [BindProperty]
        public string Description { get; set; }

        public string Input { get; set; }


        public void OnGet() {
            GenerateNewWordAndDescription();
        }

        public void OnPost(string action) {
            if (action == "reset") {
                GenerateNewWordAndDescription();
            }
            else if (action == "submit") {
                Input = InputModel.UserInput;
            }
        }

        private void GenerateNewWordAndDescription() {
            var getWord = new GetWord(5);
            var phrase = getWord.GetRandomWord();
            Word = phrase[0];
            Description = phrase[1];
        }
    }
}
