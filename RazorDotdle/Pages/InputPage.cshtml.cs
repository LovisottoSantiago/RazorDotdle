using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDotdle.Models;
using System;

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

        public bool Next = false;

        public void OnGet() {
            if (string.IsNullOrEmpty(Word)) {
                CreateWord();
            }
        }

        public void OnPost(string action) {
            if (action == "submit") {

                if (string.IsNullOrEmpty(InputModel.UserInput)) {
                    Console.WriteLine("Input cannot be null or empty.");
                    return; 
                }
                
                Input = InputModel.UserInput;

                if (Input.Length == Word.Length) {
                    try {
                        Results = GameLogic(Word, Input);
                        bool win = CheckWin(Results);

                        if (win) {
                            Next = true;
                        }
                    }
                    catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                }
                else {
                    
                }
            }
            else if (action == "newgame") {
                InputModel.UserInput = string.Empty;
                Results = new string[0];

                // Generate new word and description
                CreateWord();

                ModelState.Clear();
            }
        }



        // Método para generar una palabra aleatoria.
        public void CreateWord() {
            var getWord = new GetWord(5);
            var phrase = getWord.GetRandomWord();
            Word = phrase[0];
            Description = phrase[1];
        }


        
        // Lógica de comparación de palabras: verde (correcto), amarillo (letra en otra posición), gris (incorrecto).
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
                throw new ArgumentException("The input and word lengths do not match.");
            }
            return results;
        }


        private bool CheckWin(string[] array) {
            foreach (var x in array) {
                if (x != "verde") {
                    return false;
                }
            }
            return true;
        }


    }
}
