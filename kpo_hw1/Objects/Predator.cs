using System;
namespace kpo_hw1.Objects;

public abstract class Predator : Animal
{
    public Predator(string name, int food, int health, int number) : base(name, food, health, number)
    {
    }
}
