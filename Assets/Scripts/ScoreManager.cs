using System.IO;
using UnityEngine;
using TMPro;

// Observer of target hits. Also saves/loads the score in a BINARY file.
public class ScoreManager : MonoBehaviour, IScoreObserver, ISaveable
{
    [SerializeField] private TMP_Text scoreText;
    private int score;

    private string FilePath
    {
        get { return Path.Combine(Application.persistentDataPath, "score.dat"); }
    }

    void OnEnable() { TargetEvents.Attach(this); }
    void OnDisable() { TargetEvents.Detach(this); }
    void Start() { UpdateText(); }

    public void OnTargetHit(Target target)
    {
        score += target.Points;
        UpdateText();
    }

    private void UpdateText()
    {
        scoreText.text = "Score: " + score;
    }

    public void Save()
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open(FilePath, FileMode.Create)))
        {
            writer.Write(score);
        }
        Debug.Log("Saved score to: " + FilePath);
    }

    public void Load()
    {
        if (!File.Exists(FilePath))
        {
            Debug.LogWarning("No score file found. Press S to save first.");
            return;
        }

        using (BinaryReader reader = new BinaryReader(File.Open(FilePath, FileMode.Open)))
        {
            score = reader.ReadInt32();
        }
        UpdateText();
        Debug.Log("Loaded score: " + score);
    }
}