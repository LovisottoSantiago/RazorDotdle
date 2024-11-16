using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDotdle.Models;
using System.Diagnostics;

namespace RazorDotdle.Pages {
    public class InputPageModel : PageModel {
        [BindProperty]
        public InputModel InputModel { get; set; }

        [BindProperty]
        public string Word { get; set; }

        [BindProperty]
        public string Description { get; set; }

        public string Input { get; set; }

        [BindProperty]
        public string[] Results { get; set; }


        public void OnGet() {
            if (Word == null) {
                CreateWord();
            }
        }

        public void OnPost(string action) {
            if (action == "reset") {
                Word = null;
                Description = null;
                CreateWord();
            }
            else if (action == "submit") {
                Input = InputModel.UserInput;
                try {
                    if (Input.Length == Word.Length) {
                        Results = GameLogic(Word, Input);
                        bool flag = false;
                        foreach (var result in Results) {
                            if (result == "verde") {
                                flag = true;
                            }
                        }
                        if (flag) {
                            CreateWord();
                        }
                    }
                }
                catch (Exception) {

                }
            }
        }

        public void CreateWord() {
            var getWord = new GetWord(5);
            var phrase = getWord.GetRandomWord();
            Word = phrase[0];
            Description = phrase[1];
        }
        private string[] GameLogic(string word, string guess) {
            string[] results = new string[word.Length];
            if (guess.Length == word.Length) {
                for (int i = 0; i < word.Length; i++) {
                    if (guess[i] == word[i]) {
                        results[i] = "verde";
                    }
                    else if (word.Contains(guess[i])) {
                        results[i] = "amarillo";
                    }
                    else {
                        results[i] = "gris";
                    }
                }
            }            
            else {
                throw new ArgumentException("Parameter cannot be null");
            }
            return results;
        }


    }
}
