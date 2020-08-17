using System;
using System.Linq;

namespace ExerciseApp2.Exercises
{
    static class ExerciseStringExtensions
    {
        public static string RemoveSpaces(this string input)
        {
            // I don't like this approach
            char[] charArray = input.Where(c => c != ' ').ToArray();
            string output = new string(charArray);

            return output;
        }
    }

    class Exercise2A2 : IExercise
    {
        public void Run()
        {
            PrintNumSpaces(GLOBALS.TEST_STRING);

            string testResult = GLOBALS.TEST_STRING.RemoveSpaces();
            Console.WriteLine(testResult);
        }

        private void PrintNumSpaces(string input)
        {
            int numSpaces = input.Count(x => x == ' ');
            Console.WriteLine($"Number of spaces: {numSpaces}");
        }
    }
}
