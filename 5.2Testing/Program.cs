using System.Diagnostics;

Solution solution = new();

Dictionary<long, List<long>> output = solution.SeedMapper("almanac.txt.");
// foreach (var element in output){
//     Console.WriteLine($"{element.Key}, ");
//     foreach (var listItem in element.Value) {
//         Console.WriteLine($", {listItem}");
//     }
// }

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
    public KeyValuePair<long, long> CumulativeSeedRange(Dictionary<long, long> ranges){
        long min = ranges.Keys.Min();
        long max = ranges.Values.Max();
        foreach ( var element in ranges){
            if (element.Key < min){
                min = element.Key;
            }
            if (element.Value > max){
                max = element.Value;
            }
        }
        return new KeyValuePair<long, long>(min, max);
    }
    public long[] SeedRange(List<long> seeds){

        Dictionary<long, long> seedRanges = new();
        for (int i = 0; i < seeds.Count-1; i+=2){
            seedRanges.Add(seeds[i], seeds[i] + seeds[i+1]);
        }
        KeyValuePair<long, long> cumulativeRange = CumulativeSeedRange(seedRanges);
        long[] output = [cumulativeRange.Key, cumulativeRange.Value];
        foreach (var key in seedRanges){
            Console.WriteLine($"{key.Key}, {key.Value}");
        }
        return output;
    }
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
        long[] seedRange = SeedRange(seedList);

        Dictionary<int, List<(long, long, long)>> seedMap = MapParser(filename);
        Stopwatch stopwatch = new();

        long currentMap = -1;
        long oldMap = -1;
        double currentPercent = 0.0;
        long count = 0;
        for (long j = 2009732824; j < 2334892581; j++){
            output.Add(j, new List<long>());
            for (int i = 0; i < seedMap.Count; i++){
                oldMap = currentMap;
                foreach (var map in seedMap[i]){
                    if (currentMap == -1){
                        currentMap = j;
                    }
                    if (currentMap >= map.Item2 && currentMap < map.Item2+map.Item3){
                        currentMap = currentMap - map.Item2 + map.Item1;
                        output[j].Add(currentMap);
                        break;
                    }
                }
                    if (oldMap == currentMap){
                        if (currentMap == -1){
                            output[j].Add(j);
                            currentMap = j;
                        }
                        else {
                            output[j].Add(currentMap);
                        }
                        
                    }
            }
            currentMap =-1;
            oldMap = -1;
            count ++;
            if (count%10000000 == 0){
                stopwatch.Stop();
                TimeSpan elapsedTime = stopwatch.Elapsed;
                currentPercent++;
                stopwatch.Start();
                Console.WriteLine($"Loading... iterating on {j} Total Elapsed time {elapsedTime.TotalSeconds} seconds");
            }
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
        // Console.WriteLine($"Below is the seed values");
        // foreach (long element in seedValues){
        // Console.WriteLine(element);
        // }
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

950527520, 1035708720 = 702443113
546703948, 670481659 = 800883183
63627802, 342739753 =  183085156 
1141059215, 1387526140 = 1069644087
1655973293, 1754184219 = 2491050754
3948361820, 4041166330 = 2355386760
2424412143, 2672147551 = 125742456
4140139679, 4222712326  = 1169277931
2009732824, 2334892581
actual ranges

4413243179 to 4600664300 is best possible range, but there are no values in that range given
found with this calculation: find range on best humidity-location map using this range move up one
find applicable range
item2 + (lower range - (upperrange-lowerrange))
item2 + (upper range - (upperrange-lowerrange))
6 =  1639932830 to 1827353951 7th row
5 = 3rd row 3174646584 to 3362067705
4 = 2nd row 3147576550 to 3334997671
3 = last row 2436011869 to 2623432990
2 = 2436011869 to 2623432990 no change

1 = rows 
13, 3110048583 to 3205731488
12, 153825796 to 161585024
4, 1387266751 to 1420265461
2, 2211297482 to 2251641531




0 
13, 3110048583 to 3205731488 = no change
12, 153825796 to 161585024  = 352063978 to 355487241
4, 1387266751 to 1420265461 = 1916785101 to 1949783811
2, 2211297482 to 2251641531 = 2716924855 to 2757268904




next best range is on line 26, i know it must be better than previous answer of 196167384, but still in this range so i will set the upper
limit to 196167383 and lower limit to lowest possible which is 187421121

lower = lower - item1 +item 2
higher =  lower+ range
current range = 8746262
6 = 187421121 to 196167383, 26th row
5 = 5th row 1146625210 to 1155371472
4 = 4th row 1958765801 to 1967512063
3 18th row 3062859960 to 3071606222
2 18th row 2729941923 to 2738688185
1 12th row 268093957 to 276840219
0 24th row 430250956 to 438997218

123777711,
, 658317841
, 713185807
, 1107272631
, 1203308227
, 1555812536
, 284906338
, 196167384

output range is item 1 + item3
best mapping from first problem

seed to soil row 3 557997407 to 704365279

soil to fertilizer, row 5 and row 9
5 = 2197749180 to 2391235007 
9 = 608365229 to 751206583

fertilizer to water
from 5 2197749180 to 2391235007 = not foudn
from 9 maps to row 20 574992878 to 1535290150

water to light
from row 5 




0 - 100m not right answer
189786876 from 100m-150m - not right answer
183085156 from 150m to 250m - not right answer
1.01b from 250m to 350m
179661893 from 350m to 450m
520m from 450m to 550m
800m from 550m to 650m
234m from 650m to 850m
702m from 850m to 1b
2.1b from 1b to 1.2b
 573m from 3110048583 to 3205731488
 92743746 from 1916785100 to 1949783825
147077072 from 2716924855 to 2757268904
from 1.2b to 1.5b

*/