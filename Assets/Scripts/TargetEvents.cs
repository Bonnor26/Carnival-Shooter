using System.Collections.Generic;

// The "subject" in the Observer pattern.
public static class TargetEvents
{
    private static readonly List<IScoreObserver> observers = new List<IScoreObserver>();

    public static void Attach(IScoreObserver o)
    {
        if (!observers.Contains(o)) observers.Add(o);
    }

    public static void Detach(IScoreObserver o)
    {
        observers.Remove(o);
    }

    public static void Notify(Target target)
    {
        for (int i = observers.Count - 1; i >= 0; i--)
            observers[i].OnTargetHit(target);
    }
}