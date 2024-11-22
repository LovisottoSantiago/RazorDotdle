using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDotdle.Models;
using System.Diagnostics;

namespace RazorDotdle.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public InputModel InputModel { get; set; }

        [BindProperty]
        public string Word { get; set; }

        [BindProperty]
        public string Description { get; set; }

        [BindProperty]
        public string Topic { get; set; }

        public string Input { get; set; }

        [BindProperty]
        public string[] Results { get; set; }

        [BindProperty]
        public bool langSwitch { get; set; }


        // Propiedades para manejar las traducciones
        public string Placeholder { get; set; }
        public string EnterButton { get; set; }
        public string ExampleAnswer { get; set; }
        public string NewGameButton { get; set; }
        public string LinkGithub { get; set; }
        public string LinkPortfolio { get; set; }
        public string LinkCards { get; set; }
        public string EditCards { get; set; }

        public void OnGet() {
            if (string.IsNullOrEmpty(Word)) {
                CreateWord(langSwitch);
            }

            LoadLanguageStrings(langSwitch);
        }


        public void OnPost(string action) {
            if (action == "submit") {

                if (string.IsNullOrEmpty(InputModel.UserInput)) {
                    Console.WriteLine("Input cannot be null or empty.");
                    return;
                }

                Input = InputModel.UserInput.ToLower();

                if (Input.Length == Word.Length && InputModel.UserLifes > 0) {
                    try {
                        Results = GameLogic(Word, Input);
                        bool win = CheckWin(Results);

                        if (win) {
                            Debug.WriteLine("Ganaste");
                            InputModel.UserLifes = 4;
                        }
                        else {
                            InputModel.decreaseLife();
                            Debug.WriteLine("Vidas: " + InputModel.UserLifes);
                            TempData["UserLifes"] = InputModel.UserLifes;
                        }
                    }
                    catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            else if (action == "newgame") {
                newGame();
            }
            else if (action == "us") {
                langSwitch = true;
            }
            else if (action == "es") {
                langSwitch = false;
            }

            LoadLanguageStrings(langSwitch);
        }



        public void CreateWord(bool isEnglish) {
            var getWord = new GetWord(5);
            getWord.ChangeLanguage(isEnglish); 
            var phrase = getWord.GetRandomWord();
            Word = phrase[0];
            Description = phrase[1];
            Topic = phrase[2];
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

        public void newGame() {
            InputModel.UserInput = string.Empty;
            Results = new string[0];

            // Generate new word and description
            CreateWord(langSwitch);

            ModelState.Clear();
        }


        // Función para cargar los textos según el idioma
        private void LoadLanguageStrings(bool langSwitch) {
            if (langSwitch) // inglés
            {
                Placeholder = "Enter your word";
                EnterButton = "Enter";
                ExampleAnswer = "Your answer";
                NewGameButton = "↺";
                LinkGithub = "Github";
                LinkPortfolio = "My Portfolio";
                LinkCards = "Add words";
                EditCards = "https://github.com/LovisottoSantiago/RazorDotdle/edit/main/wwwroot/data/5-words-ENG.json";
            }
            else // español
            {
                Placeholder = "Ingresa tu palabra";
                EnterButton = "Enviar";
                ExampleAnswer = "Tu respuesta";
                NewGameButton = "↺";
                LinkGithub = "Github";
                LinkPortfolio = "Mi Portafolio";
                LinkCards = "Agregá más";
                EditCards = "https://github.com/LovisottoSantiago/RazorDotdle/edit/main/wwwroot/data/5-words-ES.json";
            }
        }


    }
}
