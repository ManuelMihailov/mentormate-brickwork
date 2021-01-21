using System;

namespace Mentormate_Brickwork
{
    class Program
    {
        static void Main(string[] args)
        {
            //Try catch to handle the exceptions and write their message into the console
            try
            {
                //Declare two integers for N & M and writing
                int n;
                int m;
                //Declare three arrays for the first layer, the second layer and the visualization of the second layer according to point 5
                string[,] layerOne;
                string[,] layerTwo;
                string[,] layerTwoFancy;
                //Get first line of the input from the console and call the method to assign them to N and M
                AssignDimensions(Console.ReadLine(), out n, out m);
                //Call method for assigning the first layer to the layerOne array
                AssignFirstLayer(n, m, out layerOne);
                //Assign value of layerTwo to the result of the CreateBrickwork method
                layerTwo = CreateBrickwork(n, m, layerOne);
                //Assign value of layerTwoFancy to the result of the CreateFinalBrickwork method
                layerTwoFancy = CreateFinalBrickwork(n, m, layerTwo);
                //Call method to draw layerTwo in the console
                DrawArray(layerTwo);
                //Call method to draw layerTwoFancy to draw in the console
                DrawArrayBrikwork(layerTwoFancy);
            }
            catch (Exception ex)
            {
                //Write in the console the message of the exception
                Console.WriteLine(ex.Message);
            }
        }

        //Method that takes a string in the format of "(number) (number)" splits it and assigns it to already declared integers
        static void AssignDimensions(string input, out int x, out int y)
        {
            //Making an array that holds both numbers as strings
            var split = input.Split(" ");
            //Parsing the first number to an integer
            var x1 = int.Parse(split[0]);
            //Invoke the method for validating a dimension using the first number as a parameter
            ValidateDimension(x1);
            //Parsing the second number to an integer
            var y1 = int.Parse(split[1]);
            //Invoke the method for validating a dimension using the second number as a parameter
            ValidateDimension(y1);
            //After validating instantiate both integers and assign both numbers to them
            x = x1;
            y = y1;
        }

        //Method for assigning first layer to an already declared two-dimensional array
        static void AssignFirstLayer(int x, int y, out string[,] layer)
        {
            //Instantiate the declared layer with both integers from the params for the method as  the dimensions 
            layer = new string[x, y];
            //For loop to read each line based on the x param and the value of the integer "i" being the index for the row
            for (int i = 0; i < x; i++)
            {
                //Create an integer with the value 0 to use as an index for the column in the array
                int counter = 0;
                //Foreach loop that works with the console input, splitting the numbers into an array
                foreach (var number in Console.ReadLine().Split(" "))
                {
                    //Sets value of cell in the multidimensional array
                    layer[i, counter] = number;
                    counter++;
                }
            }
            //invoke method to check if bricklayer is valid
            ValidateLayer(layer, x, y);
        }

        //Method for visualizing a layer
        static void DrawArray(string[,] layer)
        {
            //Empty line in the console so it looks better
            Console.WriteLine("");
            //Set x to the number of rows so we can cycle through them in a for loop
            int x = layer.GetLength(0);
            for (int n = 0; n < x; n++)
            {
                //Calls exntension method for the two-dimensional array to get all values from a row and join them in a string so they can be written them down in the console
                Console.WriteLine(String.Join(" ", layer.GetRow(n)));
            }
        }

        //Only difference from the above method is that when it joins the strings, it doesnt add an empty space in between them
        static void DrawArrayBrikwork(string[,] layer)
        {
            Console.WriteLine("");
            int x = layer.GetLength(0);
            for (int n = 0; n < x; n++)
            {
                Console.WriteLine(String.Join("", layer.GetRow(n)));
            }
        }

