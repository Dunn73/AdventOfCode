Solution solution = new();

var output = solution.MazeParser("maze.txt");
for (int i = 0; i < output.GetLength(0); i++){

    for (int j = 0; j < output.GetLength(1); j++){

    }
}

Console.ReadKey();

class Solution {
    Dictionary<char, int> output = new();
    public char[,] MazeParser(string filename) {
        string[] lines = File.ReadAllLines(filename);
        char[,] maze = new char[lines.Length,lines[0].Length];
        for (int i = 0; i < lines.Length; i++){
            for (int j = 0; j < lines[i].Length; j++){
                maze[i,j] = lines[i][j];
            }
        }

        return maze;
    }
}

/*
s is at position [58, 51]
s goes left and up
since its a loop i can just count how long before it comes back to start and divide by 2
i can turn this whole file into a character board
take the coordinates of s
begin moving through the loop
counting on each valid move
look at the coordinate of current loop
when i reach starting loop again end the sequence
each pipe will do 2 things, note the direction it came from, the direction its going, and alert the next pipe the direction it received
*/ 