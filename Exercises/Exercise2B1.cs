using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ExerciseApp2.Exercises
{
    [Description("User Creation/List/Login")]
    class Exercise2B1 : IExercise
    {
        bool quit = false;

        public void Run()
        {
            User.AddUser("test", "test");
            User.AddUser("test2", "test2");
            User.SaveAllUsersToFile();
            User.LoadAllUsersFromFile();

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
            foreach (User user in User.Users)
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
            [JsonPropertyName("users")]
            public static List<User> Users { get; set; } = new List<User>();
            [JsonPropertyName("user")]
            public string UserName { get; set; }
            [JsonPropertyName("passhash")]
            public string PasswordHash { get; set; }

            private User()
            {
                // Required for JsonSerializer.Deserialize
            }

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
                Users.Add(new User(userName, password));
            }

            public static bool TryLogin(string username, string password)
            {
                var userEntry = Users.FirstOrDefault(x => x.UserName == username);

                if (!(userEntry is null))
                {
                    string inputPasswordHash = ComputeHash(password);

                    if (userEntry.PasswordHash == inputPasswordHash)
                        return true;
                }

                return false;
            }

            public static void SaveAllUsersToFile()
            {
                using (StreamWriter writer = new StreamWriter(GLOBALS.DB_PATH))
                {
                    writer.Write(JsonSerializer.Serialize(Users));
                }
            }

            public static void LoadAllUsersFromFile()
            {
                using (StreamReader reader = new StreamReader(GLOBALS.DB_PATH))
                {
                    Users = JsonSerializer.Deserialize<List<User>>(reader.ReadToEnd());
                }
            }
        }
    }
}
