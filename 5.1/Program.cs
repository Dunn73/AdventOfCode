
Solution solution = new();

// List<long> output = solution.seedParser("almanac.txt");

// foreach (long element in output){
//     Console.WriteLine(element);
// }
Dictionary<long, List<long>> output = solution.SeedMapper("almanac.txt.");
foreach (var element in output){
    Console.WriteLine($"{element.Key}, ");
    foreach (var listItem in element.Value) {
        Console.WriteLine($", {listItem}");
    }
}

Console.WriteLine($"The lowest location number is: {solution.LowestLocation(output)}");

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
// 38552430
    public long LowestLocation(Dictionary<long, List<long>> input){
        long currentLowest = 0;
        long lowest = 0;
        foreach (var key in input){
            if (lowest == 0){
                lowest = key.Value.Last();
            }
            currentLowest = key.Value.Last();
            lowest = Math.Min(lowest, key.Value.Last());
            
        }
        return lowest;
    }
    public Dictionary<long, List<long>> SeedMapper(string filename){
        Dictionary<long, List<long>> output = new();
        List<long> seedList = seedParser(filename);
        Dictionary<int, List<(long, long, long)>> seedMap = MapParser(filename);

        long currentMap = -1;
        long oldMap = -1;
        foreach (var element in seedList){
            output.Add(element, new List<long>());
            for (int i = 0; i < seedMap.Count; i++){
                oldMap = currentMap;
                foreach (var map in seedMap[i]){
                    if (currentMap == -1){
                        currentMap = element;
                    }
                    if (currentMap >= map.Item2 && currentMap < map.Item2+map.Item3){
                        currentMap = currentMap - map.Item2 + map.Item1;
                        output[element].Add(currentMap);
                        break;
                    }
                }
                    if (oldMap == currentMap){
                        if (currentMap == -1){
                            output[element].Add(element);
                            currentMap = element;
                        }
                        else {
                            output[element].Add(currentMap);
                        }
                        
                    }
            }
            currentMap =-1;
            oldMap = -1; 
        }
        return output;
    }
    public List<long> seedParser(string filename){
        string ?lines = File.ReadLines(filename).FirstOrDefault();

        List<long> seedValues = new();

        string combinedDigits ="";
        for (int i = 0; i < lines.Length; i++){
            if (digits.ContainsKey(lines[i])){
                combinedDigits += lines[i];
            }
            if (lines[i] == ' ' && combinedDigits != ""){
                seedValues.Add(Convert.ToInt64(combinedDigits));
                combinedDigits = "";
            }
        }
        Console.WriteLine($"Below is the seed values");
        foreach (long element in seedValues){
        Console.WriteLine(element);
}
        return seedValues;
    }
    public Dictionary<int, List<(long, long, long)>> MapParser(string filename){
        Dictionary<int, List<(long, long, long)>> output = new();
        int currentMap = 0;
        int currentDigit = 0;
        string combinedDigits1 ="";
        string combinedDigits2 ="";
        string combinedDigits3 ="";

        for (int i = 0; i < 7; i++){
            output.Add(i, new List<(long, long, long)>());
        }
        var lines = File.ReadLines(filename).Skip(3);
        foreach ( var element in lines){
            try {
                for (int i = 0; i < element.Length; i++){
                    if (digits.ContainsKey(element[i])){
                        switch (currentDigit){
                            case 0:
                                combinedDigits1 += element[i];
                                break;
                            case 1:
                                combinedDigits2 += element[i];
                                break;
                            case 2:
                                combinedDigits3 += element[i];
                                break;
                        }   
                    }
                    if (element[i] == ' '){
                        if (combinedDigits1 != "" || combinedDigits2 != "" || combinedDigits3 != ""){
                            currentDigit ++;
                        }
                    }
                    if (i == element.Length-1){
                        output[currentMap].Add((Convert.ToInt64(combinedDigits1),Convert.ToInt64(combinedDigits2),Convert.ToInt64(combinedDigits3))) ;
                        combinedDigits1 = "";
                        combinedDigits2 ="";
                        combinedDigits3 ="";
                        currentDigit = 0;
                    }
                }
            }
            catch {
                currentMap++;
            }
        }
        foreach(var element in output){
            Console.WriteLine($"\nBelow is map # {element.Key}");
            foreach (var item in element.Value){
                Console.WriteLine(item);
            }
        }
        return output;
    }
}





/*

1st number = what the seed corresponds to
2nd number = start number of seed
3rd number = range from start

2nd number + 3rd number-1 = max 
2nd number = min

make a dictionary<string, List<long>

*/