using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace ExerciseApp2.Exercises
{
    [Description("Unique Characters")]
    class Exercise2A3 : IExercise
    {
        public void Run()
        {
            Console.WriteLine($"List of distrinct characters in {GLOBALS.TEST_STRING}");
            PrintAllDistinctCharacters(GLOBALS.TEST_STRING);
            Console.WriteLine("--------");
            Console.WriteLine($"Number of unique letters in {GLOBALS.TEST_STRING}");
            PrintNumDistinctCharacters(GLOBALS.TEST_STRING);
        }

        // Write a method that takes a string as a parameter and prints the number of each unique letter in the string.
        private void PrintNumDistinctCharacters(string input)
        {
            string stringWithOnlyCharacters = StringWithOnlyCharacters(input);

            char[] characters = stringWithOnlyCharacters.Distinct().ToArray();

            foreach (char c in characters)
            {
                int numChar = input.Count(x => x == c);
                Console.WriteLine($"Num of {c}'s: {numChar}");
            }
        }

        //Write a method that takes a string as a parameter and prints each unique letter in that string.
        private void PrintAllDistinctCharacters(string input)
        {
            string stringWithOnlyCharacters = StringWithOnlyCharacters(input);
            Console.WriteLine(GetUniqueCharacters(stringWithOnlyCharacters));
        }

        private string StringWithOnlyCharacters(string input)
        {
            // why is there no global regex in C#? Am I missing something
            var matches = Regex.Matches(input, @"[a-z]", RegexOptions.IgnoreCase);

            var matchArray = from Match match in matches
                             select match.Value;

            return String.Join("", matchArray);
        }

        private char[] GetUniqueCharacters(string input)
        {
            return input.Distinct().ToArray();
        }
    }
}
