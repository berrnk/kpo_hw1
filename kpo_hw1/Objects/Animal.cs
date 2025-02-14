using System;
using kpo_hw1.Interfaces;
namespace kpo_hw1.Objects;

public abstract class Animal : IAlive, IInventory
{
    public int Number { get; set; }
    public int Food { get; set; }
    public int Health { get; set; }
    public string Name { get; set; }

    public Animal(string name, int food, int health, int number)
    {
        Name = name;
        Food = food;
        Health = health;
        Number = number;
    }

    public override string ToString()
    {
        return $"{GetType().Name}: Name: {Name}, Health: {Health}, Food: {Food}, Number: {Number}";
    }
}
