using kpo_hw1.Interfaces;

namespace kpo_hw1.Places;

public class Clinic : IHealth
{
    private const int MaxHealth = 10;
    private const int HealthLimit = 5;

    public bool CheckHealth(IAlive creature)
    {
        return creature.Health > HealthLimit && creature.Health <= MaxHealth;
    }
}