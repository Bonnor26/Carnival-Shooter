using UnityEngine;
using TMPro;

// The "observer" in the Observer pattern.
public class ScoreManager : MonoBehaviour, IScoreObserver
{
    [SerializeField] private TMP_Text scoreText;
    private int score;

    void OnEnable() { TargetEvents.Attach(this); }
    void OnDisable() { TargetEvents.Detach(this); }

    public void OnTargetHit(Target target)
    {
        score += target.Points;
        scoreText.text = "Score: " + score;
    }
}