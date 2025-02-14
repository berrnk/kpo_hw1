using kpo_hw1.Interfaces;
using kpo_hw1.Objects;
using System;
using Microsoft.Extensions.DependencyInjection;
using kpo_hw1.Places;

namespace kpo_hw1
{
    public static class Program
    {
        private static readonly ServiceProvider Services = ConfigureServices();
        private static readonly Zoo Zoo = Services.GetRequiredService<Zoo>();
        private static int _number = 0;

        private static int Number
        {
            get
            {
                _number++;
                return _number;
            }
        }

        private static ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<Zoo>()
                .AddSingleton<IHealth, Clinic>()
                .AddSingleton<IAnimals, AnimalsList>()
                .AddSingleton<IThings, ThingsList>()
                .BuildServiceProvider();
        }

        private static class MenuOption
        {
            public const string AddNewAnimal = "Add New Animal";
            public const string AddNewThing = "Add New Thing";
            public const string ViewAllAnimals = "View All Animals";
            public const string ViewPettingAnimals = "View Petting Animals";
            public const string ViewFoodConsumption = "View Total Food Consumption";
            public const string ViewAllThings = "View All Things";
            public const string Exit = "Exit";
        }

        private static class AnimalType
        {
            public const string Monkey = "Monkey";
            public const string Rabbit = "Rabbit";
            public const string Tiger = "Tiger";
            public const string Wolf = "Wolf";
        }

        private static class ThingType
        {
            public const string Computer = "Computer";
            public const string Table = "Table";
        }

        private static void AddAnimal()
        {
            Console.WriteLine("Let's add a new animal to the zoo!");

            var name = PromptForInput("Enter animal's name:");
            var type = PromptForSelection("Select Animal Type", AnimalType.Monkey, AnimalType.Rabbit, AnimalType.Tiger, AnimalType.Wolf);
            Console.WriteLine($"Selected Animal Type: {type}");

            var foodConsumption = PromptForInt("Enter food consumption per day:");
            var healthLevel = PromptForInt("Enter animal's health level:");

            Animal animal = type switch
            {
                AnimalType.Monkey => new Monkey(name, foodConsumption, healthLevel, Number),
                AnimalType.Rabbit => new Rabbit(name, foodConsumption, healthLevel, Number),
                AnimalType.Tiger => new Tiger(name, foodConsumption, healthLevel, Number),
                AnimalType.Wolf => new Wolf(name, foodConsumption, healthLevel, Number),
                _ => throw new ArgumentOutOfRangeException()
            };

            if (animal is IKindness kindnessAnimal)
            {
                kindnessAnimal.Kindness = PromptForInt("Enter kindness level:");
            }

            if (Zoo.TryAddAnimal(animal))
            {
                Console.WriteLine("Animal successfully added to the zoo!");
            }
            else
            {
                Console.WriteLine("Failed to add animal to the zoo.");
            }
        }

        private static void AddThing()
        {
            Console.WriteLine("Let's add a new thing to the zoo!");

            var type = PromptForSelection("Select Thing Type", ThingType.Computer, ThingType.Table);
            Console.WriteLine($"Selected Thing Type: {type}");

            Thing thing = type switch
            {
                ThingType.Computer => new Computer(Number),
                ThingType.Table => new Table(Number),
                _ => throw new ArgumentOutOfRangeException()
            };

            Zoo.AddThing(thing);
            Console.WriteLine("Thing successfully added to the zoo!");
        }

        private static void ViewAllAnimals()
        {
            if (Zoo.TotalAnimals == 0)
            {
                Console.WriteLine("No animals in the zoo at the moment.");
                return;
            }

            Console.WriteLine("All animals currently in the zoo:");
            foreach (var animal in Zoo.Animals)
            {
                Console.WriteLine(animal);
            }
        }

        private static void ViewAllThings()
        {
            if (Zoo.TotalThings == 0)
            {
                Console.WriteLine("No things in the zoo at the moment.");
                return;
            }

            Console.WriteLine("All things currently in the zoo:");
            foreach (var thing in Zoo.Things)
            {
                Console.WriteLine(thing);
            }
        }

        private static void ViewPettingAnimals()
        {
            var pettingAnimals = Zoo.GetPettingAnimals();
            

            Console.WriteLine("Petting animals currently available in the zoo:");
            foreach (var animal in pettingAnimals)
            {
                Console.WriteLine(animal);
            }
        }

        private static void ViewFoodConsumption()
        {
            Console.WriteLine($"Total food consumption in the zoo: {Zoo.GetTotalFood()} units.");
        }

        private static void RunApplication()
        {
            while (true)
            {
                Console.Clear();
                var choice = PromptForSelection("Zoo Management System", MenuOption.AddNewAnimal, MenuOption.AddNewThing,
                    MenuOption.ViewAllAnimals, MenuOption.ViewPettingAnimals, MenuOption.ViewFoodConsumption,
                    MenuOption.ViewAllThings, MenuOption.Exit);

                switch (choice)
                {
                    case MenuOption.AddNewAnimal:
                        AddAnimal();
                        break;
                    case MenuOption.AddNewThing:
                        AddThing();
                        break;
                    case MenuOption.ViewAllAnimals:
                        ViewAllAnimals();
                        break;
                    case MenuOption.ViewPettingAnimals:
                        ViewPettingAnimals();
                        break;
                    case MenuOption.ViewFoodConsumption:
                        ViewFoodConsumption();
                        break;
                    case MenuOption.ViewAllThings:
                        ViewAllThings();
                        break;
                    case MenuOption.Exit:
                        Console.WriteLine("Exiting the Zoo Management System.");
                        return;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private static void Main(string[] args)
        {
            RunApplication();
        }

        private static string PromptForInput(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        private static int PromptForInt(string message, int maxAttempts = 3)
        {
            Console.Write(message);
            int attempts = 0;

            while (attempts < maxAttempts)
            {
                string input = Console.ReadLine().Trim();
                if (int.TryParse(input, out int result))
                {
                    return result;
                }
                else
                {
                    attempts++;
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    if (attempts < maxAttempts)
                    {
                        Console.Write(message);
                    }
                }
            }
            Console.WriteLine("Maximum attempts reached. Returning default value (0).");
            return 0; 
        }


        private static string PromptForSelection(string title, params string[] options)
        {
            Console.WriteLine($"{title}:");
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            int choice = 0;
            while (choice < 1 || choice > options.Length)
            {
                Console.Write("Please select an option by entering the number: ");
                int.TryParse(Console.ReadLine(), out choice);
            }

            return options[choice - 1];
        }
    }
}
