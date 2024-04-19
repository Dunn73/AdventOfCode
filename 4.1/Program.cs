Solution solution = new();
Dictionary<int,(List<int>,List<int>)> output = solution.CardsParser("cards.txt");
foreach (var element in output){
    Console.WriteLine($"{element.Key}, {string.Join(",", element.Value.Item1)}, {string.Join(",",element.Value.Item2)}");
}

Console.WriteLine(solution.TotalPoints("cards.txt"));
Console.ReadKey();

class Solution {
    Dictionary<char,int> digits = new() {
    {'0',0},
    {'1',1},
    {'2',2},
    {'3',3},
    {'4',4},
    {'5',5},
    {'6',6},
    {'7',7},
    {'8',8},
    {'9',9}
    };
    public int TotalPoints(string filename){
        Dictionary<int,(List<int>,List<int>)> cards = CardsParser(filename);
        int wins = 0;
        int total = 0;
        for (int i = 0; i < cards.Count; i++){
            foreach (var item in cards[i].Item2){
                if (cards[i].Item1.Contains(item)){
                    wins ++;
                }
            }
            total += 1*Convert.ToInt32(Math.Pow(2, wins-1));
            wins = 0;
        }
        return total;
    }
    public Dictionary<int,(List<int>,List<int>)> CardsParser(string filename){

        string[] lines = File.ReadAllLines(filename);
        string [] updatedLines = Array.ConvertAll(lines, line => $"{line.Remove(0,line.IndexOf(":")+1)}");

        Dictionary<int,(List<int>,List<int>)> output = new();
        bool winningNumbers;
        string combinedDigits ="";

        for (int i = 0; i < updatedLines.Length; i++){
            winningNumbers = true;
            output.Add(i, (new List<int>(), new List<int>()));
            for (int j = 0; j < updatedLines[i].Length; j++){
                if (digits.ContainsKey(updatedLines[i][j])){
                    combinedDigits += updatedLines[i][j];
                }
                else if (updatedLines[i][j] == ' ' && combinedDigits != ""){
                    if (winningNumbers){
                        output[i].Item1.Add(Convert.ToInt32(combinedDigits));
                        combinedDigits = "";
                    }
                    else {
                        output[i].Item2.Add(Convert.ToInt32(combinedDigits));
                        combinedDigits = "";
                    }
                }
                if (updatedLines[i][j] == '|'){
                    winningNumbers = false;
                }
            }
            output[i].Item2.Add(Convert.ToInt32(combinedDigits));
            combinedDigits = "";
        }
        foreach (var element in updatedLines){
            Console.WriteLine(element);
        }
        return output;
    }
     
}