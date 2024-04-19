Solution solution = new();

char[][] output = solution.SchematicParser("schematic.txt");

Console.WriteLine(solution.SumOfValidPartNumbers(output));
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

    Dictionary<(int,int), List<int>> totalAdjacencies = new(); // dictionary of coordinates, with list of numbers associated with it

    string combinedDigits ="";
    bool isValid = false;
    bool isNumber = false;
    int totalSum = 0;
    (int, int) currentValidity;

    public char[][] SchematicParser(string filename){
        char[] symbols = ['!','@','#','$','%','^','&','(',')','_','-','=','+','/'];
        string [] lines = File.ReadAllLines(filename);
        char [][] output = new char[lines.Length][];
        for (int i = 0; i < lines.Length; i++){
            output[i] = lines[i].ToCharArray();
            for (int j = 0; j < output.Length; j++) {
                if (Array.IndexOf(symbols, output[i][j]) != -1) {
                    output[i][j] = '.';
                }
            }
        }
        return output;   
    }
    public Dictionary<(int,int),List<(int, int)>> SymbolAdjacencies(char[][] schematic){
        Dictionary<(int,int),List<(int, int)>> output = new();
        for (int i = 0; i < schematic.Length; i++){ 
            for (int j = 0; j < schematic[i].Length; j++){
                if (schematic[i][j] == '*'){
                    List<(int, int)> input = [(i,j-1), (i,j+1), (i-1,j), (i+1,j), (i-1, j-1), (i-1, j+1), (i+1, j-1), (i+1, j+1)];
                    output.Add((i,j), input);
                }
            }
        }
        return output;
    }

    public int SumOfValidPartNumbers(char[][] schematic){
        Dictionary<(int,int),List<(int, int)>> validList = SymbolAdjacencies(schematic);

        for (int i = 0; i < schematic.Length; i++) {
            for (int j = 0; j < schematic[i].Length; j++){
                if (schematic[i][j] == '*' || schematic[i][j] == '.'){
                    isNumber = false;
                    if (combinedDigits != "" && isValid == true){
                        if (!totalAdjacencies.ContainsKey(currentValidity)){
                            totalAdjacencies.Add(currentValidity, new List<int>());
                            totalAdjacencies[currentValidity].Add(Convert.ToInt32(combinedDigits));
                        }
                        else {
                            totalAdjacencies[currentValidity].Add(Convert.ToInt32(combinedDigits));
                        }
                    }
                    isValid = false;
                    combinedDigits = "";
                }
                if (digits.ContainsKey(schematic[i][j])){
                    combinedDigits += schematic[i][j];
                    isNumber = true;
                    if (isValid == false){
                        foreach (var key in validList){
                            if (key.Value.Contains((i,j))){
                                isValid = true;
                                currentValidity = key.Key;
                            }
                        }
                    }
                }
            }
            if (combinedDigits != "" && isValid == true){
                if (!totalAdjacencies.ContainsKey(currentValidity)){
                            totalAdjacencies.Add(currentValidity, new List<int>());
                            totalAdjacencies[currentValidity].Add(Convert.ToInt32(combinedDigits));
                        }
                        else {
                            totalAdjacencies[currentValidity].Add(Convert.ToInt32(combinedDigits));
                        }
                isValid = false;
                combinedDigits ="";
            }
        }
        foreach (var key in totalAdjacencies){
            if (key.Value.Count == 2){
                totalSum += key.Value[0]*key.Value[1];
            }
        }
        return totalSum;
    }
}

// I want to know what the int number is and which coordinates its touching
// for the symbol, i want to know which coordinates its touching. this can just be a list of coordinates.