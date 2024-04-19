Solution solution = new();
string filePath = "calibrations.txt";
string[] lines = File.ReadAllLines(filePath);
string[] calibrationValues = Array.ConvertAll(lines, line => $"{line}");

Console.WriteLine($"The total calibration sum is: {solution.TotalCalibrationSum(calibrationValues)}");
Console.ReadKey();

class Solution {
    public int TotalCalibrationSum(string[] calibrationValues){

        Dictionary<string, string> digits = new(){
            {"one", "1"},
            {"two", "2"},
            {"three", "3"},
            {"four", "4"},
            {"five", "5"},
            {"six", "6"},
            {"seven", "7"},
            {"eight", "8"},
            {"nine", "9"},
            {"1", "1"},
            {"2", "2"},
            {"3", "3"},
            {"4", "4"},
            {"5", "5"},
            {"6", "6"},
            {"7", "7"},
            {"8", "8"},
            {"9", "9"}
        };
        int combinedValue;
        int totalValue = 0;

        for (int i = 0; i < calibrationValues.Length; i++) {
            int leftPointer = -1;
            int RightPointer = calibrationValues[i].Length;
            string leftCumulative ="";
            string rightCumulative ="";
            string reverseRightCumulative;
            string leftValue = "0";
            string rightValue = "0";
            string combinedChar = "";
            for (int j = 0; j < calibrationValues[i].Length; j++){
                if (leftValue == "0"){
                    leftPointer ++;
                    leftCumulative += calibrationValues[i][leftPointer];
                }
                if (rightValue == "0"){
                    RightPointer --;
                    rightCumulative += calibrationValues[i][RightPointer];
                }

                foreach (var key in digits.Keys){
                    if (leftCumulative.Contains(key)){
                        leftValue = digits[key];
                    }
                    reverseRightCumulative = string.Concat(rightCumulative.Reverse());
                    if (reverseRightCumulative.Contains(key)){
                        rightValue = digits[key];
                    }
                }
            }
            combinedChar = string.Concat(leftValue, rightValue);
            combinedValue = Convert.ToInt32(combinedChar);
            totalValue += combinedValue;
        } 
        return totalValue;
    }
}