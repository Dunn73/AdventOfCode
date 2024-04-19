Solution solution = new();

string parsedDirections = solution.DirectionParser("instructions.txt");
Console.WriteLine(parsedDirections);
Dictionary<string, (string, string)> parsedNodes = solution.nodeParser("instructions.txt");
foreach (var element in parsedNodes){
    Console.WriteLine($"{element.Key}, {element.Value}");
}

Console.WriteLine($"{solution.TotalInstructions(parsedNodes, parsedDirections)}");
Console.ReadKey();

class Solution {
    public int TotalInstructions(Dictionary<string, (string, string)> nodes, string directions) {
        int count = 0;
        int directionCount = 0;
        string currentNode ="AAA";

        while (currentNode != "ZZZ"){
            if (directions[directionCount] == 'L'){
                currentNode = nodes[currentNode].Item1;
            }
            else {
                currentNode = nodes[currentNode].Item2;
            }
            count++;
            if (directionCount < directions.Length-1){
                directionCount++;
            }
            else {
                directionCount = 0;
            }
        }
        return count;
    }
    public string DirectionParser(string filename) {
        string ?line = File.ReadLines(filename).FirstOrDefault();
        return line;
    }
    public Dictionary<string, (string, string)> nodeParser(string filename){
        Dictionary<string, (string, string)> output = new();
        var lines = File.ReadLines(filename).Skip(2);

        string start = "";
        string left = "";
        string right = "";

        foreach (var line in lines){
            start += line[0];
            start += line[1];
            start += line[2];
            left += line[7];
            left += line[8];
            left += line[9];
            right += line[12];
            right += line[13];
            right += line[14];
            output.Add(start, (left, right));
            start = "";
            left = "";
            right = "";
        }
        return output;
    }
}