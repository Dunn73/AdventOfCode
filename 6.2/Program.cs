Solution solution = new();
Console.WriteLine(solution.possibleWins(38677673, 234102711571236));
//Console.WriteLine(solution.MultipliedWins());
Console.ReadKey();

class Solution {
    List <(long, long)> records = [(38,234), (67, 1027), (76, 1157), (73, 1236)];
    public long MultipliedWins(){
        long multipliedWins = 0;
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
    public long possibleWins (long seconds, long distance){
        long totalwins = 0;
        for (long i = 0; i < seconds+1; i++){
            long j = seconds-i;
            if (IsWin(seconds, j, distance)){
                totalwins ++;
            };
        }
        return totalwins;
    }
    public bool IsWin(long seconds, long secondsPushed, long distance){
        if ((seconds - secondsPushed)*secondsPushed >= distance){
            return true;
        }
        return false;
    }
}