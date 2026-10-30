# Carnival Target Shooting: Save & Load

A 2D Unity game where the player shoots moving carnival targets. This version adds a **Save & Load** system. Press **S** to save the game and **L** to load it back.

## Group Members

- Connor Hewitt


## Controls

| Key | Action |
|---|---|
| **A / Left Arrow** | Move left |
| **D / Right Arrow** | Move right |
| **Space** | Shoot |
| **S** | Save the game |
| **L** | Load the saved game |

## What Gets Saved

| Data | File | Format |
|---|---|---|
| Score | `score.dat` | Binary |
| Player position and every target on screen (position, speed, size, points, color, direction) | `positions.json` | JSON |

Bullet positions are not saved, as the assignment allows.

Both files are written to Unity's `Application.persistentDataPath`. The path is printed in the Console each time you save. On Windows it is under `C:\Users\<name>\AppData\LocalLow\<CompanyName>\<ProjectName>`.

## How It Works

### ISaveable (interface)

```csharp
public interface ISaveable
{
    void Save();
    void Load();
}
```

Anything that can be saved implements this interface. Because of it, the `SaveManager` does not need to know what each object saves or what format it uses. It only calls `Save()` or `Load()`.

### SaveManager

`SaveManager` listens for the **S** and **L** keys. It keeps a `List<ISaveable>` containing the `ScoreManager` and the `TransformSaver`. When S is pressed, it calls `Save()` on every item in the list. When L is pressed, it calls `Load()` on every item.

### ScoreManager (binary file)

`ScoreManager` already acted as the Observer for scoring. It now also implements `ISaveable`.
- **Save:** opens `score.dat` with a `BinaryWriter` and writes the score as a single integer.
- **Load:** reads the integer back with a `BinaryReader`, sets the score, and refreshes the on-screen text.

### TransformSaver (JSON file)

`TransformSaver` implements `ISaveable` and handles all locations.
- **Save:** collects the player's position and finds every `Target` in the scene. For each target it records position, speed, size, points, color, and direction. All of this goes into a `TransformSaveData` object, which `JsonUtility.ToJson()` converts to text and writes to `positions.json`.
- **Load:** reads the JSON back with `JsonUtility.FromJson()`, moves the player to the saved position, destroys the targets currently in the scene, and rebuilds each saved target.

### Builder pattern reused for loading

Targets are not placed in the scene by hand, so loading cannot simply move existing objects. Instead, `TransformSaver` rebuilds each saved target with the same `TargetBuilder` used by the spawner, which restores the correct speed, size, points, and color. It then sets the saved position and direction.

### Example `positions.json`

```json
{
    "playerPosition": { "x": 1.5, "y": -4.0, "z": 0.0 },
    "targets": [
        {
            "position": { "x": -3.2, "y": 2.1, "z": 0.0 },
            "speed": 5.0,
            "size": 0.6,
            "points": 30,
            "direction": -1.0,
            "color": { "r": 0.0, "g": 1.0, "b": 1.0, "a": 1.0 }
        }
    ]
}
```

## Design Patterns from Last Week

- **Object Pool:** `BulletPool` and `Bullet` reuse bullets instead of creating and destroying them.
- **Builder:** `TargetBuilder` and `TargetDirector` create the different target types.
- **Observer:** `TargetEvents` notifies `ScoreManager` when a target is hit.

## Project Structure

```
Assets/Scripts/
  ISaveable.cs         (save/load interface)
  SaveManager.cs       (listens for S and L)
  SaveData.cs          (data classes turned into JSON)
  TransformSaver.cs    (saves positions to JSON)
  ScoreManager.cs      (observer + saves score to binary)
  Target.cs, TargetBuilder.cs, TargetDirector.cs, TargetSpawner.cs
  Bullet.cs, BulletPool.cs, Shooter.cs
  IScoreObserver.cs, TargetEvents.cs
```

## Requirements

- Unity 6 (6000.0.71f1)
- Input System package
- TextMeshPro

## Running the Project

1. Clone this repository.
2. Open the project folder in Unity Hub.
3. Open `Assets/Scenes/SampleScene`.
4. Press **Play**.
