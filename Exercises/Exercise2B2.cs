using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ExerciseApp2.Exercises
{
    class Exercise2B2 : IExercise
    {
        internal class Animal
        {
            protected string name, sound;

            public Animal(string name, string sound)
            {
                this.name = name;
                this.sound = sound;
            }

            public virtual void Greet()
            {
                Console.WriteLine($"{name} says {sound}");
            }
        }

        internal class Dog : Animal
        {
            private string breed;

            public Dog(string name, string sound, string breed) : base(name, sound)
            {
                this.breed = breed;
            }

            public override void Greet()
            {
                Console.WriteLine($"{breed} says {sound}");
            }
        }

        public void Run()
        {
            Animal[] animals =
            {
                new Dog("Dog", "Woof", "Belgian Sheepdog"),
                new Animal("Cat", "Meow"),
                new Animal("Cow", "Moo")
            };

            foreach (Animal a in animals)
            {
                a.Greet();
            }
        }
    }
}
