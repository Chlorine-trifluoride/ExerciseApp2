using System;
using System.Collections.Generic;
using System.Text;

namespace ExerciseApp2.Exercises
{
    class Exercise2A1 : IExercise
    {
        /*
         * Write two methods Sum and Difference, which both have two parameters of type int, named value1 and value2. Sum should print “The sum of *value1* and
         * *value2* is *sum*”, and Difference should print “The difference of *value1* and *value2* is *difference*.
        Call the methods from the main method multiple times with different values.
        */
        public void Run()
        {
            Sum(1, 2);
            Sum(2.5f, 4.5f);

            Difference(2, 2);
            Difference(6.5f, 3.5f);
        }

        private void Sum<T>(T value1, T value2)
        {
            dynamic a = value1;
            dynamic b = value2;

            try
            {
                Console.WriteLine($"The sum of {a} and {b} is {a + b})");
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        private void Difference<T>(T value1, T value2)
        {
            dynamic a = value1;
            dynamic b = value2;

            try
            {
                Console.WriteLine($"The difference of {a} and {b} is {a - b})");
            }

            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
