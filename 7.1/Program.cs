Solution solution = new();
Dictionary<int, (string, string)> cardData = solution.HandParser("hands.txt");
Dictionary<int, List<(string, string)>> sortedHands = solution.HandSorterType(cardData);
foreach(var element in sortedHands){
    foreach (var list in element.Value){
        Console.WriteLine($"{list.Item1}, {list.Item2}");
    }
}
Console.WriteLine(solution.countValues(sortedHands));
//foreach (var element in solution.HandParser("hands.txt"))
//Console.WriteLine(element);




Console.ReadKey();

class Solution {
    Dictionary<char, int> cardValues = new(){
        {'2',0},
        {'3',1},
        {'4',2},
        {'5',3},
        {'6',4},
        {'7',5},
        {'8',6},
        {'9',7},
        {'T',8},
        {'J',9},
        {'Q',10},
        {'K',11},
        {'A',12}
    };
    public int countValues(Dictionary<int, List<(string, string)>> sortedHands){
        int count = 1000;
        int total = 0;
        foreach (var element in sortedHands.Values){
            foreach (var value in element){
                total += Convert.ToInt32(value.Item2)*count;
                count --;
            } 
        }
        return total;
    }
    public Dictionary<int, List<(string, string)>> HandSorterType(Dictionary<int, (string, string)> input){
        Dictionary<char, int> cardTotals = new(){
        {'2',0},
        {'3',0},
        {'4',0},
        {'5',0},
        {'6',0},
        {'7',0},
        {'8',0},
        {'9',0},
        {'T',0},
        {'J',0},
        {'Q',0},
        {'K',0},
        {'A',0}
    };
        Dictionary<int, List<(string, string)>> output = new();
        for (int i = 0; i < 7; i++){
            output.Add(i, new List<(string, string)>());
        }

        foreach (var element in input){
            for (int i = 0; i < 5; i++){
                cardTotals[element.Value.Item1[i]] ++;
            }

            if (cardTotals.ContainsValue(5)){
                output[0].Add((element.Value.Item1, element.Value.Item2));
            }
            else if (cardTotals.ContainsValue(4)){
                output[1].Add((element.Value.Item1, element.Value.Item2));
            }
            else if (cardTotals.ContainsValue(3) && cardTotals.ContainsValue(2)){
                output[2].Add((element.Value.Item1, element.Value.Item2));
            }
            else if (cardTotals.ContainsValue(3)){
                output[3].Add((element.Value.Item1, element.Value.Item2));
            }
            else if (cardTotals.Values.Count(v => v == 2) == 2){        // count the number of times 2 appears to look for 2 pair
            output[4].Add((element.Value.Item1, element.Value.Item2));
            }
            else if (cardTotals.ContainsValue(2)){
                output[5].Add((element.Value.Item1, element.Value.Item2));
            }
            else {
                output[6].Add((element.Value.Item1, element.Value.Item2));
            }
            
            cardTotals = new(){{'2',0},{'3',0},{'4',0},{'5',0},{'6',0},{'7',0},{'8',0},{'9',0},{'T',0},{'J',0},{'Q',0},{'K',0},{'A',0}};
        } 

        // Sorts each list by index value
        foreach (var key in output.Keys){
            for (int i = 4; i > -1; i--){
                output[key] = output[key].OrderByDescending(item => cardValues[item.Item1[i]]).ToList();
            }
            
        }
        
        return output;
    }
    public Dictionary<int, (string, string)> HandParser(string filename){
        string[] line = File.ReadAllLines(filename);
        Dictionary<int, (string, string)> output = new();
        string combinedDigits1 ="";
        string combinedDigits2 ="";
        bool bidSwitch = false;
        int count = 0;

        foreach (var element in line){
            for (int i = 0; i < element.Length; i++){
                if (element[i] == ' '){
                    bidSwitch = true;
                }
                else if (bidSwitch){
                    combinedDigits2 += element[i];
                }
                else {
                    combinedDigits1 += element[i];
                }
            }
            output.Add(count, (combinedDigits1, combinedDigits2));
            count ++;
            combinedDigits1 ="";
            combinedDigits2 ="";
            bidSwitch = false;
        }
        return output;
    }
    

}