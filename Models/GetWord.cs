using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics.Tracing;
using System.Text.Json.Nodes;

namespace RazorDotdle.Models {
    public class GetWord {

        public string Word { get; set; }
        public string Description { get; set; }
        public int WordsLenght {  get; set; }

        private List<JObject> wordsList;

        public GetWord(int WordsLenght) {

            WordsLenght = this.WordsLenght;
            string jsonFilePath = "wwwroot/data/5-words.json";
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

            return new string[] { Word, Description };
        }


    }
}
