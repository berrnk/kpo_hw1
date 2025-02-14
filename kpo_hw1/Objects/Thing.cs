using System;
namespace kpo_hw1.Objects;

using kpo_hw1.Interfaces;

public class Thing : IInventory
{
    public int Number { get; set; }

    public Thing(int number)
    {
        Number = number;
    }

    public override string ToString()
    {
        return $"{GetType().Name}: Number: {Number}";
    }
}
