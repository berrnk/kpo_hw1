using System;
namespace kpo_hw1.Objects;
using kpo_hw1.Interfaces;

public abstract class Herbo : Animal, IKindness
{
    public int Kindness { get; set; } = 0;
    private const int DefaultKindness = 0;

    public Herbo(string name, int food, int health, int number) : base(name, food, health, number)
    {
        Kindness = DefaultKindness;
    }

    public override string ToString()
    {
        return $"{GetType().Name}: Name: {Name}, Food: {Food}, Health: {Health}, Number: {Number}, Kindness: {Kindness}";
    }
}
