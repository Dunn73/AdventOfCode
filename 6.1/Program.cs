Solution solution = new();
Console.WriteLine(solution.possibleWins(38, 234));
Console.WriteLine(solution.MultipliedWins());
Console.ReadKey();

class Solution {
    List <(int, int)> records = [(38,234), (67, 1027), (76, 1157), (73, 1236)];
    public int MultipliedWins(){
        int multipliedWins = 0;
        foreach (var element in records){
            if (multipliedWins == 0 && possibleWins(element.Item1, element.Item2) > 0){
                multipliedWins = possibleWins(element.Item1, element.Item2);
            }
            else if (multipliedWins > 0 && possibleWins(element.Item1, element.Item2) > 0){
                multipliedWins *= possibleWins(element.Item1, element.Item2);
            }

        }
        return multipliedWins;
    }
    public int possibleWins (int seconds, int distance){
        int totalwins = 0;
        for (int i = 0; i < seconds+1; i++){
            int j = seconds-i;
            if (IsWin(seconds, j, distance)){
                totalwins ++;
            };
        }
        return totalwins;
    }
    public bool IsWin(int seconds, int secondsPushed, int distance){
        if ((seconds - secondsPushed)*secondsPushed >= distance){
            return true;
        }
        return false;
    }
}