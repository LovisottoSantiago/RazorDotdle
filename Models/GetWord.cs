using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics.Tracing;
using System.Text.Json.Nodes;

namespace RazorDotdle.Models {
    public class GetWord {

        public string Word { get; set; }
        public string Description { get; set; }
        public string Topic { get; set; }
        public int WordsLenght {  get; set; }

        private bool isEng { get; set; } // true = english | false = spanish

        private List<JObject> wordsList;

        public GetWord(int WordsLenght) {

            this.WordsLenght = WordsLenght;
            isEng = true;

            string jsonFilePath = "wwwroot/data/5-words-ENG.json";

            if (isEng) {
                jsonFilePath = "wwwroot/data/5-words-ENG.json";
            } else {
                jsonFilePath = "wwwroot/data/5-words-ES.json";
            }

            string jsonString = File.ReadAllText(jsonFilePath);
            JArray jsonArray = JArray.Parse(jsonString);

            wordsList = new List<JObject>();
            
            foreach (var item in jsonArray) {
                wordsList.Add((JObject)item);
            }        
        }

        public String[] GetRandomWord() {
            Random random = Random.Shared;
            int index = random.Next(wordsList.Count);

            var randomWord = wordsList[index];
            Word = (string)randomWord["word"];
            Description = (string)randomWord["description"];
            Topic = (string)randomWord["topic"];

            return new string[] { Word, Description, Topic };
        }

        public void ChangeLanguage(bool isEnglish) {
            isEng = isEnglish;
            string jsonFilePath = isEng ? "wwwroot/data/5-words-ENG.json" : "wwwroot/data/5-words-ES.json";
            string jsonString = File.ReadAllText(jsonFilePath);
            JArray jsonArray = JArray.Parse(jsonString);

            wordsList.Clear(); // Limpiar la lista de palabras actuales

            foreach (var item in jsonArray) {
                wordsList.Add((JObject)item);
            }
        }

    }
}
