namespace RazorDotdle.Models {
    public class InputModel {

        public string UserInput {  get; set; }
        public int UserLifes { get; set; }

        public InputModel() {
            UserInput = string.Empty;
            UserLifes = 4;
        }

    }
}
