Solution solution = new();

string filePath = "games.txt";
string [] lines = File.ReadAllLines(filePath);
string [] trimmed = Array.ConvertAll(lines, line => $"{String.Concat(line.Where(c => !Char.IsWhiteSpace(c)))}");
string [] updatedLines = Array.ConvertAll(trimmed, line => $"{line.Remove(0,line.IndexOf(":")+1)}");

Dictionary<int, List<(int, int, int)>> gameData = new Dictionary<int, List<(int, int, int)>>();
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

bool isColor = false;
string currentNum ="";
(int, int ,int ) gameResult = (0, 0, 0);

for (int i = 0; i < updatedLines.Length; i++){
    for (int j = 0; j < updatedLines[i].Length; j++){
       while (updatedLines[i][j] != ';' && updatedLines[i][j] != ',' && isColor == false){
            if (digits.ContainsKey(updatedLines[i][j])){
                currentNum += updatedLines[i][j];
                j++;
            }
            else {
                if (updatedLines[i][j] == 'b'){
                    isColor = true;
                    gameResult.Item1 = Convert.ToInt32(currentNum);
                    currentNum = "";
                    j++;
                }
                else if (updatedLines[i][j] == 'g'){
                    isColor = true;
                    gameResult.Item2 = Convert.ToInt32(currentNum);
                    currentNum = "";
                    j++;
                }
                else if (updatedLines[i][j] == 'r'){
                    isColor = true;
                    gameResult.Item3 = Convert.ToInt32(currentNum);
                    currentNum = "";
                    j++;
                }
            }
            
        }
        if (updatedLines[i][j] == ','){
            isColor = false;
        }
        if (updatedLines[i][j] == ';' || j == updatedLines[i].Length-1){
            if (!gameData.ContainsKey(i+1)){
                gameData.Add(i+1, new List<(int, int, int)>());
                gameData[i+1].Add(gameResult);
            }
            else {
                gameData[i+1].Add(gameResult);
            }
            isColor = false;
            gameResult = (0,0,0);
        }
    }
}

Console.WriteLine(solution.SumOfPowers(gameData));


Console.ReadKey();
class Solution {

    public int SumOfPowers(Dictionary<int, List<(int, int, int)>> games){
        int totalSum = 0;
        int minBlue = 0;
        int minGreen = 0;
        int minRed = 0;
        foreach (var key in games){
            foreach (var game in key.Value){
                minBlue = Math.Max(minBlue, game.Item1);
                minGreen = Math.Max(minGreen, game.Item2);
                minRed = Math.Max(minRed, game.Item3);
            }
            totalSum += minBlue*minGreen*minRed;
            minBlue = 0;
            minGreen = 0;
            minRed = 0;
            
        }
        return totalSum;
    }
    
}
