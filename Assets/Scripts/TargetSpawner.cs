using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private Target targetPrefab;
    [SerializeField] private float interval = 1.5f;
    private float timer;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer > 0f) return;
        timer = interval;

        Vector3 pos = new Vector3(Random.Range(-7f, 7f), Random.Range(1f, 4f), 0f);

        switch (Random.Range(0, 3))
        {
            case 0: TargetDirector.BuildDuck(targetPrefab, pos); break;
            case 1: TargetDirector.BuildRabbit(targetPrefab, pos); break;
            default: TargetDirector.BuildBear(targetPrefab, pos); break;
        }
    }
}