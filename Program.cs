using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using ExerciseApp2.Exercises;

namespace ExerciseApp2
{
    class Program
    {
        private static bool exit = false;

        static void Main(string[] args)
        {
            Console.WriteLine("Select Menu load method");
            Console.WriteLine("-----------------------");
            Console.WriteLine("A) Load all exercises automatically using AppDomain");
            Console.WriteLine("B) Load a custom menu with descriptions");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.A:
                    LoadUsingAppDomain();
                    break;

                case ConsoleKey.B:
                    LoadCustomMenu();
                    break;

                case ConsoleKey.Q:
                case ConsoleKey.E:
                    return;

                default:
                    break;
                    
            }
        }

        static void LoadCustomMenu()
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

        static void LoadUsingAppDomain()
        {
            var type = typeof(IExercise);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(s => s.GetTypes())
                        .Where(p => type.IsAssignableFrom(p) && p.IsClass);

            while (!exit)
                PrintLoadedMainMenu(types);
        }

        static string GetDescriptionAttributeText(Type t)
        {
            string result = "";
            DescriptionAttribute descriptionAttribute = (DescriptionAttribute)t.GetCustomAttribute(typeof(DescriptionAttribute));
            result = descriptionAttribute?.Description;

            return result;
        }

        static void PrintLoadedMainMenu(IEnumerable<Type> exercises)
        {
            Console.Clear();
            Console.WriteLine("Select Exercise to run:");
            Console.WriteLine("-----------------------");

            Type[] exerArray = exercises.ToArray();

            for (int i = 0; i < exercises.Count(); i++)
            {
                Type t = exerArray[i];

                Console.WriteLine($"{i}) {t.Name} - {GetDescriptionAttributeText(t)}");
            }

            char key = Console.ReadKey(true).KeyChar;
            int selection;

            if (int.TryParse(key.ToString(), out selection))
            {
                if (selection >= exerArray.Length)
                    return; // selection out of range

                IExercise selectedExer = Activator.CreateInstance(exerArray[selection]) as IExercise;
                selectedExer.Run();

                // Stop returning 
                Console.ReadKey();
            }
        }

        static void PrintMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Select Exercise to run:");
            Console.WriteLine("-----------------------");
            Console.WriteLine("A) Exercise 2A1 (Generics Math)");
            Console.WriteLine("B) Exercise 2A2 (StringExtension)");
            Console.WriteLine("C) Exercise 2A3 (LINQ Stuffs)");
            Console.WriteLine("D) Exercise 2B1 (Users)");
            Console.WriteLine("F) Exercise 2B2 (Animals");
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