        //Method for creating the array to visualize the second layer according to the 5th point of the assessments
        static string[,] CreateFinalBrickwork(int n, int m, string[,] array)
        {
            //Sets an integer with a value equal to the highest digit count used on any brick in the brickwork / used for padding so the visualization looks pleasant and easy to read
            int paddig = (n * m / 2).ToString().Length.LimitMinimum(2);
            //Integer for the quantity of rows in the final array
            int finalN = n * 2 + 1;
            //Integer for the quantity of column in the final array
            int finalM = m * 2 + 1;
            //Declare final two-dimensional array with dimensions stated above
            string[,] finalArray = new string[finalN, finalM];
            //For loop to cycle through the final array and set the values of cells
            for (int y = 0; y < finalN; y++)
            {
                for (int x = 0; x < finalM; x++)
                {
                    //Checks if the current row and column are even, and if they are, copy value from the original second layer, if not, place the separator "*-"
                    if (x % 2 == 1 && y % 2 == 1)
                        finalArray[y, x] = array[y / 2, x / 2].PadRight(paddig, ' ');
                    else
                        finalArray[y, x] = "*-";
                }
            }
            //Two nested for loops that cycle through every cell that is between two bricks
            //First loop cycles through every cell that has a brick cell to the left or right
            for (int y = 1; y < finalN - 1; y += 2)
            {
                for (int x = 2; x < finalM - 1; x += 2)
                {
                    //Check if the values of the brick cells this one is in between, and if it is, replaces it with empty space
                    if (finalArray[y, x - 1] == finalArray[y, x + 1])
                        finalArray[y, x] = new String(' ', paddig);
                }
            }
            //Second loop cycles through every cell that has a brick cell above and below it
            for (int y = 2; y < finalN - 1; y += 2)
            {
                for (int x = 1; x < finalM - 1; x += 2)
                {
                    //Check if the values of the brick cells this one is in between, and if it is, replaces it with empty space
                    if (finalArray[y - 1, x] == finalArray[y + 1, x])
                        finalArray[y, x] = new String(' ', paddig);
                }
            }
            return finalArray;
        }

        //Method for creating the second layer
        static string[,] CreateBrickwork(int x, int y, string[,] layerOne)
        {
            //Create new two-dimensional array
            string[,] layerTwo = new string[x, y];
            //Create a counter for what brick it's placing down
            int brickCounter = 1;
            //Nested for loop to cycle through every row and column and place brick where it's possible
            for (int n = 0; n < x; n++)
            {
                for (int m = 0; m < y; m++)
                {
                    //Checks if the currently selected cell is null, if it isn't, it means there's a brick there and skips this cycle
                    if (layerTwo[n, m] != null)
                        continue;
                    //Checks if this is the last column, if not checks if the cell in this position in the original layer has the same value as the cell to the right of it in the original layer 
                    if (m < y - 1 && layerOne[n, m] != layerOne[n, m + 1])
                    {
                        //Sets both cells in the new layer to a brick
                        layerTwo[n, m] = brickCounter.ToString();
                        layerTwo[n, m + 1] = brickCounter.ToString();
                    }
                    //Checks if this is the last row, if not checks if the cell in this position in the original layer has the same value as the cell under it in the original layer 
                    else if (n < x - 1 && layerOne[n, m] != layerOne[n + 1, m])
                    {
                        //Sets both cells in the new layer to a brick
                        layerTwo[n, m] = brickCounter.ToString();
                        layerTwo[n + 1, m] = brickCounter.ToString();
                    }
                    //In case it can't place a brick it throws an exception with the message "-1"
                    else
                        throw new Exception("-1");
                    brickCounter++;
                }
            }
            return layerTwo;
        }

        //Method for validating N & M
        static void ValidateDimension(int x)
        {
            //Checks if the given integer (layer dimension) is not even or if it's less or equal to 100, if either is true, throw argument exception for invalid dimension
            if (!(x % 2 == 0 || x <= 100))
                throw new ArgumentException("Invalid dimensions for the first layer.");
        }

        //Method for validating the that there aren't any bricks which span 3 rows or columns, cycles through all cells, checks if they have a neighouring one that has the same value as them
        //If it does it marks both cells off as a "valid brick cell" and ignores them in future cicles
        static void ValidateLayer(string[,] input, int x, int y)
        {
            string[,] layer = (string[,])input.Clone();
            for (int i = 0; i < x; i++)
            {
                for (int n = 0; n < y; n++)
                {
                    //Checks if cell is a valid brick and skips cycle if it is
                    if (layer[i, n] == "x")
                        continue;
                    //Checks if it's the last column, and if not then checks if the cell to it's right is part of the same brick
                    if (n < y - 1 && layer[i, n] == layer[i, n + 1])
                    {
                        //if true, marks both of the cells as valid
                        layer[i, n] = "x";
                        layer[i, n + 1] = "x";
                    }
                    //Checks if it's the last row, and if not then checks if the cell under it is part of the same brick
                    else if (i < x - 1 && layer[i, n] == layer[i + 1, n])
                    {
                        //if true, marks both of the cells as valid
                        layer[i, n] = "x";
                        layer[i + 1, n] = "x";
                    }
                    //If a cell does not have another cell from the same brick adjacent to it, throws argument exception for invalid brick layer
                    else
                        throw new ArgumentException("Invalid brikwork layer.");
                }
            }
        }

    }
}
