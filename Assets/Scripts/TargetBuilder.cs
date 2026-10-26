using UnityEngine;

// Builder pattern: build a target one step at a time.
public class TargetBuilder
{
    private float speed = 2f;
    private float size = 1f;
    private int points = 10;
    private Color color = Color.white;

    public TargetBuilder SetSpeed(float s) { speed = s; return this; }
    public TargetBuilder SetSize(float s) { size = s; return this; }
    public TargetBuilder SetPoints(int p) { points = p; return this; }
    public TargetBuilder SetColor(Color c) { color = c; return this; }

    public Target Build(Target prefab, Vector3 position)
    {
        Target t = Object.Instantiate(prefab, position, Quaternion.identity);
        t.Speed = speed;
        t.Points = points;
        t.transform.localScale = Vector3.one * size;
        t.GetComponent<SpriteRenderer>().color = color;
        return t;
    }
}