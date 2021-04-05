namespace Test_DOne.Models.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    /// <summary>
    /// Extensions method class for the strings
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// To cast the string value to coordinate Model
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Coordinate ToCoordinate(this string value)
        {
            Coordinate c = new Coordinate();
            int indexOfWhiteSpace = 0;
            List<string> values = new List<string>();
            try
            {
                indexOfWhiteSpace = value.IndexOf<char>(i => !char.IsWhiteSpace(i));
                if (indexOfWhiteSpace > 0)
                {
                    //Split the string value by space
                    values = value.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries).ToList();
                }
                else
                {
                    //Split the string value by chars
                    values = value.Split().ToList();
                }
                //If the total of values aren't equals to 3, thrown an error
                if (values.Count != 3)
                {
                    return new Coordinate("Error: The typed values aren't a correct coordinate, please try again!");
                }
                else
                {
                    c = new Coordinate(values[0].ToInteger(), values[1].ToInteger(), values[2]);
                }
            }
            catch (Exception ex)
            {

            }
            return c;
        }
        /// <summary>
        /// To cast the string value to grid bounds model
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static GridBounds ToGridBounds(this string value)
        {
            //Declarations
            GridBounds g = new GridBounds();

            int indexOfWhiteSpace = 0;
            List<string> values = new List<string>();
            try
            {
                indexOfWhiteSpace = value.IndexOf<char>(c => !char.IsWhiteSpace(c));
                if (indexOfWhiteSpace > 0)
                {
                    //Split the string value by space
                    values = value.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries).ToList();
                }
                else
                {
                    //Split the string value by chars
                    values = value.Split().ToList();
                }
                //If the total of values aren't equals to 2, thrown an error
                if (values.Count != 2)
                {
                    return new GridBounds("Error: The typed values aren't a correct coordinate, please try again!");
                }
                else
                {
                    g = new GridBounds(values[0].ToInteger(), values[1].ToInteger());
                }
            }
            catch (Exception ex)
            {

            }
            return g;
        }
        /// <summary>
        /// To cast the string value to integer value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static int ToInteger(this string value)
        {
            int result = 0;
            try
            {
                //To remove special characters into the string
                StringBuilder sb = new StringBuilder();
                foreach (char c in value)
                {
                    if ((c >= '0' && c <= '9'))
                    {
                        sb.Append(c);
                    }
                }
                //Test if the resultant string is an integer
                if (int.TryParse(sb.ToString(), out result))
                {
                    //Parse the clean string to integer
                    result = int.Parse(sb.ToString());
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        /// <summary>
        /// Returns the index of the first element in the sequence 
        /// that satisfies a condition.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.
        /// </typeparam>
        /// <param name="source">
        /// An <see cref="IEnumerable{T}"/> that contains
        /// the elements to apply the predicate to.
        /// </param>
        /// <param name="predicate">
        /// A function to test each element for a condition.
        /// </param>
        /// <returns>
        /// The zero-based index position of the first element of <paramref name="source"/>
        /// for which <paramref name="predicate"/> returns <see langword="true"/>;
        /// or -1 if <paramref name="source"/> is empty
        /// or no element satisfies the condition.
        /// </returns>
        private static int IndexOf<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            int i = 0;

            foreach (TSource element in source)
            {
                if (predicate(element))
                    return i;

                i++;
            }

            return -1;
        }
        /// <summary>
        /// Get the list of movements from the instructions
        /// </summary>
        /// <param name="value">instructions text</param>
        /// <param name="initialize">information of the axis (X & Y) and the Cardinal Direction </param>
        /// <param name="limits">Limits of the axis (X & Y)</param>
        /// <returns></returns>
        public static List<Movement> ToMovements(this string value, Coordinate initialize, GridBounds limits)
        {
            //Declarations
            List<Movement> g = new List<Movement>();
            Coordinate currentPosition = initialize;
            string helper = value;
            string instructions = "";
            int indexOfM = 0;
            try
            {
                //Do the action while the helper has text
                while (helper.Length > 0)
                {
                    indexOfM = helper.IndexOf('M');
                    //Get the movement instructions, I mean, get the rotation instructions (can be more than 1) and the movement instruction
                    instructions = helper.Substring(0, indexOfM);
                    //Split the text into instructions in order to do the movements requested
                    string[] directions = instructions.Trim().Split();
                    foreach (string direction in directions)
                    {
                        Movement result = direction.ToMovement(currentPosition, limits);
                        result.Input = instructions + "M";
                        g.Add(result);
                        //re-init the values of the last coordinate to re-use on the next movement
                        initialize.X = result.currentValueXAxis;
                        initialize.Y = result.currentValueYAxis;
                        initialize.Z = result.currentAxis;

                    }
                    helper = helper.Substring(indexOfM + 1, helper.Length - (indexOfM + 1));
                }
            }
            catch (Exception ex)
            {

            }
            return g;
        }
        /// <summary>
        /// Cast the string value to a Movement model, the movement coordinates are breadcrumbs
        /// </summary>
        /// <param name="value">string with the instructions</param>
        /// <param name="initialize">Starting coordinates, I mea, last coordinate of the rover</param>
        /// <param name="limits">Grid bounds</param>
        /// <returns></returns>
        private static Movement ToMovement(this string value, Coordinate initialize, GridBounds limits)
        {
            //Declarations
            Movement result = new Movement(initialize.X, initialize.Y, initialize.Z);
            string direction = initialize.Z;
            try
            {
                //This loop completes the rotation before the movement is made
                foreach (char v in value)
                {
                    switch (v)
                    {
                        case 'L':
                            direction = getLeftCardinalDirection(direction);
                            break;
                        case 'R':
                            direction = getRightCardinalDirection(direction);
                            break;
                    }
                }
                //This cases are in order to decide the correct direction of the movement and do it.
                //ASSUMPTION: The rovers can't go outside of the grid bounds, but the movements continue.
                //If the movement exceeds the grid bounds, the movement is not performed and the error message will be displayed.
                switch (direction)
                {
                    case "N":
                        result.currentAxis = "N";
                        result.currentValueXAxis = result.lastValueXAxis;
                        if ((result.lastValueYAxis + 1) > limits.Y)
                        {
                            result.currentValueXAxis = result.lastValueXAxis;
                            result.error = "You cannot move the rover out of bounds because it can get lost.";
                            result.done = false;
                        }
                        else
                        {
                            result.currentValueYAxis = result.lastValueYAxis + 1;
                            result.done = true;
                        }
                        break;
                    case "O":
                        result.currentAxis = "O";
                        if ((result.lastValueXAxis - 1) < 0)
                        {
                            result.currentValueYAxis = result.lastValueYAxis;
                            result.error = "You cannot move the rover out of bounds because it can get lost.";
                            result.done = false;
                        }
                        else
                        {
                            result.currentValueXAxis = result.lastValueXAxis - 1;
                            result.done = true;
                        }
                        result.currentValueYAxis = result.lastValueYAxis;
                        break;
                    case "S":
                        result.currentAxis = "S";
                        result.currentValueXAxis = result.lastValueXAxis;
                        if ((result.lastValueYAxis - 1) < 0)
                        {
                            result.lastValueYAxis = result.lastValueYAxis;
                            result.error = "You cannot move the rover out of bounds because it can get lost.";
                            result.done = false;
                        }
                        else
                        {
                            result.currentValueYAxis = result.lastValueYAxis - 1;
                            result.done = true;
                        }
                        break;
                    case "E":
                        result.currentAxis = "E";
                        if ((result.lastValueXAxis + 1) > limits.X)
                        {
                            result.currentValueXAxis = result.lastValueXAxis;
                            result.error = "You cannot move the rover out of bounds because it can get lost.";
                            result.done = false;
                        }
                        else
                        {
                            result.currentValueXAxis = result.lastValueXAxis + 1;
                            result.done = true;
                        }
                        result.currentValueYAxis = result.lastValueYAxis;
                        break;
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        /// <summary>
        /// To get the cardinal direction, based on the instruction to turn 90 to the Left
        /// </summary>
        /// <param name="value">Current cardinal direction</param>
        /// <returns></returns>
        private static string getLeftCardinalDirection(this string value)
        {
            string direction = "";
            switch (value)
            {
                case "N":
                    direction = "O";
                    break;
                case "O":
                    direction = "S";
                    break;
                case "S":
                    direction = "E";
                    break;
                case "E":
                    direction = "N";
                    break;
            }
            return direction;
        }
        /// <summary>
        /// To get the cardinal direction, based on the instruction to turn 90 to the Right
        /// </summary>
        /// <param name="value">Current cardinal direction</param>
        /// <returns></returns>
        private static string getRightCardinalDirection(this string value)
        {
            string direction = "";
            switch (value)
            {
                case "N":
                    direction = "E";
                    break;
                case "E":
                    direction = "S";
                    break;
                case "S":
                    direction = "O";
                    break;
                case "O":
                    direction = "N";
                    break;
            }
            return direction;
        }
    }
}