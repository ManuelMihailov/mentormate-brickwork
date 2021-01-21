using System.Collections.Generic;

namespace Mentormate_Brickwork
{
    //Class for extension methods
    static class Extensions
    {
        //Extension method for two-dimensional arrays to get all elements from one row
        public static IEnumerable<T> GetRow<T>(this T[,] array, int rowIndex)
        {
            //Getting the amount of elements in a single row so we can cycle through them in the for loop below
            int columnsCount = array.GetLength(1);
            for (int colIndex = 0; colIndex < columnsCount; colIndex++)
                yield return array[rowIndex, colIndex];
        }

        //Extension method to set a minimum value for an integer to be
        public static int LimitMinimum(this int value, int min)
        {
            if (value < min)
                return min;
            else 
                return value;
        }
    }
}
