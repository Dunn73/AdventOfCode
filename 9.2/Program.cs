Solution solution = new();
Dictionary<int, List<int>> output = solution.SensorParser("sensor.txt");
// foreach (var element in output){
    
//     Console.Write($"{element.Key}");
//     foreach (var value in element.Value){
//         Console.Write($" {value}");
//     }
//     Console.WriteLine();
// }

List<List<int>> differences = solution.DifferenceList();
foreach (var element in differences){
    foreach (var number in element){
        Console.Write(number);
    }
}

Console.ReadKey();

class Solution {
    public List<List<int>> DifferenceList(){
        List<List<int>> output = new();
        Dictionary<int, List<int>> input = SensorParser("sensor.txt");
        int count = 0;
        int finalNumber = 0;
        int pointer = 0;
        for (int a = 0; a < input.Count; a++){
            List<int> currentSegment = input[a];
            output.Add(currentSegment);
            for (int i = 0; i < input[a].Count; i++){
                int currentSegmentCount = input[a].Count - count;
                List<int> currentList = new();
                for (int j = 0; j < currentSegmentCount-1; j++){
                    currentList.Add(currentSegment[j+1] - currentSegment[j]);
                }
                currentSegment = currentList;
                count++;
                output.Add(currentList);
                if (i == input[a].Count-1){
                    for (int k = input[a].Count; k > 0; k--){
                        pointer = output[k-1].First()-pointer;
                    }
                }
                finalNumber += pointer;
            };
            currentSegment.Clear();
            count = 0;
            pointer = 0;
            output.Clear();
        }

        Console.WriteLine($"finalNumber = {finalNumber}");
        Console.WriteLine("This works");
        return output;
    }
    public Dictionary<int, List<int>> SensorParser(string filename) {
        Dictionary<int, List<int>> output = new();
        string combinedDigits = "";
        string[] list = File.ReadAllLines(filename);
        int count = -1;
        foreach (var element in list){
            count++;
            output.Add(count, new List<int>());
            for (int i = 0; i < element.Length; i++){
                if (element[i] == '-'){
                    combinedDigits += '-';
                }
                else if (element[i] != ' '){
                    combinedDigits += element[i];
                }
                else {
                    output[count].Add(Convert.ToInt32(combinedDigits));
                    combinedDigits ="";
                }
                if (i == element.Length-1){
                    output[count].Add(Convert.ToInt32(combinedDigits));
                    combinedDigits ="";
                }
            }
        }
        return output;
    }
}