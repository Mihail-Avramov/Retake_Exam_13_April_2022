using System;
using System.Collections.Generic;
using System.Linq;

namespace MealPlan
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Queue<string> mealPlan = new Queue<string>(Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries));
            Stack<int> dailyCalories = new Stack<int>(Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));

            Dictionary<string, int> menue = new Dictionary<string, int>()
            {
                {"salad", 350},
                {"soup", 490},
                {"pasta", 680},
                {"steak", 790}
            };

            int mealCounter = 0;

            while (mealPlan.Any() && dailyCalories.Any())
            {
                string currentMeal = mealPlan.Dequeue();
                mealCounter++;
                int restCalories = dailyCalories.Pop();

                if (menue.ContainsKey(currentMeal))
                {
                    restCalories -= menue[currentMeal];
                }

                if (restCalories > 0)
                {
                    dailyCalories.Push(restCalories);
                }
                else
                {
                    bool isNegative = true;

                    while (dailyCalories.Any() && isNegative)
                    {
                        restCalories = dailyCalories.Pop() - Math.Abs(restCalories);
                        if (restCalories > 0)
                        {
                            isNegative = false;
                            dailyCalories.Push(restCalories);
                        }
                    }
                }
            }

            if (mealPlan.Any())
            {
                Console.WriteLine($"John ate enough, he had {mealCounter} meals.");
                Console.WriteLine($"Meals left: {string.Join(", ", mealPlan)}.");
            }
            else
            {
                Console.WriteLine($"John had {mealCounter} meals.");
                Console.WriteLine($"For the next few days, he can eat {string.Join(", ", dailyCalories)} calories.");
            }

        }
    }
}
