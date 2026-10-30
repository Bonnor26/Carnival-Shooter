using UnityEngine;

public class Target : MonoBehaviour
{
    public int Points { get; set; }
    public float Speed { get; set; }
    public float Direction { get; set; } = 1f;   // now public so it can be saved

    void Update()
    {
        transform.Translate(Vector3.right * Direction * Speed * Time.deltaTime);

        // bounce at the edges
        if (transform.position.x > 8f) Direction = -1f;
        else if (transform.position.x < -8f) Direction = 1f;
    }

    public void Hit()
    {
        TargetEvents.Notify(this);   // Observer pattern fires here
        Destroy(gameObject);
    }
}