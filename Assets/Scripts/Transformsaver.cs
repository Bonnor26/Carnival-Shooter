using System.IO;
using UnityEngine;

public class TransformSaver : MonoBehaviour, ISaveable
{
    [SerializeField] private Transform player;
    [SerializeField] private Target targetPrefab;

    private string FilePath
    {
        get { return Path.Combine(Application.persistentDataPath, "positions.json"); }
    }

    public void Save()
    {
        TransformSaveData data = new TransformSaveData();
        data.playerPosition = player.position;

        foreach (Target t in FindObjectsByType<Target>(FindObjectsSortMode.None))
        {
            TargetData td = new TargetData();
            td.position = t.transform.position;
            td.speed = t.Speed;
            td.size = t.transform.localScale.x;
            td.points = t.Points;
            td.direction = t.Direction;
            td.color = t.GetComponent<SpriteRenderer>().color;
            data.targets.Add(td);
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(FilePath, json);
        Debug.Log("Saved positions to: " + FilePath);
    }

    public void Load()
    {
        if (!File.Exists(FilePath))
        {
            Debug.LogWarning("No positions file found. Press S to save first.");
            return;
        }

        string json = File.ReadAllText(FilePath);
        TransformSaveData data = JsonUtility.FromJson<TransformSaveData>(json);

        player.position = data.playerPosition;

        foreach (Target t in FindObjectsByType<Target>(FindObjectsSortMode.None))
            Destroy(t.gameObject);

        foreach (TargetData td in data.targets)
        {
            Target t = new TargetBuilder()
                .SetSpeed(td.speed)
                .SetSize(td.size)
                .SetPoints(td.points)
                .SetColor(td.color)
                .Build(targetPrefab, td.position);
            t.Direction = td.direction;
        }

        Debug.Log("Loaded positions from: " + FilePath);
    }
}