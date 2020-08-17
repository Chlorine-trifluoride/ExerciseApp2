using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ExerciseApp2.Exercises
{
    class Exercise2B1 : IExercise
    {
        bool quit = false;

        public void Run()
        {
            while (!quit)
                DisplayMenu();
        }

        private void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("Select Option:");
            Console.WriteLine("-----------------------");
            Console.WriteLine("A) Create new account");
            Console.WriteLine("B) List all accounts");
            Console.WriteLine("C) Login to account");
            Console.WriteLine("---------------");
            Console.WriteLine("Q/E) Exit to Main Menu");

            ConsoleKey key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.A:
                    Console.WriteLine();
                    CreateNewUser();
                    break;

                case ConsoleKey.B:
                    ListAllUsers();
                    break;

                case ConsoleKey.C:
                    Console.WriteLine();
                    DisplayLogin();
                    break;

                case ConsoleKey.Q:
                case ConsoleKey.E:
                    quit = true;
                    break;

                default:
                    break;
            }
        }

        private void CreateNewUser()
        {
            string userName = "";
            for (; ; )
            {
                Console.WriteLine("Enter username: ");
                userName = Console.ReadLine();

                if (userName != "")
                    break;
            }

            for (; ; )
            {

                // TODO: use readkey
                Console.WriteLine("Enter password: ");
                string plainPassword = Console.ReadLine();

                Console.WriteLine("Verify password: ");
                string plainPassword2 = Console.ReadLine();

                if (plainPassword == "" || plainPassword != plainPassword2)
                {
                    Console.WriteLine("Empty or missmatched passwords");
                    continue;
                }

                else
                {
                    User.AddUser(userName, plainPassword);
                    Console.WriteLine("Account Created Succesfully");
                    Console.ReadKey();
                    break;
                }
            }
        }

        private void ListAllUsers()
        {
            foreach (User user in User.users)
            {
                Console.WriteLine($"{user.UserName}:{user.PasswordHash}");
            }

            Console.ReadKey();
        }

        private void DisplayLogin()
        {
            string userName = "";
            for (; ; )
            {
                Console.WriteLine("Enter username: ");
                userName = Console.ReadLine();

                if (userName != "")
                    break;
            }

            string plainPassword;
            for (; ; )
            {
                // TODO: use readkey
                Console.WriteLine("Enter password: ");
                plainPassword = Console.ReadLine();

                if (plainPassword == "")
                {
                    Console.WriteLine("Password can not be empty");
                    continue;
                }

                else
                {
                    if (User.TryLogin(userName, plainPassword))
                    {
                        Console.WriteLine("Login successfull");
                    }

                    else
                    {
                        Console.WriteLine("Login Failure");
                    }

                    Console.ReadKey();
                    return;
                }
            }
        }

        internal class User
        {
            public static List<User> users = new List<User>();
            public string UserName { get; }
            public string PasswordHash { get; }

            private User(string userName, string password)
            {
                this.UserName = userName;
                this.PasswordHash = ComputeHash(password);
            }

            private static string ComputeHash(string input)
            {
                byte[] bytes = Encoding.ASCII.GetBytes(input + GLOBALS.SALT);
                byte[] hash;

                using (SHA512 shaM = new SHA512Managed())
                {
                    hash = shaM.ComputeHash(bytes);
                }

                return Convert.ToBase64String(hash);
            }

            public static void AddUser(string userName, string password)
            {
                users.Add(new User(userName, password));
            }

            public static bool TryLogin(string username, string password)
            {
                var userEntry = users.FirstOrDefault(x => x.UserName == username);

                if (!(userEntry is null))
                {
                    string inputPasswordHash = ComputeHash(password);

                    if (userEntry.PasswordHash == inputPasswordHash)
                        return true;
                }

                return false;
            }
        }
    }
}
