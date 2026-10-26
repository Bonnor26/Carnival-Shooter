using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    [SerializeField] private BulletPool pool;
    [SerializeField] private float moveSpeed = 8f;

    void Update()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        float x = ((kb.dKey.isPressed || kb.rightArrowKey.isPressed) ? 1f : 0f)
                - ((kb.aKey.isPressed || kb.leftArrowKey.isPressed) ? 1f : 0f);
        transform.Translate(Vector3.right * x * moveSpeed * Time.deltaTime);

        if (kb.spaceKey.wasPressedThisFrame)
            pool.Get(transform.position);
    }
}