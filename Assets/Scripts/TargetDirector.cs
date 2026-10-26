using UnityEngine;

// The "director": holds the premade target recipes.
public static class TargetDirector
{
    public static Target BuildDuck(Target prefab, Vector3 pos)
    {
        return new TargetBuilder()
            .SetSpeed(2f).SetSize(1f).SetPoints(10).SetColor(Color.yellow)
            .Build(prefab, pos);
    }

    public static Target BuildRabbit(Target prefab, Vector3 pos)
    {
        return new TargetBuilder()
            .SetSpeed(5f).SetSize(0.6f).SetPoints(30).SetColor(Color.cyan)
            .Build(prefab, pos);
    }

    public static Target BuildBear(Target prefab, Vector3 pos)
    {
        return new TargetBuilder()
            .SetSpeed(1f).SetSize(1.6f).SetPoints(5).SetColor(Color.red)
            .Build(prefab, pos);
    }
}