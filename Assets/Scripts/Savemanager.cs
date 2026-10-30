using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Press S to save, L to load. Talks to everything through the ISaveable interface.
public class SaveManager : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private TransformSaver transformSaver;

    private readonly List<ISaveable> saveables = new List<ISaveable>();

    void Awake()
    {
        saveables.Add(scoreManager);
        saveables.Add(transformSaver);
    }

    void Update()
    {
        Keyboard kb = Keyboard.current;
        if (kb == null) return;

        if (kb.sKey.wasPressedThisFrame)
        {
            foreach (ISaveable s in saveables) s.Save();
        }

        if (kb.lKey.wasPressedThisFrame)
        {
            foreach (ISaveable s in saveables) s.Load();
        }
    }
}