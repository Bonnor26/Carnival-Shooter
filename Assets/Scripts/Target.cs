using UnityEngine;

public class Target : MonoBehaviour
{
    public int Points { get; set; }
    public float Speed { get; set; }
    private float direction = 1f;

    void Update()
    {
        transform.Translate(Vector3.right * direction * Speed * Time.deltaTime);

        // bounce at the edges
        if (transform.position.x > 8f) direction = -1f;
        else if (transform.position.x < -8f) direction = 1f;
    }

    public void Hit()
    {
        TargetEvents.Notify(this);   // Observer pattern fires here
        Destroy(gameObject);
    }
}