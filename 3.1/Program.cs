Solution solution = new();

char[][] output = solution.SchematicParser("schematic.txt");
foreach (var element in output){
    //Console.WriteLine(element);
}

List<(int, int)> symbolOutput = solution.SymbolAdjacencies(output);
foreach (var element in symbolOutput){
    //Console.WriteLine(element);
}

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

    string combinedDigits ="";
    bool isValid = false;
    bool isNumber = false;
    int totalSum = 0;



    public char[][] SchematicParser(string filename){
        char[] symbols = ['!','@','#','$','%','^','&','*','(',')','_','-','=','+','/'];
        string [] lines = File.ReadAllLines(filename);
        char [][] output = new char[lines.Length][];
        for (int i = 0; i < lines.Length; i++){
            output[i] = lines[i].ToCharArray();
            for (int j = 0; j < output.Length; j++) {
                if (Array.IndexOf(symbols, output[i][j]) != -1) {
                    output[i][j] = '*';
                }
            }
        }
        
        return output;   
    }
    public List<(int, int)> SymbolAdjacencies(char[][] schematic){
        List<(int, int)> output = new();
        for (int i = 0; i < schematic.Length; i++){ 
            for (int j = 0; j < schematic[i].Length; j++){
                if (schematic[i][j] == '*'){
                    output.AddRange([(i,j-1), (i,j+1), (i-1,j), (i+1,j), (i-1, j-1), (i-1, j+1), (i+1, j-1), (i+1, j+1)]);
                }
            }
        }
        return output;
    }

    public int SumOfValidPartNumbers(char[][] schematic){
        List<(int, int)> validList = SymbolAdjacencies(schematic);

        for (int i = 0; i < schematic.Length; i++) {
            for (int j = 0; j < schematic[i].Length; j++){
                if (schematic[i][j] == '*' || schematic[i][j] == '.'){
                    isNumber = false;
                    if (combinedDigits != "" && isValid == true){
                        totalSum += Convert.ToInt32(combinedDigits);
                    }
                    isValid = false;
                    combinedDigits = "";
                }
                if (digits.ContainsKey(schematic[i][j])){
                    combinedDigits += schematic[i][j];
                    isNumber = true;
                    if (isValid == false){
                        if (validList.Contains((i,j))){
                            isValid = true;
                        }
                    }
                }

            }
            if (combinedDigits != "" && isValid == true){
                totalSum += Convert.ToInt32(combinedDigits);
                isValid = false;
                combinedDigits ="";
            }
        }
        return totalSum;
    }

}

// I want to know what the int number is and which coordinates its touching
// for the symbol, i want to know which coordinates its touching. this can just be a list of coordinates.