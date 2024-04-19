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
    public long TotalInstructions(Dictionary<string, (string, string)> nodes, string directions) {
        long count = 0;
        int directionCount = 0;
        
        Dictionary<string, (string, string)> ghosts = new();
        Dictionary<string, (string, string)> ghostsCopy = new();
        bool allZ = false;

        foreach (var element in nodes){
            if (element.Key[2] == 'A'){
                ghosts.Add(element.Key, element.Value);
            }
        }
        int ghostCount = ghosts.Count;
        int currentGhosts = 0;

        while (!allZ){
            foreach (var element in ghosts){
                if (directions[directionCount] == 'L'){
                    ghostsCopy.Add(element.Value.Item1, nodes[element.Value.Item1]);
                }
                else {
                    ghostsCopy.Add(element.Value.Item2, nodes[element.Value.Item2]);
                }
        
            }
                if (directionCount < directions.Length-1){
                    directionCount++;
                }
                else {
                    directionCount = 0;
                }
                count++;
                allZ = true;
                ghosts.Clear();
                foreach (var ghost in ghostsCopy){
                    ghosts.Add(ghost.Key, ghost.Value);
                    if (ghost.Key[2] != 'Z'){
                        //Console.WriteLine($"{directionCount}");
                        allZ = false;
                    }
                    else {
                        Console.WriteLine($"{ghost.Key} contains Z at key2 at {count} and {directionCount}");
                    }
                }
                ghostsCopy.Clear();
        }


        Console.WriteLine("BELOW IS THE GHOSTS DICTIONARY");
        foreach (var element in ghosts){
            Console.WriteLine($"{element.Key}, {element.Value}");
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