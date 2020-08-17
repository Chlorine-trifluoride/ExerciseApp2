using System;
using ExerciseApp2.Exercises;

namespace ExerciseApp2
{
    class Program
    {
        private static bool exit = false;

        static void Main(string[] args)
        {
            while (!exit)
                PrintMainMenu();
        }

        static void RunExercise<T>() where T : IExercise, new()
        {
            Console.Clear();
            new T().Run();
            Console.ReadKey();
        }

        static void PrintMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Select Exercise to run:");
            Console.WriteLine("-----------------------");
            Console.WriteLine("A) Exercise 2A1");
            Console.WriteLine("B) Exercise 2A2");
            Console.WriteLine("C) Exercise 2A3");
            Console.WriteLine("D) Exercise 2B1");
            Console.WriteLine("F) Exercise 2B2");
            Console.WriteLine("---------------");
            Console.WriteLine("Q/E) Exit Program");

            ConsoleKey key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.A:
                    RunExercise<Exercise2A1>();
                    break;

                case ConsoleKey.B:
                    RunExercise<Exercise2A2>();
                    break;

                case ConsoleKey.C:
                    RunExercise<Exercise2A3>();
                    break;

                case ConsoleKey.D:
                    RunExercise<Exercise2B1>();
                    break;

                case ConsoleKey.F:
                    RunExercise<Exercise2B2>();
                    break;

                case ConsoleKey.Q:
                case ConsoleKey.E:
                    exit = true;
                    break;

                default:
                    break;
            }
        }
    }
}
